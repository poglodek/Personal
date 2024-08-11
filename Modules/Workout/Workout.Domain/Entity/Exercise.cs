using System.Text.Json;
using Workout.Domain.ValueObject;

namespace Workout.Domain.Entity;

public class Exercise : Shared.Core.Entity
{
    public Guid TrainerId { get; init; }
    public Name Name { get; private set; }
    public Description Description { get; private set; }
    public Link Link { get; private set; }
    public Guid? PrimaryId { get; private set; }
    public Exercise? Primary { get; private set; } = null;
    public Active Active { get; private set; } = new (true);
    
    private readonly List<Set> _sets = [];
    public IReadOnlyList<Set> Sets => _sets.AsReadOnly();

    private Exercise() { }
    public Exercise(Guid trainerId, Name name, Description description, Link link)
    {
        Id = Guid.NewGuid();
        TrainerId = trainerId;
        Name = name;
        Description = description;
        Link = link;
    }

    public void AddSet(Set set)
    {
        _sets.Add(set);
    }
    
    public void RemoveSet(Guid setId)
    {
        var setToRemove = _sets.FirstOrDefault(x => x.Id == setId);
        if (setToRemove is not null)
        {
            _sets.Remove(setToRemove!);
        }
    }
    
    public void SetNewDescription(Description description)
    {
        Description = description;
    }
    
    public void SetNewLink(Link link)
    {
        Link = link;
    }
 
    public void DeActivate()
    {
        Active = new (false);
    }
    
    public void Activate()
    {
        Active = new (true);
    }
    
    private void GenerateNewId()
    {
        Id = Guid.NewGuid();
    }
    private void SetSecondary(Exercise primary)
    {
        Primary = primary;
        PrimaryId = primary.Id;
    }

    public Exercise Copy()
    {
        var json = JsonSerializer.Serialize(this);
        var newExercises = JsonSerializer.Deserialize<Exercise>(json);
        newExercises!.GenerateNewId();
        newExercises!.SetSecondary(this);
        newExercises._sets.Clear();

        foreach (var set in _sets)
        {
            newExercises.AddSet(new Set(set.Repeat, set.RestTime, set.RepetitionRate, set.Description, set.Type.Value));
        }
        
        
        return newExercises;
    }
   


}