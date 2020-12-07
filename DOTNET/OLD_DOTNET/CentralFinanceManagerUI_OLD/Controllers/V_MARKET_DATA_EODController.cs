using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using CentralFinanceManagerUI.Data;
using CentralFinanceManagerUI.Models;
using CentralFinanceManagerUI.Models.SymbolViewModels;

namespace CentralFinanceManagerUI.Controllers
{


    public class V_MARKET_DATA_EODController : Controller
    {
        private readonly SqlDbContext _context;

        public V_MARKET_DATA_EODController(SqlDbContext context)
        {
            _context = context;
        }


        // Home Controller
        public ActionResult Index()
        {
            // All Variables


            DateTime startDateVol = DateTime.Now.AddDays(-7);

            DateTime startDate = DateTime.Now.AddDays(-7);

            DateTime endDate = DateTime.Now;
             
            List<V_MARKET_DATA_EOD> _market_data_eod_List = (from columns in _context.V_MARKET_DATA_EODs
                                                             where (columns.Symbol == "pypl" && columns.Market_Timestamp >= startDateVol && columns.Market_Timestamp <= endDate)
                                                             orderby columns.Symbol ascending, columns.Market_Timestamp ascending
                                                             select columns).ToList();

            List<Symbols> _listOfSymbols = (from columns in _context.Symbols
                                            orderby columns.SymbolName ascending
                                            select columns).ToList();


            List<DateTime> _selectedMarketTimeStamps = new List<DateTime>(_market_data_eod_List.Where(x => x.Symbol.Equals("pypl")).Select(x => x.Market_Timestamp).Distinct().ToArray());

            List<string> SelectedTimeStamps = new List<string>();

            foreach (var item in _selectedMarketTimeStamps)
            {
                SelectedTimeStamps.Add(item.ToShortDateString().ToString());
            };

            var _selectedCloseValues = _market_data_eod_List.Where(x => x.Symbol.Equals("pypl")).Select(x => x.Close).ToArray();

            var _selectedHighValues = _market_data_eod_List.Where(x => x.Symbol.Equals("pypl")).Select(x => x.High).ToArray();

            var _selectedLowValues = _market_data_eod_List.Where(x => x.Symbol.Equals("pypl")).Select(x => x.Low).ToArray();

            var _selectedVolume = _market_data_eod_List.Where(x => x.Symbol.Equals("pypl")).Select(x => x.AGG_Volume).ToArray();

            var dict2 = new Dictionary<string, string>();

            foreach (var item in _listOfSymbols)
            {
                dict2.Add(item.Symbol, item.SymbolName);
            }



            // All ViewBags

            ViewBag.ListOfSymbolsDic = dict2;

            ViewBag.MarketDataEodList = _market_data_eod_List;

            ViewBag.SelectedVolume = Newtonsoft.Json.JsonConvert.SerializeObject(_selectedVolume);

            ViewBag.SelectedMarketTimeStamps = Newtonsoft.Json.JsonConvert.SerializeObject(SelectedTimeStamps);

            ViewBag.SelectedCloseValues = Newtonsoft.Json.JsonConvert.SerializeObject(_selectedCloseValues);

            ViewBag.SelectedHighValues = Newtonsoft.Json.JsonConvert.SerializeObject(_selectedHighValues);

            ViewBag.SelectedLowValues = Newtonsoft.Json.JsonConvert.SerializeObject(_selectedLowValues);

            ViewBag._selectedMarketTimeStampsVol = Newtonsoft.Json.JsonConvert.SerializeObject(SelectedTimeStamps);

            return View();
        }

        [HttpPost]
        public ActionResult Index(V_MARKET_DATA_EOD SymbolData)
        {

            if (ModelState.IsValid)
            {
                // All Variables


                DateTime startDateVol = DateTime.Now.AddDays(-7);

                DateTime startDate = DateTime.Now.AddDays(-7);

                DateTime endDate = DateTime.Now;

                if (SymbolData.StartDate_IND == 1)
                {
                    startDate = DateTime.Now.AddDays(-7);
                }
                if (SymbolData.StartDate_IND == 2)
                {
                    startDate = DateTime.Now.AddDays(-14);
                }
                if (SymbolData.StartDate_IND == 3)
                {
                    startDate = DateTime.Now.AddDays(-30);
                }

                List<V_MARKET_DATA_EOD> _market_data_eod_List = (from columns in _context.V_MARKET_DATA_EODs
                                                                 where (columns.Market_Timestamp >= startDate && columns.Market_Timestamp <= endDate)
                                                                 && columns.Symbol.Equals(SymbolData.Symbol)
                                                                 orderby columns.Symbol ascending, columns.Market_Timestamp ascending
                                                                 select columns).Take(100).ToList();

                List<Symbols> _listOfSymbols = (from columns in _context.Symbols
                                                orderby columns.SymbolName ascending
                                                select columns).ToList();


                List<DateTime> _selectedMarketTimeStamps = new List<DateTime>(_market_data_eod_List.Where(x => x.Symbol.Equals(SymbolData.Symbol)).Select(x => x.Market_Timestamp).Distinct().ToArray());

                List<DateTime> _selectedMarketTimeStampsVol = new List<DateTime>(_market_data_eod_List.Where(x => x.Symbol.Equals(SymbolData.Symbol) && x.Market_Timestamp >= startDateVol).Select(x => x.Market_Timestamp).Distinct().ToArray());


                List<string> SelectedTimeStamps = new List<string>();

                List<string> SelectedTimeStampsVol = new List<string>();

                foreach (var item in _selectedMarketTimeStamps)
                {
                    SelectedTimeStamps.Add(item.ToShortDateString().ToString());
                };

                foreach (var item in _selectedMarketTimeStampsVol)
                {
                    SelectedTimeStampsVol.Add(item.ToShortDateString().ToString());
                };

                var _selectedCloseValues = _market_data_eod_List.Where(x => x.Symbol.Equals(SymbolData.Symbol)).Select(x => x.Close).ToArray();

                var _selectedHighValues = _market_data_eod_List.Where(x => x.Symbol.Equals(SymbolData.Symbol)).Select(x => x.High).ToArray();

                var _selectedLowValues = _market_data_eod_List.Where(x => x.Symbol.Equals(SymbolData.Symbol)).Select(x => x.Low).ToArray();

                var _selectedVolume = _market_data_eod_List.Where(x => x.Symbol.Equals(SymbolData.Symbol) && x.Market_Timestamp >= startDateVol).Select(x => x.AGG_Volume).ToArray();

                var dict2 = new Dictionary<string, string>();

                foreach (var item in _listOfSymbols)
                {
                    dict2.Add(item.Symbol, item.SymbolName);
                }

                Console.WriteLine(SymbolData.StartDate_IND);

                Console.WriteLine(SymbolData.Symbol);

                Console.WriteLine(startDate);
                // All ViewBags

                ViewBag.ListOfSymbolsDic = dict2;

                ViewBag.MarketDataEodList = _market_data_eod_List;

                ViewBag.SelectedVolume = Newtonsoft.Json.JsonConvert.SerializeObject(_selectedVolume);

                ViewBag.SelectedMarketTimeStamps = Newtonsoft.Json.JsonConvert.SerializeObject(SelectedTimeStamps);

                ViewBag.SelectedCloseValues = Newtonsoft.Json.JsonConvert.SerializeObject(_selectedCloseValues);

                ViewBag.SelectedHighValues = Newtonsoft.Json.JsonConvert.SerializeObject(_selectedHighValues);

                ViewBag.SelectedLowValues = Newtonsoft.Json.JsonConvert.SerializeObject(_selectedLowValues);

                ViewBag._selectedMarketTimeStampsVol = Newtonsoft.Json.JsonConvert.SerializeObject(SelectedTimeStampsVol);

            }
            return View();
        }



    }
}
