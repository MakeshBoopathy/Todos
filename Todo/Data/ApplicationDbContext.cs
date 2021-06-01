using Todo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
            {
                
            }

        public DbSet <Item> Item { get; set; }

        public DbSet<Expense> Expense { get; set; } 
    }
}
