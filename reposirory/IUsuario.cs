using jwt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jwt.reposirory
{
   public interface IUsuario
    {
        User GetUser(string Username , string password);
        User GetUserById(int Id);
        User Insert(User model);
        User Edit(User model);
        User Remove(User model);
        IEnumerable<User> GetUsersByRole(int Role);
        IEnumerable<User> GetAllUsers();

       User ValidUserName(string UserName);
    }
}
