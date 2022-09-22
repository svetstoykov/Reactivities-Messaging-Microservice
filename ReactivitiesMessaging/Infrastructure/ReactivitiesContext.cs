using System;
using System.Collections.Generic;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Infrastructure
{
    public partial class ReactivitiesContext : DbContext
    {
        public ReactivitiesContext()
        {
        }

        public ReactivitiesContext(DbContextOptions<ReactivitiesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Message> Messages { get; set; } = null!;
        public virtual DbSet<Profile> Profiles { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasIndex(e => e.ReceiverId, "IX_Messages_ReceiverId");

                entity.HasIndex(e => e.SenderId, "IX_Messages_SenderId");

                entity.HasOne(d => d.Receiver)
                    .WithMany(p => p.MessageReceivers)
                    .HasForeignKey(d => d.ReceiverId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.MessageSenders)
                    .HasForeignKey(d => d.SenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Profile>(entity =>
            {
                entity.HasIndex(e => e.Email, "IX_Profiles_Email")
                    .IsUnique();

                entity.HasIndex(e => e.PictureId, "IX_Profiles_PictureId");

                entity.HasIndex(e => e.UserName, "IX_Profiles_UserName")
                    .IsUnique();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
