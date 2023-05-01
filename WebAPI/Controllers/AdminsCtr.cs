using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mysqlx.Crud;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;
using WebAPI.FormatCheck;
using WebAPI.JWTAuthorization;
using WebAPI.Tables;
using WebAPI.Tables.Help_Tables;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/Admins")]
    [ApiController]
    //[AuthorizeAdmin]
    public class AdminsCtr : ControllerBase
    {
        BackupDatabase dbBackup = new BackupDatabase();
        AdminCheck checkAdmin = new AdminCheck();
        AdminsNoPass tbAdminNoPass = new AdminsNoPass();

        // GET: api/<Admins>
        [HttpGet]
        public IEnumerable<AdminsNoPass> Get()
        {
            return tbAdminNoPass.GetAdminsNoPass(dbBackup);
        }

        // GET api/<Admins>/5
        [HttpGet("{id}")]
        public AdminsNoPass Get(int id)
        {
            return tbAdminNoPass.GetAdminNoPass(id, dbBackup);
        }

        // POST api/<Admins>
        [HttpPost]
        public ActionResult<AdminsTb> Post([FromBody] AdminsTb admin)
        {
            try
            {
                checkAdmin.CheckAll(admin);
            }
            catch (FormatException ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, $"{ex}");
            }

            dbBackup.Admins.Add(admin);
            dbBackup.SaveChanges();
            return admin;
        }

        // PUT api/<Admins>/5
        [HttpPut("{id}")]
        public ActionResult<AdminsTb> Put(int id, [FromBody] AdminsTb admin)
        {
            AdminsTb updatedAdmin = this.dbBackup.Admins.Find(id);

            try
            {
                checkAdmin.CheckAll(admin);
            }
            catch (FormatException ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, $"{ex}");
            }

            if (updatedAdmin.Username != null)
                updatedAdmin.Username = admin.Username;
            if (updatedAdmin.Password != null)
                updatedAdmin.Password = admin.Password;
            if (updatedAdmin.Schedule != null)
                updatedAdmin.Schedule = admin.Schedule;
            if (updatedAdmin.Email != null)
                updatedAdmin.Email = admin.Email;
            if (updatedAdmin.Description != null)
                updatedAdmin.Description = admin.Description;
            if (updatedAdmin.Active != null)
                updatedAdmin.Active = admin.Active;

            this.dbBackup.SaveChanges();
            return updatedAdmin;
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
