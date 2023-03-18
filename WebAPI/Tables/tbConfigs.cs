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

        public List<int> GetComputers(int id, BackupDatabase dbBackup)
        {
            List<tbComputers> tbComputers = dbBackup.Computers.Where(x => x.ID == id).ToList();
            List<int> computers = new List<int>();

            foreach (tbComputers computer in tbComputers)
            {
                computers.Add(computer.ID);
            }

            return computers;
        }

        public List<int> GetGroups(int id, BackupDatabase dbBackup)
        {
            List<tbGroups> tbGroups = dbBackup.Groups.Where(x => x.ID == id).ToList();
            List<int> groups = new List<int>();

            foreach (tbGroups group in tbGroups)
            {
                groups.Add(group.ID);
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
