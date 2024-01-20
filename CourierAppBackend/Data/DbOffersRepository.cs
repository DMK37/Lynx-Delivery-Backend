﻿using CourierAppBackend.Abstractions.Repositories;
using CourierAppBackend.Models.Database;
using CourierAppBackend.Models.DTO;
using CourierAppBackend.Models.LecturerAPI;
using CourierAppBackend.Models.LynxDeliveryAPI;
using CourierAppBackend.Services;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CourierAppBackend.Data
{
    public class DbOffersRepository(CourierAppContext context, IAddressesRepository addressesRepository)
        : IOffersRepository
    {

        public async Task<Offer> CreateOffferFromOurInquiry(int id)
        {
            var inquiry = await context.Inquiries.FindAsync(id);
            if (inquiry is null)
                return null!;

            //also need to calulate price
            var calc = new PriceCalculator();
            var price = calc.CalculatePrice(inquiry);

            Offer offer = new()
            {
                Inquiry = inquiry,
                CreationDate = DateTime.UtcNow,
                ExpireDate = DateTime.UtcNow.AddMinutes(15),
                UpdateDate = DateTime.UtcNow,
                Status = OfferStatus.Offered,
                Price = price
            };

            await context.AddAsync(offer);
            await context.SaveChangesAsync();

            return offer;
        }

        public async Task<Offer> GetOfferById(int ID)
        {
            var result = await context.Offers
                .Include(x => x.Inquiry)
                .Include(x => x.Inquiry.SourceAddress)
                .Include(x => x.Inquiry.DestinationAddress)
                .FirstOrDefaultAsync(x => x.Id == ID);
            return result!;
        }

        public async Task<List<Offer>> GetOffers()
        {
            var res = await context.Offers
                .Include(x => x.Inquiry)
                .Include(x => x.Inquiry.SourceAddress)
                .Include(x => x.Inquiry.DestinationAddress)
                .ToListAsync();
            return res;
        }

        public async Task<List<Offer>> GetPendingOffers()
        {
            var res = await context.Offers.Where(x => x.Status == OfferStatus.Pending).Include(x => x.Inquiry)
                .Include(x => x.Inquiry.SourceAddress)
                .Include(x => x.Inquiry.DestinationAddress)
                .ToListAsync();
            return res;
        }

        public async Task<Offer> SelectOffer(OfferSelect offerSelect)
        {
            var address = await addressesRepository.AddAddress(offerSelect.CustomerInfo.Address);

            var offer = context.Offers.FirstOrDefault(x => x.Id == offerSelect.OfferId);
            if (offer == null)
                return null!;
            offer.Status = OfferStatus.Pending;
            offer.UpdateDate = DateTime.UtcNow;
            offer.CustomerInfo = new()
            {
                CompanyName = offerSelect.CustomerInfo.CompanyName,
                FirstName = offerSelect.CustomerInfo.FirstName,
                LastName = offerSelect.CustomerInfo.LastName,
                Address = address,
                Email = offerSelect.CustomerInfo.Email,
            };

            await context.SaveChangesAsync();

            return offer;
        }

        public async Task<Offer> ConfirmOffer(int id, ConfirmOfferRequest request)
        {
            var address = await addressesRepository.AddAddress(request.CustomerInfo.Address);

            var offer = context.Offers.FirstOrDefault(x => x.Id == id);
            if (offer == null)
                return null!;
            offer.Status = OfferStatus.Pending;
            offer.UpdateDate = DateTime.UtcNow;
            offer.CustomerInfo = new()
            {
                CompanyName = request.CustomerInfo.CompanyName,
                FirstName = request.CustomerInfo.FirstName,
                LastName = request.CustomerInfo.LastName,
                Address = address,
                Email = request.CustomerInfo.Email
            };

            await context.SaveChangesAsync();

            return offer;
        }

        public async Task<List<TemporaryOfferDTO>?> GetOffers(int inquiryId, List<IApiCommunicator> apis)
        {
            var inquiry = await context.Inquiries
                                       .Include(x => x.SourceAddress)
                                       .Include(x => x.Package)
                                       .Include(x => x.DestinationAddress)
                                       .FirstOrDefaultAsync(x => x.Id == inquiryId);
            if (inquiry is null)
                return null;
            if (inquiry.Status == InquiryStatus.OffersRequested)
            {
                return await context.TemporaryOffers
                                    .AsNoTracking()
                                    .Include(x => x.Inquiry)
                                    .Include(x => x.PriceItems)
                                    .Where(x => x.Inquiry.Id == inquiry.Id)
                                    .Select(res => new TemporaryOfferDTO()
                                    {
                                        Id = res.Id,
                                        Company = res.Company,
                                        TotalPrice = res.TotalPrice,
                                        ExpiringAt = res.ExpiringAt,
                                        PriceBreakDown = res.PriceItems.Select(x => new PriceBreakDownItem()
                                        {
                                            Currency = x.Currency,
                                            Amount = x.Amount,
                                            Description = x.Description
                                        }).ToList()
                                    })
                                    .ToListAsync();

            }
            var tasks = new List<Task<TemporaryOffer>>();
            for (int i = 0; i < apis.Count; i++)
                tasks[i] = apis[i].GetOffer(inquiry);
            var offers = new List<TemporaryOfferDTO>();
            Task<TemporaryOffer> timeoutTask = FakeTask();
            while (tasks.Count > 1)
            {
                var completedTask = await Task.WhenAny(tasks);
                if (completedTask == timeoutTask)
                    break;
                tasks.Remove(completedTask);
                TemporaryOffer res = await completedTask;
                if (res is not null)
                {
                    await context.TemporaryOffers.AddAsync(res);
                    await context.SaveChangesAsync();
                    offers.Add(new TemporaryOfferDTO()
                    {
                        Id = res.Id,
                        Company = res.Company,
                        TotalPrice = res.TotalPrice,
                        ExpiringAt = res.ExpiringAt,
                        PriceBreakDown = res.PriceItems.Select(x => new PriceBreakDownItem()
                        {
                            Currency = x.Currency,
                            Amount = x.Amount,
                            Description = x.Description
                        }).ToList()
                    });
                }
            }
            inquiry.Status = InquiryStatus.OffersRequested;
            await context.SaveChangesAsync();
            return offers;
        }

        public async Task<TemporaryOffer> FakeTask()
        {
            await Task.Delay(30000);
            return null!;
        }
        public async Task<CreateOfferResponse> CreateOffer(CreateOfferRequest request)
        {
            var source = await addressesRepository.AddAddress(request.SourceAddress);
            var destination = await addressesRepository.AddAddress(request.DestinationAddress);

            Inquiry inquiry = new()
            {
                DateOfInquiring = DateTime.UtcNow,
                PickupDate = request.PickupDate,
                DeliveryDate = request.DeliveryDate,
                Package = request.Package,
                SourceAddress = source,
                DestinationAddress = destination,
                IsCompany = request.IsCompany,
                HighPriority = request.HighPriority,
                DeliveryAtWeekend = request.DeliveryAtWeekend,
                Status = InquiryStatus.Created,
                CourierCompanyName = "TODO"
            };

            await context.Inquiries.AddAsync(inquiry);
            await context.SaveChangesAsync();

            var calc = new PriceCalculator();
            var price = calc.CalculatePrice(inquiry);

            Offer offer = new()
            {
                Inquiry = inquiry,
                CreationDate = DateTime.UtcNow,
                ExpireDate = DateTime.UtcNow.AddMinutes(15),
                UpdateDate = DateTime.UtcNow,
                Status = OfferStatus.Offered,
                Price = price
            };

            await context.Offers.AddAsync(offer);
            await context.SaveChangesAsync();

            var response = new CreateOfferResponse
            {
                OfferId = offer.Id,
                CreationDate = offer.CreationDate,
                ExpireDate = offer.ExpireDate,
                Price = calc.CalculatePriceIntoBreakdown(inquiry)
            };

            return response;
        }
    }

}

