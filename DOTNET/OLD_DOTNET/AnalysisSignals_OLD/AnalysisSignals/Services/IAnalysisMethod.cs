using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AnalysisSignals.Model;

namespace AnalysisSignals.Services
{
    public interface IAnalysisMethod
    {

        Task<List<AnalysisData>> DoMethod(Dictionary<string,string> dictionary);

    }
}
