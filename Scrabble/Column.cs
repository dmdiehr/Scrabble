using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrabble
{
    public class Column
    {
        public List<bool> BoolList { get; set; }
        public int TruthCount { get; set; }
        public int SpaceIndex { get; set; }

        public Column()
        {
            BoolList = new List<bool>();
            TruthCount = 0;
            SpaceIndex = -1;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (ReferenceEquals(obj, this))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            Column otherColumn = obj as Column;
            return (BoolList.SequenceEqual(otherColumn.BoolList));
        }

        public override int GetHashCode()
        {
            return BoolList.GetHashCode();
        }
    }
}
