using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using AnalysisSignals.Model;

namespace AnalysisSignals.Services
{

    public interface IDataFetcher
    {
        public Task<DataTable> ReadFromDatabase(string SqlStatement);

        public Task BulkMerge(string TargetTable, List<AnalysisData> analysisDatas);
    }
}
