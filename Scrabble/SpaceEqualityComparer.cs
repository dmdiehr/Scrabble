using System.Collections.Generic;

namespace Scrabble
{
    public class SpaceEqualityComparer : EqualityComparer<Space>
    {
        private static readonly SpaceEqualityComparer _instance = new SpaceEqualityComparer();

        public static SpaceEqualityComparer Instance { get { return _instance; } }

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
}
