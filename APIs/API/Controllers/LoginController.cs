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
            AppDbContext appobj = new AppDbContext();
            
            //password = StringCipher.Decrypt()
            return appobj.Users.Where(u => u.Name == username && u.Password == password).FirstOrDefault();
        }
    }
}
