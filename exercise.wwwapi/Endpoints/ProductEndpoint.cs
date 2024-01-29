using exercise.wwwapi.Models;
using exercise.wwwapi.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductEndpoint
    {
        public static void ConfigureProductEndpoint(this WebApplication app)
        {
            var productGroup = app.MapGroup("products");

            productGroup.MapPost("/", AddProducts);
            productGroup.MapGet("/", GetProducts);
            productGroup.MapGet("/{id}", GetProductById);
            productGroup.MapPut("/{id}", UpdateProductById);
            productGroup.MapDelete("/{id}", DeleteProductById);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        public static async Task<IResult> AddProducts(Irepository repository, ProductPost product)
        {
            if (repository.GetProducts().Any(x => x.name == product.name)) 
            {
                return TypedResults.BadRequest("Product with provided name already exists");
            }
            if (!int.TryParse(product.price.ToString(), out var n)) 
            {
                return TypedResults.BadRequest("Price must be an integer");
            }


            var newProduct = new Product() { name = product.name, category = product.category, price = product.price, Id = (repository.GetProducts().Count() + 1) };
            repository.AddProduct(newProduct);
            return TypedResults.Created($"/{newProduct.name}", newProduct);

        }



        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetProducts(string category, Irepository repository)
        {

            if (category != null && !repository.GetProducts().Any(x => x.category == category))
            {
                return TypedResults.NotFound("No products of the provided category were found");
            }
            if (category == null)
            {
                return TypedResults.Ok(repository.GetProducts());
            }

            var result = repository.GetProducts().Where(x => x.category == category).ToList();

            return TypedResults.Ok(result);

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetProductById(Irepository repository, int id)
        {
            if(repository.GetProductById(id) == null)
            {
                return TypedResults.NotFound("Product not found");
            }
            return TypedResults.Ok(repository.GetProductById(id));
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        public static async Task<IResult> UpdateProductById(Irepository repository, int id, ProductPut proPut)
        {
            var product = repository.GetProductById(id);
            if (product == null)
            {
                return TypedResults.NotFound("Product not found");
            }

            if (repository.GetProducts().Any(x => x.name == proPut.name))
            {
                return TypedResults.BadRequest("Product with provided name already exists");
            }
            if (!int.TryParse(product.price.ToString(), out var n))
            {
                return TypedResults.BadRequest("Price must be an integer");
            }

         
            var newProduct = repository.UpdateProduct(id, proPut);
            return TypedResults.Created(" ", newProduct);
        }




        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> DeleteProductById(Irepository repository, int id)
        {
            var product = repository.GetProductById(id);
            if (product == null)
            {
                return TypedResults.NotFound("Product not found");
            }
            repository.DeleteProduct(id);
            return TypedResults.Ok(product);
        }
    }
}
