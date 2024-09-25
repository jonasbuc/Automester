using System;
using System.Collections.Generic;

namespace Automester.Models;

public partial class ReadyForPickup
{
    public int PickupId { get; set; }

    public int? CarId { get; set; }

    public DateOnly? ReadyDate { get; set; }

    public virtual Car? Car { get; set; }
}
