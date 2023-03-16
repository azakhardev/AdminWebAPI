using System.Text.RegularExpressions;
using WebAPI.Tables;
namespace WebAPI.FormatCheck
{
    public class AdminCheck
    {

        public void CheckAll(tbAdmins admin)
        {
            UsernameCheck(admin.Username);
            PasswordCheck(admin.Password);
            ScheduleCheck(admin.Schedule);
            EmailCheck(admin.Email);
        }
        public void UsernameCheck(string username)
        {
            if (Regex.IsMatch(username, @"[A-Za-z0-9_]{3,50}$"))
                throw new FormatException("Invalid username");
        }

        public void PasswordCheck(string password)
        {
            if (Regex.IsMatch(password, @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,50}$"))
                throw new FormatException("Invalid password");
        }

        public void ScheduleCheck(string schedule)
        {
            if (Regex.IsMatch(schedule, @""))
                throw new FormatException("Invalid password");
        }

        public void EmailCheck(string email)
        {
            if (Regex.IsMatch(email, @"@"))
                throw new FormatException("Invalid email");
        }
    }
}
