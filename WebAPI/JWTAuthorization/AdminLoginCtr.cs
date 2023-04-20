using JWT.Algorithms;
using JWT.Builder;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Tls;
using WebAPI.Tables;
using WebAPI.Tables.Help_Tables;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.JWTAuthorization
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeAdmin]
    public class AdminLoginCtr : ControllerBase
    {
        BackupDatabase dbBackup = new BackupDatabase();

        [HttpPost]
        public ActionResult<string> Post(AdminForLogin login)
        {
            try
            {
                AdminsTb admin = dbBackup.Admins.Where(x => x.Username == login.Username).First();

                if (login.Password == admin.Password)
                {
                    string token = JwtBuilder.Create()
                    .WithAlgorithm(new HMACSHA256Algorithm())
                    .WithSecret("skuper-secret-foo")
                    .AddClaim("exp", DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds())
                    .AddClaim("username", admin.Username)
                    .AddClaim("email", admin.Email)
                    .AddClaim("active", admin.Active)
                    .Encode();

                    return Ok(new { token });
                }
                return Unauthorized(new { message = "Wrong password" });
            }
            catch
            {
                return Unauthorized(new { message = "Wrong username" });
            }
        }
    }
}
