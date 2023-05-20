using K4os.Compression.LZ4.Engine;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebAPI.FormatCheck;
using WebAPI.Tables;
using static Org.BouncyCastle.Math.EC.ECCurve;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/Configs")]
    [ApiController]
    public class ConfigsCtr : ControllerBase
    {
        BackupDatabase dbBackup = new BackupDatabase();
        ConfigCheck checkConfig = new ConfigCheck();

        // Všechny configy
        [HttpGet]
        public IEnumerable<ConfigsTb> Get()
        {
            return dbBackup.Configs.Include(x => x.GroupsConfigs).Include(x => x.ComputersConfigs).Include(x => x.Sources).Include(x => x.Destinations);
        }

        // Určitý config
        [HttpGet("{id}")]
        public ConfigsTb Get(int id)
        {
            return dbBackup.Configs.Include(x => x.GroupsConfigs).Include(x => x.ComputersConfigs).Include(x => x.Sources).Include(x => x.Destinations).Where(x => x.ID == id).FirstOrDefault();
        }

        // Počítače pro určitý config
        [HttpGet("{configId}/Computers")]
        public List<ComputersTb> GetComputers(int configId)
        {
            List<ComputersTb> computers = new List<ComputersTb>();

            foreach (var config in dbBackup.Configs.Find(configId).GetComputers(configId, dbBackup))
            {
                computers.Add(config);
            }

            List<GroupsConfigsTb> groupsConfigs = new List<GroupsConfigsTb>();
            groupsConfigs = dbBackup.GroupsConfigs.Where(x => x.ConfigID == configId).ToList();

            foreach (var groupsConfig in groupsConfigs)
            {
                foreach (var groupConfig in dbBackup.Groups.Find(groupsConfig.GroupID).GetComputers(groupsConfig.GroupID, dbBackup))
                {
                    computers.Add(groupConfig);
                }
            }

            return computers;
        }

        // Skupiny pro určitý config
        [HttpGet("{id}/Groups")]
        public List<GroupsTb> GetGroups(int id)
        {
            return dbBackup.Configs.Find(id).GetGroups(id, dbBackup);
        }

        // Zdrojové cesty pro určitý config
        [HttpGet("{id}/Sources")]
        public List<SourcesTb> GetSources(int id)
        {
            return dbBackup.Configs.Find(id).GetSourcePaths(id, dbBackup);
        }

        // Destinace pro určitý config
        [HttpGet("{id}/Destinations")]
        public List<DestinationsTb> GetDestinations(int id)
        {
            return dbBackup.Configs.Find(id).GetDestinationPaths(id, dbBackup);
        }

        // Snapshot pro určitý config a počítač
        [HttpGet("{configID}/{computerID}/Snapshot")]
        public string GetSnapshot(int configID, int computerID)
        {
            return dbBackup.Configs.Find(configID).GetSnapshot(configID, computerID, dbBackup);
        }

        //Id posledního configu
        [HttpGet("LastConfig")]
        public int GetLastConfigId()
        {
            return dbBackup.Configs.OrderBy(x => x.ID).Last().ID;
        }

        // Přidání configu
        [HttpPost]
        public ActionResult<ConfigsTb> Post([FromBody] ConfigsTb config)
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

        // Změna configu
        [HttpPut("{id}")]
        public ActionResult<ConfigsTb> Put(int id, [FromBody] ConfigsTb config)
        {
            ConfigsTb updatedConfig = dbBackup.Configs.Find(id);

            if (config.ConfigName != null)
                updatedConfig.ConfigName = config.ConfigName;
            if (config.CreationDate != null)
                updatedConfig.CreationDate = config.CreationDate;
            if (config.Algorithm != null)
                updatedConfig.Algorithm = config.Algorithm;
            if (config.MaxPackageAmount != null)
                updatedConfig.MaxPackageAmount = config.MaxPackageAmount;
            if (config.MaxPackageSize != null)
                updatedConfig.MaxPackageSize = config.MaxPackageSize;
            if (config.Schedule != null)
                updatedConfig.Schedule = config.Schedule;
            if (config.Zip != null)
                updatedConfig.Zip = config.Zip;

            try
            {
                checkConfig.CheckAll(updatedConfig);
                dbBackup.SaveChanges();
            }
            catch (FormatException ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, $"{ex}");
            }

            return updatedConfig;
        }

        // Změna zdrojové cesty pro určitý config
        [HttpPut("{sourceId}/Source")]
        public SourcesTb PutSourcePath(int sourceId, [FromBody] SourcesTb source)
        {
            SourcesTb updatedSource = dbBackup.Sources.Find(sourceId);

            if (source.SourcePath != null)
                updatedSource.SourcePath = source.SourcePath;
            if (source.FileName != null)
                updatedSource.FileName = source.FileName;
            if (source.UpdateDate != null)
                updatedSource.UpdateDate = source.UpdateDate;
            
            dbBackup.SaveChanges();

            return updatedSource;
        }

        // Změna destinace pro určitý config
        [HttpPut("{destinationId}/Destination")]
        public DestinationsTb PutDestinationPath(int destinationId, [FromBody] DestinationsTb destination) 
        {
            DestinationsTb updatedDestination = dbBackup.Destinations.Find(destinationId);

            if (destination.DestinationPath != null)
                updatedDestination.DestinationPath = destination.DestinationPath;
            
            dbBackup.SaveChanges();

            return updatedDestination;

        }

        // Odstranění určitého configu
        [HttpDelete("{id}")]
        public ActionResult<string> Delete(int id)
        {
            try
            {
                dbBackup.Configs.Remove(dbBackup.Configs.Find(id));
                dbBackup.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, $"{ex}");
            }

            return "Config deleted successfully.";
        }

        // Odstranění určité zdrojové cesty
        [HttpDelete("{sourceId}/Source")]
        public ActionResult<string> DeleteSource(int sourceId)
        {
            try
            {
                dbBackup.Sources.Remove(dbBackup.Sources.Find(sourceId));
                dbBackup.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, $"{ex}");
            }

            return "Source for config deleted successfully.";
        }

        // Odstranění určité destinace
        [HttpDelete("{destinationId}/Destination")]
        public ActionResult<string> DeleteDestination(int destinationId)
        {
            try
            {
                dbBackup.Destinations.Remove(dbBackup.Destinations.Find(destinationId));
                dbBackup.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, $"{ex}");
            }

            return "Destiantion for config deleted successfully.";
        }
    }
}
