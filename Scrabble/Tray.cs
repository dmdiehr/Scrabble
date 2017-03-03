using System;
using System.Collections.Generic;
using System.Linq;

namespace Scrabble
{
    public class Tray
    {
        //CONSTRUCTOR
        public Tray(string tray)
        {
            if (tray.Length > 7 && tray.Length < 1)
            {
                throw new ArgumentException("The Tray must have between 1 and 7 Tiles");
            }

            List<Tile> newTileList = new List<Tile>();

            foreach (char c in tray)
            {
                Tile newTile = new Tile(c);
                newTileList.Add(newTile);
            }
            Tiles = newTileList;
        }

        public Tray(ICollection<Tile> tiles)
        {
            Tiles = tiles.ToList();
        }

        //FIELDS
        public List<Tile> Tiles { get; set; }

        //ACCESSORS
        public string GetTilesString()
        {
            string returnString = "";

            foreach (Tile tile in Tiles)
            {
                returnString += tile.GetLetter();
            }
            return returnString;
        }
    }
}