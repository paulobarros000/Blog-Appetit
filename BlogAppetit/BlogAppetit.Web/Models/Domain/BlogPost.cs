namespace BlogAppetit.Web.Models.Domain
{
    public class BlogPost
    {
        public Guid Id { get; set; }   //unique identifier do c# para o id da db
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; } //resumo do post na pagina
        public string? FeaturedImageUrl { get; set; } // imagem de destaque do post, o ? quer dizer que pode ser null
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public bool Visible { get; set; }

        public ICollection<Tag> Tags { get; set; } //many to many com a tags

        public ICollection<BlogPostLike> Likes { get; set; } //many to many com a likes 

        public ICollection<BlogPostComment> Comments { get; set; } //one to many com a comments
    }
}
