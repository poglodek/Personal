using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workout.Domain.Entity;
using Workout.Domain.ValueObject;

namespace Workout.Infrastructure.Database.Configuration;

public class WorkoutSessionConfiguration : IEntityTypeConfiguration<WorkoutSession>
{
    public void Configure(EntityTypeBuilder<WorkoutSession> builder)
    {
        builder.ToTable("WorkoutSession", "workout");

        builder.HasKey(x => x.Id);

        // Indexes for efficient querying
        builder.HasIndex(x => new { x.WardId, x.SessionDate });
        builder.HasIndex(x => new { x.TrainerId, x.SessionDate });
        builder.HasIndex(x => new { x.WorkoutId, x.ScheduledDate });
        builder.HasIndex(x => x.WorkoutPlanId);

        // Navigation properties
        builder.HasMany(x => x.ExercisePerformances)
            .WithOne()
            .HasForeignKey("WorkoutSessionId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(x => x.ExercisePerformances)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .AutoInclude();

        // Value object conversions
        builder.Property(x => x.Status)
            .HasConversion(x => x.Value, c => SessionStatus.Planned)
            .HasColumnName("Status");

        builder.Property(x => x.Notes)
            .HasConversion(x => x.Value, c => new SessionNotes(c))
            .HasColumnName("Notes");

        builder.Property(x => x.WorkoutSnapshot)
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => JsonSerializer.Deserialize<WorkoutSnapshot>(v, (JsonSerializerOptions?)null)!)
            .HasColumnType("jsonb");
    }
}
