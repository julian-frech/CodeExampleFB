
using Analysis.Core.Stocks;
using Analysis.Core.Time;
using System;
using System.Collections.Generic;
using System.Text;

namespace Analysis.Calculators
{
    /// <summary>
    /// Abhängigkeiten eines Kalkulators innerhalb eines Aktienverlaufs.
    /// </summary>
    public enum Dependency
    {
        //Tageswerte
        DayHigh,
        DayLow,
        DayClose,
        DayOpen,
        DayVolume,
        
        //Verlaufswerte
        High,
        Low,
        Close,
        Open,
        Volume
    }

    /// <summary>
    /// Methoden für die Enumeration TimeInterval
    /// </summary>
    static class DependencyMethods
    {

        /// <summary>
        /// Antwortet die relevanten Daten eines Aktienverlaufs mit zugehörigem Zeitstempel
        /// </summary>
        public static List<ValueTimeAware<decimal>> GetRelevantData(this Dependency dependency, Stock stock)
        {
            List<ValueTimeAware<decimal>> relevantData = new List<ValueTimeAware<decimal>>();
            LinkedListNode<StockTradingValue> currentNode;
            Func<LinkedListNode<StockTradingValue>, ValueTimeAware<decimal>> function;

            switch (dependency)
            {
                case Dependency.DayHigh:
                    currentNode = stock.TradingDays.First;
                    function = (node) => new ValueTimeAware<decimal>(node.Value.High, node.Value.Timestamp);
                    break;

                case Dependency.DayLow:
                    currentNode = stock.TradingDays.First;
                    function = (node) => new ValueTimeAware<decimal>(node.Value.Low, node.Value.Timestamp);
                    break;

                case Dependency.DayClose:
                    currentNode = stock.TradingDays.First;
                    function = (node) => new ValueTimeAware<decimal>(node.Value.Close, node.Value.Timestamp);
                    break;

                case Dependency.DayOpen:
                    currentNode = stock.TradingDays.First;
                    function = (node) => new ValueTimeAware<decimal>(node.Value.Open, node.Value.Timestamp);
                    break;

                case Dependency.DayVolume:
                    currentNode = stock.TradingDays.First;
                    function = (node) => new ValueTimeAware<decimal>(node.Value.Volume, node.Value.Timestamp);
                    break;

                case Dependency.High:
                    currentNode = stock.TradingValues.First;
                    function = (node) => new ValueTimeAware<decimal>(node.Value.High, node.Value.Timestamp);
                    break;

                case Dependency.Low:
                    currentNode = stock.TradingValues.First;
                    function = (node) => new ValueTimeAware<decimal>(node.Value.Low, node.Value.Timestamp);
                    break;

                case Dependency.Close:
                    currentNode = stock.TradingValues.First;
                    function = (node) => new ValueTimeAware<decimal>(node.Value.Close, node.Value.Timestamp);
                    break;

                case Dependency.Open:
                    currentNode = stock.TradingValues.First;
                    function = (node) => new ValueTimeAware<decimal>(node.Value.Open, node.Value.Timestamp);
                    break;

                case Dependency.Volume:
                    currentNode = stock.TradingValues.First;
                    function = (node) => new ValueTimeAware<decimal>(node.Value.Volume, node.Value.Timestamp);
                    break;

                default:
                    throw new ArgumentException(String.Format("Could not find the relevant data of dependency {0}.", dependency));
            }

            while (currentNode != null)
            {
                relevantData.Add(function.Invoke(currentNode));
                currentNode = currentNode.Next;
            }
            return relevantData;

        }
    }
}
