using System;
using System.ComponentModel.DataAnnotations;

namespace FinanceBroGraphQL.GraphQL
{
    /// <summary>
    /// Subset of UserDepotMainObject class necessary to create a new element.
    /// </summary>
    public class DeleteDepotInput
    {
        [Required]
        public int DepotId { get; set; } 
    }
}
