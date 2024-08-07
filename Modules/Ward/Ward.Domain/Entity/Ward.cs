using Ward.Domain.ValueObject;

namespace Ward.Domain.Entity;

public class Ward : Shared.Core.Entity
{
    public Trainer Trainer { get; private set; }
    
    public Ward(Guid id, Guid trainerId)
    {
        Id = id;
        Trainer = new Trainer(trainerId);
    }
    
    public void AssignToTrainer(Guid trainerId)
    {
        Trainer = new Trainer(trainerId);
    }
}