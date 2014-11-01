using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VierGewinntInterfaceLibrary
{
    /// <summary>
    /// Stellt ein 7x6 Felder großes Spielfeld bereit
    /// </summary>
    public abstract class APlayground
    {

        /// <summary>
        /// Stellt das Spielfeld in der aktuellen Konstellation dar. Verwendet das Enum EPlaygroundFieldStates, um den aktuellen Feldstatus zu definieren
        /// </summary>
        public EPlaygroundFieldStates[,] Playground = new EPlaygroundFieldStates[7, 6];

        /// <summary>
        /// Stellt den eigene Spielstein dar
        /// </summary>
        protected readonly EPlaygroundFieldStates OwnCoin;

        /// <summary>
        /// Stellt den gegnerischen Spielstein dar
        /// </summary>
        protected readonly EPlaygroundFieldStates OpponentCoin;

        /// <summary>
        /// Erzeugt eine Instanz des Spielfeldes und legt den eigenen Spielstein fest
        /// </summary>
        /// <param name="OwnCoin">Der eigene Spielstein</param>
        public APlayground(EPlaygroundFieldStates OwnCoin)
        {
            if (OwnCoin == EPlaygroundFieldStates.EMPTY)
                throw new InvalidOperationException("Du musst dir einen gültigen Spielstein zuweisen!");

            this.OwnCoin = OwnCoin;
            this.OpponentCoin = OwnCoin == EPlaygroundFieldStates.PLAYER1 ? EPlaygroundFieldStates.PLAYER2 : EPlaygroundFieldStates.PLAYER1;
        }

        /// <summary>
        /// Liefert den Spielstein zu einem Spieler zurück
        /// </summary>
        /// <param name="Player">EPlayers - Der Spieler, dessen Spielstein gesucht wird</param>
        /// <returns>EPlaygroundFieldStates - Der Spielstein des Spielers</returns>
        protected EPlaygroundFieldStates GetCoinOfPlayer(EPlayers Player)
        {
            return (Player == EPlayers.SELF) ? this.OwnCoin : this.OpponentCoin;
        }

        /// <summary>
        /// Liefert den Spieler zu einem Spielstein zurück
        /// </summary>
        /// <param name="Coin">EPlaygroundFieldStates - Der Spielstein, dessen Spieler gesucht wird</param>
        /// <returns>EPlayers - Der Spieler des Spielsteins</returns>
        protected EPlayers GetPlayerOfCoinr(EPlaygroundFieldStates Coin)
        {
            return (Coin == this.OwnCoin) ? EPlayers.SELF : EPlayers.OPPONENT;
        }

        /// <summary>
        /// Initialisiert das Spielfeld. Setzt alle Felder auf "Empty"
        /// </summary>
        public void InitPlayground()
        {
            for (int x = 0; x < this.Playground.GetLength(0); x++)
                for (int y = 0; y < this.Playground.GetLength(1); y++)
                    this.Playground[x, y] = EPlaygroundFieldStates.EMPTY;
        }

        /// <summary>
        /// Setzt einen Spielstein auf das Spielfeld
        /// </summary>
        /// <param name="Point">Der IPoint mit X/Y Koordinate</param>
        /// <param name="State">Der Spielstein, der gesetzt werden soll</param>
        public abstract void SetCoinToPoint(MovePoint Point, EPlaygroundFieldStates State);

        /// <summary>
        /// Liefert den Status des übergebenen Feldes
        /// </summary>
        /// <param name="Point"></param>
        /// <returns>EPlaygroundFieldStates - den aktuellen Feldstatus des übergebenen Koordinatenpunktes</returns>
        public EPlaygroundFieldStates GetFieldState(MovePoint Point)
        {
            if (!Point.IsValidPoint())
                throw new IndexOutOfRangeException("Der gewählte Punkt liegt nicht im Spielfeld!");

            return this.Playground[Point.X, Point.Y];
        }
    }
}
