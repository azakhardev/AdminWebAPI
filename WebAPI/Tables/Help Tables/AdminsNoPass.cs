using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebAPI.Tables.Help_Tables
{
    public class AdminsNoPass
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Schedule { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }

        public List<AdminsNoPass> GetAdminsNoPass(BackupDatabase dbBackup)
        {

            List<AdminsNoPass> adminsNoPass = new List<AdminsNoPass>();
            foreach (AdminsTb item in dbBackup.Admins)
            {
                AdminsNoPass adminNoPass = new AdminsNoPass()
                {
                    Username = item.Username,
                    Description = item.Description,
                    Active = item.Active,
                    Email = item.Email,
                    ID = item.ID,
                    Schedule = item.Schedule
                };

                adminsNoPass.Add(adminNoPass);
            }

            return adminsNoPass;
        }

        public ActionResult<AdminsNoPass> GetAdminNoPass(int adminID, BackupDatabase dbBackup)
        {
            AdminsTb admin = dbBackup.Admins.Find(adminID);
            if (admin != null)
            {
                AdminsNoPass tbAdminNoPass = new AdminsNoPass()
                {
                    ID = admin.ID,
                    Username = admin.Username,
                    Active = admin.Active,
                    Description = admin.Description,
                    Email = admin.Email,
                    Schedule = admin.Schedule
                };
                return tbAdminNoPass;
            }
            throw new Exception("Admin with that id doesn't exist.");
        }
    }
}
