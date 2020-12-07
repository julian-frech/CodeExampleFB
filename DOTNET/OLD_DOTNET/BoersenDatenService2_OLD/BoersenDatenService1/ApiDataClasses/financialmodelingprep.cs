using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace BoersenDatenService2.ApiDataClasses
{
    public class financialmodelingprep
    {
        public string Symbol { get; set; }
        public Profile Profile { get; set; }
    }

    public partial class Profile
    {
        public decimal? Price { get; set; }

        public string Beta { get; set; }

        public decimal? VolAvg { get; set; }

        public string MktCap { get; set; }

        public decimal? LastDiv { get; set; }

        public string Range { get; set; }

        public decimal? Changes { get; set; }

        public string ChangesPercentage { get; set; }

        public string CompanyName { get; set; }

        public string Exchange { get; set; }

        public string Industry { get; set; }

        public string Website { get; set; }

        public string Description { get; set; }

        public string Ceo { get; set; }

        public string Sector { get; set; }

        public string Image { get; set; }
    }





}
