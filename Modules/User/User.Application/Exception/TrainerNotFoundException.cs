using Shared.Exceptions;

namespace User.Application.Exception;

public class UserNotFoundException : BaseException
{
    
        
   
    public override string Message => "User_not_found";
   public UserNotFoundException(Guid id) : base($"Training with id {id} not found")
   {
        
   }
   
   public UserNotFoundException(string mail) : base($"Training with email {mail} not found")
   {
        
   }

}