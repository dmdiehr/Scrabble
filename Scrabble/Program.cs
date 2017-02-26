using System;
using System.IO;
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

                if (input.ToLower() == "words")
                {
                    newGame.DisplayDictionary();
                }

                if (input.ToLower() == "board")
                {
                    newGame.DisplayBoard();
                }

                if (input.ToLower() == "random")
                {
                    newGame.RandomBoard();
                }

                if (input.ToLower() == "caps")
                {
                    string[] newDictionary = newGame.GetDictionary();
                    for (int i = 0; i < newDictionary.Length; i++)
                    {
                        newDictionary[i] = newDictionary[i].ToUpper();
                        Console.WriteLine(newDictionary[i] + "word #" + i);                        
                    }
                    File.WriteAllLines("C:\\Users\\David\\Desktop\\Code\\C#-VS\\Scrabble\\Scrabble\\Dictionary\\caps.txt", newDictionary);

                }
            } while (input != "exit");

            Console.WriteLine();
            Console.WriteLine("*************************THAT'S ALL FOLKS******************************************************");
            Console.ReadLine();

        }
    }
}
