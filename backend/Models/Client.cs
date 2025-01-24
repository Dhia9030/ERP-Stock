using System;
using System.Collections.Generic;

namespace StockManagement.Models
{
    public class Client
    {
        public int ClientId { get; set; } // Unique identifier for the client
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime RegistrationDate { get; set; } // Client's registration date

        // Relationship: A client can have multiple orders
        public ICollection<Order> Orders { get; set; }
    }
}
