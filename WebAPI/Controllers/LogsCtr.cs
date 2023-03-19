using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebAPI.FormatCheck;
using WebAPI.Tables;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/Logs")]
    [ApiController]
    public class LogsCtr : ControllerBase
    {
        BackupDatabase dbBackup = new BackupDatabase();
        LogCheck checkLog = new LogCheck();

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
        public ActionResult<tbLogs> Post([FromBody] tbLogs log)
        {
            try
            {
                checkLog.CheckAll(log);
            }
            catch (FormatException ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, $"{ex}");
            }

            dbBackup.Logs.Add(log);
            dbBackup.SaveChanges();

            return log;
        }

        // PUT api/<Logs>/5
        [HttpPut("{id}")]
        public ActionResult<tbLogs> Put(int id, [FromBody] tbLogs log)
        {
            
            //DateTime newDate, bool newError, string newMessage
            tbLogs updatedLog = this.dbBackup.Logs.Find(id);

            try
            {
                checkLog.CheckAll(log);
            }
            catch (FormatException ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, $"{ex}");
            }


            updatedLog.Date = log.Date;
            updatedLog.Errors = log.Errors;
            updatedLog.Message = log.Message;
            dbBackup.SaveChanges();

            return log;
        }

        // DELETE api/<Logs>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            dbBackup.Logs.Remove(dbBackup.Logs.Find(id));
        }
    }
}
