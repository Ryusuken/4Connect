using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VierGewinntInterfaceLibrary
{
    public enum EPlayerDifficulty
    {
        /// <summary>
        /// Setzt zufällig einen gültigen Zug, es sei denn er gewinnt man kann mit einem anderen Zug in der aktuellen Runde gewinnen oder den Gegenspieler am Gewinnen hindern
        /// </summary>
        EASY,
        /// <summary>
        /// Prüft den sinnvollsten Zug auf Basis einer kurzen Vorausberechnung
        /// </summary>
        MEDIUM,
        /// <summary>
        /// Spielt ein nahezu perfektes Spiel, so dass ein Sieg gegen diesen Spieler nahezu unmöglich ist
        /// </summary>
        HARD
    }
}
