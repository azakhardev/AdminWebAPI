using System.Text.RegularExpressions;
using WebAPI.Tables;

namespace WebAPI.FormatCheck
{
    public class LogCheck
    {
        public void CheckAll(tbLogs logs)
        {
            DateCheck(logs.Date);
        }

        public void DateCheck(DateTime Date)
        {
            if (Date < DateTime.Now)
                return;
            throw new FormatException("Invalid date");
        }
    }
}
