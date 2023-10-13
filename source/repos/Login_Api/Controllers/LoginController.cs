using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Login_Api.Model;
using Dapper;
using Login_Api.Repository;
using System.Collections.Generic;
using System.Collections.Immutable;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Login_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {


        //instance for class which contain crud methods
        LoginRepository Obj_loging;

        public LoginController()
        {
            Obj_loging = new LoginRepository();
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginModel model)
        {
            string un = model.Username;
            string pw = model.Password;

           // LoginRepository loginRepository = new LoginRepository();

            List<LoginModel> user = Obj_loging.Return_Login_Details(un, pw);

            if (user != null && user.Count > 0)
            {

                string token = Obj_loging.GenerateJwtToken(user[0].Username); // Assuming you have a username property in your LoginModel

                return new ObjectResult(new { Message = "Login successful", Token = token });
                // Authentication successful
                //return Ok(new { Message = "Login successful" });
            }


            /*if (user.Username == model.Username && user.Password == model.Password)
            {
                // Authentication successful
                return "Login successful";
            }*/
            return Unauthorized(new { Message = "Invalid username or password" });
        }




    }
}
