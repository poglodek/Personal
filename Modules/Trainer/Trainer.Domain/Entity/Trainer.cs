using Shared.Core;
using Trainer.Domain.ValueObject;

namespace Trainer.Domain.Entity;

public class Trainer : Shared.Core.Entity
{
    public Name FirstName { get; private set; }
    public Name LastName { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public Mail MailAddress { get; private set; }
    public Address Address { get; private set; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset UpdateAt { get; private set; }
    public DateTimeOffset LastLogin { get; private set; }
    public Blocked? Blocked { get; private set; }
    public DateOnly DateOfBirth { get; init; }
    public string Password { get; init; }
    
    public Trainer(Name firstName, Name lastName, PhoneNumber phoneNumber, Mail mailAddress, Address address, DateTimeOffset createdAt, DateTimeOffset updateAt, DateOnly dateOfBirth, string password)
    {
        Id = new Id(Guid.NewGuid());
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        MailAddress = mailAddress;
        Address = address;
        CreatedAt = createdAt;
        UpdateAt = updateAt;
        DateOfBirth = dateOfBirth;
        Password = password;
    }

    private void Update(TimeProvider timeProvider) => UpdateAt = timeProvider.GetUtcNow();

    public void Block(TimeProvider timeProvider, string reason)
    {
        Blocked = new Blocked(timeProvider.GetLocalNow(), reason);
        Update(timeProvider);
    }

    public void Unlock(TimeProvider timeProvider)
    {
        Blocked = null;
        Update(timeProvider);
    }
    

}