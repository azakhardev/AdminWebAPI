using System.ComponentModel.DataAnnotations.Schema;
namespace WebAPI.Tables
{
    [Table("Groups")]
    public class tbGroups
    {
        public int ID { get; set; } 
        public int ComputerID { get; set; }
        public string GroupName { get; set; }
        public string Description { get; set; }

        //[ForeignKey("ComputerID")]
        //public virtual tbComputers Computers { get; set; }

        [ForeignKey("GroupID")]
        public virtual List<tbGroupsConfigs> GroupsConfigs { get; set; }
    }
}
