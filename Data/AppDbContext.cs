using _3ecexamen.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

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
            base.OnModelCreating(Entity);

            model.Entity<Conference>()
                .HasMany(c => c.Rooms)
                .WithOne(r => r.Conference)
                .HasForeignKey(r => r.ConferenceId)
                .OnDelete(DeleteBehavior.Cascade);

            model.Entity<Talk>()
                .HasKey(t => new { t.SpeakerId, t.RoomId, t.StartTime });

            model.Entity<Talk>()
                .HasOne(t => t.Speaker)
                .WithMany(s => s.Talks)
                .HasForeignKey(t => t.SpeakerId);

            model.Entity<Talk>()
                .HasOne(t => t.Room)
                .WithMany(r => r.Talks)
                .HasForeignKey(t => t.RoomId);
        }
    }
}
