using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CentralFinanceManagerUI.Data;
using CentralFinanceManagerUI.Models.UserDepots;
using CentralFinanceManagerUI.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using CentralFinanceManagerUI.Models.SymbolViewModels;

namespace CentralFinanceManagerUI.Controllers
{
    public class UserDepotController : Controller
    {
        private readonly SqlDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;

        public UserDepotController(SqlDbContext context,
            UserManager<ApplicationUser> userManager,
                   SignInManager<ApplicationUser> signInManager,
                   ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
        }

        // GET: UserDepot
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userName = User.FindFirstValue(ClaimTypes.Name);

            var DepotList_Anonym = from s in _context.UserDepot
                                   where s.UserHK == userId
                                   select
                               new
                               {
                                   DepotName = s.DepotName,
                                   UserName = userName,
                                   DepotId = s.DepotId,
                                   UserHK = s.UserHK,
                                   ValidFrom = s.ValidFrom,
                                   ValidTo = s.ValidTo
                               };

            IEnumerable<UserDepot> DepotList = DepotList_Anonym.Select(x => new UserDepot
            {
                DepotName = x.DepotName,
                UserName = userName,
                DepotId = x.DepotId,
                UserHK = x.UserHK,
                ValidFrom = x.ValidFrom,
                ValidTo = x.ValidTo
            }).ToList();

            return View(DepotList);
        }

        // GET: UserDepot/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (id == null)
            {
                return NotFound();
            }

            List<V_DepotComponentsAggregated> f_UserDepot =  _context.V_DepotComponentsAggregated.Where(x => x.DepotId == id && x.UserHK == userId).OrderByDescending(x => x.Percentage).ToList();
            if (f_UserDepot == null)
            {
                return NotFound();
            }

            List<Symbols> _listOfSymbols = (from columns in _context.Symbols
                                            orderby columns.SymbolName ascending
                                            select columns).ToList();

            Dictionary<string, string> dict = new Dictionary<string, string>();

            foreach (var item in _listOfSymbols)
            {
                dict.Add(item.Symbol, item.SymbolName);
            }

            ViewBag.ListOfSymbolsDic = dict;

            ViewBag.ViewDepotDetails = f_UserDepot;

            ViewBag.CurrentDepot = id;

            return View();
        }

        // GET: F_UserDepot/Create
        public IActionResult Create()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View();
        }

        public IActionResult CreateNew()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View();
        }

        public IActionResult EditDepot()
        {
            return View();
        }

        // POST: F_UserDepot/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateNew([Bind("UserHK,DepotName")] UserDepot f_UserDepot)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (ModelState.IsValid)
            {

                int NewDepotObjId = CreateNewDepotObj(userId);

                var NewObj = _context.UserDepotObjs.Where(x=> x.UserHK == userId).OrderByDescending(x => x.DepotId).FirstOrDefault();

                Console.WriteLine("Habe neue DepotID erstellt:{0}", NewObj.DepotId.ToString());

                f_UserDepot.UserHK = userId;

                f_UserDepot.ValidFrom = DateTime.Now;

                f_UserDepot.ValidTo = DateTime.Parse("9999-12-31");

                f_UserDepot.DepotId = NewObj.DepotId;

                Console.WriteLine(NewObj.DepotId);

                _context.Add(f_UserDepot);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(f_UserDepot);
        }

        // GET: F_UserDepot/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (id == null)
            {
                return NotFound();
            }

            var f_UserDepot = await _context.UserDepot.FindAsync(id);

            string test_User = f_UserDepot.UserHK;

            if (f_UserDepot == null || test_User != userId)
            {
                return NotFound();
            }
            return View(f_UserDepot);
        }

        // POST: F_UserDepot/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("UserDepotHK,DepotId,UserHK,ValidFrom,ValidTo")] UserDepot f_UserDepot)
        {
            if (id != f_UserDepot.DepotId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(f_UserDepot);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserDepotExists((int)f_UserDepot.DepotId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(f_UserDepot);
        }

        // GET: F_UserDepot/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var f_UserDepot = await _context.UserDepot
                .FirstOrDefaultAsync(m => m.DepotId == id);
            if (f_UserDepot == null)
            {
                return NotFound();
            }

            return View(f_UserDepot);
        }

        // POST: F_UserDepot/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var f_UserDepot = await _context.UserDepot.FindAsync(id);
            _context.UserDepot.Remove(f_UserDepot);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserDepotExists(int id)
        {
            return _context.UserDepot.Any(e => e.DepotId == id);
        }


        private int CreateNewDepotObj(string UserHK)
        {
            UserDepotObj NewObj = new UserDepotObj
            {
                UserHK = UserHK
            };

            _context.Add(NewObj);

            _context.SaveChanges();

            return 1;
        }

        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateNewDepotComp([Bind("DepotId,USerHK,Symbol,MarketValue,ValidFrom,ValidTo,Quantity")] DepotComponents depotComponents, int depotId)
        {

            if (ModelState.IsValid)
            {
                _context.Add(depotComponents);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new {id = depotId });
            }
            return RedirectToAction(nameof(Details), new { id = depotId });
        }

        // GET: UserDepotComp/Delete/5
        public async Task<IActionResult> DeleteDepotComp(int? depotComponentId, int depotId)
        {

            var depotComponents = await _context.DepotComponents.FindAsync(depotComponentId);
            _context.DepotComponents.Remove(depotComponents);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = depotId });



        }



    }
}
