using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models.Model;

namespace APILog.Data
{
    public class APILogContext : DbContext
    {
        public APILogContext (DbContextOptions<APILogContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Model.Log> Log { get; set; }
    }
}
