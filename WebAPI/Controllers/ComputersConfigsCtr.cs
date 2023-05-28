using Microsoft.AspNetCore.Mvc;
using Mysqlx.Crud;
using System.Net;
using WebAPI.Tables;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/ComputersConfigs")]
    [ApiController]
    public class ComputersConfigsCtr : ControllerBase
    {
        public BackupDatabase dbBackup = new BackupDatabase();

        // Přidání relace pro určitý počítač a config
        [HttpPost("{computerId}/{configId}")]
        public ActionResult<string> Post(int computerId, int configId)
        {

            if (dbBackup.Computers.Find(computerId) == null)
                return $"Computer with id {computerId} doesn't exist.";

            if (dbBackup.Configs.Find(configId) == null)
                return $"Config with id {configId} doesn't exist.";

            ComputersConfigsTb pcCf = new ComputersConfigsTb() { ConfigID = configId, ComputerID = computerId, Snapshot = "" };
            dbBackup.ComputersConfigs.Add(pcCf);
            dbBackup.SaveChanges();

            return $"Relation with computer (id:{computerId}) and config (id:{configId}) created successfully.";
        }

        // Odstranění relace pro určitý počítač a config
        [HttpDelete("{computerId}/{configId}")]
        public ActionResult<string> Delete(int computerId, int configId)
        {
            try
            {
                dbBackup.ComputersConfigs.Remove(dbBackup.ComputersConfigs.Where(x => x.ComputerID == computerId).Where(x => x.ConfigID == configId).FirstOrDefault());
                dbBackup.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, $"{ex}");
            }
            return "Relation deleted successfully";
        }
    }
}
