namespace BlogAppetit.Web.Models.ViewModels
{
    public class UserViewModel //vai ser a nossa lista de users 
    {
       public List<User> Users { get; set; } //vai ser a nossa lista de users

        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }    
        public bool AdminRole { get; set; } 
    }
}
