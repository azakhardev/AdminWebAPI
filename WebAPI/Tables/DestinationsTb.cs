﻿using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Tables
{
    [Table("Destinations")]
    public class DestinationsTb
    {
        public int ID { get; set; }
        public int ConfigID { get; set; }
        public string DestinationPath { get; set; }

        //[ForeignKey("ConfigID")]
        //public virtual tbConfigs Configs { get; set; }
    }
}
