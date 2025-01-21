using Microsoft.EntityFrameworkCore;
using PhotoHub.Models.DBObjects;

namespace PhotoHub.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser);

                entity.Property(e => e.IdUser).ValueGeneratedOnAdd();
                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.HasIndex(e => e.Email).IsUnique();
            });

            modelBuilder.Entity<BlogPost>(entity =>
            {
                entity.HasKey(e => e.IdBlogPost);

                entity.Property(e => e.IdBlogPost).ValueGeneratedOnAdd();
                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Content)
                    .HasMaxLength(1000)
                    .IsUnicode(false);
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");
                entity.HasOne(e => e.Author)
                    .WithMany()
                    .HasForeignKey(e => e.AuthorId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.HasKey(e => e.IdImage);

                entity.Property(e => e.IdImage).ValueGeneratedOnAdd();
                entity.HasOne(e => e.BlogPost)
                    .WithMany()
                    .HasForeignKey(e => e.IdBlogPost)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => e.IdComment);

                entity.Property(e => e.IdComment).ValueGeneratedOnAdd();
                entity.Property(e => e.Content)
                    .HasMaxLength(500)
                    .IsUnicode(false);
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.HasOne(e => e.BlogPost)
                    .WithMany()
                    .HasForeignKey(e => e.IdBlogPost)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.IdUser)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
