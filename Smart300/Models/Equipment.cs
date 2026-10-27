using System;
using System.Collections.Generic;

namespace Smart300.Models;

public partial class Equipment
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string SerialNumber { get; set; } = null!;

    public int CompanyId { get; set; }

    public int ObjectTypeId { get; set; }

    public string? ImageUrl { get; set; }

    public string? Address { get; set; }

    public DateOnly? CommissioningDate { get; set; }

    public DateOnly? WarrantyUntil { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual ObjectType ObjectType { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
