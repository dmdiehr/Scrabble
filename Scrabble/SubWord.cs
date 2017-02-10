using System;
using System.Collections.Generic;
using System.Linq;

namespace Scrabble
{
    public class SubWord
    {
        //A Subword, unlike a Play, does in fact include spaces and tiles from the board.
        
        //FIELDS
        private List<Tuple<Space, Tile>> _pairs;

        private string _word;
        public string Word { get { return _word; } }

        private int _score;
        public int Score { get { return _score; } }

        //CONSTURCTORS
        public SubWord(List<Tuple<Space, Tile>> pairs)
        {

            _pairs = pairs.OrderBy(x => x.Item1, SpaceComparer.Instance).ToList();
            _word = ExtractWord();
            _score = SubWordScore();           
        }

        //METHODS
        public string ExtractWord()
        {
            if (_pairs.Count == 1)
                return "";

            string returnString = "";
            foreach (Tuple<Space, Tile> pair in _pairs)
            {
                try
                {
                    returnString += pair.Item2.GetLetter();
                }
                catch (NullReferenceException)
                {

                    returnString += pair.Item1.GetTileLetter();
                }
                          
            }

            return returnString;
        }

        public int SubWordScore()
        {
            if (_pairs.Count == 1)
                return 0;

            int score = 0;
            int totalWordMultiplier = 1;

            foreach (Tuple<Space, Tile> pair in _pairs)
            {
                totalWordMultiplier *= pair.Item1.WordMultiplier;
                try
                {
                    score += (pair.Item1.LetterMultiplier * pair.Item2.GetValue());
                }
                catch (NullReferenceException)
                {

                    score += (pair.Item1.LetterMultiplier * pair.Item1.GetTile().GetValue());
                }
                
            }

            return score * totalWordMultiplier;
        }


    }
}
