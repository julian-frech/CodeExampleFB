using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceBro.Models
{
    public class ApiTrigger
    {
        public string JsonBody { get; set; }

        public ApiTrigger()
        {
        }
    }


}
