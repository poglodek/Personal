using MediatR;
using Workout.Application.Dto;
using Workout.Application.Repositories;
using Workout.Domain.Entity;
using Workout.Domain.ValueObject;

namespace Workout.Application.Command.WorkoutSession.CreateWorkoutSession;

public class CreateWorkoutSessionCommandHandler : IRequestHandler<CreateWorkoutSessionCommand, WorkoutSessionDto>
{
    private readonly IWorkoutPlanRepository _workoutPlanRepository;
    private readonly IWorkoutSessionRepository _workoutSessionRepository;

    public CreateWorkoutSessionCommandHandler(
        IWorkoutPlanRepository workoutPlanRepository,
        IWorkoutSessionRepository workoutSessionRepository)
    {
        _workoutPlanRepository = workoutPlanRepository;
        _workoutSessionRepository = workoutSessionRepository;
    }

    public async Task<WorkoutSessionDto> Handle(CreateWorkoutSessionCommand request, CancellationToken cancellationToken)
    {
        // Load the workout plan
        var workoutPlan = await _workoutPlanRepository.GetWorkoutPlanByIdAsync(request.WorkoutPlanId, cancellationToken);
        if (workoutPlan == null)
        {
            throw new InvalidOperationException($"Workout plan with ID {request.WorkoutPlanId} not found");
        }

        // Find the workout within the plan
        var workout = workoutPlan.Workouts.FirstOrDefault(w => w.Id == request.WorkoutId);
        if (workout == null)
        {
            throw new InvalidOperationException($"Workout with ID {request.WorkoutId} not found in plan {request.WorkoutPlanId}");
        }

        // Check if session already exists for this workout and date
        var exists = await _workoutSessionRepository.ExistsForWorkoutAndDateAsync(request.WorkoutId, request.ScheduledDate, cancellationToken);
        if (exists)
        {
            throw new InvalidOperationException($"Session already exists for workout {request.WorkoutId} on {request.ScheduledDate}");
        }

        // Create the workout snapshot
        var workoutSnapshot = new WorkoutSnapshot(workout.Name.Value, workout.Description.Value);

        // Create the session
        var session = new Domain.Entity.WorkoutSession(
            workout.Id,
            workoutPlan.Id,
            workoutPlan.WardId,
            workoutPlan.TrainerId,
            request.ScheduledDate,
            workoutSnapshot
        );

        // Snapshot exercises and sets
        var exerciseIndex = 0;
        foreach (var exercise in workout.Exercises)
        {
            var exercisePerformance = new ExercisePerformance(
                exercise.Id,
                exercise.Name,
                exercise.Description,
                exerciseIndex++
            );

            var setIndex = 0;
            foreach (var set in exercise.Sets)
            {
                var setPerformance = new SetPerformance(
                    set.Id,
                    setIndex++,
                    set.Repeat,
                    set.RestTime,
                    set.RepetitionRate
                );

                exercisePerformance.AddSetPerformance(setPerformance);
            }

            session.AddExercisePerformance(exercisePerformance);
        }

        // Save the session
        await _workoutSessionRepository.AddAsync(session, cancellationToken);

        return session.MapToDto();
    }
}
