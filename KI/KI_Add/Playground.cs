using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VierGewinntInterfaceLibrary;

namespace KI
{
    public class Playground : APlayground
    {

        public Playground(EPlaygroundFieldStates OwnCoin)
            : base(OwnCoin)
        {
            InitPlayground();
        }

        public override void SetCoinToPoint(MovePoint Point, EPlaygroundFieldStates State)
        {
            Playground[Point.X, Point.Y] = State;
        }

        public EPlaygroundFieldStates[,] clone()
        {
            EPlaygroundFieldStates[,] current =  this.Playground;
            EPlaygroundFieldStates[,] ground = new EPlaygroundFieldStates[current.GetLength(0), current.GetLength(1)];
            for (int i= 0 ; i< current.GetLength(0) ;i++)
                for (int j = 0; j < current.GetLength(1); j++)
                {
                    ground[i,j]=current[i,j];
                }

            return ground;
        }
    }
}
