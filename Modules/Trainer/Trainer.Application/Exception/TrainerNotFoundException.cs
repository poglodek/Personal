using Shared.Exceptions;

namespace Trainer.Application.Exception;

public class TrainerNotFoundException(Guid id) : BaseException($"Training with id {id} not found")
{
    public override string Message => "trainer_not_found";
}