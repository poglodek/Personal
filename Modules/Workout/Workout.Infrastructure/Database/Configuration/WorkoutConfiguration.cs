using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Workout.Domain.Entity;
using Workout.Domain.ValueObject;
using Type = Workout.Domain.ValueObject.Type;

namespace Workout.Infrastructure.Database.Configuration;

public class WorkoutConfiguration : IEntityTypeConfiguration<Domain.Entity.Workout>
{
    public void Configure(EntityTypeBuilder<Domain.Entity.Workout> builder)
    {
        builder.ToTable("Workout", "workout");
        
        builder.HasMany(x=>x.Exercises)
            .WithOne()
            .HasForeignKey("WorkoutId");
        
        builder.Navigation(x => x.Exercises)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
        
        builder.HasKey(x=> x.Id);
        
        builder.Property(x => x.Name)
            .HasConversion(x => x.Value, c => new Name(c));
   
        builder.Property(x => x.Description)
            .HasConversion(x => x.Value, c => new Description(c));
        
        var datesConverter = new ValueConverter<List<Date>, string>(
            v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
            v => JsonSerializer.Deserialize<List<Date>>(v, (JsonSerializerOptions)null));
        
        builder.Property(typeof(List<Date>), "_dates")
            .HasField("_dates")
            .HasConversion(datesConverter)
            .HasColumnType("jsonb");

        builder.Ignore(x => x.Dates);
        



    }
}