using System;
using System.Collections.Generic;
using System.Linq;

namespace Scrabble
{
    public class SubWord
    {
        //A Subword, unlike a Play, does in fact include spaces and tiles from the board.
        
        //FIELDS
        private List<Space> _pairs;

        private string _word;
        public string Word { get { return _word; } }

        private int _score;
        public int Score { get { return _score; } }

        //CONSTURCTORS
        public SubWord(List<Space> pairs)
        {
            _pairs = pairs;
            _word = ExtractWord();
            _score = SubWordScore();

            _pairs.Sort(SpaceComparer.Instance);

            foreach (Space space in _pairs)
            {
                if (space.GetTile() == null)
                    throw new Exception("You're constructing your SubWord with empty Spaces... don't do that.");
            }

        }

        //METHODS
        public string ExtractWord()
        {
            if (_pairs.Count == 1)
                return "";

            string returnString = "";
            foreach (var item in _pairs)
            {
                returnString += item.GetTileLetter();                
            }

            return returnString;
        }

        public int SubWordScore()
        {
            if (_pairs.Count == 1)
                return 0;

            int score = 0;
            int totalWordMultiplier = 1;

            foreach (var item in _pairs)
            {
                totalWordMultiplier *= item.WordMultiplier;
                score += ( item.LetterMultiplier * item.GetTile().GetValue());
            }

            return score * totalWordMultiplier;
        }


    }
}
