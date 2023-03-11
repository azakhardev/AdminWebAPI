using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Tables
{
    [Table("GroupConfig")]
    public class GroupConfig
    {
        public int GroupID { get; set; }

        public int ConfigID { get; set; }
    }
}
