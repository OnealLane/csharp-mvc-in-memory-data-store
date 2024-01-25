using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    public interface Irepository
    {

        Product AddProduct(Product product);

        IEnumerable<Product> GetProducts();

        Product GetProductById(int id);

        Product UpdateProduct(int id, ProductPut proPut);

        Product DeleteProduct(int id);
    }
}
