using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebAPI.Tables;
using static Org.BouncyCastle.Math.EC.ECCurve;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComputersGroupsCtr : ControllerBase
    {
        public BackupDatabase dbBackup = new BackupDatabase();

        // Přidání relace pro určitý počítač a skupinu
        [HttpPost("{computerId}/{groupId}")]
        public ActionResult<string> Post(int computerId, int groupId)
        {

            if (dbBackup.Computers.Find(computerId) == null)
                return $"Computer with id {computerId} doesn't exist.";

            if (dbBackup.Groups.Find(groupId) == null)
                return $"Group with id {groupId} doesn't exist.";

            ComputersGroupsTb pcGrp = new ComputersGroupsTb() { GroupID = groupId, ComputerID = computerId };
            dbBackup.ComputersGroups.Add(pcGrp);
            dbBackup.SaveChanges();

            return $"Relation with computer (id:{computerId}) and group (id:{groupId}) created successfully.";
        }

        // Odstranění relace pro určitý počítač a skupinu
        [HttpDelete("{computerId}/{groupId}")]
        public ActionResult<string> Delete(int computerId, int groupId)
        {
            try
            {
                dbBackup.ComputersGroups.Remove(dbBackup.ComputersGroups.Where(x => x.ComputerID == computerId).Where(x => x.GroupID == groupId).FirstOrDefault());
                dbBackup.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, $"{ex}");
            }
            return "Relation deleted succesfully.";
        }
    }
}
