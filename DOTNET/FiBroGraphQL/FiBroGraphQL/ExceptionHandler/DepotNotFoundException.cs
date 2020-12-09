using System;

namespace FiBroGraphQL.ExceptionHandler
{
    public class DepotNotFoundException : Exception
    {
        public int UseDepotId { get; internal set; }
    }
}
