using System;
using System.Data.SqlClient;

namespace BoersenDatenService2.Services
{
    public interface IErrorService
    {
        abstract void ErrorHandler(int FlowId, int ErrorCode);
    }
    
}
