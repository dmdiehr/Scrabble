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

        public bool DoesExclude()
        {
            bool returnBool = false;



            return returnBool;
        }
    }
}
