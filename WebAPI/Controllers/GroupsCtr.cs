using Microsoft.AspNetCore.Mvc;
using WebAPI.Tables;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/Groups")]
    [ApiController]
    public class GroupsCtr : ControllerBase
    {
        BackupDatabase dbBackup = new BackupDatabase();
        // GET: api/<Groups>
        [HttpGet]
        public IEnumerable<tbGroups> Get()
        {
            return dbBackup.Groups;
        }

        // GET api/<Groups>/5
        [HttpGet("{id}")]
        public tbGroups Get(int id)
        {
            return dbBackup.Groups.Find(id);
        }

        // POST api/<Groups>
        [HttpPost]
        public void Post(string groupName, int computersId, string description)
        {
            dbBackup.Groups.Add(new tbGroups() {GroupName = groupName, Computers = dbBackup.Computers.Find(computersId), Description = description});
        }

        // PUT api/<Groups>/5
        [HttpPut("{id}")]
        public void Put(int id, string newGroupName, int newComputersId, string newDescription)
        {
            tbGroups updatedGroup = dbBackup.Groups.Find(id);

            updatedGroup.GroupName = newGroupName;
            updatedGroup.Computers = dbBackup.Computers.Find(newComputersId);
            updatedGroup.Description = newDescription;
        }

        // DELETE api/<Groups>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            dbBackup.Groups.Remove(dbBackup.Groups.Find(id));
        }
    }
}
