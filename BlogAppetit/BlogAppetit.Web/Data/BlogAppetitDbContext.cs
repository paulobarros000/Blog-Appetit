using BlogAppetit.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlogAppetit.Web.Data

{
    public class BlogAppetitDbContext : DbContext
    {
        public BlogAppetitDbContext(DbContextOptions<BlogAppetitDbContext> options) : base(options)
        {
        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<BlogPostLike> BlogPostLike { get; set; }
        public DbSet<BlogPostComment> BlogPostComment { get; set; }
    }
}
