using System;

namespace Scrabble
{
    public class Tile
    {
        //CONTRUCTOR
        public Tile(char c)
        {
            //Blank tiles not yet implemented

            if ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'))
            {
                _letter = Char.ToLower(c);
            }
            else
            {
                throw new ArgumentException("A Tile can only contain an English Letter");
            }

            if ("aeionrtlsu".Contains(_letter + ""))
            {
                _value = 1;
            }
            else if ("dg".Contains(_letter + ""))
            {
                _value = 2;
            }
            else if ("bcmp".Contains(_letter + ""))
            {
                _value = 3;
            }
            else if ("fhvwy".Contains(_letter + ""))
            {
                _value = 4;
            }
            else if ("k".Contains(_letter + ""))
            {
                _value = 5;
            }
            else if ("jx".Contains(_letter + ""))
            {
                _value = 8;
            }
            else if ("qz".Contains(_letter + ""))
            {
                _value = 10;
            }
            else
            {
                throw new Exception("Dude, do you not know the whole alphabet or something?");
            }

        }
        // I should probably add a constructor overload that accepts a string //

        //FIELDS
        private char _letter;
        private int _value;

        //METHODS
        public char GetLetter()
        {
            return _letter;
        }
        public int GetValue()
        {
            return _value;
        }
    }
}