
using BlogAppetit.Web.Models.Domain;

namespace BlogAppetit.Web.Repositories

{
    public interface ITagRepository
    {
        Task <IEnumerable<Tag>> GetAllAsync();
        Task<Tag> GetAsync(Guid id);
        Task <Tag> AddAsync(Tag tag);
        Task <Tag?> UpdateAsync(Tag tag); //pode ser null
        Task <Tag?> DeleteAsync(Guid id); //pode ser null ou rebenta

    }
}
