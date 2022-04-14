using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models.Model;

namespace ProjMongoDBUser.Data
{
    public class ProjMongoDBUserContext : DbContext
    {
        public ProjMongoDBUserContext (DbContextOptions<ProjMongoDBUserContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Model.User> User { get; set; }
    }
}
