using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models.ModelSQL;

namespace ProjAirport.Data
{
    public class ProjAirportContext : DbContext
    {
        public ProjAirportContext (DbContextOptions<ProjAirportContext> options)
            : base(options)
        {
        }

        public DbSet<Models.ModelSQL.Airport> Airport { get; set; }
    }
}
