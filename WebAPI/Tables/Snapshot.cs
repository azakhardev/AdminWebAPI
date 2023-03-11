using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace WebAPI.Tables
{
    [Table("Snapshot")]
    public class Snapshot
    {
        public int ID { get; set; }

        public int ConfigID { get; set; }

        public int SnapshotVersion { get; set; }

        public int Size { get; set; }

        public int MaxPackageAmount { get; set; }

        public int MaxPackageSize { get; set; }

    }
}
