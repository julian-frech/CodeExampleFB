using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CentralFinanceManagerUI.Models
{
    [Table("F_MARKET_DATA", Schema = "production")]

    public class F_MARKET_DATA
    {

        [Key, Column(Order = 0)] public int ColumnID { get; set; }
        public string Symbol { get; set; }

        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public string Name { get; set; }
        [Display(Name = "Traded Volume")]
        public int AGG_Volume { get; set; }

        [Display(Name = "Trading Day")]
        [DataType(DataType.DateTime)]
        public DateTime Market_Timestamp { get; set; }


        [NotMapped]
        public int StartDate_IND { get; set; }

    }
}
