using System;
using System.Collections.Generic;

namespace WebOrder.Models;

public partial class Client
{
    public int ClientId { get; set; }

    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public DateTime RegistrationDate { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<ProductItem> ProductItems { get; set; } = new List<ProductItem>();
}
