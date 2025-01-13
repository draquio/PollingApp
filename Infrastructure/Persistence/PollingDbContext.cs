using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class PollingDbContext : DbContext
    {
        public PollingDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Poll> Polls { get; set; }
        public DbSet<PollOption> PollOptions { get; set; }
        public DbSet<VoteTracking> VoteTrackings { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User
            modelBuilder.Entity<User>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<User>()
                .HasIndex(user => user.Email)
                .IsUnique();
            modelBuilder.Entity<User>()
                .HasIndex(user => user.Username)
                .IsUnique();

            modelBuilder.Entity<Poll>()
                .HasMany(p => p.Options)
                .WithOne().OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<VoteTracking>()
                .HasOne(v => v.Poll)
                .WithMany()
                .HasForeignKey(v => v.PollId);

            base.OnModelCreating(modelBuilder);

        }
    }
}
