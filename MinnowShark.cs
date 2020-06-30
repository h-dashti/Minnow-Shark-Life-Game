using System;
using System.Collections.Generic;
using System.Data;

namespace MinnowShark_Lifegame
{
    //public enum byte {Empty = 0, Minnow = 1, Shark  = 2};
    public struct Loc
    {
        public int x, y;
        public Loc(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public struct Minnow
    {
        // age: the age of aquate. 
        //(x,y) : the location of minnow.
        // breed : if aquatic has surivived in this long time, the aquatic has an offspring.  
        public int age, x, y, f_breed;                 
           
        public Minnow(int age, int x, int y)
        {
            this.age = age;
            this.x = x;
            this.y = y;
            this.f_breed = 0;
        }

    }
    public struct Shark
    {
        // age: the age of aquate. 
        //(x,y) : the location of minnow.
        // breed : if aquatic has surivived in this long time, the aquatic has an offspring.   
        public int age, x, y, s_breed;                  
        public int n_strave;                           // if a shark moves strave times without eating, the shark dies.
        public Shark(int age, int x, int y)
        {
            this.age = age;
            this.x = x;
            this.y = y;
            this.s_breed = 0;
            this.n_strave = 0;
        }
    }
    
    public class MinnowShark
    {
        public int stravesTimes = 10;
        public int breedTimesMinnow =  4;
        public int breedTimeShark = 20;
        public int ageDiingMinnow = 55;
        public int ageDiingShark = 100;


        public List<Minnow> minnows;
        public List<Shark> sharks;
        public int nMinnows = 20;
        public int nSharks = 2;
        public int nRows = 40, nCols = 40;
        public byte[,] cellsNew;
        private byte[,] cellsOld;
        private Random rand = new Random();

        //public const int mBreed =  

        public MinnowShark()
        {
            cellsNew = new byte[nRows, nCols];
            cellsOld = new byte[nRows, nCols];

            minnows = new List<Minnow>(nMinnows);
            sharks = new List<Shark>(nSharks);
            int i, x, y;
            for (i = 0; i < nMinnows; )
            {
                x = rand.Next(nRows);
                y = rand.Next(nCols);
                if (cellsNew[x, y] == 0)
                {
                    cellsNew[x, y] = 1;
                    minnows[i] = new Minnow(rand.Next(ageDiingMinnow - 10), x, y);
                    i++;
                }
            }

            for (i = 0; i < nSharks; )
            {
                x = rand.Next(nRows);
                y = rand.Next(nCols);
                if (cellsNew[x, y] == 0)
                {
                    cellsNew[x, y] = 2;
                    sharks[i] = new Shark(rand.Next(ageDiingShark - 40), x, y);
                    i++;
                }
            }
        }



        public void NextStep()
        {
            int i, x, y, tempIndex;
            List<Loc> tempEmptyLoc = new List<Loc>();
            List<Loc> tempSharkFoodLoc = new List<Loc>();
            List<Minnow> addedMinnows = new List<Minnow>();
            List<Minnow> removedMinnows = new List<Minnow>();
            List<Minnow> addedSharks = new List<Minnow>();
            List<Minnow> removedSharks = new List<Minnow>();
            

            for (x = 0; x < nRows; x++)
                for (y = 0; y < nCols; y++)
                    cellsOld[x, y] = cellsNew[x, y];


            // part for minnow
            for (i = 0; i < nMinnows; i++)
            {
                x = minnows[i].x;
                y = minnows[i].y;

                int x1 = (x == 0) ? nRows - 1 : x - 1;
                int x2 = (x == nRows - 1) ? 0 : x + 1;
                int y1 = (y == 0) ? nCols - 1 : y - 1;
                int y2 = (y == nCols - 1) ? 0 : y + 1;

                if (cellsOld[x1, y] == 0)
                    tempEmptyLoc.Add(new Loc(x1, y));
                if (cellsOld[x2, y] == 0)
                    tempEmptyLoc.Add(new Loc(x2, y));
                if (cellsOld[x, y1] == 0)
                    tempEmptyLoc.Add(new Loc(x, y1));
                if (cellsOld[x, y2] == 0)
                    tempEmptyLoc.Add(new Loc(x, y2));

                int count = tempEmptyLoc.Count;
                if (count > 0)
                {
                    tempIndex = rand.Next(count);
                    cellsNew[tempEmptyLoc[tempIndex].x, tempEmptyLoc[tempIndex].y] = 1;
                    cellsNew[x, y] = 0;
                }

                minnows[i].f_breed++;
                if (minnows[i].f_breed == breedTimesMinnow)
                {
                    minnows[i].f_breed = 0;
                    addedMinnows.Add(new Minnow(0, x, y));
                }
            }
            tempEmptyLoc.Clear();




            // part for shark
            for (i = 0; i < nSharks; i++)
            {
                x = sharks[i].x;
                y = sharks[i].y;

                int x1 = (x == 0) ? nRows - 1 : x - 1;
                int x2 = (x == nRows - 1) ? 0 : x + 1;
                int y1 = (y == 0) ? nCols - 1 : y - 1;
                int y2 = (y == nCols - 1) ? 0 : y + 1;

                if (cellsOld[x1, y] == 0)
                    tempEmptyLoc.Add(new Loc(x1, y));
                else if (cellsOld[x1, y] == 1)
                    tempSharkFoodLoc.Add(new Loc(x1, y));

                if (cellsOld[x2, y] == 0)
                    tempEmptyLoc.Add(new Loc(x2, y));
                else if (cellsOld[x2, y] == 1)
                    tempSharkFoodLoc.Add(new Loc(x2, y));

                if (cellsOld[x, y1] == 0)
                    tempEmptyLoc.Add(new Loc(x, y1));
                else if (cellsOld[x, y1] == 1)
                    tempSharkFoodLoc.Add(new Loc(x, y1));

                if (cellsOld[x, y2] == 0)
                    tempEmptyLoc.Add(new Loc(x, y2));
                else if (cellsOld[x, y2] == 1)
                    tempSharkFoodLoc.Add(new Loc(x, y2));

                int countEmpty = tempEmptyLoc.Count;
                int countSharkFood = tempSharkFoodLoc.Count;

                if (countSharkFood > 0)
                {
                    tempIndex = rand.Next(countSharkFood);

                }

                if (count > 0)
                {
                    tempIndex = rand.Next(tempEmptyLoc.Count);
                    cellsNew[tempEmptyLoc[tempIndex].x, tempEmptyLoc[tempIndex].y] = byte.Shark;
                }


                sharks[i].f_breed++;
                if (sharks[i].f_breed == breedTimesMinnow)
                {
                    sharks[i].f_breed = 0;
                    addedMinnows.Add(new Minnow(0, x, y));
                }
            }


           
        }


    }

}
