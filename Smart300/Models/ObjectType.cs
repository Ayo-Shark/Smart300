using System;
using System.Collections.Generic;

namespace Smart300.Models;

public partial class ObjectType
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Equipment> Equipment { get; set; } = new List<Equipment>();
}
