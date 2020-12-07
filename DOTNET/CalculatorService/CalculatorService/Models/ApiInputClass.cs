using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CalculatorService.Models
{
    public class ApiInputClass
    {
        [Required]
        public string Method { get; set; }
        [Required]
        public string Symbol { get; set; }
        [Required]
        public int Interval { get; set; }
        [DefaultValue(true)]
        public decimal? Weighting { get; set; } = 1;

        public ApiInputClass() { }

        public ApiInputClass(string _method, string _symbol, int _interval, decimal? _weighting)
        {
            this.Method = _method;
            this.Symbol = _symbol;
            this.Interval = _interval;
            this.Weighting = (_weighting is null)? 1:_weighting;
        }

    }
}
