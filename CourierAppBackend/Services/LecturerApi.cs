using CourierAppBackend.Models;
using CourierAppBackend.ModelsDTO;
using CourierAppBackend.ModelsLecturerApi;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SendGrid.Helpers.Mail;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace CourierAppBackend.Services
{
    public class LecturerApi : IExternalApi
    {
        private readonly ExternalApisOptions _options;
        public LecturerApi(IOptions<ExternalApisOptions> options)
        {
            _options = options.Value;
        }
        public async Task<string> GetToken()
        {
            HttpClient client = new();
            var request = new HttpRequestMessage(HttpMethod.Post, _options.tokenEndPoint)
            {
                Content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "client_credentials"),
                    new KeyValuePair<string, string>("scope", _options.Scope),
                    new KeyValuePair<string, string>("client_id", _options.client_id),
                    new KeyValuePair<string, string>("client_secret", _options.client_secret)
                })
            };
            var response = await client.SendAsync(request);
            if (response.StatusCode != HttpStatusCode.OK)
                return String.Empty;
            string responseBody = await response.Content.ReadAsStringAsync();
            var jsonResponse = JObject.Parse(responseBody);
            string accessToken = jsonResponse["access_token"]!.ToString();
            return accessToken;
        }

        public async Task<OfferInfo> GetOffer(Inquiry inquiry)
        {
            LecturerCreateInquireRequest req = new()
            {
                Dimensions = new()
                {
                    Width = inquiry.Package.Width,
                    Height = inquiry.Package.Height,
                    Length = inquiry.Package.Length,
                },
                Weight = inquiry.Package.Weight,
                source = new()
                {
                    HouseNumber = inquiry.SourceAddress.HouseNumber,
                    ApartmentNumber = inquiry.SourceAddress.ApartmentNumber,
                    City = inquiry.SourceAddress.City,
                    ZipCode = inquiry.SourceAddress.PostalCode
                },
                destination = new()
                {
                    HouseNumber = inquiry.DestinationAddress.HouseNumber,
                    ApartmentNumber = inquiry.DestinationAddress.ApartmentNumber,
                    City = inquiry.DestinationAddress.City,
                    ZipCode = inquiry.DestinationAddress.PostalCode
                },
                PickupDate = inquiry.PickupDate,
                DeliveryDay = inquiry.DeliveryDate,
                deliveryInWeekend = inquiry.DeliveryAtWeekend,
                Priority = inquiry.HighPriority ? "High" : "Low",
                isComapny = inquiry.IsCompany
            };
            var token = await GetToken();
            HttpClient client = new();

            var request = new HttpRequestMessage(HttpMethod.Post, _options.apiEndPoint + "/Inquires");
            request.Headers.Add("Authorization", $"Bearer {token}");
            string jsonBody = JsonConvert.SerializeObject(req);
            request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            var response = await client.SendAsync(request);
            if (response.StatusCode != HttpStatusCode.OK)
                return null!;

            string responseBody = await response.Content.ReadAsStringAsync();
            LecturerCreateInquireResponse apiResponse = JsonConvert.DeserializeObject<LecturerCreateInquireResponse>(responseBody)!;
            return new OfferInfo()
            {
                Company = "Lecturer",
                InquiryId = apiResponse.InquiryId,
                TotalPrice = apiResponse.TotalPrice,
                ExpiringAt = apiResponse.ExpiringAt,
                PriceBreakDown = apiResponse.PriceBreakDown
            };
        }
    }
}
