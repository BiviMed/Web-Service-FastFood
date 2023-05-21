using Azure;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WSFastFood.Data;
using WSFastFood.Models.Dtos;
using WSFastFood.Models.Entities;
using WSFastFood.Models.Responses;
using WSFastFood.Services.ProductsServices;
using WSFastFood.Services.UsersServices;

namespace WSFastFood.Services.OrdersServices
{
    public class OrderService : IOrderService
    {
        private readonly FastFoodContext _context;
        private readonly IUserService _userService;
        private readonly IProductsService _productsService;      
        

        public OrderService(FastFoodContext context, IUserService userService, 
            IProductsService productsService) 
        {
            _context = context;
            _userService = userService;
            _productsService = productsService;
        }


        public GeneralResponse GetAllOrders()
        {
            GeneralResponse response = new();

            try
            {                
                var lstOrders = _context.Orders.Select(o =>
                new
                {
                    Id = o.Id,
                    Date = o.Date,
                    Total = o.Total,
                    OrderDetails = o.OrderDetails.OrderBy(o => o.Id).Select(od => 
                        new {
                            Id = od.Id, 
                            Price = od.Price, 
                            Quantity = od.Quantity, 
                            Amount = od.Amount
                        }).ToList()
                });

                response.Success = 1;
                response.Data = lstOrders;
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }

            return response;
        }


        public async Task<GeneralResponse> AddOrderAsync(GenerateOrderDto orderModel)
        {
            GeneralResponse response = new();
            try
            {
                response = await _userService.SearchUser(orderModel.UserId);
                if (response.Success == 0)
                {
                    return response;
                }

                response = await CreateOrderToSend(orderModel);
                if (response.Success == 0)
                {
                    return response;
                }
                
                Order addOrder = (Order)response.Data!;
                response = await SendOrder(addOrder);
                if (response.Success == 0)
                {
                    return response;
                }

                response.Success = 1;
                response.Data = null;
            }
            catch (Exception e)
            {                
                response.Success = 0;
                response.Data = null;
                response.Message = e.Message;
            }

            return response;
        }


        private async Task<GeneralResponse> CreateOrderToSend(GenerateOrderDto orderModel)
        {
            GeneralResponse response = new();
            IEnumerable<int> productsIds = orderModel.OrderDetails.Select(x => x.ProductId).ToList();
            IEnumerable<int> productsQuantities = orderModel.OrderDetails.Select(x => x.Quantity).ToList();
            List<decimal> productsPrices = new();
            List<decimal> productsAmounts = new();

            response = await CreateOrder(orderModel,productsIds, productsQuantities, productsPrices, productsAmounts);
  
            return response;            
        }

        private async Task<GeneralResponse> CreateOrder(GenerateOrderDto orderModel,IEnumerable<int> productsIds, IEnumerable<int> productsQuantities, List<decimal> productsPrices, List<decimal> productsAmounts)
        {
            GeneralResponse response = new();            
            decimal total = 0;
            int i = 0;
            foreach (int id in productsIds)
            {
                response = await _productsService.SearchProduct(id);
                if (response.Success == 0)
                {
                    return response;
                }

                Product product = (Product)response.Data!;
                decimal price = product.Price;
                int quantity = productsQuantities.ElementAt(i);
                decimal amount = quantity * price;

                productsPrices.Add(price);
                productsAmounts.Add(amount);
                total += amount;
                i += 1;
            }

            GenerateOrderModel(orderModel,productsIds,productsQuantities,productsPrices,productsAmounts,total, out Order? addOrder);
            if (addOrder == null || addOrder.Total == 0 || orderModel.OrderDetails == null)
            {
                response.Success = 0;
                response.Message = "Ha ocurrido un error al momento de crear la Orden para ser enviada";
                response.Data = null;
                return response;
            }

            response.Success = 1;
            response.Message = null;
            response.Data = addOrder;
            return response;
        }

        private void GenerateOrderModel(GenerateOrderDto orderModel, IEnumerable<int> productsIds, IEnumerable<int> productsQuantities, List<decimal> productsPrices, List<decimal> productsAmounts, decimal total, out Order? addOrder)
        {
            OrderDetail addOrderDetails;
            List<OrderDetail> lstOrderDetails = new List<OrderDetail>();
            for (int index = 0; index < productsIds.Count(); index++)
            {
                addOrderDetails = new()
                {
                    ProductId = productsIds.ElementAt(index),
                    Price = productsPrices.ElementAt(index),
                    Quantity = productsQuantities.ElementAt(index),
                    Amount = productsAmounts.ElementAt(index)
                };
                lstOrderDetails.Add(addOrderDetails);
            }

            Order addOrderModel = new()
            {
                Date = DateTime.Now,
                UserId = orderModel.UserId,
                Total = total,
                OrderDetails = lstOrderDetails
            };

            addOrder = addOrderModel;
        }

        private async Task<GeneralResponse> SendOrder(Order addOrder)
        {
            GeneralResponse response = new();
            try
            {
                using (await _context.Database.BeginTransactionAsync())
                {
                    try
                    {     
                        await _context.Orders.AddAsync(addOrder);
                        await _context.SaveChangesAsync();
                        await _context.Database.CommitTransactionAsync();

                        response.Data = null;
                        response.Message = "Orden añadida con éxito";
                        response.Success = 1;                        
                    }
                    catch (Exception)
                    {
                        await _context.Database.RollbackTransactionAsync();
                        response.Success = 0;
                        response.Data = null;
                        response.Message = "Se ha producido un error al momento de agregar la Orden";
                    }
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }
            return response;
        }


    }
}
