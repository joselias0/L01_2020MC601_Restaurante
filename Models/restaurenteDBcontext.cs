using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
namespace L01_2020MC601.Models
{
    public class restaurenteDBcontext : DbContext
    {
        public restaurenteDBcontext(DbContextOptions<restaurenteDBcontext> options) : base(options)
        {

        } 

        public DbSet<platos> platos { get; set; }
        public DbSet<motoristas> motoristas{ get; set; }
        public DbSet<pedidos> pedidos { get; set; }
    }
}
