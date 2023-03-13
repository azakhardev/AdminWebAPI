using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Tables
{
    [Keyless]
    [Table("ComputerConfig")]
    public class tbComputerConfig
    {
        public int ComputerID { get; set; }
        public int ConfigID { get; set; }

        [ForeignKey("ComputerID")]
        public virtual tbComputers Computers { get; set; }

        [ForeignKey("ConfigID")]
        public virtual tbConfigs Configs { get; set; }
    }
}
