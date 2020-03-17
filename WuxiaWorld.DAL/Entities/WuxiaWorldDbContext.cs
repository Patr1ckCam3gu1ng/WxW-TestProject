namespace WuxiaWorld.DAL.Entities {

    using Microsoft.EntityFrameworkCore;

    public class WuxiaWorldDbContext : DbContext {

        public WuxiaWorldDbContext() {

        }

        public WuxiaWorldDbContext(DbContextOptions options) : base(options) {
        }

        public virtual DbSet<Genres> Genres { get; set; }
        public virtual DbSet<Novels> Novels { get; set; }
        public virtual DbSet<Chapters> Chapters { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<NovelGenres> NovelGenres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.Entity<Genres>()
                .HasIndex(b => b.GenreName)
                .IsUnique();

            modelBuilder.Entity<Genres>().ToTable(nameof(Genres));
            modelBuilder.Entity<Novels>().ToTable(nameof(Novels));
            modelBuilder.Entity<Chapters>().ToTable(nameof(Chapters));
            modelBuilder.Entity<Users>().ToTable(nameof(Users));
            modelBuilder.Entity<NovelGenres>().ToTable(nameof(NovelGenres));
        }
    }

}