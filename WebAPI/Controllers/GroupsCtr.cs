using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.RegularExpressions;
using WebAPI.FormatCheck;
using WebAPI.Tables;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/Groups")]
    [ApiController]
    public class GroupsCtr : ControllerBase
    {
        BackupDatabase dbBackup = new BackupDatabase();
        GroupCheck checkGroup = new GroupCheck();

        // Všechny skupiny
        [HttpGet]
        public IEnumerable<GroupsTb> Get()
        {
            return dbBackup.Groups.Include(x => x.GroupsConfigs).Include(x => x.ComputersGroups);
        }

        // Určitá skupina
        [HttpGet("{id}")]
        public GroupsTb Get(int id)
        {
            return dbBackup.Groups.Include(x => x.GroupsConfigs).Include(x => x.ComputersGroups).Where(x => x.ID == id).FirstOrDefault();
        }

        // Všechny počítače pro určitou skupinu
        [HttpGet("{id}/Computers")]
        public List<ComputersTb> GetComputers(int id)
        {
            return dbBackup.Groups.Find(id).GetComputers(id, dbBackup);
        }

        // Všechny configy pro určitou skupinu
        [HttpGet("{id}/Configs")]
        public List<ConfigsTb> GetConfigs(int id)
        {
            return dbBackup.Groups.Find(id).GetConfigs(id, dbBackup);
        }

        /*
        // Všechny počítače které nejsou přiřazené k uřčité skupině
        [HttpGet("{groupId}/UnassignedComputers")]
        public List<ConfigsTb> GetUnassignedConfigs(int groupId)
        {
            List<ConfigsTb> allConfigs = dbBackup.Configs.ToList();
            List<ConfigsTb> configsInGrp = dbBackup.Groups.Find(groupId).GetConfigs(groupId, dbBackup);
            return
        }

        // Všechny počítače pro určitou skupinu
        [HttpGet("{groupId}/UnassignedComputers")]
        public List<ComputersTb> GetUnassignedComputers(int groupId)
        {
            return dbBackup.Groups.Find(groupId).GetComputers(groupId, dbBackup);
        }*/

        //Id posledního configu
        [HttpGet("LastGroup")]
        public int GetLastGroupId()
        {
            return dbBackup.Groups.OrderBy(x => x.ID).Last().ID;
        }

        // Přidání nové skupiny
        [HttpPost]
        public ActionResult<GroupsTb> Post([FromBody] GroupsTb group)
        {
            try
            {
                checkGroup.CheckAll(group);
                dbBackup.Groups.Add(group);
            }
            catch (FormatException ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, $"{ex}");
            }

            dbBackup.SaveChanges();

            return group;
        }

        // Změna určité skupiny
        [HttpPut("{id}")]
        public ActionResult<GroupsTb> Put(int id, [FromBody] GroupsTb group)
        {
            GroupsTb updatedGroup = this.dbBackup.Groups.Find(id);

            if (group.GroupName != null)
                updatedGroup.GroupName = group.GroupName;
            if (group.Description != null)
                updatedGroup.Description = group.Description;
            if (group.LastBackup != null)
                updatedGroup.LastBackup = group.LastBackup;
            if (group.BackupStatus != null)
                updatedGroup.BackupStatus = group.BackupStatus;

            try
            {
                checkGroup.CheckAll(updatedGroup);
                dbBackup.SaveChanges();
            }
            catch (FormatException ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, $"{ex}");
            }

            return updatedGroup;
        }

        // Odstranění určité skupiny
        [HttpDelete("{id}")]
        public ActionResult<string> Delete(int id)
        {
            try
            {
                dbBackup.Groups.Remove(dbBackup.Groups.Find(id));
                dbBackup.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, $"{ex}");
            }

            return "Group deleted successfully.";
        }
    }
}
