using Analysis.Core.Stocks;
using Analysis.Core.Time;
using System;
using System.Collections.Generic;
using System.Text;

namespace Analysis.Data.DataInterpolation
{
    /// <summary>
    /// Lineare interpolation des Aktienkurses und des gehandelten Volumens zwischen zwei Verlaufsdatenpunkten.
    /// </summary>
    public class LinearDataInterpolation : IDataInterpolation
    {
        public List<StockTradingValue> Interpolate(StockTradingValue first, StockTradingValue last)
        {
            //Validiere
            if (first.IsTradingDay != last.IsTradingDay)
            {
                throw new ArgumentException("Between Daily and minutely data cant be interpolated");
            }

            List<StockTradingValue> missingDatapoints = new List<StockTradingValue>();
            DateTime startTime = first.Timestamp;
            DateTime lastTime = last.Timestamp;
            TimeInterval interval = first.IsTradingDay ? TimeInterval.DAILY : TimeInterval.MINUTELY;
            TimeGrid timeGrid = new TimeGrid(startTime, lastTime, interval);

            string symbol = first.Symbol;
            bool isTRadingDay = first.IsTradingDay;
            decimal firstOpen = first.Open;
            decimal lastOpen = last.Open;
            decimal firstClose = first.Close;
            decimal lastClose = last.Close;
            long firstVolume = first.Volume;
            long lastVolume = last.Volume;

            decimal gradientClose = (lastClose - firstClose) / (timeGrid.Times.Count);
            decimal gradientOpen = (lastOpen - firstOpen) / (timeGrid.Times.Count);
            decimal gradientVolume = (lastVolume - firstVolume) / (timeGrid.Times.Count);

            for (int i = 1; i< timeGrid.Times.Count-1; i++)
            {
                DateTime nextTime = timeGrid.Times[i];
                decimal open = firstOpen + (i * gradientOpen);
                decimal close = firstClose + (i* gradientClose);
                decimal high = Math.Max(open, close);
                decimal low = Math.Min (open, close);
                long volume = (long)(firstVolume + (i* gradientVolume));
                StockTradingValue nextValue = new StockTradingValue(symbol, nextTime, isTRadingDay, open, high, low, close, volume);
                missingDatapoints.Add(nextValue);
            }

            return missingDatapoints;
        }
    }
}
