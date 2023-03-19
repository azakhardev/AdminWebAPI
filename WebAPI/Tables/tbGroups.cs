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

        public List<int> GetComputersID(int groupID, BackupDatabase dbBackup)
        {
            List<tbComputersGroups> tbComputersGroups= dbBackup.ComputersGroups.Where(x => x.GroupID == groupID).ToList();
            List<int> computers = new List<int>();

            foreach (tbComputersGroups computer in tbComputersGroups)
            {
                computers.Add(computer.ComputerID);
            }

            return computers;
        }

        public List<int> GetConfigsID(int groupID, BackupDatabase dbBackup)
        {
            List<tbGroupsConfigs> tbGroupsConfigs = dbBackup.GroupsConfigs.Where(x => x.GroupID == groupID).ToList();
            List<int> configs = new List<int>();

            foreach (tbGroupsConfigs config in tbGroupsConfigs)
            {
                configs.Add(config.ConfigID);
            }

            return configs;
        }
    }
}
