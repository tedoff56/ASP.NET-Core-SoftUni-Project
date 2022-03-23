namespace LightBulbsStore.Core.Models.Product;

public class ProductViewModel
{
    public ProductViewModel()
    {
        this.Products = new HashSet<ProductViewModel>();
    }
    
    public string Name { get; set; }

    public string CategoryName { get; set; }

    public decimal Price { get; set; }

    public string ImageUrl { get; set; }

    public string Description { get; set; }

    public IEnumerable<ProductViewModel> Products { get; set; }
}