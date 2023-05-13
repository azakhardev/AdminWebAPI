using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebAPI.Tables;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsConfigsCtr : ControllerBase
    {
        public BackupDatabase dbBackup = new BackupDatabase();

        // Přidání relace pro určitou skupinu a config
        [HttpPost("{groupId}/{configId}")]
        public ActionResult<string> Post(int groupId, int configId)
        {

            if (dbBackup.Configs.Find(configId) == null)
                return $"Config with id {configId} doesn't exist.";

            if (dbBackup.Groups.Find(groupId) == null)
                return $"Group with id {groupId} doesn't exist.";

            GroupsConfigsTb grpCf = new GroupsConfigsTb() { ConfigID = configId, GroupID = groupId };
            dbBackup.GroupsConfigs.Add(grpCf);
            dbBackup.SaveChanges();

            return $"Relation with group (id:{groupId}) and config (id:{configId}) created successfully.";
        }

        // Odstranění relace pro určitou skupinu a config
        [HttpDelete("{groupId}/{configId}")]
        public ActionResult<string> Delete(int groupId, int configId)
        {
            try
            {
                dbBackup.GroupsConfigs.Remove(dbBackup.GroupsConfigs.Where(x => x.GroupID == groupId).Where(x => x.ConfigID == configId).LastOrDefault());
                dbBackup.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, $"{ex}");
            }

            return "Relation deleted successfully.";
        }
    }
}

