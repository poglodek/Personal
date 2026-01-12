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

    private User(Name firstName, Name lastName, Mail mailAddress, DateOnly dateOfBirth, TimeProvider timeProvider)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        MailAddress = mailAddress;
        DateOfBirth = dateOfBirth;
        Role = Role.User;
        CreatedAt = timeProvider.GetUtcNow();
        
        Update(timeProvider);
        RaiseUp(new UserCreated(Id));
    }
    
    private User(Name firstName, Name lastName, PhoneNumber phoneNumber, Mail mailAddress, Address address, DateOnly dateOfBirth, TimeProvider timeProvider): this(firstName,lastName,mailAddress,dateOfBirth,timeProvider)
    {
        PhoneNumber = phoneNumber;
        Address = address;
    }

    public static User CreateInstance(Name firstName, Name lastName, PhoneNumber phoneNumber, Mail mailAddress, Address address, DateOnly dateOfBirth, TimeProvider timeProvider)
    {
        return new User(firstName, lastName, phoneNumber, mailAddress, address, dateOfBirth, timeProvider);
    }

    public static User CreateWard(Name firstName, Name lastName, Mail mailAddress,
        DateOnly dateOfBirth, TimeProvider timeProvider, Guid trainerId)
    {
        var user = new User(firstName, lastName,  mailAddress,  dateOfBirth, timeProvider);
        
        user.RaiseUp(new WardCreated(trainerId, user.Id));

        return user;
    }

    public void SetPassword(PasswordHash hash, TimeProvider timeProvider)
    {
        Password = hash;
        Update(timeProvider);
    }
    
    private void Update(TimeProvider timeProvider) => UpdateAt = timeProvider.GetUtcNow();

    public void Activate(TimeProvider timeProvider)
    {
        Activated = new DateReason(timeProvider.GetUtcNow(), "Account was activated");
        
        RaiseUp(new UserActivated(Id));
    }
        
    
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