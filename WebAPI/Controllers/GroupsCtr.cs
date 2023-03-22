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

        // GET: api/<Groups>
        [HttpGet]
        public IEnumerable<GroupsTb> Get()
        {
            return dbBackup.Groups.Include(x => x.GroupsConfigs).Include(x => x.ComputersGroups);
        }

        // GET api/<Groups>/5
        [HttpGet("{id}")]
        public GroupsTb Get(int id)
        {
            return dbBackup.Groups.Include(x => x.GroupsConfigs).Include(x => x.ComputersGroups).Where(x => x.ID == id).FirstOrDefault();
        }

        // GET api/<Groups>/5/Computers
        [HttpGet("{id}/Computers")]
        public List<ComputersTb> GetComputers(int id)
        {
            return dbBackup.Groups.Find(id).GetComputers(id, dbBackup);
        }

        // GET api/<Groups>/5/Configs
        [HttpGet("{id}/Configs")]
        public List<ConfigsTb> GetConfigs(int id)
        {
            return dbBackup.Groups.Find(id).GetConfigs(id, dbBackup);
        }

        // POST api/<Groups>
        [HttpPost]
        public ActionResult<GroupsTb> Post([FromBody] GroupsTb group)
        {
            try
            {
                checkGroup.CheckAll(group);
            }
            catch (FormatException ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, $"{ex}");
            }

            dbBackup.Groups.Add(group);
            dbBackup.SaveChanges();

            return group;
        }

        // PUT api/<Groups>/5
        [HttpPut("{id}")]
        public ActionResult<GroupsTb> Put(int id, [FromBody] GroupsTb group)
        {            
            GroupsTb updatedGroup = this.dbBackup.Groups.Find(id);

            try
            {
                checkGroup.CheckAll(group);
            }
            catch (FormatException ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, $"{ex}");
            }

            updatedGroup.GroupName = group.GroupName;
            updatedGroup.Description = group.Description;
            dbBackup.SaveChanges();

            return updatedGroup;
        }

        // DELETE api/<Groups>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            dbBackup.Groups.Remove(dbBackup.Groups.Find(id));
        }
    }
}
