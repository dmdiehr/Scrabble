using System;

namespace Scrabble
{
    public class Tile
    {
        //CONTRUCTOR
        public Tile(char c)
        {
            //Blank tiles not yet implemented

            if ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || (c == '?'))
            {
                _letter = Char.ToUpper(c);
            }
            else
            {
                throw new ArgumentException("A Tile can only contain an English Letter or '?' (representing a blank tile)");
            }

            if (_letter == '?')
            {
                _value = 0;
            }           
            else if ("AEIONRTLSU".Contains(_letter + ""))
            {
                _value = 1;
            }
            else if ("DG".Contains(_letter + ""))
            {
                _value = 2;
            }
            else if ("BCMP".Contains(_letter + ""))
            {
                _value = 3;
            }
            else if ("FHVWY".Contains(_letter + "")) 
            {
                _value = 4;
            }
            else if ("K".Contains(_letter + ""))
            {
                _value = 5;
            }
            else if ("JX".Contains(_letter + ""))
            {
                _value = 8;
            }
            else if ("QZ".Contains(_letter + ""))
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
        public void SetBlank(char c)
        {
            if (this._letter != '?')
                throw new InvalidOperationException("SetBlank can only be performed on a Tile with the value '?'");

            _letter = c;
        }
    }
}