using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PayMobIntegration.Extenions;
using PayMobIntegration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PayMobIntegration.Services
{
    public class Paymob : IPayment
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _client;

        public Paymob(IConfiguration configuration, IHttpClientFactory http)
        {
            _configuration = configuration;
            _client = http.CreateClient();
        }

        public async Task<string> GetPaymentToken(string Token, string OrderId, decimal Total)
        {
            var Msg = string.Empty;
            try
            {
                var RequestData = new
                {
                    auth_token = Token,
                    amount_cents = Total,
                    expiration = 3600,
                    order_id = OrderId,
                    billing_data = new
                    {
                        apartment = "803",
                        email = "claudette09@exa.com",
                        floor = "42",
                        first_name = "Clifford",
                        street = "Ethan Land",
                        building = "8028",
                        phone_number = "+86(8)9135210487",
                        shipping_method = "PKG",
                        postal_code = "01898",
                        city = "Jaskolskiburgh",
                        country = "CR",
                        last_name = "Nicolas",
                        state = "Utah"
                    },
                    currency = "EGP",
                    integration_id = GetIntegrationId(),
                    lock_order_when_paid = "false"

                };

                var Response = await _client.PostAsync(GetPayMentKeyUrl(), RequestData.ToJsonObject());
                if (!Response.IsSuccessStatusCode)
                    return Msg = "Error";
                var ResponseContent = await Response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<dynamic>(ResponseContent).token;
            }
            catch (Exception ex)
            {
                Msg = $"Error:{ex.Message}";
            }
            return Msg;
        }

        public async Task<string> GetToken()
        {
            var Msg = string.Empty;
            try
            {
                var Response = await _client.PostAsync(GetAuthUrl(), new { api_key = GetApiKey() }.ToJsonObject());
                if (!Response.IsSuccessStatusCode)
                    return Msg = "Error";
                var ResponseContent = await Response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<dynamic>(ResponseContent).token;
            }
            catch (Exception ex)
            {
                Msg = $"Error:{ex.Message}";
            }
            return Msg;
        }

        public async Task<string> RegisterOrder(string Token, List<CartItem> Items)
        {
            var Msg = string.Empty;
            try
            {
                var RequestData = new
                {
                    auth_token = Token,
                    delivery_needed = "false",
                    amount_cents = Items.Sum(x => x.Price * x.Quantity),
                    currency = "EGP",
                    Items = Items
                };

                var Response = await _client.PostAsync(GetOrdersUrl(), RequestData.ToJsonObject());
                if (!Response.IsSuccessStatusCode)
                    return Msg = "Error";
                var ResponseContent = await Response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<dynamic>(ResponseContent).id;
            }
            catch (Exception ex)
            {
                Msg = $"Error:{ex.Message}";
            }
            return Msg;
        }

        private string GetApiKey() => _configuration.GetValue<string>("Paymob_api_Key");

        private string GetIntegrationId() => _configuration.GetValue<string>("Paymob_integration_Id");

        private string GetOrdersUrl() => _configuration.GetValue<string>("Paymob_RegOrder_Url");

        private string GetAuthUrl() => _configuration.GetValue<string>("Paymob_Auth_Url");

        private string GetPayMentKeyUrl() => _configuration.GetValue<string>("Paymob_PaymentKey_Url");
    }
}
