namespace WuxiaWorld.DAL.Entities {

    using Microsoft.EntityFrameworkCore;

    public class WuxiaWorldDbContext : DbContext {
        public WuxiaWorldDbContext(DbContextOptions options) : base(options) {
        }

        private DbSet<Genres> Genre { get; set; }
        private DbSet<Novels> Novels { get; set; }
        private DbSet<Chapters> Chapters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.Entity<Genres>().ToTable(nameof(Genre));
            modelBuilder.Entity<Novels>().ToTable(nameof(Novels));
            modelBuilder.Entity<Chapters>().ToTable(nameof(Chapters));
        }
    }

}