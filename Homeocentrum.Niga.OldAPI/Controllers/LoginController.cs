using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Homeocentrum.Niga.OldAPI.Model;

namespace Homeocentrum.Niga.OldAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : BaseAPIController
    {
        /// <summary>
        /// Retired. Its tokens were signed with a hardcoded key that neither API accepts, so it only served as an
        /// unthrottled, unaudited password check. Clients sign in through POST /api/Account/Login.
        /// </summary>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]LoginModel model)
        {
            return StatusCode(StatusCodes.Status410Gone, new { success = false, message = "This sign-in endpoint is retired. Use /api/Account/Login." });
        }
    }
}
