using CourierAppBackend.Configuration;
using CourierAppBackend.Models.Database;
using CourierAppBackend.Models.DTO;
using CourierAppBackend.Models.LecturerAPI;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SendGrid.Helpers.Mail;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace CourierAppBackend.Services;

public class LecturerAPI(IOptions<LecturerAPIOptions> options) 
    : IApiCommunicator
{
    private readonly LecturerAPIOptions _options = options.Value;

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

    public async Task<TemporaryOffer> GetOffer(Inquiry inquiry)
    {
        // TODO
        CreateInquireRequest req = inquiry.ToRequest();
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
        CreateInquireResponse apiResponse = JsonConvert.DeserializeObject<CreateInquireResponse>(responseBody)!;
        return new TemporaryOffer()
        {
            Inquiry = inquiry,
            Company = "Lecturer Company",
            InquiryID = apiResponse.InquiryId,
            TotalPrice = apiResponse.TotalPrice,
            ExpiringAt = apiResponse.ExpiringAt.ToUniversalTime(),
            Currency = apiResponse.Currency,
            PriceItems = apiResponse.PriceBreakDown.Select(x => new PriceItem()
            {
                Amount = x.Amount,
                Description = x.Description,
                Currency = x.Currency,
            }).ToList()
        };
    }
}
