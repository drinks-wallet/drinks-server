using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Drinks.Entities;
using JetBrains.Annotations;

namespace Drinks.Web.Models.Admin
{
    public class EditAccountModel
    {
        public EditAccountModel() { }

        public EditAccountModel(IEnumerable<User> users)
        {
            Users = GenerateSelectListEnumerable(users, null);
        }

        public EditAccountModel(IEnumerable<User> users, int? selectedUserId)
        {
            SelectedUserId = selectedUserId;
            Users = GenerateSelectListEnumerable(users.OrderBy(x => x.Name), selectedUserId);
        }

        [UsedImplicitly]
        public int Id { get; set; }
        [UsedImplicitly]
        public string Name { get; set; }
        [UsedImplicitly]
        public string Username { get; set; }
        [UsedImplicitly]
        public string Password { get; set; }
        [UsedImplicitly]
        public bool IsAdmin { get; set; }
        [UsedImplicitly]
        public string BadgeId { get; set; }
        [UsedImplicitly]
        public IEnumerable<SelectListItem> Users { get; set; }
        [UsedImplicitly]
        public int? SelectedUserId { get; set; }
        [UsedImplicitly]
        public string SuccessMessage { get; set; }
        [UsedImplicitly]
        public int DiscountPercentage { get; set; }

        static IEnumerable<SelectListItem> GenerateSelectListEnumerable(IEnumerable<User> users, int? selectedUserId)
        {
            return selectedUserId == null
                ? users.Select(x =>
                        new SelectListItem
                        {
                            Text = x.Name,
                            Value = x.Id.ToString(CultureInfo.InvariantCulture.NumberFormat)
                        })
                : users.Select(x =>
                        new SelectListItem
                        {
                            Selected = x.Id == selectedUserId,
                            Text = x.Name,
                            Value = x.Id.ToString(CultureInfo.InvariantCulture.NumberFormat)
                        });
        }
    }
}