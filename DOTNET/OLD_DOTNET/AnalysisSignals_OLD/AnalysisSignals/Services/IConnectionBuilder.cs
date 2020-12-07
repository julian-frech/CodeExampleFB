using System;
using System.Data.SqlClient;

namespace AnalysisSignals.Services
{
    public interface IConnectionBuilder
    {
        abstract SqlConnection connectionToDatabase();
    }
}
