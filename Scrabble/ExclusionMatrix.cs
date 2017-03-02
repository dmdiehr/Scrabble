using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrabble
{
    class PlacementMatrix
    {
        //FIELDS
        private Placement _placement;
        private string _tray;
        private Game _game;
        private bool[,] _exclusionArray;
        public List<Space> PrimaryWordSpaces { get; }


        //CONSTRUCTOR

        public PlacementMatrix (Placement placement, Game game)
        {
            if (placement.IsSingle())
                throw new ArgumentException("An PlacementMatrix is not appropriate for single tile placements");

            _placement = placement;
            _game = game;
            char[] temp = game.GetTrayString().ToCharArray();
            Array.Sort(temp);
            _tray = new string(temp);
            _exclusionArray = makeArray();

           PrimaryWordSpaces = new List<Space>();
           PrimaryWordSpaces.AddRange(_placement.Anchors);
           PrimaryWordSpaces.AddRange(_placement.GetSpaceList());
           PrimaryWordSpaces.Sort(SpaceComparer.Instance);


        }

        //METHODS

        private bool[,] makeArray()
        {
            bool[,] returnArray = new bool[_placement.GetSpaceList().Count(), _tray.Length];






            return returnArray;
        }

        public List<Tuple<int, char>> AnchorTuples()
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

        public List<Tuple<int, char>> ExclusionTuples()
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
                if (PrimaryWordSpaces[i].GetTile() != null)
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
                            string word = _game.SingleSubWord(Tuple.Create(PrimaryWordSpaces[i], new Tile(newChar)), subWordDirection).Word;
                            if (!_game.GetDictionary().Contains(word))
                            {
                                returnList.Add(Tuple.Create(i, newChar));
                            }
                        }                                                 
                    }                        
                    else
                    {
                        string word = _game.SingleSubWord(Tuple.Create(PrimaryWordSpaces[i], new Tile(letter)), subWordDirection).Word;
                        if (!_game.GetDictionary().Contains(word))
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
