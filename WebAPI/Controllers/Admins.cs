using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Admins : ControllerBase
    {
        BackupDatabase bd = new BackupDatabase();   

        // GET: api/<Admins>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<Admins>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<Admins>
        [HttpPost]
        public void Post([FromBody] string value)
        {
            Tables.Admins admin = new Tables.Admins() {Username = "Karel",  Password = "123456", Description = "xxx",Email= "karel@mail.com", Active = true,};
            bd.Add(admin);
            bd.SaveChanges();
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
