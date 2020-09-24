using System;
using System.Collections.Generic;

namespace UserStore
{
    
    public class UserRepository : IUserRepository
    {
        static List<User> _userList;

        public UserRepository()
        {

            _userList = new List<User>();
            _userList.Add(new User
            {
                UserId = "001",
                Name = "Tom",
                Email = "tom@123",
                Password = "abc"
            });
            _userList.Add(new User
            {
                UserId = "002",
                Name = "Jerry",
                Email = "jerry@123",
                Password = "xyz"
            });

        }

        public IEnumerable<User> GetAll()
        {
            return _userList;
        }

        public void AddUser(User user)
        {
            _userList.Add(user);
        }

        public void Remove(string id)
        {
            User user = _userList.Find(User => User.UserId == id);
            _userList.Remove(user);
        }

        public void UpdateUser(string id, User newUser)
        {
            int index = _userList.FindIndex(User => User.UserId == id);
            _userList.RemoveAt(index);
            _userList.Insert(index, newUser);
        }
    }
}
