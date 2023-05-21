using WSFastFood.Models.Dtos;
using WSFastFood.Models.Responses;

namespace WSFastFood.Services.OrdersServices
{
    public interface IOrderService
    {
        public Task<GeneralResponse> AddOrderAsync(GenerateOrderDto orderModel);
        public GeneralResponse GetAllOrders();
    }
}
