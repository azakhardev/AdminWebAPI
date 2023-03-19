using K4os.Compression.LZ4.Engine;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.OpenApi.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace WebAPI.Tables
{
    [Table("Configs")]
    public class tbConfigs
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
        public virtual List<tbComputersConfigs> ComputersConfigs { get; set; }

        [ForeignKey("ConfigID")]
        public virtual List<tbGroupsConfigs> GroupsConfigs { get; set; }

        [ForeignKey("ConfigID")]
        public virtual List<tbSources> Sources { get; set; }

        [ForeignKey("ConfigID")]
        public virtual List<tbDestinations> Destinations { get; set; }

        public List<tbComputers> GetComputers(int id, BackupDatabase dbBackup)
        {
            List<tbComputersConfigs> tbComputersConfigs = dbBackup.ComputersConfigs.Where(x => x.ID == id).ToList();
            List<tbComputers> computers = new List<tbComputers>();

            foreach (tbComputersConfigs computer in tbComputersConfigs)
            {
                computers.Add(dbBackup.Computers.Find(computer.ComputerID));
            }

            return computers;
        }

        public List<tbGroups> GetGroups(int id, BackupDatabase dbBackup)
        {
            List<tbGroupsConfigs> tbGroupsConfigs = dbBackup.GroupsConfigs.Where(x => x.ID == id).ToList();
            List<tbGroups> groups = new List<tbGroups>();

            foreach (tbGroupsConfigs group in tbGroupsConfigs)
            {
                groups.Add(dbBackup.Groups.Find(group.GroupID));
            }

            return groups;
        }

        public List<string> GetSourcePaths(int id, BackupDatabase dbBackup)
        {
            List<tbSources> tbSources = dbBackup.Sources.Where(x => x.ConfigID == id).ToList();
            List<string> sources = new List<string>();

            foreach (tbSources source in tbSources)
            {
                sources.Add(source.SourcePath);
            }

            return sources;
        }

        public List<string> GetDestinationPaths(int id, BackupDatabase dbBackup)
        {
            List<tbDestinations> tbDestinations = dbBackup.Destinations.Where(x => x.ConfigID == id).ToList();
            List<string> destiantions = new List<string>();

            foreach (tbDestinations destination in tbDestinations)
            {
                destiantions.Add(destination.DestinationPath);
            }

            return destiantions;
        }

    }
}
