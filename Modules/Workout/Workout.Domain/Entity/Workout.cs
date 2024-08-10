using Workout.Domain.ValueObject;

namespace Workout.Domain.Entity;

public class Workout : Shared.Core.Entity
{
    public Name Name { get; private set; }
    public Description Description { get; private set; }
    private readonly List<Exercise> _exercises = [];
    private readonly List<Date> _dates = [];
    public IReadOnlyList<Exercise> Exercises => _exercises.AsReadOnly();
    public IReadOnlyList<Date> Dates => _dates.ToList().AsReadOnly();

    private Workout(){}
    
    public Workout(Name name, Description description)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
    }
    
    public void AddExercise(Exercise exercise)
    {
        _exercises.Add(exercise);
    }
    
    public void RemoveExercise(Exercise? exercise)
    {
        var exerciseToRemove = _exercises.FirstOrDefault(x => x.Id == exercise?.Id);
        if (exercise is not null)
        {
            _exercises.Remove(exerciseToRemove!);
        }
    }
    
    public void AddDate(Date date)
    {
        if (_dates.Contains(date))
        {
            return;
        }
        
        _dates.Add(date);
    }
    
    public void RemoveDate(Date date)
    {
        _dates.Remove(date);
    }
    
    public void ChangeName(Name name)
    {
        Name = name;
    }
    
    public void ChangeDescription(Description description)
    {
        Description = description;
    }
    
}