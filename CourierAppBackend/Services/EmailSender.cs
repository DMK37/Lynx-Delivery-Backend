using Microsoft.Extensions.Options;
using SendGrid.Helpers.Mail;
using SendGrid;
using System.Net.Mail;
using CourierAppBackend.Models.Database;
using CourierAppBackend.Abstractions.Services;
using Humanizer;
using CourierAppBackend.Configuration;
using System;
using CourierAppBackend.Models.DTO;

namespace CourierAppBackend.Services;

public class EmailSender(ISendGridClient sendGridClient, IOptions<SendGridOptions> options) 
    : IMessageSender
{
    private readonly SendGridOptions _options = options.Value;

    public async Task SendOfferSelectedMessage(Offer offer)
    {
        var from = new EmailAddress(_options.FromEmail, _options.FromName);
        var to = new EmailAddress(offer.CustomerInfo!.Email, offer.CustomerInfo.FirstName);
        var dynamicTemplateData = new
        {
            offer_id = offer.Id,
            first_name = to.Name
        };
        var message = MailHelper.CreateSingleTemplateEmail(from, to, _options.OfferSelectedTemplate, dynamicTemplateData);
        _ = await sendGridClient.SendEmailAsync(message);
    }

    public async Task SendOrderCreatedMessage(OrderDTO order)
    {
        var from = new EmailAddress(_options.FromEmail, _options.FromName);
        var to = new EmailAddress(order.Offer.CustomerInfo!.Email, order.Offer.CustomerInfo.FirstName);
        var dynamicTemplateData = new
        {
            order_id = order.Id,
            first_name = to.Name
        };
        var message = MailHelper.CreateSingleTemplateEmail(from, to, _options.OfferAcceptedTemplate, dynamicTemplateData);
        _ = await sendGridClient.SendEmailAsync(message);
    }
}
