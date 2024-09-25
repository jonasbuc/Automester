using System;
using System.Collections.Generic;

namespace Automester.Models;

public partial class Car
{
    public int CarId { get; set; }

    public int? CustomerId { get; set; }

    public string? RegistrationNumber { get; set; }

    public string? Brand { get; set; }

    public string? Model { get; set; }

    public int? Year { get; set; }

    public int? Mileage { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<ReadyForPickup> ReadyForPickups { get; set; } = new List<ReadyForPickup>();

    public virtual ICollection<Repair> Repairs { get; set; } = new List<Repair>();

    public virtual ICollection<Workshop> Workshops { get; set; } = new List<Workshop>();
}
