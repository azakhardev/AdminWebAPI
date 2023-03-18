using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Tables
{
    
    [Table("ComputersConfigs")]
    public class tbComputersConfigs
    {
        public int ID { get; set; }
        public int ComputerID { get; set; }
        public int ConfigID { get; set; }
        public string Snapshot { get; set; }

        //[ForeignKey("ComputerID")]
        //public virtual tbComputers Computers { get; set; }

        //[ForeignKey("ConfigID")]
        //public virtual tbConfigs Configs { get; set; }

        [ForeignKey("ComputersConfigsID")]
        public virtual List<tbLogs> Logs { get; set; }

    }
}
