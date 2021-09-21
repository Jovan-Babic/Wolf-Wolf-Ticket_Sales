using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Wolf_Wolf_TicketSales.Helpers;

namespace Wolf_Wolf_TicketSales.DataLayer
{
    public static class DbSeed
    {

        public static void SeedData(DataContext context)
        {
            var users = new List<User>();
            var roles = new List<Role>
            {
                new Role { Id = 1, Name = "ADMIN" },
                new Role { Id = 2, Name = "USER" }
            };

            users.Add(new User
            {
                Fullname = "Admin User",
                Username = "admin",
                Password = PasswordHelper.EncodePasswordToBase64("admin"),
                RoleId = 1
            });

            users.Add(new User 
            {
                Fullname = "Regular User 1",
                Username = "user1",
                Password = PasswordHelper.EncodePasswordToBase64("user"),
                RoleId = 2
            });

            users.Add(new User
            {
                Fullname = "Regular User 2",
                Username = "user2",
                Password = PasswordHelper.EncodePasswordToBase64("user"),
                RoleId = 2
            });

            users.Add(new User
            {
                Fullname = "Regular User 3",
                Username = "user3",
                Password = PasswordHelper.EncodePasswordToBase64("user"),
                RoleId = 2
            });

            users.Add(new User
            {
                Fullname = "Regular User 4",
                Username = "user4",
                Password = PasswordHelper.EncodePasswordToBase64("user"),
                RoleId = 2
            });

            context.AddRange(roles);
            context.AddRange(users);

            context.Database.OpenConnection();
            try
            {
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Roles ON");
                context.SaveChanges();
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Roles OFF");
            }
            finally
            {
                context.Database.CloseConnection();
            }

            context.SaveChanges();
        }


        
      
    }
}
