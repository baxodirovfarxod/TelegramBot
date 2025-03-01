using Microsoft.EntityFrameworkCore;
using RamazonBot.Dal.Entities;
using RamazonBot.Dal.EntityConfigurations;

namespace RamazonBot.Dal;

public class MainContext : DbContext
{
    public DbSet<TelegramBot> TelegramBots { get; set; }
    public DbSet<RamazonTimes> RamazonTimes { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=WIN-BNO54FDBS2G\\SQLEXPRESS;Database=TelegramBot;User Id=sa;Password=1;TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TelegramBotConfiguration());
        modelBuilder.ApplyConfiguration(new RamazonTimesConfiguration());
    }
}
