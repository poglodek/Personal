using Shared.Exceptions;

namespace Trainer.Application.Exception;

public class TrainerNotFoundException : BaseException
{
    
        
   
    public override string Message => "trainer_not_found";
   public TrainerNotFoundException(Guid id) : base($"Training with id {id} not found")
   {
        
   }
   
   public TrainerNotFoundException(string mail) : base($"Training with email {mail} not found")
   {
        
   }

}