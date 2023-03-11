using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Tables
{
    [Table("Source")]
    public class Source
    {
        public int ID { get; set; }

        public int ConfigsID { get; set; }

        public string SourcePath { get; set; }

        public string FileName { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime UpdateDate { get; set;}
    }
}
