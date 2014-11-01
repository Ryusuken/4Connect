using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VierGewinntInterfaceLibrary;

namespace KI
{
    public abstract class AbstractKI : IPlayer
    {
        EPlayerDifficulty difficulty;
        public MovePoint move = new MovePoint();
        public Playground playgr = new Playground(EPlaygroundFieldStates.PLAYER1);

        public AbstractKI()
        {

        }


        /// <summary>
        /// Setzt die Spielerschwierigkeit für diese Runde des Spiels.
        /// </summary>
        /// <param name="Difficulty">EPlayerDiffuculty - Spielerschwierigkeit</param>
        public void SetDifficutly(EPlayerDifficulty Difficulty)
        {
            difficulty = Difficulty;
        }

        /// <summary>
        /// Liefert die aktuelle Spielerschwierigkeit zurück
        /// </summary>
        /// <returns>EPlayerDifficulty - Aktuelle Spielschwierigkeit</returns>
        public EPlayerDifficulty GetDifficulty()
        {
            return difficulty;
        }

        /// <summary>
        /// Fordert den nächsten Schritt des Spielers an, wenn dieser an der Reihe ist
        /// </summary>
        /// <returns>AInteraction des nächsten Spielzuges</returns>
        public abstract AInteraction GetNextMove();

        /// <summary>
        /// Übernimmt den aktuellen Zug des Gegenspielers
        /// </summary>
        /// <param name="Interaction">AInteraction des Gegenspielers</param>
        /// <returns>Bool - Ob der Zug erlaubt ist oder nicht</returns>
        public bool SetOppositePlayerMove(AInteraction Interaction)
        {
            if (Interaction.Player == EPlayers.OPPONENT)
            {
                if (Interaction.Point.IsValidPoint() && playgr.GetFieldState(Interaction.Point) == EPlaygroundFieldStates.EMPTY && compareSlot(Interaction.Point))
                {
                    playgr.SetCoinToPoint(Interaction.Point, EPlaygroundFieldStates.PLAYER2);
                    move = Interaction.Point;
                }
                else
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Liefert den Namen des Spielers
        /// </summary>
        /// <returns>String - Name des Spielers</returns>
        public string GetPlayerName()
        {
            return "Player1";
        }

        /// <summary>
        /// Gibt an, ob das Spiel mit dem letzten Zug geendet hat
        /// </summary>
        /// <returns>Bool - True, wenn das Spiel beendet ist, ansonsten false</returns>
        public bool HasGameEnded(out EPlayerWinStates Winner)
        {
            if (verticalcheck(move) == 4)
            {
                Winner = EPlayerWinStates.SELF;
                return true;
            }
            else
                if (horizontalcheck(move) == 4)
                {
                    Winner = EPlayerWinStates.SELF;
                    return true;
                }
                else
                    if (diagonalcheckdown(move) == 4)
                    {
                        Winner = EPlayerWinStates.SELF;
                        return true;
                    }
                    else
                        if (diagonalcheckhigh(move) == 4)
                        {
                            Winner = EPlayerWinStates.SELF;
                            return true;
                        }
            Winner = EPlayerWinStates.NO_ONE;
            return false;
        }

        /// <summary>
        /// Gibt die Meldung weiter, dass die Gegenseite das Spiel als Gewonnen gemeldet hat
        /// </summary>
        /// <returns>Bool - Ob die Meldung korrekt ist oder nicht</returns>
        public bool YouHaveLost()
        {
            return false;
        }

        /// <summary>
        /// Gibt die Meldung weiter, dass die Gegenseite das Spiel als Verloren gemeldet hat
        /// </summary>
        /// <returns>Bool - Ob die Meldung korrekt ist oder nicht</returns>
        public bool YouHaveWon()
        {
            return true;
        }

        /// <summary>
        /// Gibt die Meldung weiter, dass die Gegenseite das Spiel als Unentschieden gemeldet hat
        /// </summary>
        /// <returns>Bool - Ob die Meldung korrekt ist oder nicht</returns>
        public bool GameDraw()
        {
            return true;
        }

        /// <summary>
        /// Dient zum übergeben des Spielfeldes
        /// </summary>
        /// <returns>Das aktuelle Spielfeld</returns>
        public EPlaygroundFieldStates[,] getfield()
        {
            return playgr.Playground;
        }

        /// <summary>
        /// Überprüft wo der Stein landet beim einwerfen
        /// </summary>
        /// <param name="point">Spielzug des Spielers</param>
        /// <returns>Wie weit der Stein reinfallen muss</returns>
        public MovePoint checkslot(MovePoint point)
        {
            MovePoint check;
            check = point;
            check.Y = 5;
            while (playgr.GetFieldState(check) != EPlaygroundFieldStates.EMPTY)
                check.Y--;
            return check;
        }

        /// <summary>
        /// Überprüft ob stein gesetzt werden kann
        /// </summary>
        /// <param name="point">Spielzug des Gegenspielers</param>
        /// <returns>True oder false ob stein gesetzt werden kann</returns>
        public bool compareSlot(MovePoint point)
        {
            int x = point.X;
            int y = point.Y;
            MovePoint check = new MovePoint(x, y);
            check.Y = 5;
            while (playgr.GetFieldState(check) != EPlaygroundFieldStates.EMPTY)
                check.Y--;
            if (check.X == point.X && check.Y == point.Y)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Überprüft ob nach Ausgeführten zug eine Vertikale viererkette entstanden ist
        /// </summary>
        /// <param name="point">Spielzug</param>
        /// <returns>Anzahl der Spielsteine</returns>
        public int verticalcheck(MovePoint point)
        {
            int x = point.X;
            int y = point.Y;
            MovePoint check = new MovePoint(x, y);
            int i, j;
            EPlaygroundFieldStates current = playgr.GetFieldState(check);
            int count = 0;
            for (i = check.Y; i >= 0; i--)
            {
                check.Y = i;
                if (playgr.GetFieldState(check) == current)
                    count++;
                else
                    break;
            }
            for (j = y + 1; j <= 5; j++)
            {
                if (j <= 5)
                {
                    check.Y = j;
                    if (playgr.GetFieldState(check) == current)
                        count++;
                    else
                        break;
                }
            }
            return count;
        }

        /// <summary>
        /// Überprüft ob nach Ausgeführten zug eine Horizontale viererkette entstanden ist
        /// </summary>
        /// <param name="point">Spielzug</param>
        /// <returns>Anzahl der Spielsteine</returns>
        public int horizontalcheck(MovePoint point)
        {
            int x = point.X;
            int y = point.Y;
            MovePoint check = new MovePoint(x, y);
            int i, j;
            EPlaygroundFieldStates current = playgr.GetFieldState(check);
            int count = 0;
            for (i = check.X; i >= 0; i--)
            {
                check.X = i;
                if (playgr.GetFieldState(check) == current)
                    count++;
                else
                    break;
            }
            for (j = x + 1; j <= 6; j++)
            {
                if (j <= 6)
                {
                    check.X = j;
                    if (playgr.GetFieldState(check) == current)
                        count++;
                    else
                        break;
                }
            }
            return count;
        }

        /// <summary>
        /// Überprüft ob nach Ausgeführten zug eine Diagonale absteigende viererkette entstanden ist
        /// </summary>
        /// <param name="point">Spielzug</param>
        /// <returns>Anzahl der Spielsteine</returns>
        public int diagonalcheckdown(MovePoint point)
        {
            int x = point.X;
            int y = point.Y;
            MovePoint check = new MovePoint(x, y);
            EPlaygroundFieldStates current = playgr.GetFieldState(check);
            int count = 0;
            //weniger
            while (check.X <= 6 && check.Y <= 5 && check.X >= 0 && check.Y >= 0)
            {
                if (playgr.GetFieldState(check) == current)
                {
                    count += 1;
                    check.X += 1;
                    check.Y += 1;
                }
                else
                    break;
            }
            check.X = x - 1;
            check.Y = y - 1;
            if (check.X >= 0 && check.Y >= 0)
            {
                //mehr
                while (check.Y >= 0 && check.X >= 0)
                {
                    if (playgr.GetFieldState(check) == current)
                    {
                        count += 1;
                        check.X -= 1;
                        check.Y -= 1;
                    }
                    else
                        break;
                }
            }
            return count;
        }

        /// <summary>
        /// Überprüft ob nach Ausgeführten zug eine Diagonale aufsteigende viererkette entstanden ist
        /// </summary>
        /// <param name="point">Spielzug</param>
        /// <returns>Anzahl der Spielsteine</returns>
        public int diagonalcheckhigh(MovePoint point)
        {
            int x = point.X;
            int y = point.Y;
            MovePoint check = new MovePoint(x, y);
            EPlaygroundFieldStates current = playgr.GetFieldState(check);
            int count = 0;
            //weniger
            while (check.X >= 0 && check.Y <= 5 && check.Y >= 0 && check.X <= 6)
            {
                if (playgr.GetFieldState(check) == current)
                {
                    count += 1;
                    check.X -= 1;
                    check.Y += 1;
                }
                else
                    break;
            }
            check.X = x + 1;
            check.Y = y - 1;
            if (check.X <= 6 && check.Y >= 0 && check.X >= 0 && check.Y <= 5)
            {
                //mehr
                while (check.Y >= 0 && check.X <= 6 && check.Y <= 5 && check.X >= 0)
                {
                    if (playgr.GetFieldState(check) == current)
                    {
                        count += 1;
                        check.X += 1;
                        check.Y -= 1;
                    }
                    else
                        break;
                }
            }
            return count;
        }

        /// <summary>
        /// Überprüft ob drei Steine vertikal liegen
        /// </summary>
        /// <param name="point">Spielzug</param>
        /// <returns>den zu setzenden Punkt</returns>
        public MovePoint vertpoint(MovePoint point)
        {
            int x = point.X; int y = point.Y;
            MovePoint check = new MovePoint(x, y + 1);
            if (verticalcheck(point) == 3 && check.Y >= 0)
                return check;
            return point;
        }

        /// <summary>
        /// Überprüft ob drei Steine horizontal liegen
        /// </summary>
        /// <param name="point">Spielzug</param>
        /// <returns>den zu setzenden Punkt</returns>
        public MovePoint horpoint(MovePoint point)
        {
            int x = point.X; int y = point.Y;
            MovePoint check = new MovePoint(0, y);
            MovePoint free = new MovePoint(0, 0);
            MovePoint checkdown = new MovePoint(x, y);
            int count = 0;
            int duo = 0;
            EPlaygroundFieldStates current = playgr.GetFieldState(point);
            for (int i = 0; i <= 6; i++)
            {
                check.X = i;
                if (playgr.GetFieldState(check) == current)
                {
                    count++;
                    if (duo > 2)
                    {
                        duo = 0;
                        count = 1;
                    }
                }
                else
                {
                    if (playgr.GetFieldState(check) == EPlaygroundFieldStates.EMPTY)
                    {
                        checkdown.X = check.X;
                        checkdown.Y = check.Y + 1;
                        if (duo <= 2 && count != 3)
                        {
                            if (checkdown.Y <= 5 && checkdown.X <= 6 && checkdown.Y >= 0 && checkdown.X >= 0)
                            {
                                if (playgr.GetFieldState(checkdown) != EPlaygroundFieldStates.EMPTY)
                                {
                                    free.X = check.X;
                                    free.Y = check.Y;
                                }
                            }
                            else
                            {
                                free.X = check.X;
                                free.Y = check.Y;
                            }
                        }
                        if (duo == 0 && count == 3)
                        {
                            if (checkdown.Y >= 5 && checkdown.X >= 6 && checkdown.Y >= 0 && checkdown.X >= 0)
                            {
                                if (playgr.GetFieldState(checkdown) != EPlaygroundFieldStates.EMPTY)
                                {
                                    free.X = check.X;
                                    free.Y = check.Y;
                                }
                            }
                            else
                            {
                                free.X = check.X;
                                free.Y = check.Y;
                            }
                        }
                        duo++;
                    }
                }
            }
            if (duo < 2)
                count = 0;
            if (count >= 3)
                return free;
            else
                return point;
        }

        /// <summary>
        /// Überprüft ob drei Steine diagonal absteigend liegen
        /// </summary>
        /// <param name="point">Spielzug</param>
        /// <returns>den zu setzenden Punkt</returns>
        public MovePoint diadownpoint(MovePoint point)
        {
            int x = point.X;
            int y = point.Y;
            int duo = 0;
            MovePoint check = new MovePoint(x, y);
            MovePoint free = new MovePoint(0, 0);
            MovePoint checkdown = new MovePoint(x, y);
            EPlaygroundFieldStates current = playgr.GetFieldState(check);
            int count = 0;
            //weniger
            while (check.X > -1 && check.Y < 6 && check.Y > -1 && check.X < 7)
            {
                if (playgr.GetFieldState(check) == current)
                {
                    if (duo > 2)
                    {
                        duo = 0;
                        count = 1;
                    }
                    count += 1;
                    check.X += 1;
                    check.Y += 1;
                }
                else
                    if (playgr.GetFieldState(check) == EPlaygroundFieldStates.EMPTY)
                    {
                        if (duo < 1)
                        {
                            free.X = check.X;
                            free.Y = check.Y;
                        }
                        check.X += 1;
                        check.Y += 1;
                        duo++;
                    }
                    else
                    {
                        check.X += 1;
                        check.Y += 1;
                        duo++;
                    }
            }
            check.X = x - 1;
            check.Y = y - 1;
            if (check.X <= 6 && check.Y >= 0 && check.X >= 0 && check.Y <= 5)
            {
                //mehr
                while (check.Y > -1 && check.X < 7 && check.Y < 6 && check.X > -1)
                {
                    if (playgr.GetFieldState(check) == current)
                    {
                        if (duo > 2)
                        {
                            duo = 0;
                            count = 1;
                        }
                        count += 1;
                        check.X -= 1;
                        check.Y -= 1;
                    }
                    else
                        if (playgr.GetFieldState(check) == EPlaygroundFieldStates.EMPTY)
                        {
                            if (duo < 1)
                            {
                                free.X = check.X;
                                free.Y = check.Y;
                            }
                            check.X -= 1;
                            check.Y -= 1;
                            duo++;
                        }
                        else
                        {
                            check.X -= 1;
                            check.Y -= 1;
                            duo++;
                        }
                }
            }

            if (duo < 2)
                count = 0;
            if (count >= 3)
            {
                checkdown.X = free.X;
                checkdown.Y = free.Y + 1;
                if (checkdown.Y >= 0 && checkdown.Y <= 5)
                    if (playgr.GetFieldState(checkdown) != EPlaygroundFieldStates.EMPTY)
                        return free;
            }
            return point;
        }

        /// <summary>
        /// Überprüft ob drei Steine diagonal aufsteigend liegen
        /// </summary>
        /// <param name="point">Spielzug</param>
        /// <returns>den zu setzenden Punkt</returns>
        public MovePoint diahighpoint(MovePoint point)
        {
            int x = point.X;
            int y = point.Y;
            int duo = 0;
            MovePoint check = new MovePoint(x, y);
            MovePoint free = new MovePoint(0, 0);
            MovePoint checkdown = new MovePoint(x, y);
            EPlaygroundFieldStates current = playgr.GetFieldState(check);
            int count = 0;
            //weniger
            while (check.X > -1 && check.Y < 6 && check.Y > -1 && check.X < 7)
            {
                if (playgr.GetFieldState(check) == current)
                {
                    if (duo > 2)
                    {
                        duo = 0;
                        count = 1;
                    }
                    count += 1;
                    check.X -= 1;
                    check.Y += 1;
                }
                else
                    if (playgr.GetFieldState(check) == EPlaygroundFieldStates.EMPTY)
                    {
                        if (duo < 1)
                        {
                            free.X = check.X;
                            free.Y = check.Y;
                        }
                        check.X -= 1;
                        check.Y += 1;
                        duo++;
                    }
                    else
                    {
                        check.X -= 1;
                        check.Y += 1;
                        duo++;
                    }
            }
            check.X = x + 1;
            check.Y = y - 1;
            if (check.X <= 6 && check.Y >= 0 && check.X >= 0 && check.Y <= 5)
            {
                //mehr
                while (check.Y > -1 && check.X < 7 && check.Y < 6 && check.X > -1)
                {
                    if (playgr.GetFieldState(check) == current)
                    {
                        if (duo > 2)
                        {
                            duo = 0;
                            count = 1;
                        }
                        count += 1;
                        check.X += 1;
                        check.Y -= 1;
                    }
                    else
                        if (playgr.GetFieldState(check) == EPlaygroundFieldStates.EMPTY)
                        {
                            if (duo < 1)
                            {
                                free.X = check.X;
                                free.Y = check.Y;
                            }
                            check.X += 1;
                            check.Y -= 1;
                            duo++;
                        }
                        else
                        {
                            check.X += 1;
                            check.Y -= 1;
                            duo++;
                        }
                }
            }
            if (duo < 2)
                count = 0;
            if (count >= 3)
            {
                checkdown.X = free.X;
                checkdown.Y = free.Y + 1;
                if (checkdown.Y >= 0 && checkdown.Y <= 5)
                    if (playgr.GetFieldState(checkdown) != EPlaygroundFieldStates.EMPTY)
                        return free;
            }
            return point;
        }

        /// <summary>
        /// Ruft die Überprüfung des Spielfeldes auf
        /// </summary>
        /// <param name="points">Spielzug</param>
        /// <returns>den zu setzenden Punkt</returns>
        public MovePoint nearend(MovePoint points)
        {
            MovePoint movep;
            MovePoint temppoint = new MovePoint();

            movep = vertpoint(points);
            if (movep != points)
                return movep;

            movep = horpoint(points);
            if (movep != points)
                return movep;
            else
            {
                temppoint.X = points.X - 1;
                temppoint.Y = points.Y - 1;
                if (temppoint.X <= 6 && temppoint.X >= 0 && temppoint.Y <= 5 && temppoint.Y >= 0)
                {
                    movep = horpoint(temppoint);
                    if (movep != temppoint)
                        return movep;
                    else
                    {
                        temppoint.X = points.X + 1;
                        temppoint.Y = points.Y - 1;
                        if (temppoint.X <= 6 && temppoint.X >= 0 && temppoint.Y <= 5 && temppoint.Y >= 0)
                        {
                            movep = horpoint(temppoint);
                            if (movep != temppoint)
                                return movep;
                        }
                    }
                }
            }

            movep = diadownpoint(points);
            if (movep != points)
                return movep;
            else
            {
                temppoint.X = points.X + 1;
                temppoint.Y = points.Y;
                if (temppoint.X <= 6 && temppoint.X >= 0)
                {
                    movep = diadownpoint(temppoint);
                    if (movep != temppoint)
                        return movep;
                }
            }

            movep = diahighpoint(points);
            if (movep != points)
                return movep;
            else
            {
                temppoint.X = points.X - 1;
                temppoint.Y = points.Y;
                if (temppoint.X >= 0 && temppoint.X <= 6)
                {
                    movep = diahighpoint(temppoint);
                    if (movep != temppoint)
                        return movep;
                }
            }
            return points;
        }
    }
}
