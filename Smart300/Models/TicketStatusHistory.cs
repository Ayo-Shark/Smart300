using System;
using System.Collections.Generic;

namespace Smart300.Models;

public partial class TicketStatusHistory
{
    public int Id { get; set; }

    public int TicketId { get; set; }

    public int StatusId { get; set; }

    public int? ChangedBy { get; set; }

    public DateTime? ChangedAt { get; set; }

    public virtual User? ChangedByNavigation { get; set; }

    public virtual TicketStatus Status { get; set; } = null!;

    public virtual Ticket Ticket { get; set; } = null!;
}
