using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Tables
{
    [Table("Destinations")]
    public class tbDestinations
    {
        public int ID { get; set; }
        public int ConfigID { get; set; }
        public string DestinationPath { get; set; }

        [ForeignKey("ID")]
        public virtual tbConfigs Configs { get; set; }
    }
}
