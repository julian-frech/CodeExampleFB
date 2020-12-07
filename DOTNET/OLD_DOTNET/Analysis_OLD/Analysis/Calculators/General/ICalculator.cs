using System;
using System.Collections.Generic;
using System.Text;
using Analysis.Core.Stocks;
using Analysis.Core.Time;

namespace Analysis.Calculators
{

    /// <summary>
    /// Allgemeines Interface für Berechnungsalgorithmen des Analyse-Tools, die Aktienverläufe analysieren.
    /// </summary>
    public interface ICalculator<T>
    {
        /// <summary>
        /// Führt konkrete Berechnungen auf dem zugewiesenen Aktienverlauf durch. 
        /// </summary>
        public List<ValueTimeAware<T>> Calculate(Stock stock);

        /// <summary>
        /// Beschreibt alle Abhängigkeiten des Kalkulators innerhalb eines Aktienverlaufs.
        /// Diese Methode wird für das Cachen benötigt, um sagen zu können, wann eine Berechnung auf aktualisiertem
        /// Stand neue Berechnungen liefern könnte.
        /// </summary>
        public List<Dependency> GetDependencies(); 
    }
}
