using System;
using System.Collections.Generic;

namespace Scrabble
{
    public class SpaceComparer : Comparer<Space>
    {
        private static readonly SpaceComparer _instance = new SpaceComparer();

        public static SpaceComparer Instance { get { return _instance; } }

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
    public class SpaceCoordsEqualityComparer : EqualityComparer<Space>
    {
        private static readonly SpaceCoordsEqualityComparer _instance = new SpaceCoordsEqualityComparer();

        public static SpaceCoordsEqualityComparer Instance { get { return _instance; } }

        public override bool Equals(Space a, Space b)
        {
            if (a == null && b == null)
                return true;
            if (a == null || b == null)
                return false;

            return a.GetCoords().Equals(b.GetCoords());
        }

        public override int GetHashCode(Space obj)
        {
            return obj.GetX().GetHashCode() ^ obj.GetY().GetHashCode();
        }
    }
    public class SpaceTileEqualityComparer : EqualityComparer<Space>
    {
        private static readonly SpaceTileEqualityComparer _instance = new SpaceTileEqualityComparer();

        public static SpaceTileEqualityComparer Instance { get { return _instance; } }

        public override bool Equals(Space a, Space b)
        {
            if (a == null && b == null)
                return true;
            if (a == null || b == null)
                return false;

            return (a.GetCoords().Equals(b.GetCoords())) && (a.GetTileLetter() == b.GetTileLetter());
        }

        public override int GetHashCode(Space obj)
        {
            return obj.GetX().GetHashCode() ^ obj.GetY().GetHashCode();
        }
    }
}
