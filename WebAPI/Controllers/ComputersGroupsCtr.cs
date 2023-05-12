using Microsoft.AspNetCore.Mvc;
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

        // POST api/<ComputersGroupsCtr>
        [HttpPost("{pcId}/{grpId}")]
        public void Post(int pcId, int grpId)
        {
            ComputersGroupsTb pcGrp = new ComputersGroupsTb() { GroupID = grpId, ComputerID = pcId};

            dbBackup.ComputersGroups.Add(pcGrp);
            dbBackup.SaveChanges();
        }

        // DELETE api/<ComputersGroupsCtr>/5
        [HttpDelete("{computerId}/{groupId}")]
        public void Delete(int computerId, int groupId)
        {
            if (dbBackup.ComputersGroups.Where(x => x.ComputerID == computerId).Where(x => x.GroupID == groupId) != null)
            {
                dbBackup.ComputersGroups.Remove(dbBackup.ComputersGroups.Where(x => x.ComputerID == computerId).Where(x => x.GroupID == groupId).FirstOrDefault());
            }

            dbBackup.SaveChanges();
        }
    }
}
