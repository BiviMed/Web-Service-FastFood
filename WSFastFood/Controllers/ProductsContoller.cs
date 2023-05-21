using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WSFastFood.Data;
using WSFastFood.Models.Dtos;
using WSFastFood.Models.Entities;
using WSFastFood.Models.Responses;
using WSFastFood.Services.ProductsServices;

namespace WSFastFood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsContoller : ControllerBase
    {            
        private readonly IProductsService _productsService;

        public ProductsContoller(IProductsService productsService)
        {  
            _productsService = productsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            GeneralResponse response = new();
            response = await _productsService.SearchProducts();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            GeneralResponse response = new();
            response = await _productsService.SearchProduct(id);                       
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductDto product)
        {
            GeneralResponse response = new();
            if (!ModelState.IsValid)
            {
                response.Message = "Producto Inválido";
                return BadRequest(response);
            }

            response = await _productsService.AddProduct(product);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> PutProduct(ProductDto product)
        {
            GeneralResponse response = new();
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            response = await _productsService.EditProduct(product);

            if (response.Success == 0)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            GeneralResponse response = new();
            response = await _productsService.SearchProduct(id);

            if (response.Success == 0)
            {
                return BadRequest(response);
            }

            Product deleteProduct = (Product)response.Data!;
            response = await _productsService.DeleteProduct(deleteProduct);

            if (response.Success == 0)
            {
                return BadRequest(response);
            }                       
            return Ok(response);
        }
    }
}
