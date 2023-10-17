using Microsoft.AspNetCore.Identity;

namespace BlogAppetit.Web.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAll();
    }
}
