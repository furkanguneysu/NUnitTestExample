using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NUnitTestExample.Books.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NUnitTestExample.Books.Database.Configuration
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(b => b.Id).ValueGeneratedOnAdd();
            builder.Property(b => b.Name).IsRequired().HasMaxLength(40);
            builder.Property(b => b.Price).IsRequired();
            builder.Property(b => b.Author).IsRequired();
        }
    }
}
