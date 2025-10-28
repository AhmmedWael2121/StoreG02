using System.ComponentModel.DataAnnotations.Schema;
namespace Store.Domain.Entities.Products
{
    public class Product : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string? PictureUrl { get; set; }

        
        
        [ForeignKey(nameof(Type))]
        public int TypeId { get; set; }
        public ProductType? Type { get; set; }


        [ForeignKey(nameof(Brand))]
        public int BrandId { get; set; }
        public ProductBrand? Brand { get; set; }
    }
}
