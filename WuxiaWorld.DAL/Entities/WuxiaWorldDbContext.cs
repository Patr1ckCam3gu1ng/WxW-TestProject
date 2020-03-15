namespace WuxiaWorld.DAL.Entities {

    using Microsoft.EntityFrameworkCore;

    public class WuxiaWorldDbContext : DbContext {

        public WuxiaWorldDbContext() {

        }

        public WuxiaWorldDbContext(DbContextOptions options) : base(options) {
        }

        public virtual DbSet<Genres> Genres { get; set; }
        public DbSet<Novels> Novels { get; set; }
        public DbSet<Chapters> Chapters { get; set; }
        public DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.Entity<Genres>()
                .HasIndex(b => b.GenreName)
                .IsUnique();

            modelBuilder.Entity<Genres>().ToTable(nameof(Genres));
            modelBuilder.Entity<Novels>().ToTable(nameof(Novels));
            modelBuilder.Entity<Chapters>().ToTable(nameof(Chapters));
            modelBuilder.Entity<Users>().ToTable(nameof(Users));
        }
    }

}