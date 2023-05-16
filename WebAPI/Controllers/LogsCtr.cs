using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebAPI.FormatCheck;
using WebAPI.Tables;
using static Org.BouncyCastle.Math.EC.ECCurve;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/Logs")]
    [ApiController]
    public class LogsCtr : ControllerBase
    {
        BackupDatabase dbBackup = new BackupDatabase();
        LogCheck checkLog = new LogCheck();

        // Všechny reporty
        [HttpGet]
        public IEnumerable<LogsTb> Get()
        {
            return dbBackup.Logs;
        }

        // Určitý report
        [HttpGet("{id}")]
        public LogsTb Get(int id)
        {
            return dbBackup.Logs.Find(id);
        }

        //// Jméno určitého počítače podle ID
        //[HttpGet("ComputerName/{computerId}")]
        //public string GetPcName(int computerId)
        //{
        //    return dbBackup.Computers.Find(computerId).ComputerName;
        //}

        //// Jméno určitého configu podle ID
        //[HttpGet("ConfigName/{configId}")]
        //public string GetCfName(int configId)
        //{
        //    return dbBackup.Configs.Find(configId).ConfigName;
        //}

        // Určitý reportu pro určitý počítač a config
        [HttpGet("{computerId}/{configId}")]
        public ActionResult<int> GetComputersConfigs(int computerId, int configId) 
        {
            try
            {
                int computersConfigsId = dbBackup.ComputersConfigs.Where(x => x.ComputerID == computerId).Where(x => x.ConfigID == configId).FirstOrDefault().ID;
                return computersConfigsId;
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, $"{ex}");
            }
        }

        // Přidání reportu
        [HttpPost]
        public ActionResult<LogsTb> Post([FromBody] LogsTb log)
        {
            log.ComputerName = dbBackup.Computers.Find(log.ComputerId).ComputerName;
            log.ConfigName = dbBackup.Configs.Find(log.ConfigId).ConfigName;

            try
            {
                checkLog.CheckAll(log);
                dbBackup.Logs.Add(log);

                if (dbBackup.Logs.Count() >= 999)
                {
                    int x = dbBackup.Logs.Count() - 999;
                    for (int i = 0; i < x; i++)
                    {
                        dbBackup.Logs.Remove(dbBackup.Logs.First());
                    }
                }

                dbBackup.SaveChanges();
            }
            catch (FormatException ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, $"{ex}");
            }

            return log;
        }

        // Odstranění reportu
        [HttpDelete("{id}")]
        public ActionResult<string> Delete(int id)
        {
            try
            {
                dbBackup.Logs.Remove(dbBackup.Logs.Find(id));
                dbBackup.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, $"{ex}");
            }

            return "Report deleted successfully";
        }
    }
}