using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralFinanceManagerUI.Models.UserDepots
{
    [Table("F_UserDepot", Schema = "dbo")]
    public class UserDepot
    {
        [Key, Column(Order = 0)] public int? DepotId { get; set; }

        //public string UserDepotHK { get; set; }

        public string UserHK { get; set; }

        public string DepotName { get; set; }

        public DateTime? ValidFrom { get; set; }

        public DateTime? ValidTo { get; set; }

        [NotMapped]
        public string UserName { get; set; }
    }

    [Table("D_UserDepot", Schema = "dbo")]
    public class UserDepotObj
    {
        [Key, Column(Order = 0)] public int? DepotId { get; set; }

        public string UserHK { get; set; }

    }

    [Table("F_DepotComponents", Schema = "dbo")]
    public class DepotComponents
    {
        [Key, Column(Order = 0)] public int? DepotComponentId { get; set; }

        public int? DepotId { get; set; }

        public int? Quantity { get; set; }

        public string Symbol { get; set; }

        public decimal? MarketValue { get; set; }

        public string USerHK { get; set; }

        public DateTime? ValidFrom { get; set; }

        public DateTime? ValidTo { get; set; }

    }


    public class UserDepotViewModel
    {
        public IEnumerable<UserDepot> UserDepots { get; set; }
        public IEnumerable<V_DepotComponentsAggregated> V_DepotComponentsAggregateds { get; set; }
    }

}
