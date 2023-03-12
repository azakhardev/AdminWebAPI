using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Tables
{
    [Table("GroupConfig")]
    public class tbGroupConfig
    {
        public int GroupID { get; set; }

        public int ConfigID { get; set; }

        [ForeignKey("GroupID")]
        public virtual tbGroups Groups { get; set; }

        [ForeignKey("ConfigID")]
        public virtual tbConfigs Configs { get; set; }
    }
}
