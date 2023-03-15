using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Tables
{
    [Keyless]
    [Table("ComputerConfig")]
    public class tbComputerConfigs
    {
        public int ComputerID { get; set; }
        public int ConfigID { get; set; }
        public string Snapshot { get; set; }

        [ForeignKey("ComputerID")]
        public virtual tbComputers Computers { get; set; }

        [ForeignKey("ConfigID")]
        public virtual tbConfigs Configs { get; set; }

        [ForeignKey("ComputerID")]
        public virtual List<tbLogs> ComputerLogs { get; set; }

        [ForeignKey("ConfigID")]
        public virtual List<tbLogs> ConfigLogs { get; set; }
    }
}
