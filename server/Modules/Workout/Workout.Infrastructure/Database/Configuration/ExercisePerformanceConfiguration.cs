using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workout.Domain.Entity;
using Workout.Domain.ValueObject;

namespace Workout.Infrastructure.Database.Configuration;

public class ExercisePerformanceConfiguration : IEntityTypeConfiguration<ExercisePerformance>
{
    public void Configure(EntityTypeBuilder<ExercisePerformance> builder)
    {
        builder.ToTable("ExercisePerformance", "workout");

        builder.HasKey(x => x.Id);

        // Navigation properties
        builder.HasMany(x => x.SetPerformances)
            .WithOne()
            .HasForeignKey("ExercisePerformanceId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(x => x.SetPerformances)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .AutoInclude();

        // Value object conversions
        builder.Property(x => x.ExerciseName)
            .HasConversion(x => x.Value, c => new Name(c))
            .HasColumnName("ExerciseName");

        builder.Property(x => x.ExerciseDescription)
            .HasConversion(x => x.Value, c => new Description(c))
            .HasColumnName("ExerciseDescription");
    }
}
