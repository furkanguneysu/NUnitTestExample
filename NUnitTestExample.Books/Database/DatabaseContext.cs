using Microsoft.EntityFrameworkCore;
using NUnitTestExample.Books.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace NUnitTestExample.Books.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"data source=DESKTOP-MSQNV8O; initial catalog = NUnitTestExample.Books;persist security info=True;user id=sa; password=1234");
        }
    }
}
