using Homeocentrum.Niga.OldAPI.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Homeocentrum.Niga.OldAPI.Business.Interface
{
    public interface IOrderService
    {
        Task<string> GenerateOrderAsync(OrderModel orderModel);
    }
}
