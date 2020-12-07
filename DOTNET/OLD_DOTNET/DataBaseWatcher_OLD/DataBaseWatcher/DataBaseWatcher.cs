using System;
using System.Collections.Generic;
using DataBaseWatcher.Data;
using DataBaseWatcher.Model;
using DataBaseWatcher.Services;

namespace DataBaseWatcher
{
    public class DataBaseWatcher
    {
        readonly IDataFetcher dataFetcher;
        readonly IConnectionBuilder connectionBuilder;

        public DataBaseWatcher(IDataFetcher dataFetcher, IConnectionBuilder connectionBuilder)
        {
            this.connectionBuilder = connectionBuilder;
            this.dataFetcher = dataFetcher;
        }

        public void PassParameters()
        {
            List<string> Liste = this.dataFetcher.GetFlowData();

            foreach (var item in Liste)
            {
                Console.WriteLine(item);
            }


        }
    }
}
