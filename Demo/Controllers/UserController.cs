using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AzureProjectDemo.Models;
using AzureProjectDemo.Manager;
using Microsoft.AspNetCore.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AzureProjectDemo.Controllers
{
    [Route("api/[controller]")]
    //[EnableCors("AllowAllOrigins")]
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



        // POST api/user/create
        //[HttpPost("create")]
        //public string CreateUser([FromBody]User user)
        //{
        //    DBManager dbManager = new DBManager();

        //    if (dbManager.CreateUser(user) == 1)
        //        return "User created successfully";
        //    else
        //        return "User creation failed";
        //}
        [HttpPost("Create")]
        public IActionResult CreateUser([FromBody]User user)
        {
            DBManager dbManager = new DBManager();

            if (dbManager.CreateUser(user) == 1)
                //return CreatedAtRoute("GetUser",user.ID, user);
                return new ObjectResult("User created successfully");
            else
                return StatusCode(500);
        }

        // POST api/user/update
        //[HttpPost("update")]
        //public string UpdateUser([FromBody]User user)
        //{
        //    DBManager dbManager = new DBManager();

        //    if (dbManager.UpdateUser(user) == 1)
        //        return "User saved successfully";
        //    else
        //        return "User not found";
        //}

        private bool IsUserRecordValid(User user)
        {
            if (user == null ||
                user.ID < 1 ||
                (user.FirstName.Length < 1 && user.FirstName.Length > 50) ||
                (user.LastName.Length < 1 && user.LastName.Length > 50) ||
                (user.Age < 1 && user.Age > 120) ||
                (user.EmailId.Length < 1 && user.EmailId.Length > 50)) 
                return false;
            else
                return true;
        }
    }
}
