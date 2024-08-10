using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workout.Domain.Entity;
using Workout.Domain.ValueObject;
using Type = Workout.Domain.ValueObject.Type;

namespace Workout.Infrastructure.Database.Configuration;

public class ExerciseConfiguration : IEntityTypeConfiguration<Exercise>
{
    public void Configure(EntityTypeBuilder<Exercise> builder)
    {
        
        builder.ToTable("Exercise", "workout");
        
        builder.HasKey(x=> x.Id);
        builder.HasIndex(x => x.TrainerId);
        builder.HasIndex(x => new {x.Id, x.TrainerId});
        
        builder.HasMany(x=>x.Sets)
            .WithOne();
        
        builder.Navigation(x => x.Sets)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
        
        builder.HasOne(x => x.Primary)
            .WithOne()
            .HasForeignKey<Exercise>(x => x.PrimaryId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Property(x => x.Name)
            .HasConversion(x => x.Value, c => new Name(c));
        
        builder.Property(x => x.Description)
            .HasConversion(x => x.Value, c => new Description(c));
        
        builder.Property(x => x.Active)
            .HasConversion(x => x.Value, c => new Active(c));
        
        
        builder.Property(x => x.Link)
            .HasConversion(x => x.Value, c => new Link(c));
        


    }
}