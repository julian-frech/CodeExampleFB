using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CentralFinanceManagerUI.Models.SymbolViewModels
{
    [Table("V_SymbolNames", Schema = "ANALYSIS")]

    public class Symbols
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Symbol { get; set; }
        [Display(Name = "Company")]
        public string SymbolName { get; set; }

    }
}
