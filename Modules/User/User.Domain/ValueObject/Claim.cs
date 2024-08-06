namespace User.Domain.ValueObject;

public record Claim(string Value)
{
    public static List<Claim> DefaultUserClaims => new()
    {
        new("User"),
        new("ViewWorkout"),
        new("ViewExercise"),
        new("GetUserById"),
    };
    
    //Add claims from default user claims
    public static List<Claim> DefaultTrainerClaims => new(DefaultUserClaims)
    {
        new("AddWorkout"),
        new("RemoveWorkout"),
        new("AddExercise"),
        new("RemoveExercise"),
    };
}