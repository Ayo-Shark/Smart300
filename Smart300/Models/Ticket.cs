using System;
using System.Collections.Generic;

namespace Smart300.Models;

public partial class Ticket
{
    public int Id { get; set; }

    public string TicketNumber { get; set; } = null!;

    public string Subject { get; set; } = null!;

    public string? Description { get; set; }

    public int TypeId { get; set; }

    public int CustomerId { get; set; }

    public int CompanyId { get; set; }

    public int? EquipmentId { get; set; }

    public int PriorityId { get; set; }

    public int StatusId { get; set; }

    public int? AssigneeId { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DetectedAt { get; set; }

    public DateTime? Deadline { get; set; }

    public DateTime? ClosedAt { get; set; }

    public decimal? EstimatedCost { get; set; }

    public bool IsDeleted { get; set; }

    public string? CancellationReason { get; set; }

    public virtual Employee? Assignee { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;

    public virtual Equipment? Equipment { get; set; }

    public virtual Priority Priority { get; set; } = null!;

    public virtual TicketStatus Status { get; set; } = null!;

    public virtual ICollection<TicketStatusHistory> TicketStatusHistories { get; set; } = new List<TicketStatusHistory>();

    public virtual TicketType Type { get; set; } = null!;
}
