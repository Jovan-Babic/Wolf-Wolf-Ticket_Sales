using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wolf_Wolf_TicketSales.DataLayer;
using Wolf_Wolf_TicketSales.Models;

namespace Wolf_Wolf_TicketSales.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }

        public async Task<IList<UserTicketsModel>> GetTicketsAsync(int userId)
        {
            var ticketList = from t in _context.UserTickets
                             from c in _context.Concerts
                             where c.Id == t.ConcertId && t.UserId == userId
                             select new UserTicketsModel { Concert = c.Name, Location = c.Location, TicketsOwned = t.TicketsBought };

            return await ticketList.ToListAsync();
        }

        public IList<ConcertOffersModel> OfferConcertTickets()
        {
            List<ConcertOffersModel> concertOffers = new List<ConcertOffersModel>();
            var concerts = _context.Concerts.Where(x => x.TicketsAvailable > 0);
            foreach (var item in concerts)
            {
                string tickets = item.TicketsAvailable == 0 ? "CONCERT SOLD" : $"{item.TicketsAvailable}";
                concertOffers.Add(new ConcertOffersModel { ConcertId = item.Id, ConcertLocation = item.Location, ConcertName = item.Name, TicketsAvailable = tickets });
            }

            return concertOffers;
        }

        public async Task BuyTicketsAsync(int userId, int concertId, int ticketAmount)
        {
            var concert = _context.Concerts.Where(x => x.Id == concertId).FirstOrDefault();

            if (concert != null)
            {
                if (concert.TicketsAvailable < ticketAmount)
                {
                    throw new Exception($"Only {concert.TicketsAvailable} ticets left. You can not buy more than that.");
                }

                await AddOrUpdateTicketsForUserAsync(userId, concertId, ticketAmount);

                concert.TicketsAvailable -= ticketAmount; //decrease available ticets amount by ticketAmount
                concert.Updated = DateTimeOffset.UtcNow;

                await _context.SaveChangesAsync();

            }
            else
            {
                throw new Exception("Concert does not exist!");
            }
        }

        public async Task<UserModel> GetUserByUsernameAsync(string username)
        {
            var user = await _context.Users.Where(x => x.Username == username).FirstOrDefaultAsync();

            if(user == null)
            {
                throw new Exception("Invalid username");
            }
            return new UserModel { Id = user.Id, FullName = user.Fullname, Username = user.Username };
        }

       

        public async Task<BuyTicketsModel> GetConcertInfoAsync(int concertId)
        {
            var concert = await _context.Concerts.Where(x => x.Id == concertId).FirstOrDefaultAsync();
            if(concert == null)
            {
                throw new Exception("Invalid concertId");
            }

            return new BuyTicketsModel { ConcertId = concert.Id, ConcertName = concert.Name, Location = concert.Location };
        }

        #region Private Methods

        private async Task AddOrUpdateTicketsForUserAsync(int userId, int concertId, int tickets)
        {
            var userTickets = _context.UserTickets.Where(x => x.UserId == userId && x.ConcertId == concertId).FirstOrDefault();

            if (userTickets != null)
            {
                userTickets.TicketsBought += tickets;
            }
            else
            {
               await _context.AddAsync<UserTicket>(new UserTicket { UserId = userId, ConcertId = concertId, TicketsBought = tickets });
            }

            await _context.SaveChangesAsync();
        }

        #endregion
    }
}
