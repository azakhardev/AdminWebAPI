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
    public class AdminLoginCtr : ControllerBase
    {
        BackupDatabase dbBackup = new BackupDatabase();

        [HttpPost]
        public IActionResult Post(AdminForLogin login)
        {
            try
            {
                AdminsTb admin = dbBackup.Admins.Where(x => x.Username == login.Username).First();

                if (admin.Password == login.Password)
                {
                    string token = JwtBuilder.Create()
                    .WithAlgorithm(new HMACSHA256Algorithm())
                    .WithSecret("super-secret-foo")
                    .AddClaim("exp", DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds())
                    .AddClaim("username", admin.Username)
                    .Encode();

                    return Ok(new { token = token });
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
