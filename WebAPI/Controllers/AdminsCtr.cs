using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mysqlx.Crud;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using WebAPI.Tables;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/Admins")]
    [ApiController]
    public class AdminsCtr : ControllerBase
    {
        BackupDatabase dbBackup = new BackupDatabase();

        // GET: api/<Admins>
        [HttpGet]
        public IEnumerable<tbAdmins> Get()
        {
            return dbBackup.Admins;
        }

        // GET api/<Admins>/5
        [HttpGet("{id}")]
        public tbAdmins Get(int id)
        {
            return dbBackup.Admins.Find(id);
        }

        // POST api/<Admins>
        [HttpPost]
        public tbAdmins Post([FromBody] tbAdmins admin)
        {
            //string username, string password, string schedule, string email, string description, bool active
            //dbBackup.Admins.Add(new tbAdmins() {Username = username, Password = password,Schedule = schedule, Email = email, Description = description,Active = active });
            if (Regex.IsMatch(admin.Username, @"[A-Za-z0-9_]{3,50}$"))
                if (Regex.IsMatch(admin.Password, @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,50}$"))
                    if (Regex.IsMatch(admin.Schedule, @""))
                        if (Regex.IsMatch(admin.Email, @"@"))
                            dbBackup.Admins.Add(admin);
            dbBackup.SaveChanges();

            return admin;
        }

        // PUT api/<Admins>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] tbAdmins admin)
        {
            // string newUsername, string newPassword, string newSchedule, string newEmail, string newDescription, bool newActive
            tbAdmins updatedAdmin = this.dbBackup.Admins.Find(id);

            if (Regex.IsMatch(admin.Username, @"[A-Za-z0-9_]{3,50}$"))
                updatedAdmin.Username = admin.Username;
            if (Regex.IsMatch(admin.Password, @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$"))
                updatedAdmin.Password = admin.Password;
            if (Regex.IsMatch(admin.Schedule,@""))
            updatedAdmin.Schedule = admin.Schedule;
            if (Regex.IsMatch(admin.Email, @"@?"))
                updatedAdmin.Email = admin.Email;
            updatedAdmin.Description = admin.Description;
            updatedAdmin.Active = admin.Active;

            this.dbBackup.SaveChanges();
        }

        // DELETE api/<Admins>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            dbBackup.Admins.Remove(dbBackup.Admins.Find(id));
            dbBackup.SaveChanges();
        }
    }
}
