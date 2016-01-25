using RibbitMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RibbitMvc.Services
{
    public interface IUserService
    {
        IEnumerable<User> All(bool includeProfile);
        void Follow(string username, User follower);
        void Unfollow(string username, User follower);
        User GetAllFor(int id);
        User GetAllFor(string username);
        User GetBy(int id);
        User GetBy(string username);
        User Create(string username, string password, UserProfile profile, DateTime? created = null);
    }
}