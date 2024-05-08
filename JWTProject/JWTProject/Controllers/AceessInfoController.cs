using JWTProject.Models;
using JWTProject.Store.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWTProject.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    //[AllowAnonymous]
    public class AceessInfoController : ControllerBase
    {
        private readonly IAccessInfoStore _accessInfoStore;
        
        public AceessInfoController(IAccessInfoStore accessInfoStore)
        {
                _accessInfoStore = accessInfoStore;
        }
        [Authorize]
        [HttpPost]
        public List<UserAccess> AccessForUser()
        {
            List<UserAccess> useraccess = _accessInfoStore.AccessForuser();
            return useraccess;
        }
        //[Authorize]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public List<AdminAccess> AccessForAdmin()
        {
            List<AdminAccess> useraccess = _accessInfoStore.AccessForAdmin();
            return useraccess;
        }
    }
}
