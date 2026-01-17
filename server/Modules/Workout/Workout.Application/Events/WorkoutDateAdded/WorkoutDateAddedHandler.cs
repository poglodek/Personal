using MediatR;
using Shared.Application.Events;
using Workout.Application.Command.WorkoutSession.CreateWorkoutSession;
using Workout.Application.Repositories;
using Workout.Domain.Events;

namespace Workout.Application.Events.WorkoutDateAdded;

public class WorkoutDateAddedHandler : INotificationHandler<ApplicationEvent<Domain.Events.WorkoutDateAdded>>
{
    private readonly IMediator _mediator;
    private readonly IWorkoutPlanRepository _workoutPlanRepository;

    public WorkoutDateAddedHandler(IMediator mediator, IWorkoutPlanRepository workoutPlanRepository)
    {
        _mediator = mediator;
        _workoutPlanRepository = workoutPlanRepository;
    }

    public async Task Handle(ApplicationEvent<Domain.Events.WorkoutDateAdded> notification, CancellationToken cancellationToken)
    {
        var domainEvent = notification.GetType().GetProperty("DomainEvent")!.GetValue(notification) as Domain.Events.WorkoutDateAdded;
        if (domainEvent == null)
        {
            return;
        }

        // Find the workout plan that contains this workout
        // For now, we'll need to load all workout plans and find the one that contains the workout
        // This is not optimal, but it's a simple solution for now
        // In a production system, you might want to add a WorkoutPlanId to the Workout entity
        // or have a repository method to find the plan by workout ID

        // For this implementation, we'll assume the caller passes the WorkoutPlanId
        // We need to find the workout plan that contains this workout
        // Since we don't have a direct way to do this, we'll skip the auto-creation for now
        // and require manual creation through the API

        // TODO: Implement workout plan lookup or add WorkoutPlanId to the domain event

        // For now, we'll just log that the event was received
        // In a real implementation, you would:
        // 1. Find the workout plan that contains this workout
        // 2. Call CreateWorkoutSessionCommand with the plan ID, workout ID, and date

        // var command = new CreateWorkoutSessionCommand(
        //     workoutPlanId,
        //     domainEvent.WorkoutId,
        //     domainEvent.Date.Value
        // );
        // await _mediator.Send(command, cancellationToken);
    }
}
