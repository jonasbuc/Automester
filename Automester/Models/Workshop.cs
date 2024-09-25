using System;
using System.Collections.Generic;

namespace Automester.Models;

public partial class Workshop
{
    public int WorkshopId { get; set; }

    public int? CarId { get; set; }

    public DateOnly? ArrivalDate { get; set; }

    public virtual Car? Car { get; set; }
}
