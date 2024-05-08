using JWTProject.Models;

namespace JWTProject.Store.Abstractions
{
    public interface ILoginStore
    {
        string RegisterUser(Login login);
        string ValidateUser(string username, string password);
    }
}
