using System;
using System.Data.SqlClient;

namespace FlowController.Interfaces
{
    public interface IConnectionBuilder
    {
        abstract SqlConnection connectionToDatabase();
    }
}
