using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AzureProjectDemo.Models;
using AzureProjectDemo.Manager;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AzureProjectDemo.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        // GET api/user
        [HttpGet]
        public List<User> GetAllUsers()
        {
            DBManager dbManager = new DBManager();
            List<User> users = dbManager.GetAllUsers();
            return users;
        }

        // GET api/user/1
        [HttpGet("{id}", Name = "GetUser")]
        public IActionResult GetUser(int id)
        {
            DBManager dbManager = new DBManager();
            User user = dbManager.GetUser(id);
            if (user == null)
                return NotFound("User not found");
            else
                return new ObjectResult(user);

        }

        // POST api/user/Create
        [HttpPost("Create")]
        public IActionResult CreateUser([FromBody]User user)
        {
            DBManager dbManager = new DBManager();
            if (dbManager.CreateUser(user) == 1)
                return new ObjectResult("User created successfully");
            else
                return StatusCode(500);
        }

    }
}
