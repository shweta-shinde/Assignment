using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {
        // GET: api/<UserManagementController>
         static UserStore.IUserRepository _userRepository;
        IServiceProvider _provider;
        public UserManagementController(UserStore.IUserRepository repo, IServiceProvider provider)
        {
            _userRepository = repo;
            this._provider = provider;

        }
        [HttpGet("all")]
        public IEnumerable<UserStore.User> Get()
        {
            return _userRepository.GetAll();
        }

        [HttpPost("add")]
        public void Post([FromBody] UserStore.User user)
        {
            _userRepository.AddUser(user);
        }
      
        [HttpDelete("delete/{id}")]
        public void Delete(string id)
        {
            _userRepository.Remove(id);
        }
        [HttpPut("update/{id}")]
        public void Put(string id, [FromBody] UserStore.User user)
        {
           _userRepository.UpdateUser(id, user);
        }
    }
}
