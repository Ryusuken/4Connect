using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VierGewinntInterfaceLibrary
{
    /// <summary>
    /// Dieses Interface stallt Methodenkonstrukte bereit, die für die Interaktion mit der UI benötigt werden. Die Standardmäßige Schwierigkeit soll Medium sein.
    /// </summary>
    public interface IPlayer
    {
        /// <summary>
        /// Setzt die Spielerschwierigkeit für diese Runde des Spiels.
        /// </summary>
        /// <param name="Difficulty">EPlayerDiffuculty - Spielerschwierigkeit</param>
        void SetDifficutly(EPlayerDifficulty Difficulty);

        /// <summary>
        /// Liefert die aktuelle Spielerschwierigkeit zurück
        /// </summary>
        /// <returns>EPlayerDifficulty - Aktuelle Spielschwierigkeit</returns>
        EPlayerDifficulty GetDifficulty();

        /// <summary>
        /// Fordert den nächsten Schritt des Spielers an, wenn dieser an der Reihe ist
        /// </summary>
        /// <returns>AInteraction des nächsten Spielzuges</returns>
        AInteraction GetNextMove();

        /// <summary>
        /// Übernimmt den aktuellen Zug des Gegenspielers
        /// </summary>
        /// <param name="Interaction">AInteraction des Gegenspielers</param>
        /// <returns>Bool - Ob der Zug erlaubt ist oder nicht</returns>
        bool SetOppositePlayerMove(AInteraction Interaction);

        /// <summary>
        /// Liefert den Namen des Spielers
        /// </summary>
        /// <returns>String - Name des Spielers</returns>
        string GetPlayerName();

        /// <summary>
        /// Gibt an, ob das Spiel mit dem letzten Zug geendet hat
        /// </summary>
        /// <returns>Bool - True, wenn das Spiel beendet ist, ansonsten false</returns>
        bool HasGameEnded(out EPlayerWinStates Winner);

        /// <summary>
        /// Gibt die Meldung weiter, dass die Gegenseite das Spiel als Gewonnen gemeldet hat
        /// </summary>
        /// <returns>Bool - Ob die Meldung korrekt ist oder nicht</returns>
        bool YouHaveLost();

        /// <summary>
        /// Gibt die Meldung weiter, dass die Gegenseite das Spiel als Verloren gemeldet hat
        /// </summary>
        /// <returns>Bool - Ob die Meldung korrekt ist oder nicht</returns>
        bool YouHaveWon();

        /// <summary>
        /// Gibt die Meldung weiter, dass die Gegenseite das Spiel als Unentschieden gemeldet hat
        /// </summary>
        /// <returns>Bool - Ob die Meldung korrekt ist oder nicht</returns>
        bool GameDraw();

    }
}
