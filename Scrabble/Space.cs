using System;

namespace Scrabble
{
    public sealed class Space
    {
        //FIELDS

        private Tuple<int, int> _coords;
        public Tile _tile { get; set; }
        public int WordMultiplier { get; set; }
        public int LetterMultiplier { get; set; }
        
        //CONTRUCTOR

        public Space(int x, int y)
        {
            _coords = Tuple.Create(x, y);
            _tile = null;
            WordMultiplier = 1;
            LetterMultiplier = 1;
        }

        public Space (int x, int y, char letter)
        {
            _coords = Tuple.Create(x, y);
            _tile = new Tile(letter);
            WordMultiplier = 1;
            LetterMultiplier = 1;
        }

        public Space (int x, int y, Tile tile)
        {
            _coords = Tuple.Create(x, y);
            _tile = tile;
            WordMultiplier = 1;
            LetterMultiplier = 1;
        }

        //ACCESSORS

        public Tuple<int, int> GetCoords()
        {
            return _coords;
        }

        public int GetX()
        {
            return _coords.Item1;
        }
        
        public int GetY()
        {
            return _coords.Item2;
        }

        public void SetTile(Tile tile)
        {
            _tile = tile;
        }

        public void SetTile(Char c)
        {
            _tile = new Tile(c);
        }

        public void SetTile(String s)
        {
            _tile = new Tile(Convert.ToChar(s));
        }

        public Tile GetTile()
        {
            return _tile;
        }

        public Char? GetTileLetter()
        {
            try
            {
                return _tile.GetLetter();
            }
            catch (NullReferenceException)
            {

                return null;
            }

        }

        public void RemoveTile()
        {
            _tile = null;
        }

        public string GetCoordsString()
        {
            return "(" + _coords.Item1 + "x, " + _coords.Item2 + "y)";
        }

        public string GetString()
        {
            char tileLetter;
            int tileValue;
            if (_tile != null)
            {
                tileLetter = _tile.GetLetter();
                tileValue = _tile.GetValue();
            }
            else
            {
                tileLetter = '_';
                tileValue = 0;
            }
                         
            return string.Format("x = {0}, y = {1}, Letter = {2}, Value = {3}, Letter Multiplier = {4}, Word Multiplier = {5}", GetX(), GetY(), tileLetter, tileValue, LetterMultiplier, WordMultiplier);
        }

        //METHODS

        public bool IsOccupied()
        {
            return (_tile != null);
        }

        public Space GetAdjacentNorth()
        {
            return new Space(GetX(), GetY() - 1);
        }
        public Space GetAdjacentSouth()
        {
            return new Space(GetX(), GetY() + 1);
        }
        public Space GetAdjacentWest()
        {
            return new Space(GetX() - 1, GetY());
        }
        public Space GetAdjacentEast()
        {
            return new Space(GetX() + 1, GetY());
        }

    }
}