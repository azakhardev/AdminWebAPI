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

        //public List<int> GetComputersID(int computerID, BackupDatabase dbBackup) 
        //{
        //    List<tbComputers> tbComputers = dbBackup.Computers.Where(x => x.ID == computerID).ToList();
        //    List<int> computers = new List<int>();

        //    foreach (tbComputers item in tbComputers)
        //    {
        //        computers.Add(item.ID);
        //    }

        //    return computers;
        //}
    }
}
