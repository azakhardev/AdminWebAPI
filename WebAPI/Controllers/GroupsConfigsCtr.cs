using Microsoft.AspNetCore.Mvc;
using WebAPI.Tables;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsConfigsCtr : ControllerBase
    {
        public BackupDatabase dbBackup = new BackupDatabase();

        // POST api/<GroupsConfigsCtr>
        [HttpPost("{grpId}/{cfId}")]
        public void Post(int grpId, int cfId)
        {
            GroupsConfigsTb grpCf = new GroupsConfigsTb() { ConfigID = cfId, GroupID = grpId};

            dbBackup.GroupsConfigs.Add(grpCf);
            dbBackup.SaveChanges();
        }

        // DELETE api/<GroupsConfigsCtr>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            dbBackup.ComputersConfigs.Remove(dbBackup.ComputersConfigs.Find(id));
            dbBackup.SaveChanges();
        }
    }
}

