using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VierGewinntInterfaceLibrary;

namespace KI
{
    public class Evaluate
    {
        private int checkvert(EPlaygroundFieldStates[,] plground, EPlayers player)
        {
            int countself = 0;
            int countopponent = 0;
            int countselftemp = 0;
            int countopptemp = 0;
            for (int i = 0; i <= 6; i++)
                for (int j = 5; j >= 0; j--)
                    if (plground[i, j] == EPlaygroundFieldStates.PLAYER1)
                    {
                        countself++;
                        //countopponent = 0;
                    }
                    else
                        if (plground[i, j] == EPlaygroundFieldStates.PLAYER2)
                        {
                            countopponent++;
                            //countself = 0;
                        }
                        else
                        {
                            if (countself > countselftemp)
                                countselftemp = countself;
                            if (countopponent > countopptemp)
                                countopptemp = countopponent;
                            countself = 0;
                            countopponent = 0;
                            break;
                        }
            if (player == EPlayers.SELF)
                return countselftemp;
            else
                return countopptemp;
        }

        private int checkhor(EPlaygroundFieldStates[,] plground, EPlayers player)
        {
            int countself;
            int countopponent;
            int countselftemp = 0;
            int countopptemp = 0;
            int duo;
            for (int i = 5; i >= 0; i--)
            {
                duo = 0;
                countself = 0;
                countopponent = 0;
                for (int j = 0; j <= 6; j++)
                    if (plground[j, i] == EPlaygroundFieldStates.PLAYER1)
                    {

                        countself++;
                        if (duo < 2)
                        {
                            if (countopponent < 4) 
                            countopponent = 0;
                            duo = 0;
                        }
                    }
                    else
                        if (plground[j, i] == EPlaygroundFieldStates.PLAYER2)
                        {
                            if (duo < 2)
                            {
                                if (countself < 4)
                                    countself = 0;
                                duo = 0;
                            }
                            countopponent++;
                        }
                        else
                        {
                            duo++;
                        }
                if (countself > countselftemp)
                    countselftemp = countself;
                if (countopponent > countopptemp)
                    countopptemp = countopponent;
            }

            if (player == EPlayers.SELF)
            //if (countselftemp > countopptemp)
                return countselftemp;
            else
                return countopptemp;
        }

        private int checkdiaright(EPlaygroundFieldStates[,] plground, EPlayers player)
        {
            int countself = 0;
            int countopponent = 0;
            int countselftemp = 0;
            int countopptemp = 0;
            for (int i = 0; i <= 6; i++)
                for (int j = 5; j >= 0; j--)
                {
                    int x = i;

                    int y = j;
                    while (x >= 0 && x <= 6 && y <= 5 && y >= 0)
                    {
                        if (plground[x, y] == EPlaygroundFieldStates.PLAYER1)
                        {
                            countself++;
                            countopponent = 0;
                            x++;
                            y--;
                        }
                        else
                            if (plground[x, y] == EPlaygroundFieldStates.PLAYER2)
                            {
                                countself = 0;
                                countopponent++;
                                x++;
                                y--;
                            }
                            else
                            {
                                if (countself > countselftemp)
                                    countselftemp = countself;
                                if (countopponent > countopptemp)
                                    countopptemp = countopponent;
                                countself = 0;
                                countopponent = 0;
                                break;
                            }
                    }
                }
            if (player == EPlayers.SELF)
                return countselftemp;
            else
                return countopptemp;
        }

        private int checkdialeft(EPlaygroundFieldStates[,] plground, EPlayers player)
        {
            int countself = 0;
            int countopponent = 0;
            int countselftemp = 0;
            int countopptemp = 0;
            for (int i = 0; i <= 6; i++)
                for (int j = 5; j >= 0; j--)
                {
                    int x = i;
                    int y = j;
                    while (x >= 0 && x <= 6 && y <= 5 && y >= 0)
                    {
                        if (plground[x, y] == EPlaygroundFieldStates.PLAYER1)
                        {
                            countself++;
                            countopponent = 0;
                            x--;
                            y--;
                        }
                        else
                            if (plground[x, y] == EPlaygroundFieldStates.PLAYER2)
                            {
                                countself = 0;
                                countopponent++;
                                x--;
                                y--;
                            }
                            else
                            {
                                if (countself > countselftemp)
                                    countselftemp = countself;
                                if (countopponent > countopptemp)
                                    countopptemp = countopponent;
                                countself = 0;
                                countopponent = 0;
                                break;
                            }
                    }
                }
            if (player == EPlayers.SELF)
                return countselftemp;
            else
                return countopptemp;
        }

        private int points(int counter, EPlayers player)
        {
            switch (counter)
            {
                case 1: if (player == EPlayers.SELF)
                        return -10000;
                    else
                        return 10;
                case 2: if (player == EPlayers.SELF)
                        return -1000;
                    else
                        return 100;
                case 3: if (player == EPlayers.SELF)
                        return -100;
                    else
                        return 1000;
                case 4: if (player == EPlayers.SELF)
                        return -10;
                    else
                        return 10000;
                default: return 0;
            };
        }

        public int evalue(EPlaygroundFieldStates[,] plground, EPlayers player)
        {
            //int vertvalue = points(checkvert(plground, player), player);
            int horvalue = points(checkhor(plground, player), player);
            //int diarightvalue = points(checkdiaright(plground, player), player);
            //int dialeftvalue = points(checkdialeft(plground, player), player);
            Console.WriteLine("----- Die Bewertung Lautet: " + horvalue + " -----");
            System.Threading.Thread.Sleep(100);
            return (/*vertvalue + */horvalue /*+ diarightvalue + dialeftvalue*/);
        }

    }
}
