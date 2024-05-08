using JWTProject.Models;

namespace JWTProject.Store.Abstractions
{
    public interface IAccessInfoStore
    {
        List<UserAccess> AccessForuser();
        List<AdminAccess> AccessForAdmin();
    }
}
