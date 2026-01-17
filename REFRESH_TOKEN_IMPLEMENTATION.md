# Refresh Token Implementation Summary

## Overview
Successfully implemented refresh token functionality for the existing JWT-based authentication system.

## Changes Made

### 1. Database Layer

#### RefreshToken Entity (Already existed)
- `server/Modules/User/User.Domain/Entity/RefreshToken.cs`
- Properties: UserId, TokenHash, CreatedAt, ExpiresAt, RevokedAt, ReplacedByTokenHash
- Methods: `Create()`, `IsActive()`, `Revoke()`

#### Database Configuration
- **NEW**: `server/Modules/User/User.Infrastructure/Database/Configuration/RefreshTokenConfiguration.cs`
  - EF Core configuration for RefreshToken entity
  - Indexes on TokenHash and UserId for performance

#### Database Context
- **UPDATED**: `server/Modules/User/User.Infrastructure/Database/UserDbContext.cs`
  - Added `DbSet<RefreshToken> RefreshTokens` property

#### Migration
- **NEW**: `server/Modules/User/User.Infrastructure/Database/Migration/20260116201229_AddRefreshTokens.cs`
  - Creates RefreshTokens table with proper indexes
  - Run migration: `cd server && dotnet ef database update --project Modules/User/User.Infrastructure`

### 2. Repository Layer

#### IRefreshTokenRepository
- **NEW**: `server/Modules/User/User.Application/Repositories/IRefreshTokenRepository.cs`
  - `AddAsync()` - Add new refresh token
  - `GetByTokenHashAsync()` - Retrieve token by hash
  - `GetActiveTokensByUserIdAsync()` - Get all active tokens for a user

#### RefreshTokenRepository Implementation
- **NEW**: `server/Modules/User/User.Infrastructure/Repositories/RefreshTokenRepository.cs`
  - Implements all repository methods with EF Core

#### Dependency Registration
- **UPDATED**: `server/Modules/User/User.Infrastructure/Extensions.cs`
  - Registered `IRefreshTokenRepository` in DI container

### 3. Authentication Framework

#### IJsonWebTokenManager Interface
- **UPDATED**: `server/Framework/Auth/IJsonWebTokenManager.cs`
  - Added `GenerateRefreshToken()` - generates cryptographically secure random token
  - Added `HashRefreshToken()` - hashes token using SHA256

#### JsonWebTokenManager Implementation
- **UPDATED**: `server/Framework/Auth/JsonWebTokenManager.cs`
  - Implemented `GenerateRefreshToken()` using `RandomNumberGenerator`
  - Implemented `HashRefreshToken()` using `SHA256`

### 4. Application Layer

#### RefreshTokenRequestCommandHandler
- **UPDATED**: `server/Modules/User/User.Application/Command/RefreshToken/RefreshTokenRequestCommandHandler.cs`
  - Validates refresh token (exists, not expired, not revoked)
  - Validates user ID matches token
  - Checks user is activated and not blocked
  - Revokes old token and creates new one (token rotation)
  - Generates new JWT access token
  - Returns new token pair

#### LoginUserRequestHandler
- **UPDATED**: `server/Modules/User/User.Application/Command/LoginUser/LoginUserRequestHandler.cs`
  - Generates refresh token on successful login
  - Stores hashed refresh token in database
  - Returns JWT with refresh token

### 5. API Endpoints

#### UserModule
- **UPDATED**: `server/Modules/User/UserModule.Shared/UserModule.cs`
  - Fixed `/api/Users/refresh-token` endpoint to use `RefreshTokenRequestCommand`
  - Returns new token pair (access token + refresh token)

### 6. Configuration

#### appsettings.Development.json
- **UPDATED**: `server/Personal.API/appsettings.Development.json`
  - Added `RefreshExpireInHours: 168` (7 days) to auth section

## Security Features

1. **Token Hashing**: Refresh tokens are hashed using SHA256 before storage
2. **Token Rotation**: Old refresh token is revoked when used, new one is issued
3. **Expiration**: Refresh tokens expire after 168 hours (7 days, configurable)
4. **User Validation**: Checks user is activated and not blocked on every refresh
5. **Token-User Binding**: Validates refresh token belongs to requesting user

## API Usage

### Login (Get Initial Tokens)
```http
POST /api/Users/login
Content-Type: application/json

{
  "email": "user@example.com",
  "password": "password123"
}
```

**Response:**
```json
{
  "accessToken": "eyJhbGc...",
  "refreshToken": "base64-encoded-random-token",
  "userId": "guid",
  "role": "User",
  "expiresAt": "2026-01-17T05:00:00Z"
}
```

### Refresh Access Token
```http
POST /api/Users/refresh-token
Content-Type: application/json

{
  "userId": "guid-from-login",
  "refreshToken": "token-from-login"
}
```

**Response:**
```json
{
  "accessToken": "eyJhbGc...",
  "refreshToken": "new-base64-encoded-token",
  "userId": "guid",
  "role": "User",
  "expiresAt": "2026-01-17T05:00:00Z"
}
```

## Configuration Options

In `appsettings.json`:
```json
{
  "auth": {
    "JwtKey": "your-secret-key",
    "JwtIssuer": "your-issuer",
    "ExpiresInHours": 8,
    "RefreshExpireInHours": 168
  }
}
```

- `ExpiresInHours`: Access token lifetime (default: 8 hours)
- `RefreshExpireInHours`: Refresh token lifetime (default: 168 hours / 7 days)

## Database Migration

To apply the migration:
```bash
cd server
dotnet ef database update --project Modules/User/User.Infrastructure --context UserDbContext
```

Or run the application - migrations are applied automatically on startup via `UseInfra()`.

## Testing

Build the solution:
```bash
cd server
dotnet build Personal.sln -c Release
```

Run the API:
```bash
cd server/Personal.API
dotnet run
```

## Implementation Notes

- Refresh tokens are stored hashed in the database for security
- Old refresh tokens are marked as revoked and linked to their replacement
- The system implements token rotation (each refresh issues a new token)
- All refresh operations are logged for security auditing
- The implementation follows the existing modular architecture patterns
