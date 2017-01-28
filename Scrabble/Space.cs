﻿using System;

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

        //ACCESSORS

        public Tuple<int, int> GetCoords()
        {
            return _coords;
        }

        public string GetCoordsString()
        {
            return "(" + _coords.Item1 + "X, " + _coords.Item2 + "Y)";
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

        public Char GetTileLetter()
        {
            return _tile.GetLetter();
        }

        public void RemoveTile()
        {
            _tile = null;
        }

        //METHODS

        public bool IsOccupied()
        {
            return (_tile != null);
        }      
    }
}