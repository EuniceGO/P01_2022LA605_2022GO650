﻿using Microsoft.EntityFrameworkCore;
using static P01_2022LA605_2022GO650.Models.espacioParqueo;

namespace P01_2022LA605_2022GO650.Models
{
    public class ParqueoContext : DbContext
    {
        public ParqueoContext(DbContextOptions<ParqueoContext> options) : base(options) { }

        public DbSet<usuario> usuarios { get; set; }
        public DbSet<Sucursal> Sucursales { get; set; }
        public DbSet<espacioParqueo> EspaciosParqueo { get; set; }
        public DbSet<reserva> Reservas { get; set; }
    }
}
