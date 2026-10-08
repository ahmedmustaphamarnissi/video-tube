using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace back_end.Models;

public partial class YouTubeCloneContext : DbContext
{
    public YouTubeCloneContext()
    {
    }

    public YouTubeCloneContext(DbContextOptions<YouTubeCloneContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Channel> Channels { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Video> Videos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=;Database=YouTubeClone;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasIndex(e => e.IconName, "UQ_Categories_IconName").IsUnique();

            entity.HasIndex(e => e.Name, "UQ_Categories_Name").IsUnique();

            entity.Property(e => e.IconName).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Channel>(entity =>
        {
            entity.HasIndex(e => e.Name, "UQ_Channels_Name").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.PictureKey).HasMaxLength(500);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.Property(e => e.CommentDate).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.CommentText).HasMaxLength(1000);

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comments_Users");

            entity.HasOne(d => d.Video).WithMany(p => p.Comments)
                .HasForeignKey(d => d.VideoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comments_Videos");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.UserName, "UQ_Users_UserName").IsUnique();

            entity.Property(e => e.PictureKey).HasMaxLength(500);
            entity.Property(e => e.UserName).HasMaxLength(50);
        });

        modelBuilder.Entity<Video>(entity =>
        {
            entity.Property(e => e.AdditionDate).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.ThumbnailKey).HasMaxLength(500);
            entity.Property(e => e.Title).HasMaxLength(200);
            entity.Property(e => e.VideoKey).HasMaxLength(500);

            entity.HasOne(d => d.Category).WithMany(p => p.Videos)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Videos_Categories");

            entity.HasOne(d => d.Channel).WithMany(p => p.Videos)
                .HasForeignKey(d => d.ChannelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Videos_Channels");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
