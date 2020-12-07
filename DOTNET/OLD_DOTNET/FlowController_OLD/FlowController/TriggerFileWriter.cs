using System.Collections.Generic;
using FlowController.Interfaces;
using FlowController.Model;
using FlowController.Data;
using System;
using System.IO;

namespace BoersenDatenService1.Interfaces
{
    public class TriggerFileWriter
    {
        readonly IConnectionBuilder connectionBuilder;

        readonly IDataFetcher datafetcher;

        string path = @"home/FlowControlDir";

        public TriggerFileWriter(IConnectionBuilder connectionBuilder, IDataFetcher datafetcher)
        {
            this.connectionBuilder = connectionBuilder;
            this.datafetcher = datafetcher;
        }

        public void PassParameters()
        {
            List<configuration_FlowControl> Liste = this.datafetcher.GetFlowData();

            if (Liste.Count != 0)
            {
                List<string> test = new List<string>();

                foreach (var item in Liste)
                {

                    test.Add(item.FlowID.ToString());

                    Directory.CreateDirectory(path + "/" + item.AppName);

                    File.AppendAllText(path + "/" + item.AppName + "/" + item.AppName + "_" + item.FlowID + ".txt", item.Parameter + "AnalysisId="+item.AnalysisId+";");

                }

                int SuccessEnd = this.datafetcher.UpdateFlowData(test);
            }
        }
    }
}
