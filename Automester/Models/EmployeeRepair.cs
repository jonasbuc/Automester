using System;
using System.Collections.Generic;

namespace Automester.Models;

public partial class EmployeeRepair
{
    public int EmployeeId { get; set; }

    public int RepairId { get; set; }

    public decimal? HoursWorked { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual Repair Repair { get; set; } = null!;
}
