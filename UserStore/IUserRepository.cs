using System;
using System.Collections.Generic;
using System.Text;

namespace UserStore
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        void AddUser(User user);
        void UpdateUser(string id, User newUser);
        void Remove(string id);
    }
}
