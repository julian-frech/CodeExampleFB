using System;
using System.ComponentModel.DataAnnotations;

namespace FinanceBroGraphQL.GraphQL
{
    /// <summary>
    /// Subset of UserDepotMainObject necessary to create a new depot
    /// </summary>
    public class CreateDepotInput
    {
        [Required]
        public string DepotName { get; set; }
        [Required]
        public string UserName { get; set; }

    }
}
