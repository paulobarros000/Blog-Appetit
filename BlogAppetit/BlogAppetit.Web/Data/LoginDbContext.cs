using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogAppetit.Web.Data
{
    public class LoginDbContext : IdentityDbContext
    {
        public LoginDbContext(DbContextOptions<LoginDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Fazer os nossos roles (User e Admin)

            var userId = Guid.NewGuid().ToString();
            var adminId = Guid.NewGuid().ToString();

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name= "User",
                    NormalizedName = "User",
                    Id = userId,
                    ConcurrencyStamp = userId,

                },

                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Id = adminId,
                    ConcurrencyStamp = adminId,
                }
            };

            
            builder.Entity<IdentityRole>().HasData(roles); // Meter os nosso roles na db 

            //Seed o Admin à unha para os nossos testes

            var adminUser = new IdentityUser
            {
                UserName = "admin@blogappetit.com",
                Email = "superadmin@blogappetit.com",
                NormalizedEmail = "superadmin@blogappetit.com".ToUpper(),
                NormalizedUserName = "superadmin@blogappetit.com".ToUpper(),
                Id = adminId,
            };

            adminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(adminUser, "Admin1234!"); 

            builder.Entity<IdentityUser>().HasData(adminUser);

            //Adicionar roles ao admin tipo ele alem de ser um admin tambem vai ser um user normal

            var adminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = adminId,
                    UserId = adminId,
                },
                new IdentityUserRole<string>
                {
                    RoleId = userId,
                    UserId = adminId,

                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles); //Meter os roles do admin na db




        }
    }
}
