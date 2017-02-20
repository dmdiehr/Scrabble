using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrabble
{
    public class Play
    {
        //In case you forget, a Play is a Placement with Tiles associated with specific Spaces. 
        //It does not include spaces or tiles from the board. (Those are anchors.) 

        //FIELDS
        private List<Tuple<Space, Tile>> _playList;
        private Game _game;
        private SubWord[] _subWords;
        private int _score;

        //CONSTRUCTORS
        public Play(List<Tuple<Space, Tile>> playList, Game game)
        {
            _playList = playList;
            _game = game;
            _subWords = SubWords();
            _score = CalculateScore();

            if (!GetPlacement().IsValid(game))
            {
                throw new Exception("This play consists of an invalid placement ");
            }
        }

        //ACCESSORS

        public Placement GetPlacement()
        {
            List<Space> placementList = new List<Space>();
            foreach (Tuple<Space, Tile> item in _playList)
            {
                placementList.Add(item.Item1);
            }
            return new Placement(placementList);
        }

        public List<SubWord> GetSubWords()
        {
            List<SubWord> returnList = new List<SubWord>();
            foreach (SubWord subWord in _subWords)
            {
                if (subWord.Word.Length > 1)
                {
                    returnList.Add(subWord);
                }
            }
            return returnList;
        }

        public int GetScore()
        {
            return _score;
        }

        //METHODS

        private SubWord[] SubWords()
        {
            SubWord[] subwords = new SubWord[_playList.Count +1];

            //Single tile placements are a special case
            //Essentially, single tile placements have no primary SubWord
            //And instead have two secondary words. Or at least that's how I'll approach them.
            if (GetPlacement().IsSingle())
            {
                //Get Horizontal SubWord
                SubWord horizontal = SingleSubWord(_playList[0], "horizontal");

                //Get Vertical SubWord
                SubWord vertical = SingleSubWord(_playList[0], "vertical");

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

                return subwords;
            }

            List<Tuple<Space, Tile>> mainWord = _playList.ToList();          
            Placement thisPlacement = GetPlacement();
            List<Space> anchors = thisPlacement.GetAnchors(_game);

            foreach (Space anchor in anchors)
            {
                mainWord.Add(Tuple.Create(anchor, anchor.GetTile()));
            }                    

            SubWord primaryWord = new SubWord(mainWord, _game);
            subwords[0] = primaryWord;

            List<Space> playSpaces = thisPlacement.GetSpaceList();
            


            if (GetPlacement().IsHorizontal())
            {
                //Build a vertical subword for each space in the Play
                for (int i = 1; i < subwords.Length; i++)
                {
                    subwords[i] = SingleSubWord(_playList[i - 1], "vertical");
                }

            }

            if (GetPlacement().IsVertical())
            {
                //Build a horizontal subword for each space in the Play
                for (int i = 1; i < subwords.Length; i++)
                {
                    subwords[i] = SingleSubWord(_playList[i - 1], "horizontal");
                }
            }
            return subwords;
        }

        public SubWord SingleSubWord(Tuple<Space, Tile> tuple, string direction)
        {
            #region //Custom Exceptions for Parameter Checking
            if (direction != "vertical" && direction != "horizontal")
                throw new Exception("The direction parameter must be \"vertical\" or \"horizontal\"");
            if (tuple.Item2 == null)
                throw new Exception("The space parameter must have a space whose tile is not null");
            #endregion

            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>> { tuple };
            Space currentSpace = tuple.Item1;

            if (direction == "horizontal")
            {
                while (_game.GetSpace(currentSpace.GetAdjacentWest()) != null && _game.GetSpace(currentSpace.GetAdjacentWest()).GetTile() != null)
                {
                    currentSpace = currentSpace.GetAdjacentWest();
                    tupleList.Add(Tuple.Create(_game.GetSpace(currentSpace), currentSpace.GetTile()));                    
                }

                currentSpace = tuple.Item1;

                while (_game.GetSpace(currentSpace.GetAdjacentEast()) != null && _game.GetSpace(currentSpace.GetAdjacentEast()).GetTile() != null)
                {
                    currentSpace = currentSpace.GetAdjacentEast();
                    tupleList.Add(Tuple.Create(_game.GetSpace(currentSpace), currentSpace.GetTile()));
                }
            }

            if (direction == "vertical")
            {
                while (_game.GetSpace(currentSpace.GetAdjacentNorth()) != null && _game.GetSpace(currentSpace.GetAdjacentNorth()).GetTile() != null)
                {
                    currentSpace = currentSpace.GetAdjacentNorth();
                    tupleList.Add(Tuple.Create(_game.GetSpace(currentSpace), currentSpace.GetTile()));
                }

                currentSpace = tuple.Item1;

                while (_game.GetSpace(currentSpace.GetAdjacentSouth()) != null && _game.GetSpace(currentSpace.GetAdjacentSouth()).GetTile() != null)
                {
                    currentSpace = currentSpace.GetAdjacentSouth();
                    tupleList.Add(Tuple.Create(_game.GetSpace(currentSpace), currentSpace.GetTile()));
                }
            }

            return new SubWord(tupleList, _game);

        }

        public int CalculateScore()
        {
            int score = 0;

            try
            {
                foreach (SubWord word in _subWords)
                {
                    score += word.SubWordScore();
                }

            }
            catch { }

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
