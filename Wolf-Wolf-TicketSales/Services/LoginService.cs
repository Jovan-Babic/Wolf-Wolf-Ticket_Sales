using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Wolf_Wolf_TicketSales.DataLayer;
using Wolf_Wolf_TicketSales.Helpers;
using Wolf_Wolf_TicketSales.Models;

namespace Wolf_Wolf_TicketSales.Services
{
    public class LoginService : ILoginService
    {
        private readonly DataContext _context;

        public LoginService(DataContext context)
        {
            _context = context;
        }

        public async Task<UserModel> AuthenticateuserAsync(string username, string password)
        {
            var user = _context.Users.Where(x => x.Username == username).FirstOrDefault();
            var roles = _context.Roles;

            if (user == null)
            {
                throw new Exception("Invalid username");
            }
            if (PasswordHelper.EncodePasswordToBase64(password).Equals(user.Password, StringComparison.OrdinalIgnoreCase))
            {
                return new UserModel
                {
                    FullName = user.Fullname,
                    Username = user.Username,
                    Role = (await roles.Where(x => x.Id == user.RoleId).FirstOrDefaultAsync()).Name
                };
            }
            else
            {
                throw new Exception("Invalid Password");
            }
        }
    }
}
