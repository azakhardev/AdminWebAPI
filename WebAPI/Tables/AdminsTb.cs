﻿using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Tables
{
    [Table("Admins")]
    public class AdminsTb
    {
        public int ID { get; set; } 
        public string Username { get; set; }    
        public string Password { get; set; }
        public string Schedule { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }

        
    }
}
