using _15PuzzleGame.Entities;
using Microsoft.EntityFrameworkCore;

namespace _15PuzzleGame
{
    public class PuzzleContext : DbContext
    {
        public PuzzleContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<MyUser> Users { get; set; }
        public DbSet<Result> Results { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Result>()
                .HasOne<MyUser>()
                .WithMany()
                .HasForeignKey(r => r.MyUserId);
        }
    }
}
