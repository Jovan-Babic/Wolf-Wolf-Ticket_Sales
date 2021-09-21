using System.Collections.Generic;

namespace Wolf_Wolf_TicketSales.Models
{
    public class UserTicketsModel
    {
        public string Concert { get; set; }
        public string Location { get; set; }
        public int TicketsOwned { get; set; }
    }

    public class UserTicketsWithOffersModel
    {
        public IList<UserTicketsModel> UserTickets { get; set; }
        public IList<ConcertOffersModel> ConcertOffers { get; set; }
    }
}
