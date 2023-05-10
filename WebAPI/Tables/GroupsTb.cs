using System.ComponentModel.DataAnnotations.Schema;
namespace WebAPI.Tables
{
    [Table("Groups")]
    public class GroupsTb
    {        
        public int ID { get; set; }
        public string? GroupName { get; set; }
        public string? Description { get; set; }
        public DateTime? LastBackup { get; set; }
        public string? BackupStatus { get; set; }

        [ForeignKey("GroupID")]
        public virtual List<ComputersGroupsTb>? ComputersGroups { get; set; }

        [ForeignKey("GroupID")]
        public virtual List<GroupsConfigsTb>? GroupsConfigs { get; set; }

        public List<ComputersTb> GetComputers(int groupID, BackupDatabase dbBackup)
        {
            List<ComputersGroupsTb> tbComputersGroups= dbBackup.ComputersGroups.Where(x => x.GroupID == groupID).ToList();
            List<ComputersTb> computers = new List<ComputersTb>();

            foreach (ComputersGroupsTb computer in tbComputersGroups)
            {
                computers.Add(dbBackup.Computers.Find(computer.ComputerID));
            }

            return computers;
        }

        public List<ConfigsTb> GetConfigs(int groupID, BackupDatabase dbBackup)
        {
            List<GroupsConfigsTb> tbGroupsConfigs = dbBackup.GroupsConfigs.Where(x => x.GroupID == groupID).ToList();
            List<ConfigsTb> configs = new List<ConfigsTb>();

            foreach (GroupsConfigsTb config in tbGroupsConfigs)
            {
                configs.Add(dbBackup.Configs.Find(config.ConfigID));
            }

            return configs;
        }
    }
}
