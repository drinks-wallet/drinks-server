using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;
using System.Web.Mvc;
using CompareAttribute = System.ComponentModel.DataAnnotations.CompareAttribute;

namespace Drinks.Web.Models.Account
{
    public class MyProfileModel
    {
        [UsedImplicitly]
        public MyProfileModel() { }

        public MyProfileModel([NotNull] string successMessage)
        {
            SuccessMessage = successMessage;
        }

        [Required, Remote("ValidatePassword", "Account")]
        public string OldPassword { get; set; }
        [Required, Compare("ConfirmNewPassword"), ]
        public string NewPassword { get; set; }
        [Required]
        public string ConfirmNewPassword { get; set; }
        public string SuccessMessage { get; set; }
    }
}