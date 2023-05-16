using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Tables
{
    [Table("Logs")]
    public class LogsTb
    {
        public int ID { get; set; }

        public int ComputersConfigsID { get; set; }

        public string? ConfigName { get; set; }

        public int ComputerId { get; set; }

        public string? ComputerName { get; set; }

        public int ConfigId { get; set; }

        public DateTime Date { get; set; }

        public string Errors { get; set; }        

        public string? Message { get; set; }

        //[ForeignKey("ComputerConfigsID")]
        //public virtual tbComputersConfigs ComputersConfigs { get; set; }

    }
}
