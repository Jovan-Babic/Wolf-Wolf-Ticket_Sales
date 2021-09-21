using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wolf_Wolf_TicketSales.DataLayer;
using Wolf_Wolf_TicketSales.Models;

namespace Wolf_Wolf_TicketSales.Services
{
    public class AdminService : IAdminService
    {
        private readonly DataContext _context;

        public AdminService(DataContext context)
        {
            _context = context;
        }

        public IList<ConcertInfoModel> GetConcertsInfo()
        {
            List<ConcertInfoModel> concertList = new List<ConcertInfoModel>();
            foreach (var item in _context.Concerts)
            {
                string ticketsSold = item.TicketsAvailable == 0 ? "Concert SOLD!" : $"{item.Tickets - item.TicketsAvailable}";

                concertList.Add(new ConcertInfoModel { ConcertName = item.Name, Location = item.Location, TicketsSold = ticketsSold });
            }

            return concertList;
        }

        public async Task CreateConcertAsync(ConcertCreateModel model)
        {
            _context.Add<Concert>(new Concert { Name = model.Name, Location = model.Location, Tickets = model.Tickets, TicketsAvailable = model.Tickets, Created = DateTimeOffset.UtcNow });
            await _context.SaveChangesAsync();
        }
    }
}
