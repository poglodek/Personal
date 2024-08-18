using MediatR;
using Workout.Application.Dto;
using Workout.Application.Exception;
using Workout.Application.Repositories;
using Workout.Domain.Entity;
using Workout.Domain.ValueObject;

namespace Workout.Application.Command.Exercise.AddSet;

public class CreateSetCommandHandler : IRequestHandler<CreateSetCommand,SetDto>
{
    private readonly IExerciseRepository _exerciseRepository;

    public CreateSetCommandHandler(IExerciseRepository exerciseRepository)
    {
        _exerciseRepository = exerciseRepository;
    }
    
    public async Task<SetDto> Handle(CreateSetCommand request, CancellationToken cancellationToken)
    {
        var exercise = await _exerciseRepository.GetExerciseByIdAsync(request.ExerciseId, cancellationToken);

        if (exercise is null)
        {
            throw new ExerciseNotFound(request.ExerciseId);
        }
        
        var repeat = new Repeat(request.Repeat);
        var restTime = new RestTime(request.RestTimeSeconds);
        var repetitionRate = new RepetitionRate(request.RepetitionRate.A, request.RepetitionRate.B, request.RepetitionRate.C, request.RepetitionRate.D);
        var description = new Description(request.Description);
        var set = new Set(repeat, restTime, repetitionRate, description, request.Type);
        
        exercise.AddSet(set);

        return set.ToDto();
    }
}