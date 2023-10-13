using System.Data.SqlClient;
using Login_Api.Model;
using Dapper;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace Login_Api.Repository
{
    public class LoginRepository
    {
        public readonly string connectionString;


        public LoginRepository()
        {

            connectionString = @"Data Source = DESKTOP-Q5KI2MS; Initial Catalog = Login; Integrated Security = True";
        }


        public List<LoginModel> Return_Login_Details(string Username, string Password)

        {
            try
            {
                //LoginModel user = new LoginModel(Username, Password);

                List<LoginModel> LoginDetails = new List<LoginModel>();

                var connection = new SqlConnection(connectionString);
                connection.Open();
                LoginDetails = connection.Query<LoginModel>($" SELECT * FROM logindetail WHERE Username = '{Username}' AND Password = '{Password}' ").ToList();
                connection.Close();

                return LoginDetails;

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


        public string GenerateJwtToken(string username)
        {

            string secretKey = GenerateRandomSecretKey();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenHandler = new JwtSecurityTokenHandler();

            //var key = Encoding.ASCII.GetBytes("your-secret-key"); // Replace with your actual secret key
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GenerateRandomSecretKey()
        {
            const int keyLength = 32; // 256 bits
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] keyBytes = new byte[keyLength];
                rng.GetBytes(keyBytes);
                return Convert.ToBase64String(keyBytes);
            }
        }
    }
}
