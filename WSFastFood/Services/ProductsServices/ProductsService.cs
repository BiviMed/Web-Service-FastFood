using Microsoft.EntityFrameworkCore;
using System.Globalization;
using WSFastFood.Data;
using WSFastFood.Models.Dtos;
using WSFastFood.Models.Entities;
using WSFastFood.Models.Responses;

namespace WSFastFood.Services.ProductsServices
{
    public class ProductsService : IProductsService
    {
        private readonly FastFoodContext _context;

        public ProductsService(FastFoodContext context)
        {
            _context = context;
        }


        public async Task<GeneralResponse> SearchProducts()
        {
            GeneralResponse response = new();

            try
            {
                List<Product> lstProducts = await _context.Products.OrderBy(p => p.Id).ToListAsync();
                response.Success = 1;
                response.Data = lstProducts;
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<GeneralResponse> SearchProduct(int id)
        {
            GeneralResponse response = new();
            try
            {
                var product = await _context.Products.FindAsync(id);

                if (product == null)
                {
                    response.Message = "Producto no encontrado";
                }
                else
                {
                    response.Success = 1;
                    response.Data = product;
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<GeneralResponse> AddProduct(ProductDto product)
        {
            GeneralResponse response = new();

            Product addProduct = new()
            {
                Name = product.Name,
                Price = product.Price
            };

            try
            {
                await _context.Products.AddAsync(addProduct);
                await _context.SaveChangesAsync();
                response.Success = 1;
                response.Message = "Producto agregado con éxito";
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<GeneralResponse> EditProduct(ProductDto product)
        {
            GeneralResponse response = new();

            Product editProduct = new()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };

            try
            {
                _context.Products.Entry(editProduct).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                response.Success = 1;
                response.Message = "Producto modificado con éxito";

            }
            catch (Exception e)
            {
                response.Success = 0;
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<GeneralResponse> DeleteProduct(Product product)
        {
            GeneralResponse response = new();

            try
            {
                _context.Remove(product);
                await _context.SaveChangesAsync();
                response.Success = 1;
                response.Message = "Producto eliminado con éxito";

            }
            catch (Exception e)
            {
                response.Success = 0;
                response.Message = e.Message;
            }

            return response;
        }
    }
}
