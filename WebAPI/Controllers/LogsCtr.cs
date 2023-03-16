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
        public tbLogs Post([FromBody] tbLogs log)
        {
            //DateTime date, bool error, string message
            //dbBackup.Logs.Add(new tbLogs() {Date = date, Errors = error, Message = message});
            dbBackup.Logs.Add(log);
            dbBackup.SaveChanges();

            return log;
        }

        // PUT api/<Logs>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] tbLogs log)
        {
            //DateTime newDate, bool newError, string newMessage
            tbLogs updatedLog = this.dbBackup.Logs.Find(id);

            updatedLog.Date = log.Date;
            updatedLog.Errors = log.Errors;
            updatedLog.Message = log.Message;

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
