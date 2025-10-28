using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Configuration
{
    public class ProductTypeConfiguration : IEntityTypeConfiguration<ProductType>
    {
        public void Configure(EntityTypeBuilder<ProductType> builder)
        {
            builder.ToTable("ProductTypes");
            builder.HasKey(pt => pt.Id);
            builder.Property(pt => pt.Id)
                   .ValueGeneratedOnAdd();
            builder.Property(pt => pt.Name)
                   .IsRequired()
                   .HasMaxLength(100);
        }
    }
}
