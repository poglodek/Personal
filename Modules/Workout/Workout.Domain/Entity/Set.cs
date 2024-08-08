using Workout.Domain.ValueObject;

namespace Workout.Domain.Entity;

public class Set : Shared.Core.Entity
{
    public Repeat Repeat { get; init; } 
    public Description Description { get; init; } 
    public RestTime RestTime { get; init; } 
    public RepetitionRate RepetitionRate { get; init; }
    public string Type { get; init; }
    
    private Set() { }
    public Set( Repeat repeat, RestTime restTime, RepetitionRate repetitionRate, Description description ,string type = "Default")
    {
        Id = Guid.NewGuid();
        Repeat = repeat;
        RestTime = restTime;
        RepetitionRate = repetitionRate;
        Description = description;
        Type = type;
    }
   

   
}