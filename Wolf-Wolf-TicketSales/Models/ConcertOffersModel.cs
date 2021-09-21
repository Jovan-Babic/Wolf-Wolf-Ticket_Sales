using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wolf_Wolf_TicketSales.Models
{
    public class ConcertOffersModel
    {
        public int ConcertId { get; set; }
        public string ConcertName { get; set; }
        public string ConcertLocation { get; set; }
        public string TicketsAvailable { get; set; }
    }
}
