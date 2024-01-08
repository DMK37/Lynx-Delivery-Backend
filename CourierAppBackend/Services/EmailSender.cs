using Microsoft.Extensions.Options;
using SendGrid.Helpers.Mail;
using SendGrid;
using System.Net.Mail;
using CourierAppBackend.Models;

namespace CourierAppBackend.Services;

public class EmailSender
{
    private static EmailAddress from = new("lynxdelivery.courier@gmail.com", "Lynx Delivery");
    private readonly ISendGridClient sendGridClient;

    public EmailSender(ISendGridClient sendGridClient)
    {
        this.sendGridClient = sendGridClient;
    }

    public async Task SendOfferSelectedMessage(Offer offer)
    {
        var message = new SendGridMessage
        {
            From = from,
            Subject = "Lynx Delivery Order",
            PlainTextContent = $"Thank you for selecting our offer, you can check it's status using OfferID: {offer.Id}",
            HtmlContent = $"<p>Thank you for selecting our offer, you can check it's status using OfferID: {offer.Id}</p>"
        };
        message.AddTo(offer.CustomerInfo!.Email);
        var response = await sendGridClient.SendEmailAsync(message);
    }

    public async Task SendOrderCreatedMessage(Order order)
    {
        var message = new SendGridMessage
        {
            From = from,
            Subject = "Lynx Delivery Order",
            PlainTextContent = $"Hello, your offer has been accepted, your OrderId is {order.Id}",
            HtmlContent = $"<p>Hello, your offer has been accepted, your OrderId is {order.Id}</p>"
        };
        message.AddTo(order.Offer!.CustomerInfo!.Email);
        var response = await sendGridClient.SendEmailAsync(message);
    }
}
