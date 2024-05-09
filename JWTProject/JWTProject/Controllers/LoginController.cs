using JWTProject.Models;
using JWTProject.Store.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTProject.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IOptions<appsettings> _options;
        private readonly ILoginStore _loginStore;
        public LoginController(IOptions<appsettings> options, ILoginStore loginStore)
        {
            _options = options;
            _loginStore = loginStore;
        }
        //private readonly string _connectionString = "Server=192.168.10.28\\SQL2016;Database=JWTSample;User Id=dbuser;Password=$y$40rE@2021#$!;Integrated Security=False; Encrypt=false;TrustServerCertificate=True";
        //private readonly string _connectionString = "Data Source=192.168.10.28\\SQL2016;Initial Catalog=JWTSample;Integrated Security=False;User Id=dbuser;Password=$y$40rE@2021#$!";

        [HttpPost]
        public string RegisterUser(Login login)
        {
            string msg = _loginStore.RegisterUser(login);
            return msg;
        }

        [HttpPost]
        public string ValidateUser(string username, string password)
        {
            string token = _loginStore.ValidateUser(username, password);
            return token;
        }

    }
}
