using System;
using System.ComponentModel.DataAnnotations;

namespace FiBroGraphQL.GraphQL
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
