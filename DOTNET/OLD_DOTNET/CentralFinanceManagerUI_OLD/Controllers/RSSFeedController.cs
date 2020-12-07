using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using CentralFinanceManagerUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CentralFinanceManagerUI.Controllers
{
    public class RSSFeedController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string RSSURL= "http://www.tagesschau.de/xml/rss2")
        {
            WebClient wclient = new WebClient();

            string RSSData;

            if (RSSURL is null)
            {
               RSSData = wclient.DownloadString("http://www.tagesschau.de/xml/rss2");
            }
            else
            {

               RSSData = wclient.DownloadString(RSSURL);

            }
            XDocument xml = XDocument.Parse(RSSData);
            var RSSFeedData = (from x in xml.Descendants("item")
                               select new RSSFeed
                               {
                                   Title = ((string)x.Element("title")),
                                   Link = ((string)x.Element("link")),
                                   Description = ((string)x.Element("description")),
                                   PubDate = ((string)x.Element("pubDate"))
                               });
            ViewBag.RSSFeed = RSSFeedData;
            ViewBag.URL = RSSURL;
            return View();
        }


    }
}
