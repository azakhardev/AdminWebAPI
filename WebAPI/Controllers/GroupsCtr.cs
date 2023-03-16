using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
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
        public tbGroups Post([FromBody] tbGroups group)
        {
            if (Regex.IsMatch(group.GroupName, @"[^/\\*?\"":<>|]+$") == true)
                //string groupName, int computersId, string description
                //dbBackup.Groups.Add(new tbGroups() {GroupName = groupName, Computers = dbBackup.Computers.Find(computersId), Description = description});
                dbBackup.Groups.Add(group);
            dbBackup.SaveChanges();

            return group;
        }

        // PUT api/<Groups>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] tbGroups group)
        {
            //string newGroupName, int newComputersId, string newDescription
            tbGroups updatedGroup = this.dbBackup.Groups.Find(id);

            if (Regex.IsMatch(group.GroupName, @"[^/\\*?\"":<>|]+$") == true)
                updatedGroup.GroupName = group.GroupName;
            if (dbBackup.Computers.Find(group.ComputerID) != null)
                updatedGroup.Computers = group.Computers;
            updatedGroup.Description = group.Description;

            dbBackup.SaveChanges();
        }

        // DELETE api/<Groups>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            dbBackup.Groups.Remove(dbBackup.Groups.Find(id));
        }
    }
}
