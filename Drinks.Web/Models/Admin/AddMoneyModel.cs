using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Drinks.Entities;

namespace Drinks.Web.Models.Admin
{
    public class AddMoneyModel
    {
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

        public int UserId { get; set; }
        [Required]
        public decimal? Amount { get; set; }
        public IEnumerable<SelectListItem> Users { get; set; }
        public string SuccessMessage { get; set; }

        static IEnumerable<SelectListItem> GenerateSelectListEnumerable(IEnumerable<User> users)
        {
            return users.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString(CultureInfo.InvariantCulture.NumberFormat) });
        }
    }
}