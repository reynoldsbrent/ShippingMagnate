using api.Controllers;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Ship> Ships { get; set; }
        public DbSet<Container> Containers { get; set; }
        public DbSet<Port> Ports { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Transport> Transports { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<Handle> Handles { get; set; }
        public DbSet<ShipContainer> ShipContainers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure table names
            modelBuilder.Entity<Ship>().ToTable("SHIP");
            modelBuilder.Entity<Container>().ToTable("CONTAINER");
            modelBuilder.Entity<Port>().ToTable("PORT");
            modelBuilder.Entity<Customer>().ToTable("CUSTOMER");
            modelBuilder.Entity<Schedule>().ToTable("SCHEDULE");
            modelBuilder.Entity<Transport>().ToTable("TRANSPORTS");
            modelBuilder.Entity<Visit>().ToTable("VISITS");
            modelBuilder.Entity<Handle>().ToTable("HANDLES");
            modelBuilder.Entity<ShipContainer>().ToTable("SHIPS");

            // Configure composite keys
            modelBuilder.Entity<Schedule>()
                .HasKey(s => new { s.ShipImo, s.DeparturePortCode, s.DepartureDate });

            modelBuilder.Entity<Transport>()
                .HasKey(t => new { t.ShipImo, t.ContainerBic, t.StartDate });

            modelBuilder.Entity<Visit>()
                .HasKey(v => new { v.ShipImo, v.PortCode, v.VisitDate });

            modelBuilder.Entity<Handle>()
                .HasKey(h => new { h.PortCode, h.ContainerBic, h.HandlingDate });

            modelBuilder.Entity<ShipContainer>()
                .HasKey(sc => new { sc.CustomerId, sc.ContainerBic, sc.ShippingDate });

            // Configure relationships
            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.Ship)
                .WithMany()
                .HasForeignKey(s => s.ShipImo);

            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.ArrivalPort)
                .WithMany()
                .HasForeignKey(s => s.ArrivalPortCode);

            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.DeparturePort)
                .WithMany()
                .HasForeignKey(s => s.DeparturePortCode);

            modelBuilder.Entity<Transport>()
                .HasOne(t => t.Ship)
                .WithMany()
                .HasForeignKey(t => t.ShipImo);

            modelBuilder.Entity<Transport>()
                .HasOne(t => t.Container)
                .WithMany()
                .HasForeignKey(t => t.ContainerBic);

            modelBuilder.Entity<Visit>()
                .HasOne(v => v.Ship)
                .WithMany()
                .HasForeignKey(v => v.ShipImo);

            modelBuilder.Entity<Visit>()
                .HasOne(v => v.Port)
                .WithMany()
                .HasForeignKey(v => v.PortCode);

            modelBuilder.Entity<Handle>()
                .HasOne(h => h.Port)
                .WithMany()
                .HasForeignKey(h => h.PortCode);

            modelBuilder.Entity<Handle>()
                .HasOne(h => h.Container)
                .WithMany()
                .HasForeignKey(h => h.ContainerBic);

            modelBuilder.Entity<ShipContainer>()
                .HasOne(sc => sc.Customer)
                .WithMany()
                .HasForeignKey(sc => sc.CustomerId);

            modelBuilder.Entity<ShipContainer>()
                .HasOne(sc => sc.Container)
                .WithMany()
                .HasForeignKey(sc => sc.ContainerBic);
        }
    }
}
