using System.Collections.Generic;
using System.Threading.Tasks;
using Wolf_Wolf_TicketSales.Models;

namespace Wolf_Wolf_TicketSales.Services
{
    public interface IUserService
    {
        Task BuyTicketsAsync(int userId, int concertId, int ticketAmount);
        Task<IList<UserTicketsModel>> GetTicketsAsync(int userId);
        IList<ConcertOffersModel> OfferConcertTickets();        
        Task<UserModel> GetUserByUsernameAsync(string username);
        Task<BuyTicketsModel> GetConcertInfoAsync(int concertId);
    }
}