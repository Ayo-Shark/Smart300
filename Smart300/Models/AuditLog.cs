using System;
using System.Collections.Generic;

namespace Smart300.Models;

public partial class AuditLog
{
    public int Id { get; set; }

    public string EntityType { get; set; } = null!;

    public int EntityId { get; set; }

    public string Action { get; set; } = null!;

    public int? ChangedBy { get; set; }

    public string? OldData { get; set; }

    public string? NewData { get; set; }

    public string? Comment { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User? ChangedByNavigation { get; set; }
}
