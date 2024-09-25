using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Automester.Models;

namespace Automester.Pages.ReadyForPickups
{
    public class ReadyForPickupsModel : PageModel
    {
        private readonly Automester.Models.AutomesterContext _context;

        public ReadyForPickupsModel(Automester.Models.AutomesterContext context)
        {
            _context = context;
        }

        public IList<ReadyForPickup> ReadyForPickup { get;set; } = default!;

        public async Task OnGetAsync()
        {
            ReadyForPickup = await _context.ReadyForPickups
           .Include(r => r.Car)
               .ThenInclude(c => c.Customer) // Inkluder Customer data
           .ToListAsync();
        }
    }
}
