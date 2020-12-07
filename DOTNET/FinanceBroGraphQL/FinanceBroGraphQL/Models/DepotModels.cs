using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceBroGraphQL.Models
{
    [Table("V_MarketData", Schema = "MARKETDATA")]
    public class MarketData
    {
        public string symbol { get; set; }
        public DateTime MarketTimestamp { get; set; }
        public decimal? open { get; set; }
        public decimal? high { get; set; }
        public decimal? low { get; set; }
        public decimal? close { get; set; }
        public int? volume { get; set; }
    }

    [Table("F_Symbol_News", Schema = "MASTERDATA")]
    public class SymbolNews
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NewsId { get; set; }
        public int SymbolId { get; set; }
        [Required]
        public string symbol { get; set; }
        [Required]
        public string headline { get; set; }
        //public string source { get; set; }
        public string url { get; set; }
        public string summary { get; set; }
        public string related { get; set; }
        public string image { get; set; }
        public string language { get; set; }
        public string haspaywall { get; set; }
        [Required]
        public DateTime articledatetime { get; set; }
        public string comment { get; set; }


    }

    [Table("D_SYMBOL", Schema = "MASTERDATA")]
    public class SymbolObj
    {
        public int symbolId { get; set; }
        [Key][Required]
        public string Symbol { get; set; }
    }


    [Table("V_SYMBOL", Schema = "MASTERDATA")]
    public class SymbolFacts
    {
        public int symbolId { get; set; }
        [Key]
        [Required]
        public string Symbol { get; set; }
        public string CompanyName { get; set; }
        public string Industry { get; set; }
        public string Sector { get; set; }
        public string Website { get; set; }
        public string Description { get; set; }
        public string CEO { get; set; }
        public string SecurityName { get; set; }
        public string country { get; set; }
    }

    [Table("V_MarketDataLatestDate", Schema = "MARKETDATA")]
    public class LatestDate
    {
        [Key]
        [Required]
        public string symbol { get; set; }
        public string latestDate { get; set; }
    }


    [Table("AspNetUsers", Schema = "dbo")]
    public class UserAccount
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
    }


    [Table("D_UserDepot", Schema = "dbo")]
    public class UserDepotMainObject
    {

        public UserDepotMainObject() { }

        public UserDepotMainObject(string _userHK)
        {
            this.UserHK = _userHK;
            this.ValidFrom = DateTime.Now;
            this.ValidTo = DateTime.Parse("9999-12-31 00:00:00.000");
            this.ITS = DateTime.Now;
            this.UTS = DateTime.Now;
        }


        public UserDepotMainObject(string _UserHK,DateTime _ValidFrom, DateTime _ValidTo, DateTime _ITS, DateTime _UTS)
        {
            this.UserHK = _UserHK;
            this.ValidFrom = _ValidFrom;
            this.ValidTo = _ValidTo;
            this.ITS = _ITS;
            this.UTS = _UTS;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DepotId { get; set; }
        public string UserHK { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public DateTime ITS { get; set; }
        public DateTime UTS { get; set; }
    }

    [Table("F_UserDepot", Schema = "dbo")]
    public class UserDepotF
    {
        public UserDepotF() { }

        public UserDepotF(int _depotId, string _userHK, string _depotName)
        {
            this.DepotId = _depotId;
            this.UserHK = _userHK;
            this.DepotName = _depotName;
            this.ValidFrom = DateTime.Now;
            this.ValidTo = DateTime.Parse("9999-12-31 00:00:00.000");
            this.ITS = DateTime.Now;
            this.UTS = DateTime.Now;
        }

        public UserDepotF(int _depotId, string _userHK, string _depotName, DateTime _ValidFrom, DateTime _ValidTo, DateTime _ITS, DateTime _UTS)
        {
            this.DepotId = _depotId;
            this.UserHK = _userHK;
            this.DepotName = _depotName;
            this.ValidFrom = _ValidFrom;
            this.ValidTo = _ValidTo;
            this.ITS = _ITS;
            this.UTS = _UTS;
        }

        [Key]
        public int DepotId { get; }
        public string UserHK { get; set; }
        [Required]
        [Range(3, 100, ErrorMessage = "Name between 3 and 100 letters.")]
        public string DepotName { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public DateTime ITS { get; set; }
        public DateTime UTS { get; set; }
    }

    [Table("F_DepotComponents", Schema = "dbo")]
    public class UserDepotComponents
    {
        public UserDepotComponents() { }

        public UserDepotComponents(int _depotId, string _userHK, string _symbol, int _quantity, decimal _marketValue, DateTime _validFrom, DateTime _validTo)
        {
            this.DepotId = _depotId;
            this.UserHK = _userHK;
            this.Symbol = _symbol;
            this.Quantity = _quantity;
            this.MarketValue = _marketValue;
            this.ValidFrom = _validFrom;
            this.ValidTo = _validTo;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DepotComponentId { get;  }
        public int DepotId { get; set; }
        public string UserHK { get; set; }
        [Required]
        public string Symbol { get; set; }
        [Required]
        [Range(1, 100000, ErrorMessage = "Choose any amount greater even 1.")]
        public int Quantity { get; set; }
        [Required]
        [Range(1, 100000, ErrorMessage = "Accommodation invalid (1-100000).")]
        public decimal MarketValue { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
    }


    [Table("V_UserDepot", Schema = "MASTERDATA")]
    public class UserDepotsView
    {
        [Key]
        public int DepotId { get; }
        public string UserName { get; set; }
        [Key]
        public string Symbol { get; set; }
        public string Company { get; set; }
        public int Quantity { get; set; }
        public string DepotName { get; set; }
        public string UserHK { get; set; }
        public string UserDepotHK { get; set; }
        [Key]
        public DateTime ComponentValidFrom { get; set; }
        public DateTime ComponentValidTo { get; set; }
        public DateTime DepotValidFrom { get; set; }
        public DateTime DepotValidTo { get; set; }
    }

}
