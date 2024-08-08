using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workout.Domain.Entity;
using Workout.Domain.ValueObject;

namespace Workout.Infrastructure.Database.Configuration;

public class WorkoutPlanConfiguration  : IEntityTypeConfiguration<WorkoutPlan>
{
    public void Configure(EntityTypeBuilder<WorkoutPlan> builder)
    {
        builder.ToTable("WorkoutPlan", "workout");
        
        builder.HasKey(x=> x.Id);
        builder.HasIndex(x => x.TrainerId);
        builder.HasIndex(x => new {x.Id, x.TrainerId});
        
        builder.HasMany(x=>x.Workouts)
            .WithOne()
            .HasForeignKey("WorkoutId");
        
        builder.Navigation(x => x.Workouts)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
        
        builder.Property(x => x.Name)
            .HasConversion(x => x.Value, c => new Name(c));
        
        builder.Property(x => x.Description)
            .HasConversion(x => x.Value, c => new Description(c));
        
        builder.Property(x => x.Active)
            .HasConversion(x => x.Value, c => new Active(c));
    }
}