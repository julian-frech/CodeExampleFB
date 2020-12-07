
using Analysis.Calculators;
using Analysis.Core.Exceptions;
using System;
using System.Collections.Generic;

namespace Analysis.Core.Stocks
{
    /// <summary>
    /// Bean für Aktienverläufe
    /// </summary>
    public class Stock
    {
        //Bezeichner muss bei Initialisierung festgelegt werden.
        public string Symbol { get; }

        //Handelstage
        public LinkedList<StockTradingValue> TradingDays { get; }

        //Verlaufswerte
        public LinkedList<StockTradingValue> TradingValues { get; }

        /// <summary>
        /// Erzeugt ein Datenobjekt für einen Aktienverlauf. Wird einmal mittels String an ein gleichnamiges Symbol gebunden.
        /// </summary>
        public Stock(String symbol)
        {
            this.Symbol = symbol;
            this.TradingDays = new LinkedList<StockTradingValue>();
            this.TradingValues = new LinkedList<StockTradingValue>();
        }

        /// <summary>
        /// Fügt neue Verlaufswerte hinzu. Wirft eine Exception falls ein Wert nicht eindeutig eingetaktet werden kann.
        /// </summary>
        public void Update(List<StockTradingValue> stockTradingValues)
        {
            foreach(StockTradingValue value in stockTradingValues)
            {
                Update(value);
            }
        }

        /// <summary>
        /// Fügt einen neuen Verlaufswert hinzu. Wirft eine Exception falls ein Wert nicht eindeutig eingetaktet werden kann.
        /// </summary>
        public void Update(StockTradingValue stockTradingValue)
        {
            if (!stockTradingValue.Symbol.Equals(this.Symbol))
            {
                throw new IllegalSymbolException(this.Symbol, stockTradingValue.Symbol);
            }

            if (stockTradingValue.IsTradingDay)
            {
                UpdateList(TradingDays, stockTradingValue);
            }
            else
            {
                UpdateList(TradingValues, stockTradingValue);
            }
        }

        private void UpdateList(LinkedList<StockTradingValue> valueList , StockTradingValue stockTradingValue)
        {
            DateTime time = stockTradingValue.Timestamp;

            //Bei leerer Liste kann der neue Wert direkt gesetzt werden
            if (valueList.Count == 0)
            {
                valueList.AddFirst(stockTradingValue);
                return;
            }

            //Platziert den neuen Wert an der korrekten Stelle in der geordneten LinkedList
            LinkedListNode<StockTradingValue> currentTradingDay = valueList.First;
            LinkedListNode<StockTradingValue> lastTradingDay = valueList.Last;
            while (time.CompareTo(currentTradingDay.Value.Timestamp) >= 0)
            {
                if (time.CompareTo(currentTradingDay.Value.Timestamp) == 0)
                {
                    if (stockTradingValue.Equals(currentTradingDay.Value))
                    {
                        return;
                    }
                    throw new ArgumentException(String.Format("Received multiple stock values with different data and timestamp {0}", time));
                }
                if (currentTradingDay == lastTradingDay)
                {
                    valueList.AddLast(stockTradingValue);
                    return;
                }
                currentTradingDay = currentTradingDay.Next;
            }
            valueList.AddBefore(currentTradingDay, stockTradingValue);
        }           
    }
}
