using Microsoft.EntityFrameworkCore;
using RamazonBot.Dal.Entities;

namespace RamazonBot.Dal.EntityConfigurations;

public class TelegramBotConfiguration : IEntityTypeConfiguration<TelegramBot>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TelegramBot> builder)
    {
        builder.ToTable("TelegramBots");

        builder.HasKey(tb => tb.TelegramBotId);

        builder.Property(tb => tb.FirstName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(tb => tb.LastName)
            .IsRequired(false)
            .HasMaxLength(50);

        builder.Property(tb => tb.UserName)
            .IsRequired(false)
            .HasMaxLength(50);

        builder.Property(tb => tb.PhoneNumber)
            .IsRequired(false)
            .HasMaxLength(15);

        builder.Property(tb => tb.LastUpdatedAt)
               .HasDefaultValueSql("GETUTCDATE()")
               .ValueGeneratedOnUpdate();
    }
}
