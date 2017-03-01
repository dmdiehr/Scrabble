using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Scrabble
{
    public class Play
    {
        //In case you forget, a Play is a Placement with Tiles associated with specific Spaces. 
        //It does not include spaces or tiles from the board. (Those are anchors.) 

        //FIELDS
        private List<Tuple<Space, Tile>> _playList;
        private Game _game;
        private Placement _placement;

        //CONSTRUCTORS
        public Play(List<Tuple<Space, Tile>> playList, Game game)
        {
            _playList = playList;
            _game = game;
            _placement = GetPlacement();            

            if (!_placement.IsValid)
            {
                throw new Exception("This play consists of an invalid placement ");
            }
        }

        public Play(Placement placement, List<Tile> tiles, Game game)
        {

        }

        //ACCESSORS
        private Placement GetPlacement()
        {           
            List<Space> spaceList = new List<Space>();
            foreach (Tuple<Space, Tile> item in _playList)
            {
                spaceList.Add(item.Item1);
            }
            return new Placement(spaceList, _game);
        }
        public string GetPlayString()
        {
            string returnString = "";

            foreach (var item in _playList)
            {
                returnString += item.Item1.GetCoordsString() + ": " + item.Item2.GetLetter();
            }

            return returnString;
        }

        //METHODS

        public List<SubWord> GetSubWords()
        {
            SubWord[] subwords = new SubWord[_playList.Count +1];

            //Single tile placements are a special case
            //Essentially, single tile placements have no primary SubWord
            //And instead have two secondary words. Or at least that's how I'll approach them.
            if (_placement.IsSingle())
            {
                //Get Horizontal SubWord
                SubWord horizontal = _game.SingleSubWord(_playList[0], "horizontal");

                //Get Vertical SubWord
                SubWord vertical = _game.SingleSubWord(_playList[0], "vertical");

                //return subwords
                if (vertical == null)
                {
                    subwords[0] = horizontal;
                    subwords[1] = vertical;
                }
                else if(horizontal == null)
                {
                    subwords[0] = vertical;
                    subwords[1] = horizontal;
                }
                else if (horizontal.ExtractWord().Length >= vertical.ExtractWord().Length)
                {
                    subwords[0] = horizontal;
                    subwords[1] = vertical;
                }
                else
                {
                    subwords[0] = vertical;
                    subwords[1] = horizontal;
                }                

                return subwords.Where(x => x.ExtractWord().Length > 1).ToList();
            }
            List<Tuple<Space, Tile>> mainWord = _playList.ToList();          
            List<Space> anchors = _placement.Anchors;

            foreach (Space anchor in anchors)
            {
                mainWord.Add(Tuple.Create(anchor, anchor.GetTile()));
            }                    
            SubWord primaryWord = new SubWord(mainWord, _game);
            subwords[0] = primaryWord;

            List<Space> playSpaces = _placement.GetSpaceList();
            
            if (_placement.IsHorizontal())
            {
                //Build a vertical subword for each space in the Play
                for (int i = 1; i < subwords.Length; i++)
                {
                    subwords[i] = _game.SingleSubWord(_playList[i - 1], "vertical");
                }

            }
            if (_placement.IsVertical())
            {
                //Build a horizontal subword for each space in the Play
                for (int i = 1; i < subwords.Length; i++)
                {
                    subwords[i] = _game.SingleSubWord(_playList[i - 1], "horizontal");
                }
            }
            return subwords.Where(x => x.ExtractWord().Length > 1).ToList();
        }
     
        public int CalculateScore()
        {
            int score = 0;
            if (_playList.Count == 7)
            {
                score = 50;
            }

            //I removed this try catch because I was trying to figure out what kind of exception I was catching
            //and now the thing isn't breaking so I can't figure it out. I'd guess it was some kind of null reference
            //but I don't like having a catch all, so I'm leaving it like this until it breaks and I can remember why
            //I put it here in the first place.

            //try
            //{
            foreach (SubWord word in GetSubWords())
            {
                score += word.SubWordScore();
            }

            //}
            //catch { }


            return score;
        }

        public bool AreWordsValid()
        {
            foreach (SubWord subWord in GetSubWords())
            {
                if (!_game.GetDictionary().Contains(subWord.Word))
                    return false;
            }
            return true;
        }
    }
}
