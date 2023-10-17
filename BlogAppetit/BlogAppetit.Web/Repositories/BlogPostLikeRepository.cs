using BlogAppetit.Web.Data;
using BlogAppetit.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlogAppetit.Web.Repositories
{
    public class BlogPostLikeRepository : IBlogPostLikeRepository
    {
        private readonly BlogAppetitDbContext blogAppetitDbContext;

        public BlogPostLikeRepository(BlogAppetitDbContext blogAppetitDbContext) //termos acesso a base de dados 
        {
            this.blogAppetitDbContext = blogAppetitDbContext;
        }

        public async Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike)
        {
            await blogAppetitDbContext.BlogPostLike.AddAsync(blogPostLike);

            await blogAppetitDbContext.SaveChangesAsync();

            return blogPostLike;
        }

        public async Task<IEnumerable<BlogPostLike>> GetLikesForBlog(Guid blogPostId)
        {
           return await blogAppetitDbContext.BlogPostLike.Where(x => x.BlogPostId == blogPostId).ToListAsync(); // retorna todos os likes de um post específico
        }

        public async Task<int> GetTotalLikes(Guid blogPostId)
        {
           return await blogAppetitDbContext.BlogPostLike.CountAsync(x => x.BlogPostId == blogPostId );

        }


    }
}
