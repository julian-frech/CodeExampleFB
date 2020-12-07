using FlowController.Interfaces;
using FlowController.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace FlowController.Interfaces
{

    public interface IDataFetcher
    {
        abstract List<configuration_FlowControl> GetFlowData();

        abstract int UpdateFlowData(List<string> FlowIDs);
    }
}
