using System.ComponentModel.DataAnnotations.Schema;
namespace WebAPI.Tables
{
    [Table("Groups")]
    public class Groups
    {
        public int ID { get; set; } 
        public int ComputerID { get; set; }
        public string GroupName { get; set; }
        public string Description { get; set; }
    }
}
