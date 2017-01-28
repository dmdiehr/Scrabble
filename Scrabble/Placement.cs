using System;
using System.Collections.Generic;

namespace Scrabble
{

    public class Placement
    {
        //FIELDS
        private List<Space> _spaceList;
        
        //CONSTRUCTOR
        public Placement(List<Space> spaceList = null)
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

        //ACCESSORS

        public List<Space> GetSpaceList()
        {
            return _spaceList;
        }

        public string GetSpaceListString()
        {
            string returnString = "";

            foreach (var item in _spaceList)
            {
                returnString += item.GetCoordsString() + "\n"; 
            }

            return returnString;
        }

        public void SetSpaceList(List<Space> spaceList)
        {
            _spaceList = spaceList;
        }
        public void SetSpaceList(Space space)
        {
            _spaceList = new List<Space>() {space};
        }


        //METHODS

        public bool IsSingle(Game game)
        {
            if (_spaceList == null)
               return false;
            else
               return (_spaceList.Count == 1);
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