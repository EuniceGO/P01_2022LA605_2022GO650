using Microsoft.EntityFrameworkCore;
using static P01_2022LA605_2022GO650.Models.espacioParqueo;

namespace P01_2022LA605_2022GO650.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<usuario> Usuarios { get; set; }
        public DbSet<Sucursal> Sucursales { get; set; }
        public DbSet<espacioParqueo> EspaciosParqueo { get; set; }
        public DbSet<reserva> Reservas { get; set; }
    }
}
