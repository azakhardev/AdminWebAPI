using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Tables
{
    [Table("Destination")]
    public class Destination
    {
        public int ID { get; set; }
        public int Config { get; set; }
        public string DestinationPath { get; set; }

    }
}
