using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrabble
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ALL THE WORDS");
            Game newGame = new Game("dummy");
            string input = "";

            do
            {
                Console.WriteLine("Enter 'words', 'board', or 'exit'");
                input = Console.ReadLine();

                if (input == "words")
                {
                    newGame.DisplayDictionary();
                }

                if (input == "board")
                {
                    newGame.DisplayBoard();
                }

                if (input  == "test")
                {
                    newGame.SortDisplay();
                }
            } while (input != "exit");

            Console.WriteLine();
            Console.WriteLine("*************************THAT'S ALL FOLKS******************************************************");
            Console.ReadLine();

        }
    }
}
