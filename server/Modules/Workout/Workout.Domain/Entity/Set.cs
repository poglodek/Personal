using Workout.Domain.ValueObject;
using Type = Workout.Domain.ValueObject.Type;

namespace Workout.Domain.Entity;

public class Set : Shared.Core.Entity
{
    public Repeat Repeat { get; private set; } 
    public Description Description { get; private set; } 
    public RestTime RestTime { get; private set; } 
    public RepetitionRate RepetitionRate { get; private set; }
    public Type Type { get; private set; }
    
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
    public void SetNewRepeat(Repeat repeat)
    {
        Repeat = repeat;
    }
    
    public void SetNewDescription(Description description)
    {
        Description = description;
    }
    
    public void SetNewRestTime(RestTime restTime)
    {
        RestTime = restTime;
    }
    
    public void SetNewRepetitionRate(RepetitionRate repetitionRate)
    {
        RepetitionRate = repetitionRate;
    }
    
    public void SetNewType(string type)
    {
        Type = type;
    }
   

   
}