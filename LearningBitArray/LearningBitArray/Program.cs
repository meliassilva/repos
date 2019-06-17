using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningBitArray
{
    class Program
    {
        static void Main(string[] args)
        {
            bool[] preload = { true, false, true };

            BitArray enemyGrid = new BitArray(3);

            foreach( var item in enemyGrid)
            {
                Console.WriteLine(item);
            }

            //enemyGrid[0] = false;
            //enemyGrid[1] = true;
            //enemyGrid.Set(2, false);

        }
    }
}
