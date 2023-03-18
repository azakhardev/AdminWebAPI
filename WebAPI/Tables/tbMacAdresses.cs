using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Tables
{
    [Table("MacAdresses")]
    public class tbMacAdresses
    {
        public int ID { get; set; }
        public int ComputerID { get; set; }
        public string MacAdress { get; set; }

        [ForeignKey("ComputerID")]
        public virtual tbComputers Computers { get; set; }
    }
}
