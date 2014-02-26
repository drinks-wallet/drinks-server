using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Drinks.Entities;

namespace Drinks.Web.Models.Admin
{
    public class EditAccountModel
    {
        public EditAccountModel() { }

        public EditAccountModel(IEnumerable<User> users)
        {
            Users = GenerateSelectListEnumerable(users, null);
        }

        public EditAccountModel(IEnumerable<User> users, User selectedUser)
        {
            Users = GenerateSelectListEnumerable(users, selectedUser);
        }
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public byte[] Password { get; set; }
        public bool IsAdmin { get; set; }
        public string BadgeId { get; set; }
        public IEnumerable<SelectListItem> Users { get; set; }
        public string SuccessMessage { get; set; }
        
        static IEnumerable<SelectListItem> GenerateSelectListEnumerable(IEnumerable<User> users, User selectedUser)
        {
            return selectedUser == null
                ? users.Select(x =>
                        new SelectListItem
                        {
                            Text = x.Name,
                            Value = x.Id.ToString(CultureInfo.InvariantCulture.NumberFormat)
                        })
                : users.Select(x =>
                        new SelectListItem
                        {
                            Selected = x.Id == selectedUser.Id,
                            Text = x.Name,
                            Value = x.Id.ToString(CultureInfo.InvariantCulture.NumberFormat)
                        });
        }
    }
}