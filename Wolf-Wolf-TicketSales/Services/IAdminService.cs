using System.Collections.Generic;
using System.Threading.Tasks;
using Wolf_Wolf_TicketSales.Models;

namespace Wolf_Wolf_TicketSales.Services
{
    public interface IAdminService
    {
        Task CreateConcertAsync(ConcertCreateModel model);
        IList<ConcertInfoModel> GetConcertsInfo();
    }
}