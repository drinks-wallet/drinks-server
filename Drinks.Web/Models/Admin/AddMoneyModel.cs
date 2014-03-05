using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Drinks.Entities;
using JetBrains.Annotations;

namespace Drinks.Web.Models.Admin
{
    public class AddMoneyModel
    {
        [UsedImplicitly]
        public AddMoneyModel() { }

        public AddMoneyModel(IEnumerable<User> users)
        {
            Users = GenerateSelectListEnumerable(users);
        }

        public AddMoneyModel(string successMessage, IEnumerable<User> users)
        {
            SuccessMessage = successMessage;
            Users = GenerateSelectListEnumerable(users);
        }

        [UsedImplicitly]
        public int UserId { get; set; }
        [Required]
        [UsedImplicitly]
        public decimal? Amount { get; set; }
        [UsedImplicitly]
        public IEnumerable<SelectListItem> Users { get; set; }
        [UsedImplicitly]
        public string SuccessMessage { get; set; }

        static IEnumerable<SelectListItem> GenerateSelectListEnumerable(IEnumerable<User> users)
        {
            return users.OrderBy(x => x.Name)
                .Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString(CultureInfo.InvariantCulture.NumberFormat) });
        }
    }
}