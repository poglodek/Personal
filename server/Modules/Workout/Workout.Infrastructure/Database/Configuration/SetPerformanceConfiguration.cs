using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workout.Domain.Entity;
using Workout.Domain.ValueObject;

namespace Workout.Infrastructure.Database.Configuration;

public class SetPerformanceConfiguration : IEntityTypeConfiguration<SetPerformance>
{
    public void Configure(EntityTypeBuilder<SetPerformance> builder)
    {
        builder.ToTable("SetPerformance", "workout");

        builder.HasKey(x => x.Id);

        // Planned values conversions
        builder.Property(x => x.PlannedReps)
            .HasConversion(x => x.Value, c => new Repeat(c))
            .HasColumnName("PlannedReps");

        builder.Property(x => x.PlannedRestTime)
            .HasConversion(x => x.Seconds, c => new RestTime(c))
            .HasColumnName("PlannedRestTimeSeconds");

        builder.Property(x => x.PlannedRepetitionRate)
            .HasConversion(
                v => v != null ? JsonSerializer.Serialize(v, (JsonSerializerOptions?)null) : null,
                v => v != null ? JsonSerializer.Deserialize<RepetitionRate>(v, (JsonSerializerOptions?)null) : null)
            .HasColumnType("jsonb")
            .HasColumnName("PlannedRepetitionRate");

        // Actual values conversions
        builder.Property(x => x.ActualReps)
            .HasConversion(
                x => x != null ? x.Value : null,
                c => c != null ? new Repeat(c) : null)
            .HasColumnName("ActualReps");

        builder.Property(x => x.ActualRestTime)
            .HasConversion(
                x => x != null ? x.Seconds : (int?)null,
                c => c.HasValue ? new RestTime(c.Value) : null)
            .HasColumnName("ActualRestTimeSeconds");

        builder.Property(x => x.ActualRepetitionRate)
            .HasConversion(
                v => v != null ? JsonSerializer.Serialize(v, (JsonSerializerOptions?)null) : null,
                v => v != null ? JsonSerializer.Deserialize<RepetitionRate>(v, (JsonSerializerOptions?)null) : null)
            .HasColumnType("jsonb")
            .HasColumnName("ActualRepetitionRate");

        builder.Property(x => x.Weight)
            .HasConversion(
                v => v != null ? JsonSerializer.Serialize(v, (JsonSerializerOptions?)null) : null,
                v => v != null ? JsonSerializer.Deserialize<Weight>(v, (JsonSerializerOptions?)null) : null)
            .HasColumnType("jsonb")
            .HasColumnName("Weight");

        builder.Property(x => x.Notes)
            .HasConversion(
                x => x != null ? x.Value : null,
                c => c != null ? new Description(c) : null)
            .HasColumnName("Notes");
    }
}
