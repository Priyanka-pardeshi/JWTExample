using JWTProject.Models;
using JWTProject.Store.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTProject.Store
{
    public class LoginStore : ILoginStore
    {
        private readonly IOptions<appsettings> _options;
        public LoginStore(IOptions<appsettings> options)
        {

            _options = options;
        }

        public string RegisterUser(Login login)
        {
            try
            {
                using (var connection = new SqlConnection(_options.Value.ConnectionStrings))
                {
                    connection.Open();

                    var query = "INSERT INTO Login (Username, Password,Pan,role) VALUES (@Username, @Password,@Pan,@Role);SELECT SCOPE_IDENTITY();";

                    string EncrptedPassword = EncryptionHelper.EncryptPassword(login.Password);
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", login.UserName);
                        command.Parameters.AddWithValue("@Password", EncrptedPassword);
                        command.Parameters.AddWithValue("@Role", login.role);
                        command.Parameters.AddWithValue("@Pan", login.Pan);

                        var insertedId = Convert.ToInt32(command.ExecuteScalar());
                        return $"User is created with ID: {insertedId}";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string ValidateUser(string username, string password)
        {
            string accessToken = "";
            try
            {
                using (var connection = new SqlConnection(_options.Value.ConnectionStrings))
                {

                    connection.Open();

                    var query = $"select role from Login where Username=@Username and Password=@Password";

                    string EncrptedPassword = EncryptionHelper.EncryptPassword(password);
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", EncrptedPassword);

                        int UserRole = Convert.ToInt32(command.ExecuteScalar());
                        if (UserRole == null)
                        {
                            return "Invalid user";
                        }
                        else
                        {
                            //User is found
                            //Generate Accesstoken
                            accessToken = GenerateAccessToken(username, UserRole);

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return accessToken;
        }
        private string GenerateAccessToken(string UserName, int Roleip)
        {

            Role enumValue = (Role)Roleip; // Cast integer to enum type
            string roleStr = Enum.GetName(typeof(Role), enumValue);



            var claims = new[]
            {
                new Claim("Username", UserName),
                new Claim(System.Security.Claims.ClaimTypes.Role, roleStr),
                //new Claim(System.Security.Claims.ClaimTypes.SerialNumber, authDTO.UserId.ToString()),
             //  new Claim(System.Security.Claims.ClaimTypes.Role,  authDTO.Role),
                //new Claim(System.Security.Claims.ClaimTypes.Role,  authDTO.rmsRole),
                //new Claim(System.Security.Claims.ClaimTypes.Name,authDTO.Name)

            };



            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.Jwt.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
            issuer: _options.Value.Jwt.Issuer,
            audience: _options.Value.Jwt.Audience,
            claims: claims,
            notBefore: DateTime.Now,
            //expires: DateTime.Now.Add(TimeSpan.FromMinutes(_options.Value.Jwt.ExpiresIn)),
            expires: DateTime.Now.Add(TimeSpan.FromSeconds(_options.Value.Jwt.ExpiresIn)),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.Jwt.Secret)), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
