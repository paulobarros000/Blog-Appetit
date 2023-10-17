using BlogAppetit.Web.Data;
using BlogAppetit.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlogAppetit.Web.Repositories
{
    public class TagRepository : ITagRepository // tem de implmentar TODOS os métodos da interface
    {
        private readonly BlogAppetitDbContext blogAppetitDbContext; 

        public TagRepository(BlogAppetitDbContext blogAppetitDbContext)
        {
            this.blogAppetitDbContext = blogAppetitDbContext;
        }
        public async Task<Tag> AddAsync(Tag tag)
        {
            await blogAppetitDbContext.Tags.AddAsync(tag);
            await blogAppetitDbContext.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
           var existingTag = await blogAppetitDbContext.Tags.FindAsync(id);

            if (existingTag != null)
            {
                blogAppetitDbContext.Tags.Remove(existingTag); //pa nao precisa de ser async acho
                await blogAppetitDbContext.SaveChangesAsync();
                return existingTag; 
            }

            return null;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await blogAppetitDbContext.Tags.ToListAsync(); //vai buscar todas as tags à base de dados e coloca numa lista
        }

        public Task<Tag> GetAsync(Guid id)
        {
            return blogAppetitDbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existingTag = await blogAppetitDbContext.Tags.FindAsync(tag.Id);

            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                await blogAppetitDbContext.SaveChangesAsync();

                return existingTag;
            }

            return null; //se a existingtag for null retornamos null
        }
    }
}
