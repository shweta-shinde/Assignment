using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountManagementController : ControllerBase
    {
        static UserStore.IUserRepository _userRepository;
        IServiceProvider _provider;
        public AccountManagementController(UserStore.IUserRepository repo, IServiceProvider provider)
        {
            _userRepository = repo;
            this._provider = provider;

        }

        //[HttpGet("validate/{name}/{password}")]
        //public ContentResult validate(string name,string password)
        //{
        //    List<UserStore.User> userList = (List<UserStore.User>)_userRepository.GetAll();
        //    bool exists = userList.Exists(User => User.Name == name && User.Password == password);
        //    if(exists)
        //        return Content("Validation Successfull");
        //    return Content("Validation Failed");
        //}

        //[HttpGet("passwordrecovery/{id}/{email}")]
        //public ContentResult passwordrecovery(string id, string email)
        //{
        //    List<UserStore.User> userList = (List<UserStore.User>)_userRepository.GetAll();
        //    bool exists = userList.Exists(User => User.UserId == id && User.Email == email);
        //    if (exists)
        //        return Content($"Password Recovery link sent to {email}");
        //    return Content("Invalid credentials");
        //}
        [HttpGet("passwordchange/{id}/{oldpassword}/{newpassword}")]
        public ContentResult passwordchange(string id, string oldpassword,string newpassword)
        {
            List<UserStore.User> userList = (List<UserStore.User>)_userRepository.GetAll();
            UserStore.User user = userList.Find(User => User.UserId == id && User.Password == oldpassword);
            if (user!=null)
            {
                user.Password = newpassword;
                _userRepository.UpdateUser(user.UserId, user);
                return Content("Password Changed Sucessfully");
            }

            return Content("Invalid credentials");
        }

        [HttpPost("validate")]
        public string validate([FromBody] UserStore.User user)
        {
            List<UserStore.User> userList = (List<UserStore.User>)_userRepository.GetAll();
            bool exists = userList.Exists(User => User.Name == user.Name && User.Password == user.Password);
            if (exists)
                return "Validation Successfull";
            return "Validation Failed";
        }

        [HttpPost("signup")]
        public string signup([FromBody] UserStore.User newuser)
        {
            _userRepository.AddUser(newuser);
            return "User Added Successfully";
        }

        [HttpPost("passwordrecovery")]
        public string passwordrecovery(UserStore.User user)
        {
            List<UserStore.User> userList = (List<UserStore.User>)_userRepository.GetAll();
            bool exists = userList.Exists(User => User.UserId == user.UserId && User.Email == user.Email);
            if (exists)
                return $"Password Recovery link sent to {user.Email}";
            return "Invalid credentials";
        }
    }
}


