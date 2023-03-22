using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Tables
{    
    [Table("GroupsConfigs")]
    public class GroupsConfigsTb
    {
        public int ID { get; set; }

        public int GroupID { get; set; }

        public int ConfigID { get; set; }
    }
}
