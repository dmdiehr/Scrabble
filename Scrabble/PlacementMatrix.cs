using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrabble
{
    public class PlacementMatrix
    {
        //FIELDS
        private Placement _placement;
        private string _tray;
        public Game Game { get; }
        public bool[,] ExclusionArray { get; }
        public List<Space> PrimaryWordSpaces { get; }
        public List<Tuple<int, char>> AnchorTuples { get; }
        public List<Tuple<int, char>> ExclusionTuples { get; }
        
        //CONSTRUCTOR

        public PlacementMatrix (Placement placement)
        {
            if (placement.IsSingle())
                throw new ArgumentException("An PlacementMatrix is not appropriate for single tile placements");

            _placement = placement;
            Game = _placement.Game;
            _tray = Game.GetTrayString();
            


            PrimaryWordSpaces = new List<Space>();
            PrimaryWordSpaces.AddRange(_placement.Anchors);
            PrimaryWordSpaces.AddRange(_placement.GetSpaceList());
            PrimaryWordSpaces.Sort(SpaceComparer.Instance);
           
            AnchorTuples = GetAnchorTuples();
            ExclusionTuples = GetExclusionTuples();
            ExclusionArray = makeArray();

        }

        //METHODS

        private bool[,] makeArray()
        {
            int columnCount = _placement.GetSpaceList().Count;
            int rowCount = _tray.Length; 
            bool[,] returnArray = new bool[columnCount, rowCount];

            int primaryWordIndex = 0;
            for (int x = 0; x < columnCount; x++)
            {
                //check skip over anchors
                while (PrimaryWordSpaces[primaryWordIndex].IsOccupied())
                {
                    primaryWordIndex++;
                    if (PrimaryWordSpaces.Count == primaryWordIndex)
                        break;
                }
                    
                for (int y = 0; y < rowCount; y++)
                {
                    if(_tray[y] == '?')
                    {
                        var tempList =
                            from t in ExclusionTuples
                            where t.Item1 == primaryWordIndex
                            select t;
                 
                        if (tempList.Distinct().Count() > 25)
                        {
                            returnArray[x, y] = false;
                        }
                        else
                        {
                            returnArray[x, y] = true;
                        }
                    }
                    else if (ExclusionTuples.Contains(Tuple.Create(primaryWordIndex, _tray[y])))
                    {
                        returnArray[x, y] = false;
                    }
                    else
                    {
                        returnArray[x, y] = true;
                    }
                }
                primaryWordIndex++;
            }
            return returnArray;
        }

        public List<Tuple<int, char>> GetAnchorTuples()
        {
            List<Tuple<int, char>> returnList = new List<Tuple<int, char>>();

            for (int i = 0; i <PrimaryWordSpaces.Count; i++)
            {
                if (PrimaryWordSpaces[i].GetTile() != null)
                {
                    returnList.Add(Tuple.Create(i,PrimaryWordSpaces[i].GetTile().GetLetter()));
                }
            }

            return returnList;
        }

        public List<Tuple<int, char>> GetExclusionTuples()
        {
            List<Tuple<int, char>> returnList = new List<Tuple<int, char>>();

            string subWordDirection = "";
            if (_placement.IsHorizontal())
                subWordDirection = "vertical";
            else if (_placement.IsVertical())
                subWordDirection = "horizontal";

            for (int i = 0; i <PrimaryWordSpaces.Count; i++)
            {
                //check to see if this letter is an anchor, if so skip
                if (PrimaryWordSpaces[i].IsOccupied())
                    continue;

                foreach (char letter in _tray)
                {
                    bool blankChecked = false;
                    if (letter == '?' && !blankChecked)
                    {
                        blankChecked = true;                                         
                        for (int c = 0; c < 26; c++)
                        {
                            char newChar = (char)('A' + c);
                            string word = Game.SingleSubWord(Tuple.Create(PrimaryWordSpaces[i], new Tile(newChar)), subWordDirection).Word;

                            if (!Game.GetDictionary().Contains(word))
                            {
                                returnList.Add(Tuple.Create(i, newChar));
                            }
                        }                                                 
                    }                        
                    else
                    {
                        string word = Game.SingleSubWord(Tuple.Create(PrimaryWordSpaces[i], new Tile(letter)), subWordDirection).Word;
                        if (!Game.GetDictionary().Contains(word))
                        {
                            returnList.Add(Tuple.Create(i, letter));
                        }
                    }

                }
            }

            return returnList;
        }

        public bool DoesExclude(bool[,] boolArray = null)
        {
            if (boolArray == null)
                boolArray = ExclusionArray;

            bool returnBool = false;

            int spaceCount = boolArray.GetLength(0);
            int letterCount = boolArray.GetLength(1);

            ////check if you have enough letters
            //if (letterCount < spaceCount)
            //    return true;

            ////check if any spaces can't be filled
            //for (int i = 0; i < spaceCount; i++)
            //{
            //    bool possible = false;
            //    for (int j = 0; j < letterCount; j++)
            //    {
            //        if (boolArray[i, j] == true)
            //        {
            //            possible = true;
            //            break;
            //        }
            //    }
            //    if (!possible)
            //        return true;
            //}


            //group identical columns

            List<List<Column>> columnGroups = new List<List<Column>>();

            for (int i = 0; i < spaceCount; i++)
            {
                Column newColumn = new Column();
                newColumn.SpaceIndex = i;

                for (int j = 0; j < letterCount; j++)
                {
                    newColumn.BoolList.Add(boolArray[i, j]);
                    if (boolArray[i, j] == true)
                        newColumn.TruthCount++;
                }

                bool newColumnMatches = false;
                for (int k = 0; k < columnGroups.Count; k++)
                {
                    if (newColumn.Equals(columnGroups[k].First()))
                    {
                        newColumnMatches = true;
                        columnGroups[k].Add(newColumn);
                        break;
                    }
                }
                if (!newColumnMatches)
                {
                    List<Column> newColumnGroup = new List<Column>();
                    newColumnGroup.Add(newColumn);
                    columnGroups.Add(newColumnGroup);
                }
            }


            //check if any group's column count is greater than their T count
            foreach (var group in columnGroups)
            {
                int groupCount = group.Count;
                int trueCount = group.First().TruthCount;

                if (trueCount < groupCount)
                    return true;
            }

            ////check if any group's column count is equal to their T count
            ////if so, remove the columns and rows that are T
            ////rebuild the array and recurse with the new array
            bool[,] newArray;
            foreach (var group in columnGroups)
            {
                int groupCount = group.Count;
                int trueCount = group.First().TruthCount;

                if (trueCount == groupCount)
                {
                    List<int> columnIndexes = new List<int>();
                    foreach (Column column in group)
                    {
                        columnIndexes.Add(column.SpaceIndex);
                    }

                    List<int> rowIndexes = new List<int>();
                    for (int i = 0; i < group.First().BoolList.Count; i++)
                    {
                        if (group.First().BoolList[i] == true)
                            rowIndexes.Add(i);
                    }

                    Debug.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                    Debug.WriteLine("Column Indexes");
                    foreach (var item in columnIndexes)
                    {
                        Debug.Write(" " + item);
                    }
                    Debug.WriteLine("");
                    Debug.WriteLine("Row Indexes");
                    foreach (var item in rowIndexes)
                    {
                        Debug.Write(" " + item);
                    }
                    Debug.WriteLine("");
                    Debug.WriteLine("Inhereted Array");
                    for (int i = 0; i < letterCount; i++)
                    {
                        Debug.Write(_tray[i] + " = ");
                        for (int j = 0; j < spaceCount; j++)
                        {
                            Debug.Write(" "+boolArray[j, i]);
                        }
                        Debug.WriteLine("");
                    }
                    Debug.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

        

                    

                    int newSpaceCount = spaceCount - columnIndexes.Count;
                    int newLetterCount = letterCount - rowIndexes.Count;
                    newArray = new bool[newSpaceCount, newLetterCount];

                    int newSpaceIndex = 0;
                    for (int i = 0; i < spaceCount; i++)
                    {
                        if (columnIndexes.Contains(i))
                            continue;

                        int newLetterIndex = 0;
                        for (int j = 0; j < letterCount; j++)
                        {
                            if (rowIndexes.Contains(j))
                                continue;

                            newArray[newSpaceIndex, newLetterIndex] = boolArray[i, j];

                            newLetterIndex++;
                        }
                        newSpaceIndex++;
                    }

                    Debug.WriteLine("New Array");
                    for (int i = 0; i < newLetterCount; i++)
                    {                        
                        for (int j = 0; j < newSpaceCount; j++)
                        {
                            Debug.Write(" " + newArray[j, i]);
                        }
                        Debug.WriteLine("");
                    }
                    return DoesExclude(newArray);
                }
            }



            //if there are no such columns, return false (???)
            //I don't know if this is completely valid 
            //I just don't know any other ways to find more exclusions

            return returnBool;
        }
    }
}
