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

        public bool IsAvailable(Game game)
        {
            foreach (var item in _spaceList)
            {
                if (game.GetSpace(item).IsOccupied())
                    return false;
            }
            return true;
        }

        public bool HasNoDuplicates()
        {
            if (_spaceList == null || IsSingle())
                return true;

            for (int i = 0; i < _spaceList.Count; i++)
            {
                for (int j = i+1; j < _spaceList.Count; j++)
                {
                    if (_spaceList[i].GetCoordsString() == _spaceList[j].GetCoordsString())
                        return false;
                }
            }

            return true;
        }

        public bool IsSingle()
        {
            if (_spaceList == null)
               return false;
            else
               return (_spaceList.Count == 1);
        }

        public bool IsHorizontal()
        {
            if (IsSingle() || (_spaceList == null))
                return false;
            for (int i = 1; i < _spaceList.Count; i++)
            {
                if (_spaceList[0].GetY() != _spaceList[i].GetY())
                    return false;
            }
            return true;
        }

        public bool IsVertical()
        {
            if (IsSingle() || (_spaceList == null))
                return false;
            for (int i = 1; i < _spaceList.Count; i++)
            {
                if (_spaceList[0].GetX() != _spaceList[i].GetX())
                    return false;
            }
            return true;
        }

        public bool IsStraight()
        {
            return IsSingle() || IsHorizontal() || IsVertical();
        }

        public bool IsContiguous(Game game)
        {
            return true;
        }

        public bool IsLegal(Game game)
        {
            return IsAvailable(game) && IsStraight() && IsContiguous(game) && IsOnBoard(game);
        }

        public void PlacementSort()
        {
            _spaceList.Sort(new SpaceComparer());           
        }

    }
}