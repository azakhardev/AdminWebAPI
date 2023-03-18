using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Tables
{
    [Table("Destination")]
    public class Destination
    {
        public int ID { get; set; }
        public int ConfigID { get; set; }
        public string DestinationPath { get; set; }

        [ForeignKey("ConfigID")]
        public virtual Configs Configs { get; set; }
    }
}
