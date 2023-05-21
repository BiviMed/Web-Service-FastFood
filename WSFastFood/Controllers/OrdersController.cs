using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WSFastFood.Data;
using WSFastFood.Models.Dtos;
using WSFastFood.Models.Entities;
using WSFastFood.Models.Responses;
using WSFastFood.Services.OrdersServices;
using WSFastFood.Services.ProductsServices;
using WSFastFood.Services.UsersServices;

namespace WSFastFood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;        

        public OrdersController(IOrderService orderService) 
        {
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult GetOrdersAsync()
        {
            GeneralResponse response = _orderService.GetAllOrders();
            if (response.Success == 0)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder(GenerateOrderDto orderModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            GeneralResponse response = await _orderService.AddOrderAsync(orderModel);

            if (response.Success == 0)
            {
                return BadRequest(response);
            }                 

            return Ok(response);
        }

    }
}
