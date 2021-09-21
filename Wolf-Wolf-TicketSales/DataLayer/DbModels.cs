using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wolf_Wolf_TicketSales.DataLayer
{
    [Table("Users")]
    public class User
    {        
        [Key]
        public int Id { get; set; }
        
        [MaxLength(100)]
        public string Fullname { get; set; }
        
        [MaxLength(50)]
        public string Username { get; set; }
        
        [MaxLength(50)]
        public string Password { get; set; }

        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }

    }

    public class Role
    {
        [Key]
        public int Id { get; set; }
        
        [MaxLength(20)]
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }
    }

    public class Concert
    {
        [Key]
        public int Id { get; set; }
        
        [MaxLength(100)]
        public string Name { get; set; }
        
        [MaxLength(100)]
        public string Location { get; set;}
        
        public int Tickets { get; set; }
        public int TicketsAvailable { get; set; }
        
        public DateTimeOffset Created { get; set; }
        
        public DateTimeOffset? Updated { get; set; }

    }

    public class UserTicket
    {
        [Key, Column(Order = 0)]
        public int ConcertId { get; set; }
        [Key, Column(Order = 1)]
        public int UserId { get; set; }

        public int TicketsBought { get; set; }

        [ForeignKey("ConcertId")]
        public Concert Concert { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
