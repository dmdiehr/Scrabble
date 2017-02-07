using System;
using System.Collections.Generic;
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

        public Play() { }

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

                //Get Vertical SubWord

                //return subwords
                
            }

            List<Space> mainWord = new List<Space>();

            //First, convert the playList into Spaces with imbedded tiles
            //and include anchors from the board
            
            foreach (Tuple<Space, Tile> item in _playList)
            {
                mainWord.Add(new Space(item.Item1.GetX(), item.Item1.GetY(), item.Item2));
            }

            Placement thisPlacement = GetPlacement();
            List<Space> anchors = thisPlacement.GetAnchors(_game);

            mainWord.AddRange(anchors);

            mainWord.Sort(SpaceComparer.Instance);

            SubWord primaryWord = new SubWord(mainWord);
            subwords[0] = primaryWord;

            List<Space> playSpaces = thisPlacement.GetSpaceList();
            


            if (GetPlacement().IsHorizontal())
            {
                //Build a vertical subword for each space in the Play
            }

            if (GetPlacement().IsVertical())
            {
                //Build a horizontal subword for each space in the Play
            }

            throw new NotImplementedException();
        }

        public int CalculateScore()
        {
            int score = 0;

            foreach (SubWord word in _subWords)
            {
                score += word.SubWordScore();
            }

            return score;
        }

        public bool AreWordsValid()
        {
            foreach (var item in _subWords)
            {
                //order each subplay
                //if the subplay is length == 1 ignore it
                // concatenate the tile letters into a string
                // check the string against game._dictionary
                //return false if not in the dictionary
            }
            return true;
        }

        public bool IsPlacementValid()
        {
            return GetPlacement().IsValid(_game);
        }

        public bool IsPlayValid()
        {
            return IsPlacementValid() && AreWordsValid();
        }

    }
}
