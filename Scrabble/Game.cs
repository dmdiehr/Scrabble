using System;
using System.Collections.Generic;
using System.IO;

namespace Scrabble
{
    public class Game
    {
        //CONSTRUCTOR
        /// <summary>
        /// Should eventually implement alternate board sizes, board types, and dictionaries and/or languages
        /// </summary>
        public Game(Tray tray, List<Tuple<Space, Tile>> startingBoard = null)
        {

            _tray = tray;
            _board = EmptyBoard();

            if (startingBoard != null)
                SetBoard(startingBoard);
            
            try
            {
                _dictionary = File.ReadAllText("C:\\Users\\David\\Desktop\\Code\\C#-VS\\ScrabbleCore\\ScrabbleCore\\Dictionary\\dictionary.txt").Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            }
            catch (Exception)
            {

                Console.WriteLine("**********Something is up with your Dictionary file*******");
                _dictionary = new string[] { "this", "is", "just", "a", "test" };
            }         
        }
        public Game(string tray = "abcdefg", List<Tuple<Space, Tile>> startingBoard = null)
        {

            _board = EmptyBoard();
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

        //FIELDS

        private Space[,] _board;
        private Tray _tray;
        private string[] _dictionary;

        //METHODS
        public Space[,] GetBoard()
        {
            return _board;
        }

        public Space GetSpace(int x, int y)
        {
            return _board[x, y];          
        }
        public Space GetSpace(Space otherSpace)
        {
            return _board[otherSpace.GetX(), otherSpace.GetY()];
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

        public List<Placement> PossiblePlacements()
        {
            throw new NotImplementedException();
        }

        public Space[,] EmptyBoard()
        {
            Space[,] newBoard = new Space[15, 15];
            for (int x = 0; x < 15; x++)
            {
                for (int y = 0; y < 15; y++)
                {
                    newBoard[x, y] = new Space(x, y);
                }
            }
            return newBoard;
        }

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
            foreach (Space space in testPlacement.GetSpaceList())
            {
                Console.WriteLine(space.GetCoordsString());
            }

            Console.WriteLine("Placement After Sorting: ");
            testPlacement.PlacementSort();
            foreach (Space space in testPlacement.GetSpaceList())
            {
                Console.WriteLine(space.GetCoordsString());
            }

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
                Tile newTile = new Tile((char)('a' + rdm.Next(0, 26)));
                tiles.Add(newTile);
            }
            _board = EmptyBoard();
            SetBoard(spaces, tiles);

        }
    }
}
