using BlogAppetit.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogAppetit.Web.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LoginDbContext loginDbContext;

        public UserRepository(LoginDbContext loginDbContext)
        {
            this.loginDbContext = loginDbContext;
        }

        public async Task<IEnumerable<IdentityUser>> GetAll()
        {
           var users = await loginDbContext.Users.ToListAsync();
            return users;
        }
    }
}
