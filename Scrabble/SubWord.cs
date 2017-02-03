using System;
using System.Collections.Generic;
using System.Linq;

namespace Scrabble
{
    public class SubWord
    {
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
            foreach (var item in _pairs)
            {
                returnString += item.Item2.GetLetter();                
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
                totalWordMultiplier *= item.Item1.WordMultiplier;
                score += ( item.Item1.LetterMultiplier * item.Item2.GetValue() );
            }

            return score * totalWordMultiplier;
        }


    }
}
