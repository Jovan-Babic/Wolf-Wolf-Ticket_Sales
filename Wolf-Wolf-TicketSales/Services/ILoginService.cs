using System.Threading.Tasks;
using Wolf_Wolf_TicketSales.Models;

namespace Wolf_Wolf_TicketSales.Services
{
    public interface ILoginService
    {
        Task<UserModel> AuthenticateuserAsync(string username, string password);
    }
}