using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;

namespace Drinks.Web.Models.Admin
{
    public class CreateAccountModel
    {
        [UsedImplicitly]
        public CreateAccountModel() { }

        public CreateAccountModel(string successMessage)
        {
            SuccessMessage = successMessage;
        }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string BadgeId { get; set; }
        public bool IsAdmin { get; set; }
        public string SuccessMessage { get; set; }
        public int DiscountPercentage { get; set; }
    }
}