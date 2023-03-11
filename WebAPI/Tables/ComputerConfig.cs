using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Tables
{
    [Table("ComputerConfig")]
    public class ComputerConfig
    {
        public int ID { get; set; }
        public int ConfigID { get; set; }  

    }
}
