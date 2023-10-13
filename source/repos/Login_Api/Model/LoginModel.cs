using System.Data.SqlClient;
using Login_Api.Model;
using Dapper;


namespace Login_Api.Model
{
    public class LoginModel
    {
      
        public string Username { get; set; }
        public string Password { get; set; }

    }
}
