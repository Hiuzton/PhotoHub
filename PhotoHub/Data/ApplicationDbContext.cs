using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProgrammingClub.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }/*

    public virtual DbSet<Announcement> Announcements { get; set; }

    public virtual DbSet<CodeSnippet> CodeSnippets { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<Membership> Memberships { get; set; }

    public virtual DbSet<MembershipType> MembershipTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Announcement>(entity =>
        {
            entity.HasKey(e => e.IdAnnouncement).HasName("PK__Announce__4FBADEC1C323E3A1");

            entity.Property(e => e.IdAnnouncement).ValueGeneratedNever();
            entity.Property(e => e.EventDateTime).HasColumnType("datetime");
            entity.Property(e => e.Tags)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.Text)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.ValidFrom).HasColumnType("datetime");
            entity.Property(e => e.ValidTo).HasColumnType("datetime");
        });

        modelBuilder.Entity<CodeSnippet>(entity =>
        {
            entity.HasKey(e => e.IdCodeSnippet).HasName("PK__CodeSnip__BAE679EBEC2E9D52");

            entity.Property(e => e.IdCodeSnippet).ValueGeneratedNever();
            entity.Property(e => e.ContentCode).IsUnicode(false);
            entity.Property(e => e.DateTimeAdded).HasColumnType("datetime");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdMemberNavigation).WithMany(p => p.CodeSnippets)
                .HasForeignKey(d => d.IdMember)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CodeSnippets_Members");

            entity.HasOne(d => d.IdSnippetPreviousVersionNavigation).WithMany(p => p.InverseIdSnippetPreviousVersionNavigation)
                .HasForeignKey(d => d.IdSnippetPreviousVersion)
                .HasConstraintName("FK_CodeSnippets_CodeSnippets");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.IdMember).HasName("PK__Members__570E7FF05D6626F7");

            entity.Property(e => e.IdMember).ValueGeneratedNever();
            entity.Property(e => e.Description)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Position)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Resume).IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Membership>(entity =>
        {
            entity.HasKey(e => e.IdMembership).HasName("PK__Membersh__9F4153303416AF87");

            entity.Property(e => e.IdMembership).ValueGeneratedNever();
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.StartDate).HasColumnType("datetime");

            entity.HasOne(d => d.IdMemberNavigation).WithMany(p => p.Memberships)
                .HasForeignKey(d => d.IdMember)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Memberships_Members");

            entity.HasOne(d => d.IdMembershipTypeNavigation).WithMany(p => p.Memberships)
                .HasForeignKey(d => d.IdMembershipType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Memberships_MembershipTypes");
        });

        modelBuilder.Entity<MembershipType>(entity =>
        {
            entity.HasKey(e => e.IdMembershipType).HasName("PK__Membersh__64F6DC3AFFC1FBCF");

            entity.Property(e => e.IdMembershipType).ValueGeneratedNever();
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);*/
}
