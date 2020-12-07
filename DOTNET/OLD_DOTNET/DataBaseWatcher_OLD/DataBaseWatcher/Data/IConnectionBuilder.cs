using System;
using System.Data.SqlClient;

namespace DataBaseWatcher.Data

{
    public interface IConnectionBuilder
    {
        abstract SqlConnection connectionToDatabase();
    }
}
