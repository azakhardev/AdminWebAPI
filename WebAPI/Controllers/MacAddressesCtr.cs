using Microsoft.AspNetCore.Mvc;
using WebAPI.Tables;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/MacAddresses")]
    [ApiController]
    public class MacAddressesCtr : ControllerBase
    {
        public BackupDatabase dbBackup = new BackupDatabase();
        public MacAddressesTb tbMacAddresses = new MacAddressesTb();

        // GET: api/<MacAddresses>
        [HttpGet]
        public IEnumerable<MacAddressesTb> Get()
        {
            return dbBackup.MacAddresses;
        }

        // POST api/<MacAddresses>
        [HttpPost]
        public ActionResult<MacAddressesTb> Post([FromBody] MacAddressesTb macAddress)
        {
            try
            {

            }
            catch (FormatException ex)
            {
                
            }

            return macAddress;
        }

        // DELETE api/<MacAddresses>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
        

    }
}
