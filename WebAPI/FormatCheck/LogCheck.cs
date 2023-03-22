using System.Text.RegularExpressions;
using WebAPI.Tables;

namespace WebAPI.FormatCheck
{
    public class LogCheck
    {
        public void CheckAll(LogsTb logs)
        {
            DateInsertCheck(logs.Date);
        }

        public void DateInsertCheck(DateTime Date)
        {
            if (Date < DateTime.Now)
                return;
            throw new FormatException("Date of the log can't be in the future");
        }
    }
}
