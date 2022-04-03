using LightBulbsStore.Core.Models.Category;

namespace LightBulbsStore.Core.Models.Product;

public class ProductViewModel
{
    public string Id { get; set; }

    public string Name { get; set; }

    public CategoryViewModel Category { get; set; }

    public decimal Price { get; set; }

    public string ImageUrl { get; set; }

    public string Description { get; set; }
}