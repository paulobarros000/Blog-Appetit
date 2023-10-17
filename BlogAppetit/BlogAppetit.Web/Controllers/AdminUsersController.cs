using BlogAppetit.Web.Models.ViewModels;
using BlogAppetit.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Diagnostics.CodeAnalysis;

namespace BlogAppetit.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminUsersController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly UserManager<IdentityUser> userManager;

        public AdminUsersController(IUserRepository userRepository, UserManager<IdentityUser> userManager)
        {
            this.userRepository = userRepository;
            this.userManager = userManager;
        }

        public async Task<IActionResult> List()
        {
            var users = await userRepository.GetAll();

            var usersViewModel = new UserViewModel(); //inicializar 

            //
            usersViewModel.Users = new List<User>();  

            foreach (var user in users) {

                usersViewModel.Users.Add(new Models.ViewModels.User
                {
                    Id = Guid.Parse(user.Id),
                    Username = user.UserName,
                    Email = user.Email
                }); //vamos ter uma lista de users dentro do nosso view model
            }

            return View(usersViewModel);
        }


        //irmos buscar ao form do modal os dados do user
        [HttpPost]

        public async Task<IActionResult> List(UserViewModel request) //vai receber os dados do form do modal 
        {
            var identityUser = new IdentityUser
            {
                UserName = request.Username,
                Email = request.Email
            };

            var identityResult = await userManager.CreateAsync(identityUser, request.Password);

            if (identityResult.Succeeded) 
            {
                //meter finalmente o user no role 
                var roles = new List<string> { "User" }; 

                if (request.AdminRole) //se a checkbox do admin foi checked
                {
                    roles.Add("Admin"); //adicionamos o user ao role admin
                }

                identityResult = await userManager.AddToRolesAsync(identityUser, roles);

                if (identityResult.Succeeded)
                {
                    return RedirectToAction("List" , "AdminUsers"); //se tudo correu bem redirecionamos para a lista de users
                }
            }
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Delete(Guid id)
        { 
            var user = await userManager.FindByIdAsync(id.ToString());

            if (user != null)
            {
                var identityResult = await userManager.DeleteAsync(user);

                if (identityResult.Succeeded)
                {
                    return RedirectToAction("List", "AdminUsers");
                }
            }

            return View();
        }
        

    }
}
