using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NIGA.Centrum.Business.Interface
{
    public interface IOrderService
    {
        Task<string> GenerateOrderAsync(OrderModel orderModel);
    }
}
