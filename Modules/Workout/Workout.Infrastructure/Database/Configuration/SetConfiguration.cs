using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workout.Domain.Entity;
using Workout.Domain.ValueObject;
using Type = Workout.Domain.ValueObject.Type;

namespace Workout.Infrastructure.Database.Configuration;

public class SetConfiguration : IEntityTypeConfiguration<Set>
{
    public void Configure(EntityTypeBuilder<Set> builder)
    {
        
        builder.ToTable("Set", "workout");
        
        builder.HasKey(x=> x.Id);

        builder.Property(x => x.Repeat)
            .HasConversion(x => x.Value, c => new Repeat(c));
        
        builder.Property(x => x.RestTime)
            .HasConversion(x => x.Seconds, c => new RestTime(c));
        
        builder.Property(x => x.Type)
            .HasConversion(x => x.Value, c => new Type(c));
        
        builder.Property(x => x.Description)
            .HasConversion(x => x.Value, c => new Description(c));
        
        builder.Property(e => e.RepetitionRate)
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                v => JsonSerializer.Deserialize<RepetitionRate>(v, (JsonSerializerOptions)null))
            .HasColumnType("jsonb");


    }
}