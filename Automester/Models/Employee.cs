using System;
using System.Collections.Generic;

namespace Automester.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string? Name { get; set; }

    public DateOnly? HireDate { get; set; }

    public string? Position { get; set; }

    public virtual ICollection<EmployeeRepair> EmployeeRepairs { get; set; } = new List<EmployeeRepair>();
}
