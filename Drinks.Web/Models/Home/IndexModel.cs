namespace Drinks.Web.Models.Home
{
    using System.ComponentModel.DataAnnotations;

    public class IndexModel
    {
        [Required(ErrorMessage = "A username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "A password is requred.")]
        public string Password { get; set; }
    }
}