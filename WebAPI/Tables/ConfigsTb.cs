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
            List<ComputersConfigsTb> tbComputersConfigs = dbBackup.ComputersConfigs.Where(x => x.ID == id).ToList();
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

        public List<string> GetSourcePaths(int id, BackupDatabase dbBackup)
        {
            List<SourcesTb> tbSources = dbBackup.Sources.Where(x => x.ConfigID == id).ToList();
            List<string> sources = new List<string>();

            foreach (SourcesTb source in tbSources)
            {
                sources.Add(source.SourcePath);
            }

            return sources;
        }

        public List<string> GetDestinationPaths(int id, BackupDatabase dbBackup)
        {
            List<DestinationsTb> tbDestinations = dbBackup.Destinations.Where(x => x.ConfigID == id).ToList();
            List<string> destiantions = new List<string>();

            foreach (DestinationsTb destination in tbDestinations)
            {
                destiantions.Add(destination.DestinationPath);
            }

            return destiantions;
        }


        public string GetSnapshot(int configID, int computerID, BackupDatabase dbBackup)
        {
            ComputersConfigsTb tbComputersConfigs = dbBackup.ComputersConfigs.Where(x => x.ConfigID == configID).Where(x => x.ComputerID == computerID).FirstOrDefault();
            string computersConfigs = tbComputersConfigs.Snapshot;

            return computersConfigs;
        }

    }
}
