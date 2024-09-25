using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Automester.Models;

namespace Automester.Pages.Workshops
{
    public class WorkshopsModel : PageModel
    {
        private readonly Automester.Models.AutomesterContext _context;

        public WorkshopsModel(Automester.Models.AutomesterContext context)
        {
            _context = context;
        }

        public IList<Workshop> Workshop { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Workshop = await _context.Workshops
                .Include(w => w.Car).ToListAsync();
        }
    }
}
