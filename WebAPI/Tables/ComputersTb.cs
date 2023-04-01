using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;
using MySql.EntityFrameworkCore.Extensions;
using Org.BouncyCastle.Tls.Crypto;
using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Tables.Help_Tables;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace WebAPI.Tables
{
    [Table("Computers")]
    public class ComputersTb
    {
        public int ID { get; set; }
        public string ComputerName { get; set; }
        public string ComputerStatus { get; set; }
        public string Description { get; set; }
        public DateTime LastBackup { get; set; }
        public string BackupStatus { get; set; }

        [ForeignKey("ComputerID")]
        public virtual List<MacAddressesTb> MacAddresses { get; set; }

        [ForeignKey("ComputerID")]
        public virtual List<ComputersConfigsTb> ComputersConfigs { get; set; }

        [ForeignKey("ComputerID")]
        public virtual List<ComputersGroupsTb> ComputersGroups { get; set; }

        public List<string> GetMacAddresses(int id, BackupDatabase dbBackup)
        {
            List<MacAddressesTb> tbMacAddresses = dbBackup.MacAddresses.Where(x => x.ComputerID == id).ToList();
            List<string> macAddresses = new List<string>();

            foreach (var item in tbMacAddresses)
            {
                macAddresses.Add(item.MacAddress);
            }

            return macAddresses;
        }

        public List<ConfigsTb> GetConfigs(int computerId, BackupDatabase dbBackup)
        {
            List<ComputersConfigsTb> tbComputersConfigs = dbBackup.ComputersConfigs.Where(x => x.ComputerID == computerId).ToList();
            List<ConfigsTb> configs = new List<ConfigsTb>();

            foreach (ComputersConfigsTb config in tbComputersConfigs)
            {
                configs.Add(dbBackup.Configs.Find(config.ConfigID));
            }

            return configs;
        }

        public List<GroupsTb> GetGroups(int id, BackupDatabase dbBackup)
        {
            List<ComputersGroupsTb> tbComputersGroups = dbBackup.ComputersGroups.Where(x => x.ComputerID == id).ToList();
            List<GroupsTb> groups = new List<GroupsTb>();

            foreach (ComputersGroupsTb group in tbComputersGroups)
            {
                groups.Add(dbBackup.Groups.Find(group.GroupID));
            }

            return groups;
        }

        public List<LogsTb> GetLogs(int computerId, int configId, BackupDatabase dbBackup)
        {
            ComputersConfigsTb tbComputersConfigs = dbBackup.ComputersConfigs.Where(x => x.ComputerID == computerId && x.ConfigID == configId).Single();
            List<LogsTb> logs = new List<LogsTb>();

            //LogsTb originalLog = dbBackup.Logs.Find(log.ComputerID);
            //LogsForPC pcLog = new LogsForPC() {ComputerID = originalLog.ComputersConfigsID };

            logs = dbBackup.Logs.Where(x => x.ComputersConfigsID == tbComputersConfigs.ID).ToList();

            //List < LogsForPC >
            //foreach (var item in logs)
            //{

            //}

            return logs;
        }

        public List<string> GetSnapshots(int id, BackupDatabase dbBackup)
        {
            List<ComputersConfigsTb> tbComputersConfigs = dbBackup.ComputersConfigs.Where(x => x.ComputerID == id).ToList();
            List<string> computersConfigs = new List<string>();

            foreach (ComputersConfigsTb item in tbComputersConfigs)
            {
                computersConfigs.Add(item.Snapshot);
            }

            return computersConfigs;
        }

        //public int GetSnapshotVersion(int computerId,int configId,BackupDatabase dbBackup)
        //{
        //    return dbBackup.ComputersConfigs.Where(x => x.ComputerID == computerId).Where(x => x.ConfigID == configId).Single().SnapshotVersion;
        //}
    }
}
