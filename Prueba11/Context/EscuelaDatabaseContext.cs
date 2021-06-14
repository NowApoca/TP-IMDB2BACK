using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prueba11.Models;

namespace Prueba11.Context
{
    public class EscuelaDatabaseContext : DbContext
    {
        public
       EscuelaDatabaseContext(DbContextOptions<EscuelaDatabaseContext> options)
       : base(options)
        {
        }
        public DbSet<Estudiante> Estudiantes { get; set; }
    }
}