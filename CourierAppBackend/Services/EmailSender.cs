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

    public async Task SendOrderCreatedMessage(Order order)
    {
        var message = new SendGridMessage
        {
            From = from,
            Subject = "Lynx Delivery Order",
            PlainTextContent = "Hello, your offer has been accepted",
            HtmlContent = "<p>Hello, your offer has been accepted</p>"
        };
        message.AddTo(order.Offer!.CustomerInfo!.Email);
        var response = await sendGridClient.SendEmailAsync(message);
    }
}
