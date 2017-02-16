using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using ExtentionMethods;

namespace Scrabble
{
    public class Game
    {
    
        #region //FIELDS

        private Space[,] _board;
        private Tray _tray;
        private string[] _dictionary;

        #endregion

        #region //CONSTRUCTORS
        /// <summary>
        /// Should eventually implement alternate board sizes, board types, and dictionaries and/or languages
        /// </summary>
        public Game(Tray tray, List<Tuple<Space, Tile>> startingBoard = null)
        {

            _tray = tray;
            EmptyBoard();
            ScrabbleMultipliers();

            if (startingBoard != null)
                SetBoard(startingBoard);

            _dictionary = File.ReadAllText("C:\\Users\\David\\Desktop\\Code\\C#-VS\\Scrabble\\Scrabble\\Dictionary\\dictionary.txt").Split(new string[] { Environment.NewLine }, StringSplitOptions.None);                  
        }
        public Game(string tray = "abcdefg", List<Tuple<Space, Tile>> startingBoard = null)
        {

            EmptyBoard();
            ScrabbleMultipliers();
            _tray = new Tray(tray);
            try
            {
                _dictionary = File.ReadAllText("C:\\Users\\David\\Desktop\\Code\\C#-VS\\Scrabble\\Scrabble\\Dictionary\\dictionary.txt").Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            }
            catch (Exception)
            {

                Console.WriteLine("**********Something is up with your Dictionary file*******");
                _dictionary = new string[] { "this", "is", "just", "a", "test" };
            }
        }
        #endregion

        #region //ACCESSORS

        public Space[,] GetBoard()
        {
            return _board;
        }

        public Space GetSpace(int x, int y)
        {            
            try
            {
                return _board[x, y];
            }
            catch (IndexOutOfRangeException)
            {

                return null;
            }
        }

        public Space GetSpace(Space space)
        {
            try
            {
                return _board[space.GetX(), space.GetY()];
            }
            catch (IndexOutOfRangeException)
            {

                return null;
            }
        }

        public void SetBoard(List<Tuple<Space, Tile>> pairs)
        {
            foreach (var item in pairs)
            {
                GetSpace(item.Item1).SetTile(item.Item2);
            }
        }

        public void SetBoard(List<Space> spaces, List<Tile> tiles)
        {
            if (spaces.Count != tiles.Count)
                throw new ArgumentException("The number of spaces must match the number of tiles when setting the board.");

            List<Tuple<Space, Tile>> pairs = new List<Tuple<Space, Tile>>();
            for (int i = 0; i < spaces.Count; i++)
            {
                pairs.Add(Tuple.Create(spaces[i], tiles[i]));
            }
            SetBoard(pairs);
        }

        public void SetBoard(List<Space> spaces)
        {
            foreach (var item in spaces)
            {
                if (item.GetTile() == null)
                    throw new Exception("The SetBoard method must include a tile for each space.");
            }

            foreach (var item in spaces)
            {
                GetSpace(item).SetTile(item.GetTile());
            }

        }

        public void SetMultiplier(Space space, string bonusType, int bonusValue)
        {
            if (bonusType != "word" && bonusType != "letter")
                throw new ArgumentException("bonusType must be either \"word\" or \"letter\"");

            if (bonusType == "word")
            {
                GetSpace(space).WordMultiplier = bonusValue;
            }
            if (bonusType == "letter")
            {
                GetSpace(space).LetterMultiplier = bonusValue;
            }
        }
        public void SetMultiplier(Tuple<int, int> space, string bonusType, int bonusValue)
        {
            if (bonusType != "word" && bonusType != "letter")
                throw new ArgumentException("bonusType must be either \"word\" or \"letter\"");

            if (bonusType == "word")
            {
                GetSpace(space.Item1, space.Item1).WordMultiplier = bonusValue;
            }
            if (bonusType == "letter")
            {
                GetSpace(space.Item1, space.Item1).LetterMultiplier = bonusValue;
            }
        }
        public void SetMultiplier(int x, int y, string bonusType, int bonusValue)
        {
            if (bonusType != "word" && bonusType != "letter")
                throw new ArgumentException("bonusType must be either \"word\" or \"letter\"");

            if (bonusType == "word")
            {
                GetSpace(x, y).WordMultiplier = bonusValue;
            }
            if (bonusType == "letter")
            {
                GetSpace(x, y).LetterMultiplier = bonusValue;
            }
        }

        private void ScrabbleMultipliers()
        {
            //3W
            SetMultiplier(0, 0, "word", 3);
            SetMultiplier(7, 0, "word", 3);
            SetMultiplier(14, 0, "word", 3);
            SetMultiplier(0, 7, "word", 3);
            SetMultiplier(14, 7, "word", 3);
            SetMultiplier(0, 14, "word", 3);
            SetMultiplier(7, 14, "word", 3);
            SetMultiplier(14, 14, "word", 3);

            //2W
            SetMultiplier(1, 1, "word", 2);
            SetMultiplier(13, 1, "word", 2);
            SetMultiplier(2, 2, "word", 2);
            SetMultiplier(12, 2, "word", 2);
            SetMultiplier(3, 3, "word", 2);
            SetMultiplier(11, 3, "word", 2);
            SetMultiplier(4, 4, "word", 2);
            SetMultiplier(10, 4, "word", 2);
            SetMultiplier(7, 7, "word", 2);
            SetMultiplier(4, 10, "word", 2);
            SetMultiplier(10, 10, "word", 2);
            SetMultiplier(3, 11, "word", 2);
            SetMultiplier(11, 11, "word", 2);
            SetMultiplier(2, 12, "word", 2);
            SetMultiplier(12, 12, "word", 2);
            SetMultiplier(1, 13, "word", 2);
            SetMultiplier(13, 13, "word", 2);

            //3L
            SetMultiplier(5, 1, "letter", 3);
            SetMultiplier(9, 1, "letter", 3);
            SetMultiplier(1, 5, "letter", 3);
            SetMultiplier(5, 5, "letter", 3);
            SetMultiplier(9, 5, "letter", 3);
            SetMultiplier(13, 5, "letter", 3);
            SetMultiplier(1, 9, "letter", 3);
            SetMultiplier(5, 9, "letter", 3);
            SetMultiplier(9, 9, "letter", 3);
            SetMultiplier(13, 9, "letter", 3);
            SetMultiplier(5, 13, "letter", 3);
            SetMultiplier(9, 13, "letter", 3);

            //2L
            SetMultiplier(0, 3, "letter", 2);
            SetMultiplier(0, 11, "letter", 2);
            SetMultiplier(2, 6, "letter", 2);
            SetMultiplier(2, 8, "letter", 2);
            SetMultiplier(3, 0, "letter", 2);
            SetMultiplier(3, 7, "letter", 2);
            SetMultiplier(3, 14, "letter", 2);
            SetMultiplier(2, 6, "letter", 2);
            SetMultiplier(6, 6, "letter", 2);
            SetMultiplier(8, 6, "letter", 2);
            SetMultiplier(12, 6, "letter", 2);
            SetMultiplier(3, 7, "letter", 2);
            SetMultiplier(11, 7, "letter", 2);
            SetMultiplier(2, 8, "letter", 2);
            SetMultiplier(6, 8, "letter", 2);
            SetMultiplier(8, 8, "letter", 2);
            SetMultiplier(12, 8, "letter", 2);
            SetMultiplier(0, 11, "letter", 2);
            SetMultiplier(7, 11, "letter", 2);
            SetMultiplier(14, 11, "letter", 2);
            SetMultiplier(6, 12, "letter", 2);
            SetMultiplier(8, 12, "letter", 2);
            SetMultiplier(3, 14, "letter", 2);
            SetMultiplier(11, 14, "letter", 2);


        }

        public Tray GetTray()
        {
            return _tray;
        }

        public string GetTrayString()
        {
            return _tray.GetTilesString();
        }

        public void SetTray(Tray tray)
        {
            _tray = tray;
        }

        public void SetTray(string tray)
        {
            _tray = new Tray(tray);
        }

        public string[] GetDictionary()
        {
            return _dictionary;
        }

        #endregion

        #region //METHODS

        public void EmptyBoard()
        {
            Space[,] emptyBoard = new Space[15, 15];
            for (int x = 0; x < 15; x++)
            {
                for (int y = 0; y < 15; y++)
                {
                    emptyBoard[x, y] = new Space(x, y);
                    emptyBoard[x, y].WordMultiplier = 1;
                    emptyBoard[x, y].LetterMultiplier = 1;
                }
            }
            _board = emptyBoard;
        }

        public List<Placement> PossiblePlacements()
        {            
            HashSet<Placement> returnHash = new HashSet<Placement>();

            if (_tray.Tiles.Count == 0)
                return new List<Placement>();

            //Adds all valid single space placements
            foreach (Space space in _board)
            {
                if (new Placement(space).IsValid(this))
                    returnHash.Add(new Placement(space));
            }

            //build placements of each length. Basing each on valid placements of size -1

            for (int i = 2; i <= _tray.Tiles.Count; i++)
            {

                List<Placement> buildFromList = returnHash.Where(x => x.GetSpaceList().Count() == i - 1).ToList();

                foreach (var item in buildFromList)
                {
                    if (item.IsSingle() || item.IsHorizontal())
                    {
                        int yValue = item.GetSpaceList()[0].GetY();

                        for (int x = 0; x < 15; x++)
                        {
                            List<Space> newSpaceList = new List<Space>(item.GetSpaceList());
                            Space newSpace = new Space(x, yValue);

                            newSpaceList.Add(newSpace);
                            Placement potentialPlacement = new Placement(newSpaceList);

                            if (potentialPlacement.IsValid(this))
                            {
                                returnHash.Add(potentialPlacement);
                            }
                        }
                    }

                    if (item.IsSingle() || item.IsVertical())
                    {
                        int xValue = item.GetSpaceList()[0].GetX();

                        for (int y = 0; y < 15; y++)
                        {
                            List<Space> newSpaceList = new List<Space>(item.GetSpaceList());
                            Space newSpace = new Space(xValue, y);

                            newSpaceList.Add(newSpace);
                            Placement potentialPlacement = new Placement(newSpaceList);

                            if (potentialPlacement.IsValid(this))
                            {
                                returnHash.Add(potentialPlacement);
                            }
                        }
                    }
                }               
            }
            return returnHash.ToList();
        }

        public List<Play> FindAllPlays()
        {
            List<Play> returnList = new List<Play>();

            List<Placement> possiblePlacements = PossiblePlacements();

            foreach (Placement placement in possiblePlacements)
            {
                returnList.AddRange(placement.ValidPlays(this));
            }

            return returnList;
        }


            

        #endregion
        /// <summary>
        /// Display methods for Program
        /// </summary>
        public void DisplayDictionary()
        {
            foreach (string word in _dictionary)
            {
                Console.WriteLine(word);
            }
        }
        public void DisplayBoard()
        {
            for (int x = 0; x < _board.GetLength(0); x++)
            {
                for (int y = 0; y < _board.GetLength(0); y++)
                {
                    Space thisSpace = _board[x, y];
                    if (thisSpace.IsOccupied())
                    {
                        Console.Write(" " + thisSpace.GetTileLetter() + " ");
                    }
                    else
                    {
                        Console.Write(" _ ");
                    }
                }
                Console.WriteLine();
            }
        }
        public void SortTest()
        {            
            List<Space> spaces = new List<Space>();
            Random rdm = new Random();

            for (int i = 0; i < 10; i++)
            {
                Space newSpace = new Space(rdm.Next(0, 15), rdm.Next(0, 15));
                spaces.Add(newSpace);
            }
            Placement testPlacement = new Placement(spaces);


            Console.WriteLine("Placement Before Sorting: ");
            Console.WriteLine(testPlacement.GetSpaceListString());
            
            testPlacement.PlacementSort();
            Console.WriteLine("Placement After Sorting: ");
            Console.WriteLine(testPlacement.GetSpaceListString());

        }
        public void RandomBoard()
        {

            Random rdm = new Random();

            List<Space> spaces = new List<Space>();
            for (int i = 0; i < 50; i++)
            {
                Space newSpace = new Space(rdm.Next(0, 15), rdm.Next(0, 15));
                spaces.Add(newSpace);
            }

            List<Tile> tiles = new List<Tile>();
            for (int i = 0; i < 50; i++)
            {
                Tile newTile = new Tile((char)('A' + rdm.Next(0, 26)));
                tiles.Add(newTile);
            }
            EmptyBoard();
            SetBoard(spaces, tiles);

        }
        

    }
}
