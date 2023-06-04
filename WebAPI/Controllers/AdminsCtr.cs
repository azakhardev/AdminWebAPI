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
        AdminsNoPass AdminNoPass = new AdminsNoPass();

        // Všichni admini
        [HttpGet]
        public IEnumerable<AdminsNoPass> Get()
        {
            return AdminNoPass.GetAdminsNoPass(dbBackup);
        }

        // Určitý admin
        [HttpGet("{id}")]
        public ActionResult<AdminsNoPass> Get(int id)
        {
            try
            {
                AdminNoPass.GetAdminNoPass(id, dbBackup);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, $"{ex}");
            }

            return AdminNoPass.GetAdminNoPass(id, dbBackup);
        }

        // Id admina podle jmena
        [HttpGet("Id/{name}")]
        public ActionResult<int> GetByName(string name)
        {
            return AdminNoPass.GetAdminNoPass(name, dbBackup).Value.ID;
        }

        // Přidání admina
        [HttpPost]
        public ActionResult<AdminsTb> Post([FromBody] AdminsTb admin)
        {
            foreach (var item in dbBackup.Admins)
            {
                if (admin.Username == item.Username)
                    return StatusCode((int)HttpStatusCode.BadRequest, $"Admin with this username already exists.");
            }

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

        //Poslání mailu
        [HttpPost("Email/{adminId}")]
        public void SendEmail(int adminId)
        {
            string adminMail = dbBackup.Admins.Find(adminId).Email;
            List<LogsTb> messageList = dbBackup.Logs.Where(x => x.Errors == "Yes").ToList();
            string messageBody = "";

            foreach (var item in messageList)
            {
                messageBody = messageBody + item.Message + "\n";
            }

            EmailClient.Send(adminMail, messageBody);
        }


        // Změna určitého admina
        [HttpPut("{id}")]
        public ActionResult<AdminsTb> Put(int id, [FromBody] AdminsTb admin)
        {
            AdminsTb updatedAdmin = new AdminsTb();

            try
            {
                updatedAdmin = this.dbBackup.Admins.Find(id);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, $"{ex}");
            }

            if (admin.Username != null)
                updatedAdmin.Username = admin.Username;
            if (admin.Password != null)
                updatedAdmin.Password = admin.Password;
            if (admin.Schedule != null)
                updatedAdmin.Schedule = admin.Schedule;
            if (admin.Email != null)
                updatedAdmin.Email = admin.Email;
            if (admin.Description != null)
                updatedAdmin.Description = admin.Description;
            if (admin.Active != null)
                updatedAdmin.Active = admin.Active;

            try
            {
                checkAdmin.CheckAll(updatedAdmin);
            }
            catch (FormatException ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, $"{ex}");
            }

            this.dbBackup.SaveChanges();
            return updatedAdmin;
        }

        // Odstranění admina
        [HttpDelete("{id}")]
        public ActionResult<string> Delete(int id)
        {
            try
            {
                dbBackup.Admins.Remove(dbBackup.Admins.Find(id));
            }
            catch (FormatException ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, $"{ex}");
            }

            dbBackup.SaveChanges();
            return $"Admin deleted successfully";
        }
    }
}
