using exercise.wwwapi.Data;
using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    public class Repository : Irepository

    {
        private DataContext _db;

        public Repository(DataContext db)
        {
            _db = db;
        }



        public Product AddProduct(Product product)
        {
            _db.products.Add(product);
            _db.SaveChanges();
            return product;
        }

        public Product DeleteProduct(int id)
        {
            var product = GetProductById(id);
            _db.products.Remove(product);
            _db.SaveChanges();
            return product;
        }

        public Product GetProductById(int id)
        {
            var product = _db.products.Where(x => x.Id == id).FirstOrDefault();
            return product;
        }

        public IEnumerable<Product> GetProducts()
        {
            return _db.products.ToList();
        }

        public Product UpdateProduct(int id, ProductPut proPut)
        {

            var existingProduct = _db.products.FirstOrDefault(x => x.Id == id);

            existingProduct.name = proPut.name;
            existingProduct.category = proPut.category;
            existingProduct.price = proPut.price;

            _db.SaveChanges();
            return existingProduct;

        }
    }
}
