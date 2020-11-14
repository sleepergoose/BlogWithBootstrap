using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogWithBootstrap.Models
{
    public class BlogContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public BlogContext() : base("BlogContext")
        {
            Database.SetInitializer(new DbInit());
        }

        class DbInit : DropCreateDatabaseIfModelChanges<BlogContext>
        {
            protected override void Seed(BlogContext context)
            {
                context.Users.AddRange(new List<User> {
                        new User { Name = "admin", Role = Roles.Admin, Email = "admin@server.com" },
                        new User { Name = "user 1", Role = Roles.Author, Email = "user1@server.com" },
                        new User { Name = "user 2", Role = Roles.Author, Email = "user2@server.com" },
                        new User { Name = "user 3", Role = Roles.User, Email = "user3@server.com" },
                        new User { Name = "user 4", Role = Roles.User, Email = "user4@server.com" }
                    });
                context.SaveChanges();

                context.Posts.Add(new Post
                {
                    Title = "Title 1",
                    Text = "Text 1",
                    Category = Categories.DotNet,
                    AddingDate = DateTime.Now,
                    User = context.Users
                                    .Where(user => user.Role == Roles.Author && user.Name.Contains("1"))
                                    .FirstOrDefault()
                });
                context.Posts.Add(new Post
                {
                    Title = "Title 2",
                    Text = "Text 2",
                    Category = Categories.CSharp,
                    AddingDate = DateTime.Now,
                    User = context.Users
                                    .Where(user => user.Role == Roles.Author && user.Name.Contains("2"))
                                    .FirstOrDefault()
                });
                context.Posts.Add(new Post
                {
                    Title = "Title 3",
                    Text = "Text 3",
                    Category = Categories.CSharp,
                    AddingDate = DateTime.Now,
                    User = context.Users
                                .Where(user => user.Role == Roles.Admin)
                                .FirstOrDefault()
                });
                context.SaveChanges();

                context.Comments.Add(new Comment { Text = "Comment Text 1", PostId = 1, UserId = 1 });
                context.Comments.Add(new Comment { Text = "Comment Text 2", PostId = 1, UserId = 4 });
                context.Comments.Add(new Comment { Text = "Comment Text 3", PostId = 1, UserId = 5 });
                context.Comments.Add(new Comment { Text = "Comment Text 4", PostId = 2, UserId = 4 });
                context.Comments.Add(new Comment { Text = "Comment Text 5", PostId = 2, UserId = 5 });
                context.SaveChanges();

                base.Seed(context);
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new PostConfiguration());
            modelBuilder.Configurations.Add(new CommentConfiguration());

            base.OnModelCreating(modelBuilder); 
        }
    }

    class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            this.HasKey(user => user.UserId);
            this.Property(p => p.UserId)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            
            this.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("nvarchar")
                .HasColumnName("User_Name");

            this.Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("nvarchar")
                .HasColumnName("User_Email");

            this.Property(p => p.Role)
                .IsRequired()
                .HasMaxLength(15)
                .HasColumnType("nvarchar")
                .HasColumnName("User_Role");
        }
    }
    class PostConfiguration : EntityTypeConfiguration<Post>
    {
        public PostConfiguration()
        {
            this.HasKey(post => post.PostId);
            this.Property(p => p.PostId)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("nvarchar")
                .HasColumnName("Post_Title");

            this.Property(p => p.Text)
                .IsRequired()
                .HasColumnType("nvarchar")
                .HasColumnName("Post_Text");

            this.Property(p => p.AddingDate)
                .IsRequired()
                .HasColumnName("Adding_Date")
                .HasColumnType("datetime");

            this.Property(p => p.Category)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("Category")
                .HasColumnType("nvarchar");

            this.HasRequired(post => post.User).
                WithMany(author => author.Posts)
                .HasForeignKey(post => post.UserId)
                .WillCascadeOnDelete(false);

            this.HasMany(post => post.Comments)
                .WithRequired(comment => comment.Post)
                .WillCascadeOnDelete(true);
        }
    }
    class CommentConfiguration : EntityTypeConfiguration<Comment>
    {
        public CommentConfiguration()
        {
            this.HasKey(p => p.CommentId);

            this.Property(p => p.CommentId)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(p => p.Text)
                .IsRequired()
                .HasMaxLength(1000)
                .HasColumnType("nvarchar")
                .HasColumnName("Comment_Text");

            this.HasRequired(comment => comment.User)
                .WithMany(user => user.Comments)
                .HasForeignKey(comment => comment.UserId)
                .WillCascadeOnDelete(true);
        }
    }
}