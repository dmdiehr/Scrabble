using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrabble
{
    public class Play
    {
        //FIELDS
        private List<Tuple<Space, Tile>> _playList;
        private Game _game;
        private List<Tuple<Space, Tile>>[] _subPlays;
        private int _score;
        
        //CONSTRUCTORS
        public Play(List<Tuple<Space, Tile>> playList, Game game)
        {
            _playList = playList;
            _game = game;
            _subPlays = FindSubPlays();
            _score = CalculateScore();
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

        public List<Tuple<Space, Tile>>[] FindSubPlays()
        {
            //the array should be (_playlist.Count + 1) in size
            //the primary subplay will be the orientation of the placement
            //all other subplays will be the alternate orientation
            //might have be specific when it somes to single tile plays

            //will probably have to build the primary and secondary subplays separately
            
            // the secondary subplays should be easy - just start with the tuple we have and add the tuples north and south (or east/west)
            // until we find a null tile in each direction

            //the primary subplay will have to account for the possible mix of mutiple gaps in the placement filled by the board and vice/versa.
            throw new NotImplementedException();
        }

        public void SubPlaySort(List<Tuple<Space, Tile>> subPlay)
        {
            //this might have to be an exention method or otherwise subPlays may need to be their own class
        }
        public int CalculateScore()
        {
            //iterate through each item in _SubPlays
            //if the SubPlay length == 1 ignore it
            // for each subplay, (subscore += (tile.value * letterMultiplier) for each letter) * (all wordmultipliers * each other))
            // add all subscores together
            throw new NotImplementedException();
        }

        public bool AreWordsValid()
        {
            foreach (var item in _subPlays)
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
