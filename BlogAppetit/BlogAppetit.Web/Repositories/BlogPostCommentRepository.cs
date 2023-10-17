using BlogAppetit.Web.Data;
using BlogAppetit.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlogAppetit.Web.Repositories
{
    public class BlogPostCommentRepository : IBlogPostCommentRepository
    {
        private readonly BlogAppetitDbContext blogAppetitDbContext; //injecao de dependencia como sempre pronto isto é sempre a mesma coisa

        public BlogPostCommentRepository(BlogAppetitDbContext blogAppetitDbContext)
        {
            this.blogAppetitDbContext = blogAppetitDbContext;
        }
        public async Task<BlogPostComment> AddAsync(BlogPostComment blogPostComment)
        {
            await blogAppetitDbContext.BlogPostComment.AddAsync(blogPostComment);

            await blogAppetitDbContext.SaveChangesAsync();

            return blogPostComment;
        }

        public async Task<IEnumerable<BlogPostComment>> GetCommentsByBlogIdAsync(Guid blogPostId)
        {
           return await blogAppetitDbContext.BlogPostComment.Where(x => x.BlogPostId == blogPostId).ToListAsync(); //devolve todos os comments do post 

        }
    }
}
