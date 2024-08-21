using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using middleware.Models;

namespace middleware.Data
{
    public class middlewareContext : DbContext
    {
        public middlewareContext (DbContextOptions<middlewareContext> options)
            : base(options)
        {
        }

        public DbSet<middleware.Models.User> User { get; set; } = default!;
    }
}
