using Microsoft.AspNetCore.Mvc;
using WebAPI.Tables;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/Logs")]
    [ApiController]
    public class LogsCtr : ControllerBase
    {
        BackupDatabase dbBackup = new BackupDatabase();
        // GET: api/<Logs>
        [HttpGet]
        public IEnumerable<tbLogs> Get()
        {
            return dbBackup.Logs;
        }

        // GET api/<Logs>/5
        [HttpGet("{id}")]
        public tbLogs Get(int id)
        {
            return dbBackup.Logs.Find(id);
        }

        // POST api/<Logs>
        [HttpPost]
        public void Post(DateTime date, bool error, string message)
        {
            dbBackup.Logs.Add(new tbLogs() {Date = date, Errors = error, Message = message});
            dbBackup.SaveChanges();
        }

        // PUT api/<Logs>/5
        [HttpPut("{id}")]
        public void Put(int id, DateTime newDate, bool newError, string newMessage)
        {
            tbLogs updatedLog = dbBackup.Logs.Find(id);

            updatedLog.Date = newDate;
            updatedLog.Errors = newError;
            updatedLog.Message = newMessage;

            dbBackup.SaveChanges();
        }

        // DELETE api/<Logs>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            dbBackup.Logs.Remove(dbBackup.Logs.Find(id));
        }
    }
}
