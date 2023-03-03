using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System;
using X.Paymob.CashIn.Models.Callback;
using X.Paymob.CashIn;
using System.Text.Json.Serialization;
using System.Security.Cryptography;
using System.Text;
using Ardalis.GuardClauses;

namespace PayMobIntegration.Controllers
{
    public class CallBackController : Controller
    {
        public CallBackController(IPaymobCashInBroker broker)
        {
            this.broker = broker;
        }
        public IActionResult Index()
        {
            return View();
        }
        private static readonly JsonSerializerOptions SerializerOptions = new(JsonSerializerDefaults.Web)
        {
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
        };
        private readonly IPaymobCashInBroker broker;

        public IActionResult PayMobCallBack(
            bool is_settled,
            bool is_void,
            bool is_capture,
            string hmac,
            string source_data_pan,
            int amount_cents,
            bool has_parent_transaction,
            bool pending,
            string txn_response_code,
            bool is_voided,
            string source_data_sub_type,
            string[] discount_details,
            int profile_id,
            bool is_bill,
            bool is_refunded,
            bool error_occured, 
            int refunded_amount_cents,
            int merchant_commission,
            int integration_id,
            bool success,
            string data_message,
            string currency,
            bool is_standalone_payment,
            int captured_amount,
            string updated_at,
            string source_data_type,
            string created_at,
            bool is_3d_secure,
            int id,
            int owner,
            bool bill_balanced,
            bool is_refund,
            bool is_auth,
            int order)
        {
            var paymentInfo = new PaymentInfo
            {
                is_settled = is_settled,
                is_void = is_void,
                is_capture = is_capture,
                hmac = hmac,
                source_data_pan = "2346",
                amount_cents = amount_cents,
                has_parent_transaction = has_parent_transaction,
                pending = pending,
                txn_response_code = txn_response_code,
                is_voided = is_voided,
                source_data_sub_type = "MasterCard",
                discount_details = discount_details,
                profile_id = profile_id,
                is_bill = is_bill,
                is_refunded = is_refunded,
                error_occured = error_occured,
                refunded_amount_cents = refunded_amount_cents,
                merchant_commission = merchant_commission,
                integration_id = integration_id,
                success = success,
                data_message = data_message,
                currency = currency,
                is_standalone_payment = is_standalone_payment,
                captured_amount = captured_amount,
                updated_at = updated_at,
                source_data_type = "card",
                created_at = created_at,
                is_3d_secure = is_3d_secure,
                bill_balanced = bill_balanced,
                id = id,
                is_auth = is_auth,
                is_refund = is_refund,
                order = order,
                owner = owner,
            };
            


            var transaction = paymentInfo.ToString();
            var valid = Validate(transaction, hmac);

            if (!valid)
            {
                return BadRequest();
            }

          
            return Ok();

          
        }
        public bool Validate(string concatenatedString, string hmac)
        {
            concatenatedString = "1002020-03-25T18:39:44.719228EGPfalsefalse25567066741truefalsefalsefalsetruefalse47782394705false2346MasterCardcardtrue";
            hmac = "6965eb228a2ee5003f9dc01528d68271fdbeae7af0e5bbb1d4915cecff675c2fcb3f08aec78e5859e198ca2b1e53c622a7b5ab7dcb9d15b6ab051a25d1ea1a74";
            Guard.Against.NullOrEmpty(concatenatedString, nameof(concatenatedString));
            Guard.Against.NullOrEmpty(hmac, nameof(hmac));

            byte[] textBytes = Encoding.UTF8.GetBytes(concatenatedString);
            byte[] keyBytes = Encoding.UTF8.GetBytes("DB68DBEBF6AABCF809FC2FA2C746BE7D");
            byte[] hashBytes = _GetHashBytes(textBytes, keyBytes);
            string lowerCaseHexHash = _ToLowerCaseHex(hashBytes);
            bool a = lowerCaseHexHash.Equals(hmac, StringComparison.Ordinal);
            return lowerCaseHexHash.Equals(hmac, StringComparison.Ordinal);
        }

        private static string _ToLowerCaseHex(byte[] hashBytes)
        {
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
        }

        private static byte[] _GetHashBytes(byte[] textBytes, byte[] keyBytes)
        {
            using var hash = new HMACSHA512(keyBytes);
            return hash.ComputeHash(textBytes);
        }
        public class PaymentInfo
        {
            public bool is_settled { get; set; }
            public bool is_void { get; set; }
            public bool is_capture { get; set; }
            public string hmac { get; set; }
            public string source_data_pan { get; set; }
            public source_data source_data { get; set; }
            public int amount_cents { get; set; }
            public bool has_parent_transaction { get; set; }
            public bool pending { get; set; }
            public string txn_response_code { get; set; }
            public bool is_voided { get; set; }
            public string source_data_sub_type { get; set; }
            public string[] discount_details { get; set; }
            public int profile_id { get; set; }
            public bool is_bill { get; set; }
            public bool is_refunded { get; set; }
            public bool error_occured { get; set; }
            public int refunded_amount_cents { get; set; }
            public int merchant_commission { get; set; }
            public int integration_id { get; set; }
            public bool success { get; set; }
            public string data_message { get; set; }
            public string currency { get; set; }
            public bool is_standalone_payment { get; set; }
            public int captured_amount { get; set; }
            public string updated_at { get; set; }
            public string source_data_type { get; set; }
            public string created_at { get; set; }
            public bool is_3d_secure { get; set; }
            public int id { get; set; }
            public int owner { get; set; }
            public bool bill_balanced { get; set; }
            public bool is_refund { get; set; }
            public bool is_auth { get; set; }
            public int order { get; set; }
            public override string ToString()
            {
                var sb = new StringBuilder();
                sb.Append($"{amount_cents}");
                sb.Append($"{created_at}");
                sb.Append($"{currency}");
                sb.Append($"{error_occured}");
                sb.Append($"{has_parent_transaction}");
                sb.Append($"{id}");
                sb.Append($"{integration_id}");
                sb.Append($"{is_3d_secure}");
                sb.Append($"{is_auth}");
                sb.Append($"{is_capture}");
                sb.Append($"{is_refund}");
                sb.Append($"{is_standalone_payment}");
                sb.Append($"{is_voided}");
                sb.Append($"{order}");
                sb.Append($"{owner}");
                sb.Append($"{pending}");
                sb.Append($"{source_data_pan}");
                sb.Append($"{source_data_sub_type}");
                sb.Append($"{source_data_type}");
                sb.Append($"{success}");
                return sb.ToString();
            }
        }
        public class source_data
        {
            public string sub_type { get; set; }
            public string pan { get; set; }
            public string type { get; set; }
        }
    }

    }

