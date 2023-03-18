using Microsoft.EntityFrameworkCore;

namespace WebAPI
{
    public class DatabaseConnection : DbContext

    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=mysqlstudenti.litv.sssvt.cz;database=3b2_zakharchenkoartem_db1;user=zakharchenkoarte;password=123456;SslMode=none");
        }

    }
}
