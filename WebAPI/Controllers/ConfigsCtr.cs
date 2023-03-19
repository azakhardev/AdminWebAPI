using K4os.Compression.LZ4.Engine;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebAPI.FormatCheck;
using WebAPI.Tables;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/Configs")]
    [ApiController]
    public class ConfigsCtr : ControllerBase
    {
        BackupDatabase dbBackup = new BackupDatabase();
        ConfigCheck checkConfig = new ConfigCheck();
        
        // GET: api/<Configs>
        [HttpGet]
        public IEnumerable<tbConfigs> Get()
        {
            return dbBackup.Configs.Include(x => x.GroupsConfigs).Include(x => x.ComputersConfigs).Include(x => x.Sources).Include(x => x.Destinations);
        }

        // GET api/<Configs>/5
        [HttpGet("{id}")]
        public tbConfigs Get(int id)
        {
            return dbBackup.Configs.Include(x => x.GroupsConfigs).Include(x => x.ComputersConfigs).Include(x => x.Sources).Include(x => x.Destinations).Where(x => x.ID == id).FirstOrDefault();
        }

        // GET api/<Configs>/5/Computers
        [HttpGet("{id}/Computers")]
        public List<tbComputers> GetComputers(int id)
        {
            return dbBackup.Configs.Find(id).GetComputers(id,dbBackup);
        }

        // GET api/<Configs>/5/Groups
        [HttpGet("{id}/Groups")]
        public List<tbGroups> GetGroups(int id)
        {
            return dbBackup.Configs.Find(id).GetGroups(id, dbBackup);
        }

        // GET api/<Configs>/5/Sources
        [HttpGet("{id}/Sources")]
        public List<string> GetSources(int id)
        {
            return dbBackup.Configs.Find(id).GetSourcePaths(id, dbBackup);
        }

        // GET api/<Configs>/5/Desttinations
        [HttpGet("{id}/Destinations")]
        public List<string> GetDestinations(int id)
        {
            return dbBackup.Configs.Find(id).GetDestinationPaths(id, dbBackup);
        }
        
        // POST api/<Configs>
        [HttpPost]
        public ActionResult<tbConfigs> Post([FromBody] tbConfigs config)
        {
            try
            {
                checkConfig.CheckAll(config);
            }
            catch (FormatException ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, $"{ex}");
            }
            dbBackup.Configs.Add(config);
            dbBackup.SaveChanges();

            return config;
        }

        // PUT api/<Configs>/5
        [HttpPut("{id}")]
        public ActionResult<tbConfigs> Put(int id, [FromBody] tbConfigs config)
        {            
            tbConfigs updatedConfig = dbBackup.Configs.Find(id);

            try
            {
                checkConfig.CheckAll(config);
            }
            catch (FormatException ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, $"{ex}");
            }

            updatedConfig.ConfigName = config.ConfigName;
            updatedConfig.CreationDate = config.CreationDate;
            updatedConfig.Algorithm = config.Algorithm;
            updatedConfig.MaxPackageAmount = config.MaxPackageAmount;
            updatedConfig.MaxPackageSize = config.MaxPackageSize;
            updatedConfig.Schedule = config.Schedule;
            updatedConfig.Zip = config.Zip;
            dbBackup.SaveChanges();
            
            return updatedConfig;
        }

        // DELETE api/<Configs>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            dbBackup.Configs.Remove(dbBackup.Configs.Find(id));
        }

        // DELETE api/<Configs>/5/Source
        [HttpDelete("{id}/Source")]
        public void DeleteSource(int id)
        {
            dbBackup.Sources.Remove(dbBackup.Sources.Where(x => x.ConfigID == id).FirstOrDefault());
        }

        // DELETE api/<Configs>/5/Destination
        [HttpDelete("{id}/Destination")]
        public void DeleteDestination(int id)
        {
            dbBackup.Destinations.Remove(dbBackup.Destinations.Where(x => x.ConfigID == id).FirstOrDefault());
        }
    }
}
