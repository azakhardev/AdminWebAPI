using K4os.Compression.LZ4.Engine;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Tables;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/Configs")]
    [ApiController]
    public class ConfigsCtr : ControllerBase
    {
        BackupDatabase dbBackup = new BackupDatabase();
        // GET: api/<Configs>
        [HttpGet]
        public IEnumerable<tbConfigs> Get()
        {
            return dbBackup.Configs;
        }

        // GET api/<Configs>/5
        [HttpGet("{id}")]
        public tbConfigs Get(int id)
        {
            return dbBackup.Configs.Find(id);
        }

        // POST api/<Configs>
        [HttpPost]
        public void Post(string configName, DateTime creationDate, string algorithm, int maxPackageAmount, int maxPackageSize, string schedule, bool zip)
        {
            tbConfigs newConfig = new tbConfigs()
            {
                ConfigName = configName,
                CreationDate = creationDate,
                Algorithm = algorithm,
                MaxPackageAmount = maxPackageAmount,
                MaxPackageSize = maxPackageSize,
                Schedule = schedule,
                Zip = zip
            };

            dbBackup.Configs.Add(newConfig);
            dbBackup.SaveChanges();
        }

        // PUT api/<Configs>/5
        [HttpPut("{id}")]
        public void Put(int id, string newConfigName, DateTime newCreationDate, string newAlgorithm, int newMaxPackageAmount, int newMaxPackageSize, string newSchedule, bool newZip)
        {
            tbConfigs updatedConfig = dbBackup.Configs.Find(id);

            updatedConfig.ConfigName = newConfigName;
            updatedConfig.CreationDate = newCreationDate;
            updatedConfig.Algorithm = newAlgorithm;
            updatedConfig.MaxPackageAmount = newMaxPackageAmount;
            updatedConfig.MaxPackageSize = newMaxPackageSize;
            updatedConfig.Schedule = newSchedule;
            updatedConfig.Zip = newZip;

            dbBackup.SaveChanges();
        }

        // DELETE api/<Configs>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            dbBackup.Configs.Remove(dbBackup.Configs.Find(id));
        }
    }
}
