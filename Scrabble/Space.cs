using System;

namespace Scrabble
{
    public class Space
    {
        //CONTRUCTOR

        public Space(int x, int y)
        {
            _coords = Tuple.Create(x, y);
            _tile = null;
            WordMultiplier = 1;
            LetterMultiplier = 1;
        }

        //FIELDS

        private Tuple<int, int> _coords;
        private Tile _tile;
        public int WordMultiplier { get; set; }
        public int LetterMultiplier { get; set; }


        //METHODS

        public Tuple<int, int> GetCoords()
        {
            return _coords;
        }

        public string GetCoordsString()
        {
            return "(" + _coords.Item1 + "X, " + _coords.Item2 + "Y)";
        }

        public bool IsOccupied()
        {
            return (_tile != null);
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

        public Char GetTileLetter()
        {
            return _tile.GetLetter();
        }

        public void RemoveTile()
        {
            _tile = null;
        }
    }
}