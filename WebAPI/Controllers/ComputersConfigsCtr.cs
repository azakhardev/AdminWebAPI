using Microsoft.AspNetCore.Mvc;
using WebAPI.Tables;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComputersConfigsCtr : ControllerBase
    {
        public BackupDatabase dbBackup = new BackupDatabase();

        // POST api/<ComputersConfigsCtr>
        [HttpPost("{pcId}/{cfId}")]
        public void Post(int pcId, int cfId)
        {
            ComputersConfigsTb pcCf = new ComputersConfigsTb() {ConfigID = cfId, ComputerID = pcId, Snapshot = ""};

            dbBackup.ComputersConfigs.Add(pcCf);
            dbBackup.SaveChanges();
        }

        // DELETE api/<ComputersConfigsCtr>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            dbBackup.ComputersConfigs.Remove(dbBackup.ComputersConfigs.Find(id));
            dbBackup.SaveChanges();
        }
    }
}
