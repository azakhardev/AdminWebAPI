namespace WebAPI.Tables.Help_Tables
{
    public class tbAdminsNoPass 
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Schedule { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }

        public List<tbAdminsNoPass> CreateAdminsNoPass(BackupDatabase dbBackup) 
        {

            List<tbAdminsNoPass> adminsNoPass = new List<tbAdminsNoPass>();
            foreach (tbAdmins item in dbBackup.Admins)
            {
                tbAdminsNoPass adminNoPass = new tbAdminsNoPass()
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

        public tbAdminsNoPass GetAdminNoPass(int adminID, BackupDatabase dbBackup) 
        {
            tbAdmins admin = dbBackup.Admins.Find(adminID);
            tbAdminsNoPass tbAdminNoPass = new tbAdminsNoPass() {
                ID = admin.ID,
                Username = admin.Username,
                Active = admin.Active,
                Description = admin.Description,
                Email = admin.Email,
                Schedule = admin.Schedule
            };
            return tbAdminNoPass; 
        }
    }
}
