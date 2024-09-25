using System;
using System.Collections.Generic;

namespace Automester.Models;

public partial class Repair
{
    public int RepairId { get; set; }

    public int? CarId { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public string? Status { get; set; }

    public virtual Car? Car { get; set; }

    public virtual ICollection<EmployeeRepair> EmployeeRepairs { get; set; } = new List<EmployeeRepair>();
}
