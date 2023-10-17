using BlogAppetit.Web.Models.ViewModels;
using BlogAppetit.Web.Data;
using BlogAppetit.Web.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlogAppetit.Web.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace BlogAppetit.Web.Controllers
{
    public class AdminTagsController : Controller
    {
        private readonly ITagRepository tagRepository;

        public AdminTagsController(ITagRepository tagRepository) 
        {
            this.tagRepository = tagRepository;
        }

        [Authorize(Roles ="Admin")]
        [HttpGet]
  // vai ser chamado quando o admin fizer um get e devolve uma view
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost] // vai ser chamado quando o admin fizer um post e devolve uma view


        [Authorize(Roles = "Admin")]
        [ActionName("Add")]
        public async Task<IActionResult>Add(AddTagRequest addTagRequest) //gravar a tag na base de dados
        {
            if (ModelState.IsValid == false)
            {
                return View();
            }
            //Map do AddTagRequest para Tag domain model
            var tag =  new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };

            await tagRepository.AddAsync(tag); //chama o método AddAsync do TagRepository tudo o que precisa de falar com a db vai para o repository, separation of concerns

            return RedirectToAction("List"); //redireciona para a Lista das tags
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [ActionName("List")]
        // vai ser chamado quando o admin fizer um get e devolve uma view
        public async Task<IActionResult> List()
        { 
            var tags = await tagRepository.GetAllAsync(); //chama o método GetAllAsync do TagRepository tudo o que precisa de falar com a db vai para o repository, separation of concerns
            return View(tags);
        }

        public async Task<IActionResult>Edit(Guid id)
        {
            var tag = await tagRepository.GetAsync(id); //chama o método GetAsync do TagRepository tudo o que precisa de falar com a db vai para o repository, separation of concerns

            if ( tag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };

                return View(editTagRequest);
            }

            return View(null);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };

            var updatedTag = await tagRepository.UpdateAsync(tag); //chama o método UpdateAsync do TagRepository tudo o que precisa de falar com a db vai para o repository, separation of concerns

            if (updatedTag != null)
            {
                  
            }
            else
            {
            
            }

            return RedirectToAction("Edit" , new {id = editTagRequest.Id}); 

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {
            var deletedTag = await tagRepository.DeleteAsync(editTagRequest.Id);

            if (deletedTag != null)
            {
                return RedirectToAction("List");
            }

            return RedirectToAction("Edit" , new {id = editTagRequest.Id});

        }
    }
}
