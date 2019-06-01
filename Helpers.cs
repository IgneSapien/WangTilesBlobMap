using System.Collections.Generic;
using GoRogue;


namespace Wang
{
    public static class Helpers
    {
        public static Coord Overlaycoord(Coord startCoord)
        {
            return new Coord((startCoord.X * 2) + 1, (startCoord.Y * 2) + 1);
        }



        //Roatation of indexs in 90 degree incriments 
        public static List<int[,]> TranlateArrayList(List<int[,]> orign)
        {
            int size = orign[0].GetLength(0);
            List<int[,]> returnList = new List<int[,]>();

            foreach (int[,] a in orign)
            {
                returnList.Add(RotateMatrix(a, size));
            }

            return returnList;
        }

        //Shameless stolen from stack overflow, will look at nicer approaches later for bigger tiles
        public static int[,] RotateMatrix(int[,] matrix, int n)
        {
            int[,] ret = new int[n, n];

            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    ret[i, j] = matrix[n - j - 1, i];
                }
            }

            return ret;
        }
    }
}
