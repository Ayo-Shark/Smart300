using System;
using System.Collections.Generic;

namespace Smart300.Models;

public partial class Customer
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Inn { get; set; } = null!;

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    public virtual User User { get; set; } = null!;
}
