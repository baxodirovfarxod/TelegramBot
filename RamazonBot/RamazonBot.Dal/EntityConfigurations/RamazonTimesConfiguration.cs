using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RamazonBot.Dal.Entities;

namespace RamazonBot.Dal.EntityConfigurations;

public class RamazonTimesConfiguration : IEntityTypeConfiguration<RamazonTimes>
{
    public void Configure(EntityTypeBuilder<RamazonTimes> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Day)
            .IsRequired();

        builder.Property(r => r.Suhur)
            .IsRequired()
            .HasMaxLength(5);

        builder.Property(r => r.Iftar)
            .IsRequired()
            .HasMaxLength(5);
    }
}
