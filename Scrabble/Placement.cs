using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Scrabble
{

    public sealed class Placement
    {
        //FIELDS
        private List<Space> _spaceList;

        //CONSTRUCTOR
        public Placement(List<Space> spaceList = null)
        {
            if (spaceList == null)
            {
                spaceList = new List<Space>();
            }
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
            _spaceList = new List<Space>() { space };
        }


        //METHODS

        public override bool Equals(object obj)
        {

            if (obj == null)
                return false;
            if (ReferenceEquals(obj, this))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            Placement rhs = obj as Placement;

            if (this._spaceList.Count != rhs._spaceList.Count)
                return false;

            this.PlacementSort();
            rhs.PlacementSort();

            for (int i = 0; i < this._spaceList.Count; i++)
            {
                if (!SpaceCoordsEqualityComparer.Instance.Equals(this._spaceList[i], rhs._spaceList[i]))
                    return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            try
            {
                return this._spaceList[0].GetX() ^ this._spaceList[0].GetY();
            }
            catch
            {
                return 0;
            }
           
        }

        public bool IsOnBoard(Game game)
        {
            Space[,] board = game.GetBoard();
            foreach (Space placementSpace in _spaceList)
            {
                bool onBoard = false;
                foreach (Space boardSpace in board)
                {
                    if (SpaceCoordsEqualityComparer.Instance.Equals(placementSpace, boardSpace))
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
            if (_spaceList == null)
                return true;

            return (_spaceList.Count == _spaceList.Distinct(SpaceCoordsEqualityComparer.Instance).Count());
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
            if (_spaceList.Count <=1)
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
            if (_spaceList.Count <= 1)
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

        public bool IsFirstMove(Game game)
        {
            foreach (Space space in game.GetBoard())
            {
                if (space.GetTile() != null)
                    return false;
            }

            foreach (Space space in _spaceList)
            {
                if (space.GetCoords().Equals(Tuple.Create(7, 7)))
                    return true;
            }
            return false;
        }

        public bool IsAdjacent(Game game)
        {

            foreach (Space space in _spaceList)
            {
                try
                {
                    if (game.GetSpace(space.GetAdjacentNorth()).IsOccupied())
                        return true;
                }
                catch (IndexOutOfRangeException) { }
                catch (NullReferenceException) { }
                try
                {
                    if (game.GetSpace(space.GetAdjacentSouth()).IsOccupied())
                        return true;
                }
                catch (IndexOutOfRangeException) { }
                catch (NullReferenceException) { }

                try
                {
                    if (game.GetSpace(space.GetAdjacentEast()).IsOccupied())
                        return true;
                }
                catch (IndexOutOfRangeException) { }
                catch (NullReferenceException) { }

                try
                {
                    if (game.GetSpace(space.GetAdjacentWest()).IsOccupied())
                        return true;
                }
                catch (IndexOutOfRangeException) { }
                catch (NullReferenceException) { }
            }
            return false;
        }

        public bool IsContiguous(Game game)
        {
            if (!IsStraight() || !HasNoDuplicates() || !IsAvailable(game))
                return false;
            if (IsSingle())
                return true;

            PlacementSort();
            if (IsVertical())
            {
                int firstY = _spaceList[0].GetY();
                int lastY = _spaceList.Last().GetY();
                int xValue = _spaceList[0].GetX();
                bool listContains = true;

                for (int y = firstY + 1; y < lastY; y++)
                {
                    listContains = false;
                    foreach (Space space in _spaceList)
                    {
                        if (space.GetCoords().Equals(new Space(xValue, y).GetCoords()))
                        {
                            listContains = true;
                            break;
                        }
                    }
                    if (!listContains && (game.GetSpace(xValue, y).GetTile() == null))
                        return false;
                }
            }
            else if (IsHorizontal())
            {
                int firstX = _spaceList[0].GetX();
                int lastX = _spaceList.Last().GetX();
                int yValue = _spaceList[0].GetY();
                bool listContains = true;

                for (int x = firstX + 1; x < lastX; x++)
                {
                    listContains = false;
                    foreach (Space space in _spaceList)
                    {
                        if (space.GetCoords().Equals(new Space(x, yValue).GetCoords()))
                        {
                            listContains = true;
                            break;
                        }
                    }
                    if (!listContains && (game.GetSpace(x, yValue).GetTile() == null))
                        return false;
                }
            }

            return true;
        }

        public bool IsValid(Game game)
        {
            return IsContiguous(game) && IsOnBoard(game) && (IsAdjacent(game) || IsFirstMove(game));
        }

        public void PlacementSort()
        {
            _spaceList.Sort(SpaceComparer.Instance);
        }

        public List<Space> GetAnchors(Game game)
        {
            if (!IsValid(game))
                throw new Exception("GetAnchors method cannot be run on an invalid Placement instance");

            List<Space> anchors = new List<Space> { };
            PlacementSort();

            //make sure to test what happens to this when you run up against the edge of the board

            if (IsSingle() || IsHorizontal())
            {

                // Anchors before placement
                Space currentSpace = _spaceList[0];
                while (game.GetSpace(currentSpace.GetAdjacentWest()) != null && game.GetSpace(currentSpace.GetAdjacentWest()).GetTile() != null)
                {
                    Debug.WriteLine("Pre Add");
                    anchors.Add(game.GetSpace(currentSpace.GetAdjacentWest()));
                    currentSpace = currentSpace.GetAdjacentWest();

                }

                // Anchors mid placement
                //This may be a cheap way to do this, but since the Placement is already checked for validity
                //it's easiest to simply add any space in the "placement zone" already has a tile 

                int yValue = _spaceList[0].GetY();

                for (int x = _spaceList[0].GetX(); x < _spaceList.Last().GetX(); x++)
                {
                    if (game.GetSpace(x,yValue).GetTile() != null)
                    {
                        Debug.WriteLine("Middle Add!");
                        anchors.Add(game.GetSpace(x, yValue));
                    }
                }

                // Anchors after placement
                currentSpace = _spaceList.Last();
                Debug.WriteLine("SpaceList Last = " + _spaceList.Last().GetString());
                while (game.GetSpace(currentSpace.GetAdjacentEast()) != null && game.GetSpace(currentSpace.GetAdjacentEast()).GetTile() != null)
                {
                    Debug.WriteLine("Post Add!");
                    anchors.Add(game.GetSpace(currentSpace.GetAdjacentEast()));
                    currentSpace = currentSpace.GetAdjacentEast();

                }
            }
            else if (IsVertical())
            {

                // Anchors before placement
                Space currentSpace = _spaceList[0];
                while (game.GetSpace(currentSpace.GetAdjacentNorth()) != null && game.GetSpace(currentSpace.GetAdjacentNorth()).GetTile() != null)
                {                  
                    anchors.Add(game.GetSpace(currentSpace.GetAdjacentNorth()));
                    currentSpace = currentSpace.GetAdjacentNorth();

                }

                // Anchors mid placement
                //This may be a cheap way to do this, but since the Placement is already checked for validity
                //it's easiest to simply add any space in the "placement zone" already has a tile 

                int xValue = _spaceList[0].GetX();

                for (int y = _spaceList[0].GetY(); y < _spaceList.Last().GetY(); y++)
                {
                    if (game.GetSpace(xValue, y).GetTile() != null)
                    {
                        anchors.Add(game.GetSpace(xValue, y));
                    }
                }

                // Anchors after placement
                currentSpace = _spaceList.Last();
                while (game.GetSpace(currentSpace.GetAdjacentSouth()) != null && game.GetSpace(currentSpace.GetAdjacentSouth()).GetTile() != null)
                {
                    anchors.Add(game.GetSpace(currentSpace.GetAdjacentSouth()));
                    currentSpace = currentSpace.GetAdjacentSouth();

                }
            }

            return anchors;
        }

        public List<Play> ValidPlays(Game game)
        {
            List<Play> plays = new List<Play> { };




            return plays;
        }
    }
}