using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Homeocentrum.Niga.OldAPI.Business.Interface;
using System.Threading.Tasks;
using System;
using Homeocentrum.Niga.OldAPI.Model;
using Microsoft.AspNetCore.Authorization;

namespace Homeocentrum.Niga.OldAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("GenerateOrderId")]
        public async Task<IActionResult> GenerateOrder(OrderModel orderModel)
        {
            try
            {
                var orderId = await _orderService.GenerateOrderAsync(orderModel);
                if (orderId != null)
                {
                    return Ok(new { OrderId = orderId });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Failed to generate order");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while generating order");
            }
        }
    }
}