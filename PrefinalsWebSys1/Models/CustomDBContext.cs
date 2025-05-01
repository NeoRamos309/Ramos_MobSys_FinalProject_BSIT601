using Microsoft.EntityFrameworkCore;

namespace PrefinalsWebSys1.Models
{
    public partial class SmileBookDBContext : DbContext
    {
        public SmileBookDBContext()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string[] args = Environment.GetCommandLineArgs().Skip(1).ToArray();

            HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
            //optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("Con1"));
            optionsBuilder.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=SmileBookDB;User ID=sa;Password=pass@123;Trust Server Certificate=True");
        }
    }
}
