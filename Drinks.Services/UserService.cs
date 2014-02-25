using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Drinks.Entities;
using Drinks.Entities.Exceptions;
using Drinks.Repository;
using JetBrains.Annotations;

namespace Drinks.Services
{
    public interface IUserService
    {
        void ChangePassword(int userId, [NotNull] string oldPassword, [NotNull] string newPassword);
        void CreateUser([NotNull] User user, [NotNull] string password);
        decimal GetBalance(int userId);
        [CanBeNull]
        User GetUser(int userId);
        [CanBeNull]
        User GetUserByBadge([NotNull] string badgeId);
        [NotNull]
        IEnumerable<User> GetAllUsers();
        [NotNull]
        User ValidateUser([NotNull] string username, [NotNull] string password);
        bool IsBadgeValid([NotNull] string badge);
    }

    public class UserService : IUserService
    {
        readonly IDrinksContext _drinksContext;
        readonly IPasswordHelper _passwordHelper;

        public UserService(IUnitOfWork unitOfWork, IPasswordHelper passwordHelper)
        {
            _drinksContext = unitOfWork.DrinksContext;
            _passwordHelper = passwordHelper;
        }

        public void ChangePassword(int userId, string oldPassword, string newPassword)
        {
            var user = _drinksContext.Users.Find(userId);
            ValidateUser(user.Username, oldPassword);
            byte[] salt;
            user.Password = _passwordHelper.GenerateHashedPassword(newPassword, out salt);
            user.Salt = salt;
        }

        public void CreateUser(User user, string password)
        {
            if (UserExists(x => x.Id == user.Id))
                throw new UserExistsException("Id");
            if (UserExists(x => x.Username == user.Username))
                throw new UserExistsException("Username");

            byte[] salt;
            user.Password = _passwordHelper.GenerateHashedPassword(password, out salt);
            user.Salt = salt;

            _drinksContext.Users.Add(user);
        }

        public decimal GetBalance(int userId)
        {
            return _drinksContext.Database.SqlQuery<decimal>("SELECT Balance FROM UserBalances WHERE Id = @id", new SqlParameter("@id", userId)).SingleOrDefault();
        }

        public User GetUser(int userId)
        {
            return _drinksContext.Users.Find(userId);
        }

        public User GetUserByBadge(string badgeId)
        {
            return _drinksContext.Users.SingleOrDefault(x => x.BadgeId.Equals(badgeId, StringComparison.InvariantCultureIgnoreCase));
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _drinksContext.Users.ToArray();
        }

        public User ValidateUser(string username, string password)
        {
            User user;
            try
            {
                user = _drinksContext.Users.Single(x => x.Username == username);
            }
            catch (InvalidOperationException e)
            {
                throw new InvalidUserCredentialsException(e);
            }

            if (_passwordHelper.ValidatePassword(password, user.Salt, user.Password))
                return user;

            throw new InvalidUserCredentialsException();
        }

        public bool UserExists(Func<User, bool> predicate)
        {
            return _drinksContext.Users.Any(predicate);
        }

        public bool IsBadgeValid(string badge)
        {
            return _drinksContext.Users.Any(x => x.BadgeId.Equals(badge, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}