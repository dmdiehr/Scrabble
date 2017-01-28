using System;
using System.Collections.Generic;

namespace Scrabble
{
    public class Placement
    {
        //CONSTRUCTOR
        public Placement(List<Space> spaceList)
        {
            _spaceList = spaceList;
        }

        //CONSTRUCTOR OVERLOADS
        public Placement(Space space)
        {
            List<Space> spaceList = new List<Space>();
            spaceList.Add(space);

            _spaceList = spaceList;
        }


        //FIELDS
        private List<Space> _spaceList;

        //METHODS

       public List<Space> GetSpaceList()
        {
            return _spaceList;
        }

        public bool IsSingle(Game game)
        {
            return true;
        }

        public bool IsHorizontal(Game game)
        {
            return true;
        }

        public bool IsVertical(Game game)
        {
            return true;
        }

        public bool IsStraight(Game game)
        {
            return true;
        }

        public bool IsContiguous(Game game)
        {
            return true;
        }

        public bool IsUnique(Game game)
        {
            return true;
        }

        public bool IsOnBoard(Game game)
        {            
            Space[,] board = game.GetBoard();
            foreach (Space placementSpace in _spaceList)
            {
                bool onBoard = false;
                foreach (Space boardSpace in board)
                {
                    if (placementSpace.GetCoordsString() == boardSpace.GetCoordsString())
                    {
                        onBoard = true;
                    }                 
                }
                if (!onBoard)
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsLegal(Game game)
        {
            return (this.IsUnique(game) && this.IsStraight(game) && this.IsContiguous(game));
        }

        public void PlacementSort()
        {
            _spaceList.Sort(new SpaceComparer());           
        }

    }
}