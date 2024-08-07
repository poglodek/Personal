using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ward.Domain.ValueObject;

namespace Ward.Infrastructure.Database.Configuration;

public class WardConfiguration : IEntityTypeConfiguration<Domain.Entity.Ward>
{
    public void Configure(EntityTypeBuilder<Domain.Entity.Ward> builder)
    {
        
        builder.ToTable("Ward", "ward");
        
        builder.HasKey(x=> x.Id);

        builder.Property(c => c.Trainer)
            .HasConversion(x => x.Id, x => new Trainer(x));
    }
}