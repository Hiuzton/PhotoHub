using Microsoft.EntityFrameworkCore;
using PhotoHub.Models;

namespace ProgrammingClub.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<UserModel> Users { get; set; }
    public DbSet<BlogPostModel> BlogPosts { get; set; }
    public DbSet<ImageModel> Images { get; set; }
    public DbSet<CommentModel> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserModel>(entity =>
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

        modelBuilder.Entity<BlogPostModel>(entity =>
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
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<ImageModel>(entity =>
        {
            entity.HasKey(e => e.IdImage);

            entity.Property(e => e.IdImage).ValueGeneratedOnAdd();
            entity.HasOne(e => e.BlogPost)
                .WithMany()
                .HasForeignKey(e => e.IdBlogPost)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<CommentModel>(entity =>
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
