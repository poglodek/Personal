using System.Text.Json;
using Workout.Domain.Exception;
using Workout.Domain.ValueObject;

namespace Workout.Domain.Entity;

public class WorkoutPlan : Shared.Core.Entity
{
    public Guid WardId { get; private set; }
    public Guid TrainerId { get; init; }
    public Name Name { get; private set; }
    public Description Description { get; private set; }
    public Active Active { get; private set; } = new(true);
    private readonly List<Workout> _workouts = [];

    private WorkoutPlan() { }
    public WorkoutPlan(Guid wardId, Guid trainerId, Name name, Description description)
    {
        Id = Guid.NewGuid();
        WardId = wardId;
        TrainerId = trainerId;
        Name = name;
        Description = description;
    }
    
    public void AddWorkout(Workout workout)
    {
        _workouts.Add(workout);
    }
    
    public void RemoveWorkout(Workout? workout)
    {
        var workoutToRemove = _workouts.FirstOrDefault(x => x.Id == workout?.Id);
        if (workout is not null)
        {
            _workouts.Remove(workoutToRemove!);
        }
    }
    
    public void SetNewDescription(Description description)
    {
        Description = description;
    }
    
    public void SetNewName(Name name)
    {
        Name = name;
    }
    
    public void DeActivate()
    {
        Active = new(false);
    }
    
    public void Activate()
    {
        Active = new(true);
    }
    private void AssignNewWard(Guid wardId)
    {
        Id = Guid.NewGuid();
        WardId = wardId;
    }
    
    public WorkoutPlan ReturnWorkoutPlanToAnotherWard(Guid wardId)
    {
        if(WardId == wardId)
        {
            throw new WorkoutPlanIsAlreadyAssignedToThisWardException(Id); 
        }
        
        var json = JsonSerializer.Serialize(this);
        var plan =  JsonSerializer.Deserialize<WorkoutPlan>(json)!;
        plan.AssignNewWard(wardId);

        return plan;
    }
}