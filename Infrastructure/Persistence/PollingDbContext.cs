using Domain.Entities;
using Microsoft.EntityFrameworkCore;


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
        public DbSet<Vote> Votes { get; set; }


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
                .WithOne(o => o.Poll)
                .HasForeignKey(o => o.PollId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PollOption>()
                .HasKey(po => po.Id);

            modelBuilder.Entity<PollOption>()
                .Property(po => po.OptionText)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Vote>()
                .HasKey(v => v.Id);


            modelBuilder.Entity<Vote>()
                .HasOne(v => v.Poll)
                .WithMany()
                .HasForeignKey(v => v.PollId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Vote>()
                .HasOne(v => v.Option)
                .WithMany()
                .HasForeignKey(v => v.OptionId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);

        }
    }
}
