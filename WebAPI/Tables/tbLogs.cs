using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Tables
{
    [Table("Logs")]
    public class tbLogs
    {
        public int ID { get; set; }

        public int ComputerID { get; set; }

        public int ConfigID { get; set; }

        public DateTime Date { get; set; }

        public bool Errors { get; set; }

        public string Message { get; set; }

        //[ForeignKey("ID")]
        //public virtual tbComputersConfigs ComputersConfigs { get; set; }

    }
}
