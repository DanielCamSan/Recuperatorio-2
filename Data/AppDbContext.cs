using _3ecexamen.Entities;
using Microsoft.EntityFrameworkCore;

namespace _3ecexamen.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Conference> Conferences => Set<Conference>();
        public DbSet<Room> Rooms => Set<Room>();
        public DbSet<Speaker> Speakers => Set<Speaker>();
        public DbSet<Talk> Talks => Set<Talk>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 1:N Conference -> Rooms (FK requerida, cascade)
            modelBuilder.Entity<Conference>()
                .HasMany(c => c.Rooms)
                .WithOne(r => r.Conference)
                .HasForeignKey(r => r.ConferenceId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // Índice único: Room.Name dentro de una Conference
            modelBuilder.Entity<Room>()
                .HasIndex(r => new { r.ConferenceId, r.Name })
                .IsUnique();

            // N:M con payload: Talk (clave compuesta)
            // Incluye StartTime en la clave para permitir varias charlas sin duplicar exactamente la misma tupla.
            modelBuilder.Entity<Talk>()
                .HasKey(t => new { t.SpeakerId, t.RoomId, t.StartTime });

            modelBuilder.Entity<Talk>()
                .HasOne(t => t.Speaker)
                .WithMany(s => s.Talks)
                .HasForeignKey(t => t.SpeakerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Talk>()
                .HasOne(t => t.Room)
                .WithMany(r => r.Talks)
                .HasForeignKey(t => t.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            // Propiedades obligatorias básicas
            modelBuilder.Entity<Conference>().Property(c => c.Title).IsRequired();
            modelBuilder.Entity<Conference>().Property(c => c.City).IsRequired();
            modelBuilder.Entity<Room>().Property(r => r.Name).IsRequired();
            modelBuilder.Entity<Speaker>().Property(s => s.FullName).IsRequired();
            modelBuilder.Entity<Speaker>().Property(s => s.TopicArea).IsRequired();
            modelBuilder.Entity<Talk>().Property(t => t.StartTime).IsRequired();
            modelBuilder.Entity<Talk>().Property(t => t.EndTime).IsRequired();
        }
    }
}
