using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.Domain.ValueObject;

namespace User.Infrastructure.Database.Configuration;

internal class UserConfiguration : IEntityTypeConfiguration<Domain.Entity.User>
{
    public void Configure(EntityTypeBuilder<Domain.Entity.User> builder)
    {
        builder.ToTable("Users", "users");
        
        builder.HasKey(x=> x.Id);
        
        builder.Property(x => x.FirstName)
            .HasConversion(c => c.Value, z => new Name(z));
        
        builder.Property(x => x.Role)
            .HasConversion(c => c.Value, z => new Role(z));
        
        builder.Property(x => x.LastName)
            .HasConversion(c => c.Value, z => new Name(z));
        
        builder.Property(x => x.PhoneNumber)
            .HasConversion(c => c.Value, z => new PhoneNumber(z));
        
        builder.Property(x => x.MailAddress)
            .HasConversion(c => c.Value, z => new Mail(z));

        builder.Property(e => e.Address)
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                v => JsonSerializer.Deserialize<Address>(v, (JsonSerializerOptions)null))
            .HasColumnType("jsonb");
        
        builder.Property(e => e.Blocked)
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                v => JsonSerializer.Deserialize<DateReason>(v, (JsonSerializerOptions)null))
            .HasColumnType("jsonb");
        
        builder.Property(e => e.Activated)
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                v => JsonSerializer.Deserialize<DateReason>(v, (JsonSerializerOptions)null))
            .HasColumnType("jsonb");
        
        builder.Property(e => e.Password)
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                v => JsonSerializer.Deserialize<PasswordHash>(v, (JsonSerializerOptions)null))
            .HasColumnType("jsonb");
        
        builder.Property(e => e.Claims)
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                v => JsonSerializer.Deserialize<HashSet<Claim>>(v, (JsonSerializerOptions)null))
            .HasColumnType("jsonb");

        builder.Ignore(c => c.DomainEvents);
    }
}