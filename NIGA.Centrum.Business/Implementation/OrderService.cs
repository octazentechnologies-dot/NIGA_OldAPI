using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using NIGA.Centrum.Business.Interface;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NIGA.Centrum.Model;

namespace NIGA.Centrum.Business.Implementation
{
    public class OrderService : IOrderService
    {
        public async Task<string> GenerateOrderAsync(OrderModel orderModel)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes("rzp_live_WSDlLVrcCPFbEQ:Hxp0NS02jGsUt6cixbqoC6bB")));

                    var content = new StringContent(JsonConvert.SerializeObject(new
                    {
                        amount = orderModel.Amount * 100, // Example amount (in paisa)
                        currency =orderModel.Currency, // Example currency
                        receipt =orderModel.Receipt, // Example receipt
                        payment_capture = orderModel.PaymentCapture // Auto capture payment
                    }), Encoding.UTF8, "application/json");

                    var response = await client.PostAsync("https://api.razorpay.com/v1/orders", content);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        dynamic data = JObject.Parse(result);
                        return data.id;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                throw;
            }
        }
}
}
