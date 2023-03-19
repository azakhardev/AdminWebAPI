using System.ComponentModel.DataAnnotations.Schema;
namespace WebAPI.Tables
{
    [Table("Groups")]
    public class tbGroups
    {        
        public int ID { get; set; }
        public string GroupName { get; set; }
        public string Description { get; set; }

        [ForeignKey("GroupID")]
        public virtual List<tbComputersGroups> ComputersGroups { get; set; }

        [ForeignKey("GroupID")]
        public virtual List<tbGroupsConfigs> GroupsConfigs { get; set; }

        public List<tbComputers> GetComputers(int groupID, BackupDatabase dbBackup)
        {
            List<tbComputersGroups> tbComputersGroups= dbBackup.ComputersGroups.Where(x => x.GroupID == groupID).ToList();
            List<tbComputers> computers = new List<tbComputers>();

            foreach (tbComputersGroups computer in tbComputersGroups)
            {
                computers.Add(dbBackup.Computers.Find(computer.ComputerID));
            }

            return computers;
        }

        public List<tbConfigs> GetConfigs(int groupID, BackupDatabase dbBackup)
        {
            List<tbGroupsConfigs> tbGroupsConfigs = dbBackup.GroupsConfigs.Where(x => x.GroupID == groupID).ToList();
            List<tbConfigs> configs = new List<tbConfigs>();

            foreach (tbGroupsConfigs config in tbGroupsConfigs)
            {
                configs.Add(dbBackup.Configs.Find(config.ConfigID));
            }

            return configs;
        }
    }
}
