using CourierAppBackend.Abstractions;
using CourierAppBackend.DtoModels;
using CourierAppBackend.Models;
using CourierAppBackend.ModelsDTO;
using Microsoft.EntityFrameworkCore;

namespace CourierAppBackend.Data
{
    public class DbOrdersRepository : IOrdersRepository
    {
        private readonly CourierAppContext _context;
        private readonly IOffersRepository _offersRepository;
        public DbOrdersRepository(CourierAppContext context, IAddressesRepository addressesRepository, IOffersRepository offersRepository)
        {
            _context = context;
            _offersRepository = offersRepository;
        }
        public async Task<Order?> GetOrderById(int id)
        {
            return await _context.Orders
                        .Include(x => x.Offer)
                        .Include(x => x.Offer.CustomerInfo)
                        .Include(x => x.Offer.CustomerInfo!.Address)
                        .Include(x => x.Offer.Inquiry)
                        .Include(x => x.Offer.Inquiry.SourceAddress)
                        .Include(x => x.Offer.Inquiry.DestinationAddress)
                        .FirstOrDefaultAsync();
        }

        public async Task<List<Order>> GetOrders()
        {
            return await _context.Orders
                        .Include(x => x.Offer)
                        .Include(x => x.Offer.CustomerInfo)
                        .Include(x => x.Offer.CustomerInfo!.Address)
                        .Include(x => x.Offer.Inquiry)
                        .Include(x => x.Offer.Inquiry.SourceAddress)
                        .Include(x => x.Offer.Inquiry.DestinationAddress)
                        .ToListAsync();
        }
        public async Task<Order> CreateOrder(OrderC orderC)
        {
            var offer = await _offersRepository.GetOfferById(orderC.OfferID);
            Order order = new()
            {
                OfferID = offer.Id,
                Offer = offer,
                OrderStatus = OrderStatus.Accepted,
                LastUpdate = DateTime.Now,
                CourierName = ""
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }
        public async Task<Order?> UpdateOrder(int id, OrderU orderU)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order is null)
                return null;
            order.OrderStatus = orderU.OrderStatus;
            order.CourierName = orderU.CourierName;
            order.LastUpdate = DateTime.Now;
            order.Comment = orderU.Comment;
            await _context.SaveChangesAsync();
            return order;

        }
    }
}
