using Microsoft.AspNetCore.WebUtilities;
using MySql.EntityFrameworkCore.Extensions;
using Org.BouncyCastle.Tls.Crypto;
using System.ComponentModel.DataAnnotations.Schema;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace WebAPI.Tables
{
    [Table("Computers")]
    public class tbComputers
    {
        public int ID { get; set; }
        public string ComputerName { get; set; }
        public string ComputerStatus { get; set; }
        public string Description { get; set; }
        public DateTime LastBackup { get; set; }
        public string BackupStatus { get; set; }

        [ForeignKey("ComputerID")]
        public virtual List<tbMacAddresses> MacAddresses { get; set; }

        [ForeignKey("ComputerID")]
        public virtual List<tbComputersConfigs> ComputersConfigs { get; set; }

        [ForeignKey("ComputerID")]
        public virtual List<tbComputersGroups> ComputersGroups { get; set; }

        public List<string> GetMacAddresses(int id, BackupDatabase dbBackup) 
        {
            List<tbMacAddresses> tbMacAddresses = dbBackup.MacAdresses.Where(x => x.ComputerID == id).ToList();
            List<string> macAddresses = new List<string>();

            foreach (var item in tbMacAddresses)
            {
                macAddresses.Add(item.MacAddress);
            }

            return macAddresses;
        }

        public List<tbConfigs> GetConfigs(int id, BackupDatabase dbBackup)
        {
            List <tbComputersConfigs> tbComputersConfigs = dbBackup.ComputersConfigs.Where(x => x.ComputerID == id).ToList();
            List <tbConfigs> configs = new List<tbConfigs>();

            foreach (tbComputersConfigs config in tbComputersConfigs) 
            {                
                configs.Add(dbBackup.Configs.Find(config.ConfigID));
            }

            return configs;
        }

        public List<tbGroups> GetGroups(int id, BackupDatabase dbBackup)
        {
            List<tbComputersGroups> tbComputersGroups = dbBackup.ComputersGroups.Where(x => x.ComputerID == id).ToList();
            List<tbGroups> groups = new List<tbGroups>();

            foreach (tbComputersGroups group in tbComputersGroups)
            {
                groups.Add(dbBackup.Groups.Find(group.GroupID));
            }

            return groups;
        }

        public List<tbLogs> GetLogs(int id, BackupDatabase dbBackup) 
        {
            List<tbComputersConfigs> tbComputersConfigs = dbBackup.ComputersConfigs.Where(x => x.ComputerID == id).ToList();
            List<tbLogs> logs = new List<tbLogs>();

            foreach (tbComputersConfigs log in tbComputersConfigs)
            {
                logs.Add(dbBackup.Logs.Find(log.ComputerID));
            }

            return logs;
        }
    }
}
