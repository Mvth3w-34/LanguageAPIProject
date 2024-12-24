using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LanguageProjectBackend.Models;

namespace LanguageProjectBackend.Data
{
    public class LanguageProjectContext : DbContext
    {
        public LanguageProjectContext (DbContextOptions<LanguageProjectContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = default!; // Users table
        public DbSet<UserWord> UserWords{ get; set; } = default!; // Associative table for Users and Words
        public DbSet<NewWord> Words { get; set; } = default!; //Words Table
    }
}
