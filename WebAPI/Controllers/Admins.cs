using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebAPI.Tables;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Admins : ControllerBase
    {
        BackupDatabase dbBackup = new BackupDatabase();   
        
        // GET: api/<Admins>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            List<Tables.tbAdmins> Foo = dbBackup.Admins.FromSqlRaw("SELECT Username FROM Admins WHERE ID >= {0}",1).ToList();
            string[] administrators = new string[] { };
            foreach (Tables.tbAdmins admin in Foo) 
            {
                administrators = administrators.Append(admin.Username).ToArray();
            }
            return administrators;
        }

        // GET api/<Admins>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return dbBackup.Admins.FromSqlRaw("SELECT * FROM Admins WHERE ID = {0}", id).ToString();
        }

        // POST api/<Admins>
        [HttpPost]
        public void Post([FromBody] Tables.tbAdmins value)
        {
            dbBackup.Admins.Add(value);
            dbBackup.SaveChanges();
        }

        // PUT api/<Admins>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Admins>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
