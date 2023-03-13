using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebAPI.Tables;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/Admins")]
    [ApiController]
    public class AdminsCtr : ControllerBase
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
        public void Post([FromBody] string value)
        {
            //admin = new Tables.tbAdmins() { ID = 1, Username = "WTF", Password = "123456", Email = "wtf@nejde.to", Description = "rawr", Active = true };
            //dbBackup.Admins.Add(admin);
            //dbBackup.SaveChanges();

            //return admin;
            //dbBackup.Admins.Add(value);
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
