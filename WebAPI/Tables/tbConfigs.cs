using K4os.Compression.LZ4.Engine;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Tables
{
    [Table("Configs")]
    public class tbConfigs 
    {
        public int ID { get; set; }

        public string ConfigName { get; set; }

        public DateTime CreationDate { get; set; }

        public string Algorithm { get; set; }

        public int MaxPackageAmount { get; set; }

        public int MaxPackageSize { get; set; }

        public string Schedule { get; set; }

        public bool Zip { get; set; }

        [ForeignKey("ConfigID")]
        public virtual List<tbComputersConfigs> ComputersConfigs { get; set; }

        [ForeignKey("ConfigID")]
        public virtual List<tbGroupsConfigs> GroupsConfigs { get; set; }

        [ForeignKey("ConfigID")]
        public virtual List<tbSources> Sources { get; set; }

        [ForeignKey("ConfigID")]
        public virtual List<tbDestinations> Destinations { get; set; }
    }
}
