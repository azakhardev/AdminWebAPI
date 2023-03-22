using System.Text.RegularExpressions;
using WebAPI.Tables;
namespace WebAPI.FormatCheck
{
    public class AdminCheck
    {
        public void CheckAll(AdminsTb admin)
        {
            UsernameCheck(admin.Username);
            PasswordCheck(admin.Password);
            ScheduleCheck(admin.Schedule);
            EmailCheck(admin.Email);
        }
        public void UsernameCheck(string username)
        {
            if (Regex.IsMatch(username, @"^[A-Za-z0-9_]{3,50}$"))
                return;
            throw new FormatException("Invalid username");
        }

        public void PasswordCheck(string password)
        {
            if (Regex.IsMatch(password, @"^(?=.*[A-Za-z])(?=.*\d).{8,50}$"))
                return;
            throw new FormatException("Invalid password");
        }

        public void ScheduleCheck(string schedule)
        {
            if (Regex.IsMatch(schedule, @"^(*|\d{1,2}|\d{1,2}-\d{1,2}|\d{1,2}/\d{1,2}|\d{1,2},\d{1,2}) (*|\d{1,2}|\d{1,2}-\d{1,2}|\d{1,2}/\d{1,2}|\d{1,2},\d{1,2}) (*|\d{1,2}|\d{1,2}-\d{1,2}|\d{1,2}/\d{1,2}|\d{1,2},\d{1,2}) (*|\d{1,2}|\d{1,2}-\d{1,2}|\d{1,2}/\d{1,2}|\d{1,2},\d{1,2}) (*|\d{1,2}|\d{1,2}-\d{1,2}|\d{1,2}/\d{1,2}|\d{1,2},\d{1,2})$"))
                return;
            throw new FormatException("Invalid schedule");
        }

        public void EmailCheck(string email)
        {
            if (Regex.IsMatch(email, @"^[^@]+@[a-z0-9]+(\.[a-z0-9]+)*\.[a-z]+$"))
                return;
            throw new FormatException("Invalid email");
        }
    }
}
