using WSFastFood.Models.Dtos;
using WSFastFood.Models.Entities;
using WSFastFood.Models.Responses;

namespace WSFastFood.Services.ProductsServices
{
    public interface IProductsService
    {
        public Task<GeneralResponse> SearchProducts();
        public Task<GeneralResponse> SearchProduct(int id);
        public Task<GeneralResponse> AddProduct(ProductDto product);
        public Task<GeneralResponse> EditProduct(ProductDto product);
        public Task<GeneralResponse> DeleteProduct(Product product);
    }
}
