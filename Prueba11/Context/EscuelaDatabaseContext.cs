using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prueba11.Models;

namespace Prueba11.Context
{
    public class EscuelaDatabaseContext : DbContext
    {
        public
       EscuelaDatabaseContext(DbContextOptions<EscuelaDatabaseContext> options)
       : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<LinkedCelebrityWithCelebrity>()
                   .HasKey(table => new { table.celebrityId1, table.celebrityId2 });
            modelBuilder.Entity<LinkedItemWithItem>()
                .HasKey(table => new { table.itemId1, table.itemId2 });
            modelBuilder.Entity<LinkedItemWithCelebrity>()
                .HasKey(table => new { table.itemId, table.celebrityId});
            modelBuilder.Entity<RatingCelebrity>()
                .HasKey(table => new { table.celebrityId, table.userName});
            modelBuilder.Entity<RatingItem>()
                .HasKey(table => new { table.userName, table.itemId});
            modelBuilder.Entity<UserWatchlist>()
                .HasKey(table => new { table.userName, table.itemId });

        }
        public DbSet<Celebrity> Celebrities { get; set; }
        public DbSet<CommentCelebrity> CommentCelebrities { get; set; }
        public DbSet<CommentItem> CommentItems { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<LinkedCelebrityWithCelebrity> LinkedCelebrityWithCelebrities { get; set; }
        public DbSet<LinkedItemWithCelebrity> LinkedItemWithCelebrities { get; set; }
        public DbSet<LinkedItemWithItem> LinkedItemWithItems { get; set; }
        public DbSet<RatingCelebrity> RatingCelebrities { get; set; }
        public DbSet<RatingItem> RatingItems { get; set; }
        public DbSet<ReactionCommentItem> ReactionCommentItems { get; set; }
        public DbSet<ReactionCommentCelebrity> ReactionCommentCelebritys { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserWatchlist> UserWatchlists { get; set; }
    }
}