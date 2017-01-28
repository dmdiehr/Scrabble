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

            _board = EmptyBoard();
            _tray = tray;
            try
            {
                _dictionary = File.ReadAllText("C:\\Users\\David\\Desktop\\Code\\C#-VS\\ScrabbleCore\\ScrabbleCore\\Dictionary\\dictionary.txt").Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            }
            catch (Exception)
            {

                throw new Exception("Something's up with your dictionary file");
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


        public void SortDisplay()
        {
            Space space1 = new Space(7, 7);
            Space space2 = new Space(7, 7);
            Space space3 = new Space(1, 7);
            Space space4 = new Space(2, 3);
            Space space5 = new Space(3, 8);
            Space space6 = new Space(3, 9);
            Space space7 = new Space(4, 12);
            Space space8 = new Space(5, 1);
            Space space9 = new Space(6, 7);

            Placement newPlacement = new Placement(new List<Space> { space1, space2, space3, space4, space5, space6, space7, space8, space9});

            Console.WriteLine("Placement Before Sorting: ");
            foreach (Space space in newPlacement.GetSpaceList())
            {
                Console.WriteLine(space.GetCoordsString());
            }

            Console.WriteLine("Placement After Sorting: ");
            newPlacement.PlacementSort();
            foreach (Space space in newPlacement.GetSpaceList())
            {
                Console.WriteLine(space.GetCoordsString());
            }

        }
    }
}
