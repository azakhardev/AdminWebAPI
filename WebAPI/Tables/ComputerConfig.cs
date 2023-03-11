using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Tables
{
    [Table("ComputerConfig")]
    public class ComputerConfig
    {
        public int ComputerID { get; set; }
        public int ConfigID { get; set; }

        [ForeignKey("ComputerID")]
        public virtual Computers Computers { get; set; }

        [ForeignKey("ConfigID")]
        public virtual Configs Configs { get; set; }
    }
}
