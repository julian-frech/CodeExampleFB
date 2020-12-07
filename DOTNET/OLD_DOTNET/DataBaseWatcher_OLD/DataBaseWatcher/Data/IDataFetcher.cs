using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using DataBaseWatcher.Model;

namespace DataBaseWatcher.Data
{

    public interface IDataFetcher
    {
        public List<string> GetFlowData();
    }

}
