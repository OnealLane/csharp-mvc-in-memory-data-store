using exercise.wwwapi.Models;
using exercise.wwwapi.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> AddProducts(Irepository repository, ProductPost product)
        {

            
            {
                TypedResults.BadRequest("Price must be an integer, something else was provided / Product with provided name already exists.");
            }

            var newProduct = new Product() { name = product.name, category = product.category, price = product.price, Id = (repository.GetProducts().Count() + 1) };
            repository.AddProduct(newProduct);
            return TypedResults.Created($"/{newProduct.name}", newProduct);

        }



        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetProducts(Irepository repository)
        {
            return TypedResults.Ok(repository.GetProducts());
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetProductById(Irepository repository, int id)
        {
            return TypedResults.Ok(repository.GetProductById(id));
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        public static async Task<IResult> UpdateProductById(Irepository repository, int id, ProductPut proPut)
        {
            var newProduct = repository.UpdateProduct(id, proPut);
            return TypedResults.Created(" ", newProduct);
        }

   


    [ProducesResponseType(StatusCodes.Status200OK)]
    public static async Task<IResult> DeleteProductById(Irepository repository, int id)
    {   
        var product = repository.GetProductById(id);
        repository.DeleteProduct(id);
        return TypedResults.Ok(product);
    }
    }
}
