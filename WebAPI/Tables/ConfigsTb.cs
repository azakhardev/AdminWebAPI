using K4os.Compression.LZ4.Engine;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.OpenApi.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace WebAPI.Tables
{
    [Table("Configs")]
    public class ConfigsTb
    {
        public int ID { get; set; }

        public string ConfigName { get; set; }

        public DateTime CreationDate { get; set; }

        public string Algorithm { get; set; }

        public int MaxPackageAmount { get; set; }

        public int MaxPackageSize { get; set; }

        public string Schedule { get; set; }

        public bool Zip { get; set; }

        [ForeignKey("ConfigID")]
        public virtual List<ComputersConfigsTb> ComputersConfigs { get; set; }

        [ForeignKey("ConfigID")]
        public virtual List<GroupsConfigsTb> GroupsConfigs { get; set; }

        [ForeignKey("ConfigID")]
        public virtual List<SourcesTb> Sources { get; set; }

        [ForeignKey("ConfigID")]
        public virtual List<DestinationsTb> Destinations { get; set; }

        public List<ComputersTb> GetComputers(int id, BackupDatabase dbBackup)
        {
            List<ComputersConfigsTb> tbComputersConfigs = dbBackup.ComputersConfigs.Where(x => x.ConfigID == id).ToList();
            List<ComputersTb> computers = new List<ComputersTb>();

            foreach (ComputersConfigsTb computer in tbComputersConfigs)
            {
                computers.Add(dbBackup.Computers.Find(computer.ComputerID));
            }

            return computers;
        }

        public List<GroupsTb> GetGroups(int id, BackupDatabase dbBackup)
        {
            List<GroupsConfigsTb> tbGroupsConfigs = dbBackup.GroupsConfigs.Where(x => x.ID == id).ToList();
            List<GroupsTb> groups = new List<GroupsTb>();

            foreach (GroupsConfigsTb group in tbGroupsConfigs)
            {
                groups.Add(dbBackup.Groups.Find(group.GroupID));
            }

            return groups;
        }

        public List<SourcesTb> GetSourcePaths(int id, BackupDatabase dbBackup)
        {
            return dbBackup.Sources.Where(x => x.ConfigID == id).ToList();
        }

        public List<DestinationsTb> GetDestinationPaths(int id, BackupDatabase dbBackup)
        {
            return dbBackup.Destinations.Where(x => x.ConfigID == id).ToList();
        }

        public string GetSnapshot(int configID, int computerID, BackupDatabase dbBackup)
        {
            ComputersConfigsTb tbComputersConfigs = dbBackup.ComputersConfigs.Where(x => x.ConfigID == configID).Where(x => x.ComputerID == computerID).FirstOrDefault();
            string computersConfigs = tbComputersConfigs.Snapshot;

            return computersConfigs;
        }
    }
}
