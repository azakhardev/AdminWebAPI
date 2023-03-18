using Microsoft.AspNetCore.WebUtilities;
using MySql.EntityFrameworkCore.Extensions;
using Org.BouncyCastle.Tls.Crypto;
using System.ComponentModel.DataAnnotations.Schema;

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
        public virtual List<tbGroups> Groups { get; set; }

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

        public List<string> GetConfigs(int id, BackupDatabase dbBackup)
        {
            List <tbComputersConfigs> tbComputersConfigs = dbBackup.ComputersConfigs.Where(x => x.ComputerID == id).ToList();
            List <string> configs = new List<string>();

            foreach (tbComputersConfigs config in tbComputersConfigs) 
            {
                configs.Add($"Config ID: {config.ID}, Config Name: {dbBackup.Configs.Where(x => x.ID == id).FirstOrDefault().ConfigName}");
            }

            return configs;
        }

        public List<string> GetGroups(int id, BackupDatabase dbBackup) 
        {
            List<tbGroups> tbGroups = dbBackup.Groups.Where(x => x.ComputerID == id).ToList();
            List<string> groups = new List<string>();

            foreach (tbGroups group in tbGroups) 
            {
                groups.Add(group.GroupName);
            }
            return groups;
        }

        public List<string> GetLogs(int id, BackupDatabase dbBackup) 
        {
            List<tbComputersConfigs> tbComputersConfigs = dbBackup.ComputersConfigs.Where(x => x.ComputerID == id).ToList();
            List<string> logs = new List<string>();

            foreach (tbComputersConfigs log in tbComputersConfigs)
            {
                logs.Add($"Log ID: {log.ID}, Message: {dbBackup.Logs.Where(x => x.ID == id).FirstOrDefault().Message}");
            }

            return logs;
        }
    }
}
