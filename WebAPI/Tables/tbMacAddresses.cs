using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Tables
{
    [Table("MacAddresses")]
    public class tbMacAddresses
    {
        public int ID { get; set; }
        public int ComputerID { get; set; }
        public string MacAddress { get; set; }

        [ForeignKey("ComputerID")]
        public virtual tbComputers Computer { get; set; }
    }
}
