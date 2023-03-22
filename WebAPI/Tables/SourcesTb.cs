using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Tables
{
    [Table("Sources")]
    public class SourcesTb
    {
        public int ID { get; set; }

        public int ConfigID { get; set; }

        public string SourcePath { get; set; }

        public string FileName { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime UpdateDate { get; set;}

        //[ForeignKey("ConfigID")]
        //public virtual tbConfigs Configs { get; set; }
    }
}
