using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Tables
{
    [Table("ComputersGroups")]
    public class ComputersGroupsTb
    {
        public int ID { get; set; }

        public int ComputerID { get; set; }

        public int GroupID { get; set; }
    }
}
