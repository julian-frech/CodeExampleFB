using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AnalysisSignals.AnalysisDiffMethods;
using AnalysisSignals.Services;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex;
using Ninject;

namespace AnalysisSignals.Calculations
{

    public class AnalysisMethodss //: IAnalysisMethod
    {
        readonly IAnalysisMethod analysisMethod;

        public AnalysisMethodss([Named("SimpleMovingAverage")] IAnalysisMethod Average)
        {
            analysisMethod = Average;
        }

        /*public async Task<List<decimal>> DoMethod()
        {
            List<decimal> Dummy = await analysisMethod.DoMethod();

            return Dummy;
        }*/
    }


    /*
        public class AnalysisMethods: IAnalysisMethod
    {
        readonly IAnalysisMethod analysisMethod;

        public AnalysisMethods([Named("SimpleMovingAverage")] IAnalysisMethod Average)
        {
            analysisMethod = Average;
        }

        public async Task <List<decimal>> DoMethod()
        {
            List<decimal> Dummy = await analysisMethod.DoMethod();

            return Dummy;
        }


    };*/



}
