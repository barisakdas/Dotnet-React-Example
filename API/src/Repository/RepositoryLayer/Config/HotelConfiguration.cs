using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RepositoryLayer.Config;
public class HotelConfiguration : IEntityTypeConfiguration<HotelEntity>
{
    public void Configure(EntityTypeBuilder<HotelEntity> builder)
    {
        builder.ToTable("Hotels");

        builder.HasKey(x => x.ID);

        builder.Property(x => x.ID)
            .UseIdentityColumn();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Address)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(x => x.Phone)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Contact)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Uri)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Stars)
            .IsRequired();
    }
}
