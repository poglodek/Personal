using Shared.Exceptions;

namespace Ward.Infrastructure.Exception;

public class TrainerNotFound(Guid id) : BaseException($"Trainer for ward {id} not found")
{
    public override string ErrorMessage => "trainer_not_found";
}