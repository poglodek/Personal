using Shared.Core;
using User.Domain.DomainEvents;
using User.Domain.ValueObject;

namespace User.Domain.Entity;

public class User : Shared.Core.Entity
{
    public Name FirstName { get; private set; }
    public Name LastName { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public Mail MailAddress { get; private set; }
    public Address Address { get; private set; }
    public Role Role { get; private set; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset UpdateAt { get; private set; }
    public DateTimeOffset LastLogin { get; private set; }
    public DateReason? Blocked { get; private set; }
    public DateReason? Activated { get; private set; }
    public DateOnly DateOfBirth { get; init; }
    public PasswordHash Password { get; private set; }
    public HashSet<Claim> Claims { get; private set; } = [];

    //For EF
    private User(){}
    
    public User(Name firstName, Name lastName, PhoneNumber phoneNumber, Mail mailAddress, Address address, DateOnly dateOfBirth, TimeProvider timeProvider)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        MailAddress = mailAddress;
        Address = address;
        DateOfBirth = dateOfBirth;
        Role = Role.User;
        CreatedAt = timeProvider.GetUtcNow();
        
        Update(timeProvider);
        RaiseUp(new UserCreated(Id));
    }

    public void SetPassword(PasswordHash hash) => Password = hash;
    
    private void Update(TimeProvider timeProvider) => UpdateAt = timeProvider.GetUtcNow();

    public void Activate(TimeProvider timeProvider) =>
        Activated = new DateReason(timeProvider.GetUtcNow(), "Account was activated");
    
    public void Block(TimeProvider timeProvider, string reason)
    {
        Blocked = new DateReason(timeProvider.GetUtcNow(), reason);
        Update(timeProvider);
    }

    public void Unlock(TimeProvider timeProvider)
    {
        Blocked = null;
        Update(timeProvider);
    }

    public void AddClaim(Claim claim, TimeProvider timeProvider)
    {
        Claims.Add(claim);
        Update(timeProvider);
    }

    public void SetDefaultClaimsForUser(TimeProvider timeProvider)
    {
        Claims = Claim.DefaultUserClaims.ToHashSet();
        Update(timeProvider);
    }
    
    public void SetDefaultClaimsForTrainer(TimeProvider timeProvider)
    {
        Claims = Claim.DefaultTrainerClaims.ToHashSet();
        Update(timeProvider);
    }

    public void RemoveClaim(Claim claim, TimeProvider timeProvider)
    {
        var claimToRemove = Claims.FirstOrDefault(x => x.Value.Equals(claim.Value,StringComparison.InvariantCultureIgnoreCase));
        if (claimToRemove is not null)
        {
            Claims.Remove(claimToRemove);
        }
        Update(timeProvider);
    }


    public void SetLastLogin(TimeProvider timeProvider)
    {
        LastLogin = timeProvider.GetUtcNow();
    }
}