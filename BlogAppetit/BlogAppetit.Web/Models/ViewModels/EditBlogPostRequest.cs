using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogAppetit.Web.Models.ViewModels
{
    public class EditBlogPostRequest
    {
        public Guid Id { get; set; }
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; } //resumo do post na pagina
        public string? FeaturedImageUrl { get; set; } // imagem de destaque do post, o ? quer dizer que pode ser null
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public bool Visible { get; set; }

        public IEnumerable<SelectListItem> Tags { get; set; } //temos de ir ao controller porque ele fala com o repositorio e o repositorio fala com a db

        //ir buscar as tags

        public string[] SelectedTags { get; set; } = Array.Empty<string>();
    }
}
