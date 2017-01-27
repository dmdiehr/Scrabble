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
        public Game(string tray, List<Tuple<Space, Tile>> startingBoard = null)
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
            for (int x = _board.GetLength(0) - 1; x >= 0; x--)
            {
                for (int y = _board.GetLength(1) - 1; y >= 0; y--)
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

    }
}
