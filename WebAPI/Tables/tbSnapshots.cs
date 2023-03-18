using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace WebAPI.Tables
{
    [Table("Snapshots")]
    public class tbSnapshots
    {
        public int ID { get; set; }

        public int ConfigID { get; set; }

        public string Path { get; set; }

        public DateTime LastUpdate { get; set; }

        public int SnapshotVersion { get; set; }

        public int Size { get; set; }

        [ForeignKey ("ConfigID")]
        public virtual tbConfigs Configs { get; set; }
    }
}
