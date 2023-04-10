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
        public IEnumerable<LogsTb> Get()
        {
            return dbBackup.Logs;
        }

        // GET api/<Logs>/5
        [HttpGet("{id}")]
        public LogsTb Get(int id)
        {
            return dbBackup.Logs.Find(id);
        }

        [HttpGet("{computerId}/{configId}")]
        public int GetComputersConfigs(int computerId, int configId) 
        {
            int computersConfigsId = dbBackup.ComputersConfigs.Where(x => x.ComputerID == computerId).Where(x => x.ConfigID == configId).FirstOrDefault().ID;
            return computersConfigsId;
        }

        // POST api/<Logs>
        [HttpPost]
        public ActionResult<LogsTb> Post([FromBody] LogsTb log)
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

        // DELETE api/<Logs>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            dbBackup.Logs.Remove(dbBackup.Logs.Find(id));
        }
    }
}
