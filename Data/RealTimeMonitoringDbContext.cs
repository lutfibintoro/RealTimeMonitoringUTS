using Microsoft.EntityFrameworkCore;
using RealTimeMonitoringUTS.Data.Model;

namespace RealTimeMonitoringUTS.Data
{
    public class RealTimeMonitoringDbContext(DbContextOptions<RealTimeMonitoringDbContext> options) : DbContext(options)
    {
        public DbSet<Sensor> Sensors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sensor>(buildAction =>
            {
                buildAction.ToTable<Sensor>(name: "Sensor", schema: "dbo");

                buildAction
                .HasKey(e => e.Id);

                buildAction
                .Property<int>(e => e.Id)
                .UseIdentityColumn<int>(1, 1)
                .HasColumnName("Id")
                .IsRequired(true)
                .HasColumnType<int>("int");

                buildAction
                .Property<double>(e => e.Humidity)
                .IsRequired(true)
                .HasColumnName("Humidity")
                .HasColumnType<double>("DECIMAL(10, 2)");

                buildAction
                .Property<double>(e => e.HydrogenGas)
                .IsRequired(true)
                .HasColumnName("HydrogenGas")
                .HasColumnType<double>("DECIMAL(10, 2)");

                buildAction
                .Property<double>(e => e.MethaneGas)
                .IsRequired(true)
                .HasColumnName("MethaneGas")
                .HasColumnType<double>("DECIMAL(10, 2)");

                buildAction
                .Property<double>(e => e.TemperatureC)
                .IsRequired(true)
                .HasColumnName("TemperatureC")
                .HasColumnType<double>("DECIMAL(10, 2)");

                buildAction
                .Property<double>(e => e.Smoke)
                .IsRequired(true)
                .HasColumnName("Smoke")
                .HasColumnType<double>("DECIMAL(10, 2)");

                buildAction
                .Property<double>(e => e.LpgGas)
                .IsRequired(true)
                .HasColumnName("LpgGas")
                .HasColumnType<double>("DECIMAL(10, 2)");

                buildAction
                .Property<double>(e => e.AlcohonGas)
                .IsRequired(true)
                .HasColumnName("AlcohonGas")
                .HasColumnType<double>("DECIMAL(10, 2)");

                buildAction
                .Property<int>(e => e.X)
                .IsRequired(true)
                .HasColumnName("X")
                .HasColumnType<int>("int");

                buildAction
                .Property<int>(e => e.Y)
                .IsRequired(true)
                .HasColumnName("Y")
                .HasColumnType<int>("int");

                buildAction
                .Property<int>(e => e.Z)
                .IsRequired(true)
                .HasColumnName("Z")
                .HasColumnType<int>("int");

                buildAction
                .Property<DateTime>(e => e.AddAt)
                .IsRequired(true)
                .HasColumnName("AddAt")
                .HasColumnType<DateTime>("DATETIME");
            });
        }
    }
}
