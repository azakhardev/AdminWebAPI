using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Tables
{
    [Table("ComputersConfigs")]
    public class ComputersConfigsTb
    {
        public int ID { get; set; }
        public int ComputerID { get; set; }
        public int ConfigID { get; set; }
        public string Snapshot { get; set; }
        //public int SnapshotVersion { get; set; }

        [ForeignKey("ComputersConfigsID")]
        public virtual List<LogsTb>? Logs { get; set; }
    }
}
