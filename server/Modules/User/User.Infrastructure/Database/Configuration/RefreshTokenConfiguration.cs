using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace User.Infrastructure.Database.Configuration;

internal class RefreshTokenConfiguration : IEntityTypeConfiguration<Domain.Entity.RefreshToken>
{
    public void Configure(EntityTypeBuilder<Domain.Entity.RefreshToken> builder)
    {
        builder.ToTable("RefreshTokens", "users");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.TokenHash)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.ExpiresAt)
            .IsRequired();

        builder.Property(x => x.RevokedAt);

        builder.Property(x => x.ReplacedByTokenHash)
            .HasMaxLength(256);

        builder.HasIndex(x => x.TokenHash);
        builder.HasIndex(x => x.UserId);

        builder.Ignore(c => c.DomainEvents);
    }
}
