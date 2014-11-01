using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VierGewinntInterfaceLibrary;

namespace KI
{
    public class KI_All : AbstractKI
    {
        Evaluate evaluate = new Evaluate();
        Random lo_rand = new Random();
        EPlaygroundFieldStates[,] ground;
        int[] turn;
        MovePoint[] temppoints;
        MovePoint bestpoint = new MovePoint();
        MovePoint currenpoint = new MovePoint();
        int condepth = 1;
        int count = 0;

        public override AInteraction GetNextMove()
        {
            switch (GetDifficulty())
            {
                case EPlayerDifficulty.EASY: return KI_Med();
                case EPlayerDifficulty.MEDIUM: return KI_Med();
                case EPlayerDifficulty.HARD: return KI_Hard();
                default: return KI_Med();
            };
        }

        private AInteraction KI_Easy()
        {
            MovePoint mp;
            if (move.X != -1 && move.Y != -1)
                mp = nearend(move);
            else
                mp = move;
            if (mp.X == move.X && mp.Y == move.Y)
            {
                do
                {
                    move.X = lo_rand.Next(7);
                    move.Y = 0;
                }
                while (!move.IsValidPoint() || playgr.GetFieldState(move) != EPlaygroundFieldStates.EMPTY);
            }
            else
                move = mp;
            move = checkslot(move);
            playgr.SetCoinToPoint(move, EPlaygroundFieldStates.PLAYER1);
            KI_Med();
            Interaction test2 = new Interaction(move, EPlayers.SELF);
            return test2;
        }

        private AInteraction KI_Med()
        {
            turn = new int[condepth];
            temppoints = new MovePoint[condepth];
            int numb = 0;
            ground = playgr.clone();
            DateTime startTime = DateTime.Now;
            numb = max(condepth);
            Console.WriteLine("------- Überprüfung benötigte: " + (DateTime.Now - startTime) + " -------" + Environment.NewLine);
            Console.WriteLine("------- Anzahl der gesetzten Züge: " + count + " -------" + Environment.NewLine);
            move = bestpoint;
            playgr.SetCoinToPoint(move, EPlaygroundFieldStates.PLAYER1);
            Interaction test2 = new Interaction(move, EPlayers.SELF);
            return test2;
        }

        private AInteraction KI_Hard()
        {
            throw new NotImplementedException();
        }

        private int max(int depth)
        {
            if (depth <= 0 || noTurn(depth))
                return evaluate.evalue(ground, EPlayers.OPPONENT);
            int maxValue = int.MinValue;
            //generateTurns(EPlayers.SELF);
            while (!noTurn(depth))
            {
                setMove(depth - 1, EPlayers.SELF);
                depth--;
                int value = min(depth);
                undoMove(depth, EPlayers.SELF);
                depth++;
                if (value > maxValue)
                {
                    maxValue = value;
                    if (depth == condepth)
                        bestpoint = currenpoint;

                }
            }
            return maxValue;
        }

        private int min(int depth)
        {
            if (depth <= 0 || noTurn(depth))
                return evaluate.evalue(ground, EPlayers.SELF);
            int minValue = int.MaxValue;
            //generateTurns(EPlayers.OPPONENT)
            while (!noTurn(depth))
            {
                setMove(depth - 1, EPlayers.OPPONENT);
                depth--;
                int value = max(depth);
                undoMove(depth, EPlayers.OPPONENT);
                depth++;
                if (value < minValue)
                {
                    minValue = value;
                }
            }
            return minValue;
        }

        private void undoMove(int depth, EPlayers player)
        {
            if (player == EPlayers.SELF)
                ground[temppoints[depth].X, temppoints[depth].Y] = EPlaygroundFieldStates.EMPTY;
            else
                ground[temppoints[depth].X, temppoints[depth].Y] = EPlaygroundFieldStates.EMPTY;

        }

        private void setMove(int x, EPlayers eplayer)
        {
            MovePoint mv = new MovePoint();
            mv.X = turn[x];
            mv.Y = 0;
            turn[x]++;
            mv = checkslott(mv);
            count++;
            if (eplayer == EPlayers.SELF)
            {
                ground[mv.X, mv.Y] = EPlaygroundFieldStates.PLAYER1;
                temppoints[x] = mv;
                currenpoint = temppoints[condepth - 1];
            }
            else
            {
                ground[mv.X, mv.Y] = EPlaygroundFieldStates.PLAYER2;
                temppoints[x] = mv;

            }
        }

        private void generateTurns(EPlayers ePlayers)
        {

            //int i = 0;
            //foreach (int turns in turn)
            //{

            //    turn[i] = 0;
            //    i++;
            //}

            //MovePoint mv = new MovePoint();
            //mv.X = 0;
            //mv.Y = 5;
            //while (mv.X != 6)
            //{
            //    mv = checkslot(mv);
            //    if (ePlayers == EPlayers.OPPONENT)
            //        ground[mv.X, mv.Y] = EPlaygroundFieldStates.PLAYER2;
            //    else
            //        ground[mv.X, mv.Y] = EPlaygroundFieldStates.PLAYER1;
            //    mv.X++;
            //}
        }

        private bool noTurn(int depth)
        {
            if (turn[depth - 1] > 6)
            {
                turn[depth - 1] = 0;
                return true;
            }
            return false;
        }

        private MovePoint checkslott(MovePoint point)
        {
            MovePoint check;
            check = point;
            check.Y = 5;
            while (ground[point.X, point.Y] != EPlaygroundFieldStates.EMPTY)
                check.Y--;
            return check;

        }


    }
}
