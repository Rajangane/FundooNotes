using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.AppContexts
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Notes> Notes { get; set; }
        public DbSet<Collaborator> Collaborators { get; set; }
        public DbSet<Labels> Labels { get; set; }
    }
}
