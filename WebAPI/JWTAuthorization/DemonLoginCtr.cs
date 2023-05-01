using JWT.Algorithms;
using JWT.Builder;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Tables.Help_Tables;
using WebAPI.Tables;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.JWTAuthorization
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemonLoginCtr : ControllerBase
    {
        BackupDatabase dbBackup = new BackupDatabase();

        [HttpPost]
        public ActionResult<string> Post(ComputerForLogin login)
        {
            try
            {
                ComputersTb computer = dbBackup.Computers.Where(x => x.ID == login.ID).First();

                if (login.ComputerStatus != "Blocked")
                {
                    string token = JwtBuilder.Create()
                    .WithAlgorithm(new HMACSHA256Algorithm())
                    .WithSecret("super-secret-bar")
                    .AddClaim("exp", DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds())
                    .AddClaim("computer", computer.ComputerName)
                    .AddClaim("status", computer.ComputerStatus)
                    .Encode();

                    return Ok(new { token = token });
                }
                return Unauthorized(new { message = "Computer is blocekd. Access denied." });
            }
            catch
            {
                return Unauthorized(new { message = "Wrong computer ID." });
            }
        }
    }
}
