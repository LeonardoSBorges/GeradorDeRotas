using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GeradorDeRotasMVC.Models;

namespace GeradorDeRotasMVC.Data
{
    public class GeradorDeRotasMVCContext : DbContext
    {
        public GeradorDeRotasMVCContext (DbContextOptions<GeradorDeRotasMVCContext> options)
            : base(options)
        {
        }

        public DbSet<GeradorDeRotasMVC.Models.Person> Person { get; set; }
    }
}
