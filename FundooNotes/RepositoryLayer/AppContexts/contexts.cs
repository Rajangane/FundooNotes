using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.AppContexts
{
    public class contexts : DbContext
    { 
            public contexts(DbContextOptions options)
                : base(options)
            {
            }
            public DbSet<User> Users { get; set; }
        
    }
}
