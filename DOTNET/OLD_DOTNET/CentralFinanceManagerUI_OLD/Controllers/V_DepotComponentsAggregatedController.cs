using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using CentralFinanceManagerUI.Data;
using CentralFinanceManagerUI.Models;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace CentralFinanceManagerUI.Controllers
{


    public class V_DepotComponentsAggregatedController : Controller
    {
        private readonly SqlDbContext _context;

        public V_DepotComponentsAggregatedController(SqlDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Details(int? id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (id == null)
            {
                return NotFound();
            }

            var v_DepotComponentsAggregated = await _context.V_DepotComponentsAggregated
                .FirstOrDefaultAsync(m => m.DepotId == id && m.UserHK == userId);
            if (v_DepotComponentsAggregated == null)
            {
                return NotFound();
            }

            return View(v_DepotComponentsAggregated);
        }


        
    }
}
