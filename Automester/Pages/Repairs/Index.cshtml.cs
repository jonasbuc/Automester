using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Automester.Models;

namespace Automester.Pages.Repairs
{
    public class IndexModel : PageModel
    {
        private readonly Automester.Models.AutomesterContext _context;

        public IndexModel(Automester.Models.AutomesterContext context)
        {
            _context = context;
        }

        public IList<Repair> Repair { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Repair = await _context.Repairs
                .Include(r => r.Car).ToListAsync();
        }
    }
}
