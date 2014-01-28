using Drinks.Web.Models.Shared;

namespace Drinks.Web.Models.Admin
{
    using System.ComponentModel.DataAnnotations;
    using JetBrains.Annotations;

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
        public UserPrivilegesModel UserPrivileges { get; set; }
        public string SuccessMessage { get; set; }
    }
}