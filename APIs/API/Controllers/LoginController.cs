using API.Models;
using API.Supports;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace API.Controllers
{
    [RoutePrefix("api/Login")]
    public class LoginController : ApiController
    {

        [Route("GetUserDetail/{username}/{password}")]
        [HttpGet]
        public User isLogin(string username = "", string password = "")
        {
            var users = new List<User>();
            users.Add(new User { Id = 101, Name = "sid", RoleId = 1, Password="sid@123" });
            users.Add(new User { Id = 102, Name = "rahul", RoleId = 1,Password = "rahul@123" });
            users.Add(new User { Id = 103, Name = "ruchi", RoleId = 2 ,Password = "ruchi@123"});
            users.Add(new User { Id = 104, Name = "sugi", RoleId = 2 ,Password = "sugi@123"});

            //password = StringCipher.Decrypt()
            return users.Where(u => u.Name == username && u.Password == password).FirstOrDefault();
        }
    }
}
