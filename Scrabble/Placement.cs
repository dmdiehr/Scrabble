using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ExtentionMethods;

namespace Scrabble
{

    public sealed class Placement
    {
        //FIELDS
        private List<Space> _spaceList;
        private Game _game;
        public bool IsValid { get; }
        public List<Space> Anchors { get; }

        //CONSTRUCTOR
        public Placement(List<Space> spaceList, Game game)
        {
            _spaceList = spaceList;
            _spaceList.Sort(SpaceComparer.Instance);
            _game = game;
            IsValid = IsContiguous() && IsOnBoard() && (IsAdjacent() || IsFirstMove());
            Anchors = GetAnchors();
        }
        //CONSTRUCTOR OVERLOADS
        public Placement(Space space, Game game)
        {
            List<Space> spaceList = new List<Space>();
            spaceList.Add(space);

            _spaceList = spaceList;
            _game = game;
            IsValid = IsContiguous() && IsOnBoard() && (IsAdjacent() || IsFirstMove());
            Anchors = GetAnchors();
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

        public bool IsOnBoard()
        {
            Space[,] board = _game.GetBoard();
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

        public bool IsAvailable()
        {
            foreach (var item in _spaceList)
            {

                if (_game.GetSpace(item).IsOccupied())
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

        public bool IsFirstMove()
        {
            foreach (Space space in _game.GetBoard())
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

        public bool IsAdjacent()
        {

            foreach (Space space in _spaceList)
            {
                try
                {
                    if (_game.GetSpace(space.GetAdjacentNorth()).IsOccupied())
                        return true;
                }
                catch (IndexOutOfRangeException) { }
                catch (NullReferenceException) { }
                try
                {
                    if (_game.GetSpace(space.GetAdjacentSouth()).IsOccupied())
                        return true;
                }
                catch (IndexOutOfRangeException) { }
                catch (NullReferenceException) { }

                try
                {
                    if (_game.GetSpace(space.GetAdjacentEast()).IsOccupied())
                        return true;
                }
                catch (IndexOutOfRangeException) { }
                catch (NullReferenceException) { }

                try
                {
                    if (_game.GetSpace(space.GetAdjacentWest()).IsOccupied())
                        return true;
                }
                catch (IndexOutOfRangeException) { }
                catch (NullReferenceException) { }
            }
            return false;
        }

        public bool IsContiguous()
        {
            if (!IsStraight() || !HasNoDuplicates() || !IsAvailable())
                return false;
            if (IsSingle())
                return true;
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
                    if (!listContains && (_game.GetSpace(xValue, y).GetTile() == null))
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
                    if (!listContains && (_game.GetSpace(x, yValue).GetTile() == null))
                        return false;
                }
            }

            return true;
        }

        private List<Space> GetAnchors()
        {
            if (!IsValid)                
                return null;

            List<Space> anchors = new List<Space> { };
            //make sure to test what happens to this when you run up against the edge of the board

            if (IsHorizontal())
            {

                // Anchors before placement
                Space currentSpace = _spaceList[0];
                while (_game.GetSpace(currentSpace.GetAdjacentWest()) != null && _game.GetSpace(currentSpace.GetAdjacentWest()).GetTile() != null)
                {
                    anchors.Add(_game.GetSpace(currentSpace.GetAdjacentWest()));
                    currentSpace = currentSpace.GetAdjacentWest();

                }

                // Anchors mid placement
                //This may be a cheap way to do this, but since the Placement is already checked for validity
                //it's easiest to simply add any space in the "placement zone" already has a tile 

                int yValue = _spaceList[0].GetY();

                for (int x = _spaceList[0].GetX(); x < _spaceList.Last().GetX(); x++)
                {
                    if (_game.GetSpace(x,yValue).GetTile() != null)
                    {
                        anchors.Add(_game.GetSpace(x, yValue));
                    }
                }

                // Anchors after placement
                currentSpace = _spaceList.Last();
                while (_game.GetSpace(currentSpace.GetAdjacentEast()) != null && _game.GetSpace(currentSpace.GetAdjacentEast()).GetTile() != null)
                {
                    anchors.Add(_game.GetSpace(currentSpace.GetAdjacentEast()));
                    currentSpace = currentSpace.GetAdjacentEast();

                }
            }
            else if (IsVertical())
            {

                // Anchors before placement
                Space currentSpace = _spaceList[0];
                while (_game.GetSpace(currentSpace.GetAdjacentNorth()) != null && _game.GetSpace(currentSpace.GetAdjacentNorth()).GetTile() != null)
                {                  
                    anchors.Add(_game.GetSpace(currentSpace.GetAdjacentNorth()));
                    currentSpace = currentSpace.GetAdjacentNorth();

                }

                // Anchors mid placement
                //This may be a cheap way to do this, but since the Placement is already checked for validity
                //it's easiest to simply add any space in the "placement zone" already has a tile 

                int xValue = _spaceList[0].GetX();

                for (int y = _spaceList[0].GetY(); y < _spaceList.Last().GetY(); y++)
                {
                    if (_game.GetSpace(xValue, y).GetTile() != null)
                    {
                        anchors.Add(_game.GetSpace(xValue, y));
                    }
                }

                // Anchors after placement
                currentSpace = _spaceList.Last();
                while (_game.GetSpace(currentSpace.GetAdjacentSouth()) != null && _game.GetSpace(currentSpace.GetAdjacentSouth()).GetTile() != null)
                {
                    anchors.Add(_game.GetSpace(currentSpace.GetAdjacentSouth()));
                    currentSpace = currentSpace.GetAdjacentSouth();

                }
            }

            return anchors;
        }

        public List<Play> ValidPlays()
        {
            List<Play> plays = new List<Play>();

            if (IsSingle())
            {
                bool blankExists = false;
                int blankIndex = -1;
                List<Tile> tiles = _game.GetTray().Tiles;
                for (int i = 0; i <tiles.Count; i++)
                {
                    if(tiles[i].GetLetter() == '?')
                    {
                        blankExists = true;
                        blankIndex = i;
                        continue;
                    }
                    Play thisPlay = new Play(new List<Tuple<Space, Tile>> { Tuple.Create(_spaceList[0], tiles[i]) }, _game);
                    if (thisPlay.AreWordsValid())
                    {
                        plays.Add(thisPlay);
                    }
                }
                if (blankExists)
                {
                    for (int i = 0; i < 26; i++)
                    {
                        char tempChar = (char)('A' + i);
                        Tile blankTile = new Tile('?');
                        blankTile.SetBlank(tempChar);

                        Play thisPlay = new Play(new List<Tuple<Space, Tile>> { Tuple.Create(_spaceList[0], blankTile) }, _game);
                        if (thisPlay.AreWordsValid())
                        {
                            plays.Add(thisPlay);
                        }
                        
                    }
                }
                return plays;
            }
            
            string tray = _game.GetTrayString();

            //first find legal primary subwords by getting the tray and anchors and running them through wordfind
            //Find relative position and letters of anchors                        

            List<Space> primaryWordSpaces = new List<Space>();
            primaryWordSpaces.AddRange(GetAnchors());
            primaryWordSpaces.AddRange(_spaceList);
            primaryWordSpaces.Sort(SpaceComparer.Instance);   

            List<Tuple<int, char>> anchors = new List<Tuple<int, char>>();

            for (int i = 0; i < primaryWordSpaces.Count; i++)
            {
                if(primaryWordSpaces[i].GetTile() != null)
                {
                    anchors.Add(Tuple.Create(i, primaryWordSpaces[i].GetTile().GetLetter()));
                }
            }
            
            // Create exclusion tuples 
            // Should this be coupled with creating the anchor tuples?
            // That would be faster, but less granular

            List<Tuple<int, char>> exclusions = new List<Tuple<int, char>>();
            string subWordDirection = "";
            if (IsHorizontal())
                subWordDirection = "vertical";
            else if (IsVertical())
                subWordDirection = "horizontal";  
            for(int i = 0; i< primaryWordSpaces.Count; i++)
            {
                //check to see if this letter is an anchor, if so skip
                if (primaryWordSpaces[i].GetTile() != null)
                    continue;

                foreach (char letter in tray)
                {
                    //this will need to get fixed at some point
                    if (letter == '?')
                        continue;
                    ////////////////////////////////////////////

                    string word = _game.SingleSubWord(Tuple.Create(primaryWordSpaces[i], new Tile(letter)), subWordDirection).Word;
                    if (!_game.GetDictionary().Contains(word))
                    {
                        exclusions.Add(Tuple.Create(i, letter));
                    }

                }
            } 

            List<string> possiblePrimaryWords = _game.GetDictionary().WordFind(tray, primaryWordSpaces.Count, anchors, exclusions);

            //Now remove the anchors so the remaining letters can be matched up to the spaces in the Placement
            List<Tuple<int, char>> sortedAnchors = anchors.OrderBy(x => x.Item1).ToList();

            for(int i = 0; i < possiblePrimaryWords.Count; i++)
            {               
                string newWord = possiblePrimaryWords[i];
                for (int j = 0; j < sortedAnchors.Count; j++)
                {              
                    if (newWord[sortedAnchors[j].Item1 - j] != sortedAnchors[j].Item2)
                        throw new Exception("Bad design bro, your anchors aren't where you think they are!");

                    newWord = newWord.Remove(sortedAnchors[j].Item1 - j, 1);

                }
                possiblePrimaryWords[i] = newWord;
            }

            //At this point possiblePrimaryWords should have been scrubbed of the anchor letters

            foreach (string word in possiblePrimaryWords)
            {
                //this.PlacementSort();
                if (word.Length != _spaceList.Count)
                    throw new Exception("You done messed up! You're Play ain't the same size as your Placement.");
                
                List<string> possibleCombinations = word.WildCardCombinations(_game.GetTrayString());
                foreach (string combo in possibleCombinations)
                {
                    List<Tuple<Space, Tile>> newPlayList = new List<Tuple<Space, Tile>>();
                    for (int i = 0; i < _spaceList.Count; i++)
                    {
                        Tile thisTile = new Tile(combo[i]);
                        if (combo[i] == '?')
                            thisTile.SetBlank(word[i]);

                        newPlayList.Add(Tuple.Create(_spaceList[i], thisTile));
                       
                    }
                    plays.Add(new Play(newPlayList, _game));                    
                }
            }
            //At this point Plays should be populated with a list of Plays that have valid primary subwords
            //Now we vet that list by checking secondary subwords

            plays.RemoveAll(p => !p.AreWordsValid());
            return plays;
        }

    }
}