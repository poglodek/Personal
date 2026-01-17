using Shared.Core;
using Workout.Domain.ValueObject;

namespace Workout.Domain.Events;

public record WorkoutDateAdded(Guid WorkoutId, Date Date) : IDomainEvent;
