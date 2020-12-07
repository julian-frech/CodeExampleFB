using System;
namespace FinanceBroGraphQL.ExceptionHandler
{
    public class DepotNotFoundException: Exception
    {
        public int DepotId { get; internal set; }
    }
}
