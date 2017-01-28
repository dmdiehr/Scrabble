using System;
using System.Collections.Generic;

namespace Scrabble
{
    internal class SpaceComparer : Comparer<Space>
    {
        public override int Compare(Space a, Space b)
        {
            if (a == null && b == null)
                return 0;
            if (a == null)
                return -1;
            if (b == null)
                return 1;

            if (a.GetCoords().Equals(b.GetCoords()))
                return 0;
            if (a.GetCoords().Item1 < b.GetCoords().Item1)
                return -1;
            if (a.GetCoords().Item1 > b.GetCoords().Item1)
                return 1;

            if (a.GetCoords().Item1 == b.GetCoords().Item1)
            {
                if (a.GetCoords().Item2 < b.GetCoords().Item2)
                {
                    return -1;
                }
                if (a.GetCoords().Item2 > b.GetCoords().Item2)
                {
                    return 1;
                }
            }

            throw new Exception("you done messed up your SpaceComparer");              

            
        }
    }
}