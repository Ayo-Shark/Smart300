using System;
using System.Collections.Generic;

namespace Smart300.Models;

public partial class TicketStage
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int SortOrder { get; set; }

    public virtual ICollection<TicketStatus> TicketStatuses { get; set; } = new List<TicketStatus>();
}
