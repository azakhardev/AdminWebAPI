namespace WebAPI.Tables.Help_Tables
{
    public class AdminsNoPassTb
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Schedule { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }

        public List<AdminsNoPassTb> GetAdminsNoPass(BackupDatabase dbBackup)
        {

            List<AdminsNoPassTb> adminsNoPass = new List<AdminsNoPassTb>();
            foreach (AdminsTb item in dbBackup.Admins)
            {
                AdminsNoPassTb adminNoPass = new AdminsNoPassTb()
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

        public AdminsNoPassTb GetAdminNoPass(int adminID, BackupDatabase dbBackup)
        {
            AdminsTb admin = dbBackup.Admins.Find(adminID);
            AdminsNoPassTb tbAdminNoPass = new AdminsNoPassTb()
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
    }
}
