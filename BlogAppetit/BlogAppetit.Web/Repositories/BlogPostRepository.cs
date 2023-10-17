using BlogAppetit.Web.Data;
using BlogAppetit.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlogAppetit.Web.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BlogAppetitDbContext blogAppetitDbContext;

        public BlogPostRepository(BlogAppetitDbContext blogAppetitDbContext)
        {
            this.blogAppetitDbContext = blogAppetitDbContext;
        }

        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
            await blogAppetitDbContext.AddAsync(blogPost);

            await blogAppetitDbContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var existingBlog = await blogAppetitDbContext.BlogPosts.FindAsync(id);
            if (existingBlog != null)
            {
                blogAppetitDbContext.BlogPosts.Remove(existingBlog);
                await blogAppetitDbContext.SaveChangesAsync(); //nao tava a dar por causa disto nunca esquceer do savechanges!!!
                return existingBlog;
            }
            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await blogAppetitDbContext.BlogPosts.Include(x => x.Tags).ToListAsync(); //vai buscar todos os posts ao nosso dbcontext e transforma-os numa lista
        }

        public async Task<BlogPost?> GetAsync(Guid id) //o post pode nao existir dai o ? 
        {
            return await blogAppetitDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == id);    
        }

        public async Task<BlogPost?> GetByUrlHandleAsync(string urlHandle)
        {
            return await blogAppetitDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.UrlHandle == urlHandle); //vai buscar o post ao dbcontext e as tags agora podemos usar dentro do controller
        }
        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            var existingBlog = await blogAppetitDbContext.BlogPosts.Include(x=> x.Tags).
                FirstOrDefaultAsync(x => x.Id == blogPost.Id); //vai buscar o post ao dbcontext

            if (existingBlog != null) //se encontrarmos o blog na bd
            {
                //updatar tudo 
                existingBlog.Id = blogPost.Id;
                existingBlog.Heading = blogPost.Heading;
                existingBlog.PageTitle = blogPost.PageTitle;
                existingBlog.Content = blogPost.Content;
                existingBlog.ShortDescription = blogPost.ShortDescription;
                existingBlog.FeaturedImageUrl = blogPost.FeaturedImageUrl;
                existingBlog.UrlHandle = blogPost.UrlHandle;
                existingBlog.PublishedDate = blogPost.PublishedDate;
                existingBlog.Author = blogPost.Author;
                existingBlog.Visible = blogPost.Visible;
                existingBlog.Tags = blogPost.Tags;

                await blogAppetitDbContext.SaveChangesAsync(); //guardar as alteracoes na bd
                return existingBlog;
            }
            return null;
        }
    }
}
