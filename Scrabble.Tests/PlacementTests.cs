using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Scrabble.Tests
{
    [TestFixture]
    public class PlacementTests
    {
        #region //IsOnBoard

        [Test]
        [Category("IsOnBoard")]
        public void IsOnBoard_SingleSpaceInMiddle()
        {
            Placement sut;
            bool result = false;

            //Arrange
            Space newSpace = new Space(7, 7);
            List<Space> newList = new List<Space> { newSpace };

            sut = new Placement(newList);
            Game newGame = new Game();
            //Act
            result = sut.IsOnBoard(newGame);

            //Assert
            Assert.That(result, Is.True);

        }

        [Test]
        [Category("IsOnBoard")]
        public void IsOnBoard_NegativeXValue()
        {
            Placement sut;
            bool result = true;

            //Arrange
            Space newSpace = new Space(-1, 7);
            List<Space> newList = new List<Space> { newSpace };

            sut = new Placement(newList);
            Game newGame = new Game();
            //Act
            result = sut.IsOnBoard(newGame);

            //Assert
            Assert.That(result, Is.False);

        }

        [Test]
        [Category("IsOnBoard")]
        public void IsOnBoard_NegativeYValue()
        {
            Placement sut;
            bool result = true;

            //Arrange
            Space newSpace = new Space(7, -1);
            List<Space> newList = new List<Space> { newSpace };

            sut = new Placement(newList);
            Game newGame = new Game();
            //Act
            result = sut.IsOnBoard(newGame);

            //Assert
            Assert.That(result, Is.False);

        }

        [Test]
        [Category("IsOnBoard")]
        public void IsOnBoard_SingleSpaceBothNegativeValues()
        {
            Placement sut;
            bool result = true;

            //Arrange
            Space newSpace = new Space(-7, -7);
            List<Space> newList = new List<Space> { newSpace };

            sut = new Placement(newList);
            Game newGame = new Game();
            //Act
            result = sut.IsOnBoard(newGame);

            //Assert
            Assert.That(result, Is.False);

        }

        [Test]
        [Category("IsOnBoard")]
        public void IsOnBoard_TwoSpacesOnBoard()
        {
            Placement sut;
            bool result = false;

            //Arrange
            Space spaceOne = new Space(7, 7);
            Space spaceTwo = new Space(7, 8);
            List<Space> newList = new List<Space> { spaceOne, spaceTwo };

            sut = new Placement(newList);
            Game newGame = new Game();
            //Act
            result = sut.IsOnBoard(newGame);

            //Assert
            Assert.That(result, Is.True);

        }

        [Test]
        [Category("IsOnBoard")]
        public void IsOnBoard_OneOnOneOff()
        {
            Placement sut;
            bool result = true;

            //Arrange
            Space spaceOne = new Space(7, 7);
            Space spaceTwo = new Space(7, 20);
            List<Space> newList = new List<Space> { spaceOne, spaceTwo };

            sut = new Placement(newList);
            Game newGame = new Game();
            //Act
            result = sut.IsOnBoard(newGame);

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        [Category("IsOnBoard")]
        public void IsOnBoard_OnEdges()
        {
            Placement sut;
            bool result = false;

            //Arrange
            Space spaceOne = new Space(0, 0);
            Space spaceTwo = new Space(0, 14);
            Space spaceThree = new Space(14, 0);
            Space spaceFour = new Space(14, 14);
            List<Space> newList = new List<Space> { spaceOne, spaceTwo, spaceThree, spaceFour };

            sut = new Placement(newList);
            Game newGame = new Game();
            //Act
            result = sut.IsOnBoard(newGame);

            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        [Category("IsOnBoard")]
        public void IsOnBoard_OffEdges()
        {
            Placement sut1;
            Placement sut2;
            bool result1 = true;
            bool result2 = true;

            //Arrange
            Space spaceOne = new Space(15, 7);
            Space spaceTwo = new Space(7, 15);

            sut1 = new Placement(spaceOne);
            sut2 = new Placement(spaceTwo);
            Game newGame = new Game();
            //Act
            result1 = sut1.IsOnBoard(newGame);
            result2 = sut2.IsOnBoard(newGame);

            //Assert
            Assert.That(result1, Is.False);
            Assert.That(result2, Is.False);
        }

        [Test]
        [Category("IsOnBoard")]
        public void IsOnBoard_7On()
        {
            Placement sut;
            bool result = false;

            //Arrange
            Space spaceOne = new Space(7, 7);
            Space spaceTwo = new Space(7, 8);
            Space spaceThree = new Space(7, 9);
            Space spaceFour = new Space(7, 10);
            Space spaceFive = new Space(7, 11);
            Space spaceSix = new Space(7, 12);
            Space spaceSeven = new Space(7, 13);

            List<Space> newList = new List<Space> { spaceOne, spaceTwo, spaceThree, spaceFour, spaceFive, spaceSix, spaceSeven };
            sut = new Placement(newList);
            Game newGame = new Game();

            //Act
            result = sut.IsOnBoard(newGame);

            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        [Category("IsOnBoard")]
        public void IsOnBoard_6On1Off()
        {
            Placement sut;
            bool result = true;

            //Arrange
            Space spaceOne = new Space(7, 7);
            Space spaceTwo = new Space(7, 8);
            Space spaceThree = new Space(7, 9);
            Space spaceFour = new Space(7, 10);
            Space spaceFive = new Space(7, 11);
            Space spaceSix = new Space(7, 12);
            Space spaceSeven = new Space(7, 15);

            List<Space> newList = new List<Space> { spaceOne, spaceTwo, spaceThree, spaceFour, spaceFive, spaceSix, spaceSeven };
            sut = new Placement(newList);
            Game newGame = new Game();

            //Act
            result = sut.IsOnBoard(newGame);

            //Assert
            Assert.That(result, Is.False);
        }


        #endregion
        #region //IsAvailable

        [Test]
        [Category("IsAvailable")]
        public void IsAvailable_EmtpyBoard()
        {
            Placement sut;
            bool result = false;

            //Arrange          
            Game newGame = new Game();
            sut = new Placement(new Space(7, 7));

            //Act
            result = sut.IsAvailable(newGame);

            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        [Category("IsAvailable")]
        public void IsAvailable_SingleOccupiedSpace()
        {
            Placement sut;
            bool result = true;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            Tuple<Space, Tile> newTuple = Tuple.Create(new Space(7, 7), new Tile('a'));
            tupleList.Add(newTuple);

            Game newGame = new Game();
            newGame.SetBoard(tupleList);
            sut = new Placement(new Space(7, 7));

            //Act
            result = sut.IsAvailable(newGame);

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        [Category("IsAvailable")]
        public void IsAvailable_TwoOccupiedSpace()
        {
            Placement sut;
            bool result = true;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('a')));
            tupleList.Add(Tuple.Create(new Space(7, 8), new Tile('b')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(7, 7));
            spaceList.Add(new Space(7, 8));

            sut = new Placement(spaceList);

            //Act
            result = sut.IsAvailable(newGame);

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        [Category("IsAvailable")]
        public void IsAvailable_OneOccupiedMultipleNot()
        {
            Placement sut;
            bool result = true;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('a')));
            tupleList.Add(Tuple.Create(new Space(7, 8), new Tile('b')));
            tupleList.Add(Tuple.Create(new Space(7, 9), new Tile('c')));
            tupleList.Add(Tuple.Create(new Space(7, 10), new Tile('d')));
            tupleList.Add(Tuple.Create(new Space(7, 11), new Tile('e')));
            tupleList.Add(Tuple.Create(new Space(7, 12), new Tile('f')));
            tupleList.Add(Tuple.Create(new Space(7, 13), new Tile('g')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(7, 7));
            spaceList.Add(new Space(6, 7));
            spaceList.Add(new Space(5, 7));
            spaceList.Add(new Space(3, 7));
            spaceList.Add(new Space(1, 7));

            sut = new Placement(spaceList);

            //Act
            result = sut.IsAvailable(newGame);

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        [Category("IsAvailable")]
        public void IsAvailable_MultipleOccupiedMultipleNot()
        {
            Placement sut;
            bool result = true;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('a')));
            tupleList.Add(Tuple.Create(new Space(7, 8), new Tile('b')));
            tupleList.Add(Tuple.Create(new Space(7, 9), new Tile('c')));
            tupleList.Add(Tuple.Create(new Space(7, 10), new Tile('d')));
            tupleList.Add(Tuple.Create(new Space(7, 11), new Tile('e')));
            tupleList.Add(Tuple.Create(new Space(7, 12), new Tile('f')));
            tupleList.Add(Tuple.Create(new Space(7, 13), new Tile('g')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(7, 7));
            spaceList.Add(new Space(7, 8));
            spaceList.Add(new Space(7, 9));
            spaceList.Add(new Space(7, 6));
            spaceList.Add(new Space(7, 5));

            sut = new Placement(spaceList);

            //Act
            result = sut.IsAvailable(newGame);

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        [Category("IsAvailable")]
        public void IsAvailable_MultipleClear()
        {
            Placement sut;
            bool result = false;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('a')));
            tupleList.Add(Tuple.Create(new Space(7, 8), new Tile('b')));
            tupleList.Add(Tuple.Create(new Space(7, 9), new Tile('c')));
            tupleList.Add(Tuple.Create(new Space(7, 10), new Tile('d')));
            tupleList.Add(Tuple.Create(new Space(7, 11), new Tile('e')));
            tupleList.Add(Tuple.Create(new Space(7, 12), new Tile('f')));
            tupleList.Add(Tuple.Create(new Space(7, 13), new Tile('g')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(5, 1));
            spaceList.Add(new Space(5, 2));
            spaceList.Add(new Space(5, 3));
            spaceList.Add(new Space(5, 4));
            spaceList.Add(new Space(5, 5));

            sut = new Placement(spaceList);

            //Act
            result = sut.IsAvailable(newGame);

            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        [Category("IsAvailable")]
        public void IsAvailable_MultipleAdjacent()
        {
            Placement sut;
            bool result = false;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('a')));
            tupleList.Add(Tuple.Create(new Space(7, 8), new Tile('b')));
            tupleList.Add(Tuple.Create(new Space(7, 9), new Tile('c')));
            tupleList.Add(Tuple.Create(new Space(7, 10), new Tile('d')));
            tupleList.Add(Tuple.Create(new Space(7, 11), new Tile('e')));
            tupleList.Add(Tuple.Create(new Space(7, 12), new Tile('f')));
            tupleList.Add(Tuple.Create(new Space(7, 13), new Tile('g')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(6, 7));
            spaceList.Add(new Space(6, 8));
            spaceList.Add(new Space(6, 6));
            spaceList.Add(new Space(6, 5));
            spaceList.Add(new Space(6, 4));

            sut = new Placement(spaceList);

            //Act
            result = sut.IsAvailable(newGame);

            //Assert
            Assert.That(result, Is.True);
        }

        #endregion
        #region//HasNoDuplicates

        [Test]
        [Category("HasNoDuplicates")]
        public void HasNoDuplicates_Single()
        {
            Placement sut;
            var result = false;

            //Arrang

            sut = new Placement(new Space(7,7));
            //Act
            result = sut.HasNoDuplicates();

            //Assert
            Assert.That(result, Is.True);

        }

        [Test]
        [Category("HasNoDuplicates")]
        public void HasNoDuplicates_Empty()
        {
            Placement sut;
            var result = false;

            //Arrang

            sut = new Placement();
            //Act
            result = sut.HasNoDuplicates();

            //Assert
            Assert.That(result, Is.True);

        }

        [Test]
        [Category("HasNoDuplicates")]
        public void HasNoDuplicates_TwoTheSame()
        {
            Placement sut;
            var result = true;

            //Arrange

            List<Space> spaceList = new List<Space>() { new Space(7, 7), new Space(7, 7) };
            sut = new Placement(spaceList);
            //Act
            result = sut.HasNoDuplicates();

            //Assert
            Assert.That(result, Is.False);

        }

        [Test]
        [Category("HasNoDuplicates")]
        public void HasNoDuplicates_TwoDifferent()
        {
            Placement sut;
            var result = false;

            //Arrange

            List<Space> spaceList = new List<Space>() { new Space(7, 7), new Space(7, 8) };
            sut = new Placement(spaceList);
            //Act
            result = sut.HasNoDuplicates();

            //Assert
            Assert.That(result, Is.True);

        }

        [Test]
        [Category("HasNoDuplicates")]
        public void HasNoDuplicates_MultiDifferentOneDupe()
        {
            Placement sut;
            var result = true;

            //Arrange

            List<Space> spaceList = new List<Space>() { new Space(1, 1), new Space(6, 7), new Space(1, 7), new Space(7, 13), new Space(7, 8), new Space(7, 7), new Space(7, 7) };
            sut = new Placement(spaceList);
            //Act
            result = sut.HasNoDuplicates();

            //Assert
            Assert.That(result, Is.False);

        }

        [Test]
        [Category("HasNoDuplicates")]
        public void HasNoDuplicates_MultiDifferent()
        {
            Placement sut;
            var result = false;

            //Arrange

            List<Space> spaceList = new List<Space>() { new Space(1, 1), new Space(6, 7), new Space(1, 7), new Space(7, 13), new Space(7, 8), new Space(7, 7), new Space(7, 9) };
            sut = new Placement(spaceList);
            //Act
            result = sut.HasNoDuplicates();

            //Assert
            Assert.That(result, Is.True);

        }

        #endregion
        #region//IsFirstMove

        [Test]
        [Category("IsFirstMove")]
        public void IsFirstMove_SingleMiddleSpaceOnEmtpyBoard()
        {
            Placement sut;
            bool result = false;

            //Arrange          
            Game newGame = new Game();
            sut = new Placement(new Space(7, 7));

            //Act
            result = sut.IsFirstMove(newGame);

            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        [Category("IsFirstMove")]
        public void IsFirstMove_MiddleWordOnEmtpyBoard()
        {
            Placement sut;
            bool result = false;

            //Arrange          
            Game newGame = new Game();
            sut = new Placement(new List<Space>() { new Space(7, 7), new Space(7, 8), new Space(7, 9), new Space(7, 6)} );

            //Act
            result = sut.IsFirstMove(newGame);

            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        [Category("IsFirstMove")]
        public void IsFirstMove_MiddleSpaceOnBadBoard()
        {
            Placement sut;
            bool result = true;

            //Arrange          
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(10, 7), new Tile('a')));
            tupleList.Add(Tuple.Create(new Space(10, 8), new Tile('b')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            sut = new Placement(new Space(7, 7));

            //Act
            result = sut.IsFirstMove(newGame);

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        [Category("IsFirstMove")]
        public void IsFirstMove_OffWordOnEmtpyBoard()
        {
            Placement sut;
            bool result = true;

            //Arrange          
            Game newGame = new Game();
            sut = new Placement(new List<Space>() { new Space(8, 7), new Space(8, 8), new Space(8, 9), new Space(7, 8) });

            //Act
            result = sut.IsFirstMove(newGame);

            //Assert
            Assert.That(result, Is.False);
        }

        #endregion
        #region//IsAdjacent

        [Test]
        [Category("IsAdjacent")]
        public void IsAdjacent_SingleAdjacentTop()
        {
            Placement sut;
            bool result = false;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('a')));
            tupleList.Add(Tuple.Create(new Space(7, 8), new Tile('b')));
            tupleList.Add(Tuple.Create(new Space(7, 9), new Tile('c')));
            tupleList.Add(Tuple.Create(new Space(7, 10), new Tile('d')));
            tupleList.Add(Tuple.Create(new Space(7, 11), new Tile('e')));
            tupleList.Add(Tuple.Create(new Space(7, 12), new Tile('f')));
            tupleList.Add(Tuple.Create(new Space(7, 13), new Tile('g')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(7, 6));
            sut = new Placement(spaceList);

            //Act
            result = sut.IsAdjacent(newGame);

            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        [Category("IsAdjacent")]
        public void IsAdjacent_SingleAdjacentBottom()
        {
            Placement sut;
            bool result = false;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('a')));
            tupleList.Add(Tuple.Create(new Space(7, 8), new Tile('b')));
            tupleList.Add(Tuple.Create(new Space(7, 9), new Tile('c')));
            tupleList.Add(Tuple.Create(new Space(7, 10), new Tile('d')));
            tupleList.Add(Tuple.Create(new Space(7, 11), new Tile('e')));
            tupleList.Add(Tuple.Create(new Space(7, 12), new Tile('f')));
            tupleList.Add(Tuple.Create(new Space(7, 13), new Tile('g')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(7, 14));
            sut = new Placement(spaceList);

            //Act
            result = sut.IsAdjacent(newGame);

            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        [Category("IsAdjacent")]
        public void IsAdjacent_SingleAdjacentLeft()
        {
            Placement sut;
            bool result = false;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('a')));
            tupleList.Add(Tuple.Create(new Space(7, 8), new Tile('b')));
            tupleList.Add(Tuple.Create(new Space(7, 9), new Tile('c')));
            tupleList.Add(Tuple.Create(new Space(7, 10), new Tile('d')));
            tupleList.Add(Tuple.Create(new Space(7, 11), new Tile('e')));
            tupleList.Add(Tuple.Create(new Space(7, 12), new Tile('f')));
            tupleList.Add(Tuple.Create(new Space(7, 13), new Tile('g')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(6, 10));
            sut = new Placement(spaceList);

            //Act
            result = sut.IsAdjacent(newGame);

            //Assert
            Assert.That(result, Is.True);

        }

        [Test]
        [Category("IsAdjacent")]
        public void IsAdjacent_SingleAdjacentRight()
        {
            Placement sut;
            bool result = false;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('a')));
            tupleList.Add(Tuple.Create(new Space(7, 8), new Tile('b')));
            tupleList.Add(Tuple.Create(new Space(7, 9), new Tile('c')));
            tupleList.Add(Tuple.Create(new Space(7, 10), new Tile('d')));
            tupleList.Add(Tuple.Create(new Space(7, 11), new Tile('e')));
            tupleList.Add(Tuple.Create(new Space(7, 12), new Tile('f')));
            tupleList.Add(Tuple.Create(new Space(7, 13), new Tile('g')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(8, 10));
            sut = new Placement(spaceList);

            //Act
            result = sut.IsAdjacent(newGame);

            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        [Category("IsAdjacent")]
        public void IsAdjacent_MultipleAdjacent()
        {
            Placement sut;
            bool result = false;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('a')));
            tupleList.Add(Tuple.Create(new Space(7, 8), new Tile('b')));
            tupleList.Add(Tuple.Create(new Space(7, 9), new Tile('c')));
            tupleList.Add(Tuple.Create(new Space(7, 10), new Tile('d')));
            tupleList.Add(Tuple.Create(new Space(7, 11), new Tile('e')));
            tupleList.Add(Tuple.Create(new Space(7, 12), new Tile('f')));
            tupleList.Add(Tuple.Create(new Space(7, 13), new Tile('g')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(8, 10));
            spaceList.Add(new Space(8, 11));
            spaceList.Add(new Space(8, 12));
            spaceList.Add(new Space(8, 13));
            spaceList.Add(new Space(8, 14));

            sut = new Placement(spaceList);

            //Act
            result = sut.IsAdjacent(newGame);

            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        [Category("IsAdjacent")]
        public void IsAdjacent_AdjacentJunction()
        {
            Placement sut;
            bool result = false;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('a')));
            tupleList.Add(Tuple.Create(new Space(7, 8), new Tile('b')));
            tupleList.Add(Tuple.Create(new Space(7, 9), new Tile('c')));
            tupleList.Add(Tuple.Create(new Space(7, 10), new Tile('d')));
            tupleList.Add(Tuple.Create(new Space(7, 11), new Tile('e')));
            tupleList.Add(Tuple.Create(new Space(7, 12), new Tile('f')));
            tupleList.Add(Tuple.Create(new Space(7, 13), new Tile('g')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(7, 6));
            spaceList.Add(new Space(4, 6));
            spaceList.Add(new Space(5, 6));
            spaceList.Add(new Space(6, 6));
            spaceList.Add(new Space(8, 6));

            sut = new Placement(spaceList);

            //Act
            result = sut.IsAdjacent(newGame);

            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        [Category("IsAdjacent")]
        public void IsAdjacent_SingleOff()
        {
            Placement sut;
            bool result = true;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('a')));
            tupleList.Add(Tuple.Create(new Space(7, 8), new Tile('b')));
            tupleList.Add(Tuple.Create(new Space(7, 9), new Tile('c')));
            tupleList.Add(Tuple.Create(new Space(7, 10), new Tile('d')));
            tupleList.Add(Tuple.Create(new Space(7, 11), new Tile('e')));
            tupleList.Add(Tuple.Create(new Space(7, 12), new Tile('f')));
            tupleList.Add(Tuple.Create(new Space(7, 13), new Tile('g')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(10, 12));

            sut = new Placement(spaceList);

            //Act
            result = sut.IsAdjacent(newGame);

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        [Category("IsAdjacent")]
        public void IsAdjacent_MultipleOff()
        {
            Placement sut;
            bool result = true;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('a')));
            tupleList.Add(Tuple.Create(new Space(7, 8), new Tile('b')));
            tupleList.Add(Tuple.Create(new Space(7, 9), new Tile('c')));
            tupleList.Add(Tuple.Create(new Space(7, 10), new Tile('d')));
            tupleList.Add(Tuple.Create(new Space(7, 11), new Tile('e')));
            tupleList.Add(Tuple.Create(new Space(7, 12), new Tile('f')));
            tupleList.Add(Tuple.Create(new Space(7, 13), new Tile('g')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(2, 6));
            spaceList.Add(new Space(2, 5));
            spaceList.Add(new Space(2, 4));
            spaceList.Add(new Space(2, 3));
            spaceList.Add(new Space(2, 2));

            sut = new Placement(spaceList);

            //Act
            result = sut.IsAdjacent(newGame);

            //Assert
            Assert.That(result, Is.False);
        }
        [Test]
        [Category("IsAdjacent")]
        public void IsAdjacent_Corners()
        {          
            bool result1 = true;
            bool result2 = true;
            bool result3 = true;
            bool result4 = true;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('a')));
            tupleList.Add(Tuple.Create(new Space(7, 8), new Tile('b')));
            tupleList.Add(Tuple.Create(new Space(7, 9), new Tile('c')));
            tupleList.Add(Tuple.Create(new Space(7, 10), new Tile('d')));
            tupleList.Add(Tuple.Create(new Space(7, 11), new Tile('e')));
            tupleList.Add(Tuple.Create(new Space(7, 12), new Tile('f')));
            tupleList.Add(Tuple.Create(new Space(7, 13), new Tile('g')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            Placement sut1 = new Placement(new Space(0, 0));
            Placement sut2 = new Placement(new Space(0, 14));
            Placement sut3 = new Placement(new Space(14, 0));
            Placement sut4 = new Placement(new Space(0, 14));

            //Act
            result1 = sut1.IsAdjacent(newGame);
            result2 = sut2.IsAdjacent(newGame);
            result3 = sut3.IsAdjacent(newGame);
            result4 = sut4.IsAdjacent(newGame);

            //Assert
            Assert.That(result1, Is.False);
            Assert.That(result2, Is.False);
            Assert.That(result3, Is.False);
            Assert.That(result4, Is.False);
        }

        #endregion
        #region//IsSingle

        [Test]
        [Category("IsSingle")]
        public void IsSingle_OneSpace()
        {
            Placement sut;
            var result = false;

            //Arrange

            Space space = new Space(7, 7);

            sut = new Placement(space);
            //Act
            result = sut.IsSingle();

            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        [Category("IsSingle")]
        public void IsSingle_TwoSpaces()
        {
            Placement sut;
            var result = true;

            //Arrange

            Space space1 = new Space(7, 7);
            Space space2 = new Space(7, 8);

            sut = new Placement(new List<Space>() { space1, space2 });
            //Act
            result = sut.IsSingle();

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        [Category("IsSingle")]
        public void IsSingle_NoSpace()
        {
            Placement sut;
            var result = true;

            //Arrange
            sut = new Placement();

            //Act
            result = sut.IsSingle();

            //Assert
            Assert.That(result, Is.False);
        }

        #endregion
        #region//IsHorizontal

        [Test]
        [Category("IsHorizontal")]
        public void IsHorizontal_True()
        {
            Placement sut;
            var result = false;

            //Arrange

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(6, 7));
            spaceList.Add(new Space(1, 7));
            spaceList.Add(new Space(2, 7));
            spaceList.Add(new Space(5, 7));
            spaceList.Add(new Space(14, 7));

            sut = new Placement(spaceList);

            //Act
            result = sut.IsHorizontal();

            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        [Category("IsHorizontal")]
        public void IsHorizontal_OneOff()
        {
            Placement sut;
            var result = true;

            //Arrange

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(6, 7));
            spaceList.Add(new Space(1, 7));
            spaceList.Add(new Space(2, 7));
            spaceList.Add(new Space(5, 7));
            spaceList.Add(new Space(14, 6));

            sut = new Placement(spaceList);

            //Act
            result = sut.IsHorizontal();

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        [Category("IsHorizontal")]
        public void IsHorizontal_WayOff()
        {
            Placement sut;
            var result = true;

            //Arrange

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(6, 9));
            spaceList.Add(new Space(1, 1));
            spaceList.Add(new Space(2, 77));
            spaceList.Add(new Space(5, 7));
            spaceList.Add(new Space(14, 6));

            sut = new Placement(spaceList);

            //Act
            result = sut.IsHorizontal();

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        [Category("IsHorizontal")]
        public void IsHorizontal_Single()
        {
            Placement sut;
            var result = true;

            //Arrange

            Space space = new Space(7, 7);

            sut = new Placement(space);
            //Act
            result = sut.IsHorizontal();

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        [Category("IsHorizontal")]
        public void IsHorizontal_Empty()
        {
            Placement sut;
            var result = true;

            //Arrange         

            sut = new Placement();
            
            //Act
            result = sut.IsHorizontal();

            //Assert
            Assert.That(result, Is.False);

        }

        [Test]
        [Category("IsHorizontal")]
        public void IsHorizontal_Vertical()
        {
            Placement sut;
            var result = true;

            //Arrange

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(6, 9));
            spaceList.Add(new Space(6, 1));
            spaceList.Add(new Space(6, 2));
            spaceList.Add(new Space(6, 7));
            spaceList.Add(new Space(6, 6));

            sut = new Placement(spaceList);

            //Act
            result = sut.IsHorizontal();

            //Assert
            Assert.That(result, Is.False);

        }

        #endregion
        #region//IsVertical

        [Test]
        [Category("IsVertical")]
        public void IsVertical_True()
        {
            Placement sut;
            var result = false;

            //Arrange

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(6, 1));
            spaceList.Add(new Space(6, 2));
            spaceList.Add(new Space(6, 0));
            spaceList.Add(new Space(6, 4));
            spaceList.Add(new Space(6, 15));

            sut = new Placement(spaceList);

            //Act
            result = sut.IsVertical();

            //Assert
            Assert.That(result, Is.True);

        }

        [Test]
        [Category("IsVertical")]
        public void IsVertical_OneOff()
        {
            Placement sut;
            var result = true;

            //Arrange

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(6, 7));
            spaceList.Add(new Space(6, 8));
            spaceList.Add(new Space(6, 9));
            spaceList.Add(new Space(6, 10));
            spaceList.Add(new Space(7, 11));

            sut = new Placement(spaceList);

            //Act
            result = sut.IsVertical();

            //Assert
            Assert.That(result, Is.False);

        }

        [Test]
        [Category("IsVertical")]
        public void IsVertial_WayOff()
        {
            Placement sut;
            var result = true;

            //Arrange

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(6, 9));
            spaceList.Add(new Space(1, 1));
            spaceList.Add(new Space(2, 0));
            spaceList.Add(new Space(5, 7));
            spaceList.Add(new Space(14, 6));

            sut = new Placement(spaceList);

            //Act
            result = sut.IsVertical();

            //Assert
            Assert.That(result, Is.False);

        }

        [Test]
        [Category("IsVertical")]
        public void IsVertical_Single()
        {
            Placement sut;
            var result = true;

            //Arrange

            Space space = new Space(7, 7);

            sut = new Placement(space);
            //Act
            result = sut.IsVertical();

            //Assert
            Assert.That(result, Is.False);

        }

        [Test]
        [Category("IsVertical")]
        public void IsVertical_Empty()
        {
            Placement sut;
            var result = true;

            //Arrange         

            sut = new Placement();

            //Act
            result = sut.IsVertical();

            //Assert
            Assert.That(result, Is.False);

        }

        [Test]
        [Category("IsVertical")]
        public void IsVertical_Horizontal()
        {
            Placement sut;
            var result = true;

            //Arrange

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(12, 9));
            spaceList.Add(new Space(0, 9));
            spaceList.Add(new Space(3, 9));
            spaceList.Add(new Space(4, 9));
            spaceList.Add(new Space(10, 9));

            sut = new Placement(spaceList);

            //Act
            result = sut.IsVertical();

            //Assert
            Assert.That(result, Is.False);

        }

        #endregion
        #region//IsContiguous

        [Test]
        [Category("IsContiguous Vertical")]
        public void IsContiguous_EmptyBoardGoodVertical()
        {
            Placement sut;
            bool result = false;

            //Arrange

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(6, 1));
            spaceList.Add(new Space(6, 2));
            spaceList.Add(new Space(6, 0));
            spaceList.Add(new Space(6, 4));
            spaceList.Add(new Space(6, 3));

            sut = new Placement(spaceList);
            //Act
            result = sut.IsContiguous(new Game());

            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        [Category("IsContiguous Horizontal")]
        public void IsContiguous_EmptyBoardGoodHorizontal()
        {
            Placement sut;
            bool result = false;

            //Arrange

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(1, 7));
            spaceList.Add(new Space(2, 7));
            spaceList.Add(new Space(3, 7));
            spaceList.Add(new Space(4, 7));
            spaceList.Add(new Space(5, 7));

            sut = new Placement(spaceList);
            //Act
            result = sut.IsContiguous(new Game());

            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        [Category("IsContiguous Vertical")]
        public void IsContiguous_EmptyBoardBadVertical()
        {
            Placement sut;
            bool result = true;

            //Arrange

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(6, 1));
            spaceList.Add(new Space(6, 2));
            spaceList.Add(new Space(6, 0));
            spaceList.Add(new Space(6, 12));
            spaceList.Add(new Space(6, 3));

            sut = new Placement(spaceList);
            //Act
            result = sut.IsContiguous(new Game());

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        [Category("IsContiguous Horizontal")]
        public void IsContiguous_EmptyBoardBadHorizontal()
        {
            Placement sut;
            bool result = true;

            //Arrange

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(1, 7));
            spaceList.Add(new Space(2, 7));
            spaceList.Add(new Space(3, 7));
            spaceList.Add(new Space(4, 7));
            spaceList.Add(new Space(12, 7));

            sut = new Placement(spaceList);
            //Act
            result = sut.IsContiguous(new Game());

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        [Category("IsContiguous Horizontal")]
        public void IsContiguous_HorizontalOneGapGood()
        {
            Placement sut;
            bool result = false;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('a')));
            tupleList.Add(Tuple.Create(new Space(7, 8), new Tile('b')));
            tupleList.Add(Tuple.Create(new Space(7, 9), new Tile('c')));
            tupleList.Add(Tuple.Create(new Space(7, 10), new Tile('d')));
            tupleList.Add(Tuple.Create(new Space(7, 11), new Tile('e')));
            tupleList.Add(Tuple.Create(new Space(7, 12), new Tile('f')));
            tupleList.Add(Tuple.Create(new Space(7, 13), new Tile('g')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(5, 7));
            spaceList.Add(new Space(6, 7));
            spaceList.Add(new Space(8, 7));
            spaceList.Add(new Space(9, 7));
            spaceList.Add(new Space(10, 7));

            sut = new Placement(spaceList);
            //Act
            result = sut.IsContiguous(newGame);

            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        [Category("IsContiguous Vertical")]
        public void IsContiguous_VerticalOneGapGood()
        {
            Placement sut;
            bool result = false;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('a')));
            tupleList.Add(Tuple.Create(new Space(8, 7), new Tile('b')));
            tupleList.Add(Tuple.Create(new Space(9, 7), new Tile('c')));
            tupleList.Add(Tuple.Create(new Space(10, 7), new Tile('d')));
            tupleList.Add(Tuple.Create(new Space(11, 7), new Tile('e')));
            tupleList.Add(Tuple.Create(new Space(12, 7), new Tile('f')));
            tupleList.Add(Tuple.Create(new Space(13, 7), new Tile('g')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(8, 6));
            spaceList.Add(new Space(8, 10));
            spaceList.Add(new Space(8, 9));
            spaceList.Add(new Space(8, 8));
            spaceList.Add(new Space(8, 11));

            sut = new Placement(spaceList);
            //Act
            result = sut.IsContiguous(newGame);

            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        [Category("IsContiguous Vertical")]
        public void IsContiguous_VerticalDoubleGapGood()
        {
            Placement sut;
            bool result = false;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('a')));
            tupleList.Add(Tuple.Create(new Space(7, 8), new Tile('b')));         

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(7, 6));
            spaceList.Add(new Space(7, 5));
            spaceList.Add(new Space(7, 9));
            spaceList.Add(new Space(7, 10));
            spaceList.Add(new Space(7, 11));

            sut = new Placement(spaceList);
            //Act
            result = sut.IsContiguous(newGame);

            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        [Category("IsContiguous Horizontal")]
        public void IsContiguous_HorizontalDoubleGapGood()
        {
            Placement sut;
            bool result = false;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('a')));
            tupleList.Add(Tuple.Create(new Space(8, 7), new Tile('b')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(6, 7));
            spaceList.Add(new Space(5, 7));
            spaceList.Add(new Space(9, 7));
            spaceList.Add(new Space(10, 7));
            spaceList.Add(new Space(11, 7));

            sut = new Placement(spaceList);
            //Act
            result = sut.IsContiguous(newGame);

            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        [Category("IsContiguous Vertical")]
        public void IsContiguous_VerticalTwoSingleGapsGood()
        {
            Placement sut;
            bool result = false;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('a')));
            tupleList.Add(Tuple.Create(new Space(7, 10), new Tile('b')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(7, 6));
            spaceList.Add(new Space(7, 5));
            spaceList.Add(new Space(7, 9));
            spaceList.Add(new Space(7, 8));
            spaceList.Add(new Space(7, 11));

            sut = new Placement(spaceList);
            //Act
            result = sut.IsContiguous(newGame);

            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        [Category("IsContiguous Horizontal")]
        public void IsContiguous_HorizontalTwoSingleGapsGood()
        {
            Placement sut;
            bool result = false;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('a')));
            tupleList.Add(Tuple.Create(new Space(10, 7), new Tile('b')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(6, 7));
            spaceList.Add(new Space(5, 7));
            spaceList.Add(new Space(9, 7));
            spaceList.Add(new Space(8, 7));
            spaceList.Add(new Space(11, 7));

            sut = new Placement(spaceList);
            //Act
            result = sut.IsContiguous(newGame);

            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        [Category("IsContiguous Vertical")]
        public void IsContiguous_VerticalTripleGapGood()
        {
            Placement sut;
            bool result = false;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('a')));
            tupleList.Add(Tuple.Create(new Space(7, 8), new Tile('b')));
            tupleList.Add(Tuple.Create(new Space(7, 9), new Tile('c')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(7, 6));
            spaceList.Add(new Space(7, 5));
            spaceList.Add(new Space(7, 10));
            spaceList.Add(new Space(7, 12));
            spaceList.Add(new Space(7, 11));

            sut = new Placement(spaceList);
            //Act
            result = sut.IsContiguous(newGame);

            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        [Category("IsContiguous Horizontal")]
        public void IsContiguous_HorizontalTripleGapGood()
        {
            Placement sut;
            bool result = false;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('a')));
            tupleList.Add(Tuple.Create(new Space(8, 7), new Tile('b')));
            tupleList.Add(Tuple.Create(new Space(9, 7), new Tile('c')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(6, 7));
            spaceList.Add(new Space(5, 7));
            spaceList.Add(new Space(10, 7));
            spaceList.Add(new Space(4, 7));
            spaceList.Add(new Space(11, 7));

            sut = new Placement(spaceList);
            //Act
            result = sut.IsContiguous(newGame);

            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        [Category("IsContiguous Horizontal")]
        public void IsContiguous_HorizontalOneGapBad()
        {
            Placement sut;
            bool result = true;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('a')));
            tupleList.Add(Tuple.Create(new Space(7, 8), new Tile('b')));
            tupleList.Add(Tuple.Create(new Space(7, 9), new Tile('c')));
            tupleList.Add(Tuple.Create(new Space(7, 10), new Tile('d')));
            tupleList.Add(Tuple.Create(new Space(7, 11), new Tile('e')));
            tupleList.Add(Tuple.Create(new Space(7, 12), new Tile('f')));
            tupleList.Add(Tuple.Create(new Space(7, 13), new Tile('g')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(1, 7));
            spaceList.Add(new Space(2, 7));
            spaceList.Add(new Space(4, 7));
            spaceList.Add(new Space(5, 7));
            spaceList.Add(new Space(6, 7));

            sut = new Placement(spaceList);
            //Act
            result = sut.IsContiguous(newGame);

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        [Category("IsContiguous Vertical")]
        public void IsContiguous_VerticalOneGapBad()
        {
            Placement sut;
            bool result = true;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('a')));
            tupleList.Add(Tuple.Create(new Space(8, 7), new Tile('b')));
            tupleList.Add(Tuple.Create(new Space(9, 7), new Tile('c')));
            tupleList.Add(Tuple.Create(new Space(10, 7), new Tile('d')));
            tupleList.Add(Tuple.Create(new Space(11, 7), new Tile('e')));
            tupleList.Add(Tuple.Create(new Space(12, 7), new Tile('f')));
            tupleList.Add(Tuple.Create(new Space(13, 7), new Tile('g')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(7, 1));
            spaceList.Add(new Space(7, 2));
            spaceList.Add(new Space(7, 4));
            spaceList.Add(new Space(7, 5));
            spaceList.Add(new Space(7, 6));

            sut = new Placement(spaceList);
            //Act
            result = sut.IsContiguous(newGame);

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        [Category("IsContiguous Vertical")]
        public void IsContiguous_VerticalDoubleGapBad()
        {
            Placement sut;
            bool result = true;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('a')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(7, 6));
            spaceList.Add(new Space(7, 5));
            spaceList.Add(new Space(7, 9));
            spaceList.Add(new Space(7, 10));
            spaceList.Add(new Space(7, 11));

            sut = new Placement(spaceList);
            //Act
            result = sut.IsContiguous(newGame);

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        [Category("IsContiguous Horizontal")]
        public void IsContiguous_HorizontalDoubleGapBad()
        {
            Placement sut;
            bool result = true;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('a')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(6, 7));
            spaceList.Add(new Space(5, 7));
            spaceList.Add(new Space(9, 7));
            spaceList.Add(new Space(10, 7));
            spaceList.Add(new Space(11, 7));

            sut = new Placement(spaceList);
            //Act
            result = sut.IsContiguous(newGame);

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        [Category("IsContiguous Vertical")]
        public void IsContiguous_VerticalTwoSingleGapsBad()
        {
            Placement sut;
            bool result = true;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('a')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(7, 6));
            spaceList.Add(new Space(7, 5));
            spaceList.Add(new Space(7, 9));
            spaceList.Add(new Space(7, 8));
            spaceList.Add(new Space(7, 11));

            sut = new Placement(spaceList);
            //Act
            result = sut.IsContiguous(newGame);

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        [Category("IsContiguous Horizontal")]
        public void IsContiguous_HorizontalTwoSingleGapsBad()
        {
            Placement sut;
            bool result = true;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('a')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(6, 7));
            spaceList.Add(new Space(5, 7));
            spaceList.Add(new Space(9, 7));
            spaceList.Add(new Space(8, 7));
            spaceList.Add(new Space(11, 7));

            sut = new Placement(spaceList);
            //Act
            result = sut.IsContiguous(newGame);

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        [Category("IsContiguous Vertical")]
        public void IsContiguous_VerticalTripleGapMiddleBad()
        {
            Placement sut;
            bool result = true;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('a')));
 
            tupleList.Add(Tuple.Create(new Space(7, 9), new Tile('c')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(7, 6));
            spaceList.Add(new Space(7, 5));
            spaceList.Add(new Space(7, 10));
            spaceList.Add(new Space(7, 12));
            spaceList.Add(new Space(7, 11));

            sut = new Placement(spaceList);
            //Act
            result = sut.IsContiguous(newGame);

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        [Category("IsContiguous Vertical")]
        public void IsContiguous_VerticalTripleGapOneBad()
        {
            Placement sut;
            bool result = true;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('a')));
            tupleList.Add(Tuple.Create(new Space(7, 8), new Tile('b')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(7, 6));
            spaceList.Add(new Space(7, 5));
            spaceList.Add(new Space(7, 10));
            spaceList.Add(new Space(7, 12));
            spaceList.Add(new Space(7, 11));

            sut = new Placement(spaceList);
            //Act
            result = sut.IsContiguous(newGame);

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        [Category("IsContiguous Horizontal")]
        public void IsContiguous_HorizontalTripleGapOneBad()
        {
            Placement sut;
            bool result = true;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('a')));
            tupleList.Add(Tuple.Create(new Space(8, 7), new Tile('b')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(6, 7));
            spaceList.Add(new Space(5, 7));
            spaceList.Add(new Space(10, 7));
            spaceList.Add(new Space(4, 7));
            spaceList.Add(new Space(11, 7));

            sut = new Placement(spaceList);
            //Act
            result = sut.IsContiguous(newGame);

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        [Category("IsContiguous Horizontal")]
        public void IsContiguous_HorizontalTripleGapMiddleBad()
        {
            Placement sut;
            bool result = true;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('a')));
            tupleList.Add(Tuple.Create(new Space(9, 7), new Tile('c')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(6, 7));
            spaceList.Add(new Space(5, 7));
            spaceList.Add(new Space(10, 7));
            spaceList.Add(new Space(4, 7));
            spaceList.Add(new Space(11, 7));

            sut = new Placement(spaceList);
            //Act
            result = sut.IsContiguous(newGame);

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        [Category("IsContiguous Horizontal")]
        public void IsContiguous_HorizontalTwoPlacementCheat1()
        {
            Placement sut;
            bool result = true;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(1, 7), new Tile('a')));
            tupleList.Add(Tuple.Create(new Space(3, 7), new Tile('c')));
            tupleList.Add(Tuple.Create(new Space(10, 7), new Tile('j')));
            tupleList.Add(Tuple.Create(new Space(12, 7), new Tile('l')));


            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(2, 7));
            spaceList.Add(new Space(11, 7));

            sut = new Placement(spaceList);
            //Act
            result = sut.IsContiguous(newGame);

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        [Category("IsContiguous Horizontal")]
        public void IsContiguous_HorizontalTwoPlacementCheat2()
        {
            Placement sut;
            bool result = true;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(1, 7), new Tile('a')));
            tupleList.Add(Tuple.Create(new Space(3, 7), new Tile('c')));
            tupleList.Add(Tuple.Create(new Space(10, 7), new Tile('j')));
            tupleList.Add(Tuple.Create(new Space(12, 7), new Tile('l')));


            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(2, 7));
            spaceList.Add(new Space(4, 7));
            spaceList.Add(new Space(11, 7));

            sut = new Placement(spaceList);
            //Act
            result = sut.IsContiguous(newGame);

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        [Category("IsContiguous Horizontal")]
        public void IsContiguous_HorizontalTwoPlacementCheat3()
        {
            Placement sut;
            bool result = true;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(1, 7), new Tile('a')));
            tupleList.Add(Tuple.Create(new Space(3, 7), new Tile('c')));
            tupleList.Add(Tuple.Create(new Space(10, 7), new Tile('j')));
            tupleList.Add(Tuple.Create(new Space(12, 7), new Tile('l')));


            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(2, 7));
            spaceList.Add(new Space(4, 7));
            spaceList.Add(new Space(9, 7));
            spaceList.Add(new Space(11, 7));

            sut = new Placement(spaceList);
            //Act
            result = sut.IsContiguous(newGame);

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        [Category("IsContiguous Vertical")]
        public void IsContiguous_VerticalTwoPlacementCheat1()
        {
            Placement sut;
            bool result = true;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(7, 1), new Tile('a')));
            tupleList.Add(Tuple.Create(new Space(7, 3), new Tile('c')));
            tupleList.Add(Tuple.Create(new Space(7, 10), new Tile('j')));
            tupleList.Add(Tuple.Create(new Space(7, 12), new Tile('l')));


            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(7, 2));
            spaceList.Add(new Space(7, 11));

            sut = new Placement(spaceList);
            //Act
            result = sut.IsContiguous(newGame);

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        [Category("IsContiguous Vertical")]
        public void IsContiguous_VerticalTwoPlacementCheat2()
        {
            Placement sut;
            bool result = true;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(7, 1), new Tile('a')));
            tupleList.Add(Tuple.Create(new Space(7, 3), new Tile('c')));
            tupleList.Add(Tuple.Create(new Space(7, 10), new Tile('j')));
            tupleList.Add(Tuple.Create(new Space(7, 12), new Tile('l')));


            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(7, 2));
            spaceList.Add(new Space(7, 4));
            spaceList.Add(new Space(7, 11));

            sut = new Placement(spaceList);
            //Act
            result = sut.IsContiguous(newGame);

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        [Category("IsContiguous Vertical")]
        public void IsContiguous_VerticalTwoPlacementCheat3()
        {
            Placement sut;
            bool result = true;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(7, 1), new Tile('a')));
            tupleList.Add(Tuple.Create(new Space(7, 3), new Tile('c')));
            tupleList.Add(Tuple.Create(new Space(7, 10), new Tile('j')));
            tupleList.Add(Tuple.Create(new Space(7, 12), new Tile('l')));


            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(7, 2));
            spaceList.Add(new Space(7, 4));
            spaceList.Add(new Space(7, 9));
            spaceList.Add(new Space(7, 11));

            sut = new Placement(spaceList);
            //Act
            result = sut.IsContiguous(newGame);

            //Assert
            Assert.That(result, Is.False);
        }
        #endregion

        #region //GetAnchors
        [Test]
        [Category("GetAnchors")]
        public void GetAnchors_Vertical_DoubleMiddleGap()
        {
            Placement sut;
            List<Space> result = null;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('a')));
            tupleList.Add(Tuple.Create(new Space(7, 8), new Tile('b')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(7, 6));
            spaceList.Add(new Space(7, 5));
            spaceList.Add(new Space(7, 9));
            spaceList.Add(new Space(7, 10));
            spaceList.Add(new Space(7, 11));

            sut = new Placement(spaceList);

            var expected = new List<Space>{ new Space(7, 8, 'b'), new Space(7, 7, 'a') };
            
            //Act
            result = sut.GetAnchors(newGame);

            //Assert

            Assert.That(result.Select(s => s.GetString()).ToList(), Is.EquivalentTo(expected.Select(s => s.GetString()).ToList()));
        }

        [Test]
        [Category("GetAnchors")]
        public void GetAnchors_Horizontal_DoubleMiddleGap()
        {
            Placement sut;
            List<Space> result = null;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('a')));
            tupleList.Add(Tuple.Create(new Space(8, 7), new Tile('b')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(6, 7));
            spaceList.Add(new Space(5, 7));
            spaceList.Add(new Space(9, 7));
            spaceList.Add(new Space(10, 7));
            spaceList.Add(new Space(11, 7));

            sut = new Placement(spaceList);

            var expected = new List<Space> { new Space(7, 7, 'a'), new Space(8, 7, 'b') };
            //Act
            result = sut.GetAnchors(newGame);

            //Assert
            Assert.That(result.Select(s => s.GetString()).ToList(), Is.EquivalentTo(expected.Select(s => s.GetString()).ToList()));
        }

        [Test]
        [Category("GetAnchors")]
        public void GetAnchors_DoubleMiddleGap_WrongLetters()
        {
            Placement sut;
            List<Space> result = null;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('a')));
            tupleList.Add(Tuple.Create(new Space(7, 8), new Tile('b')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(7, 6));
            spaceList.Add(new Space(7, 5));
            spaceList.Add(new Space(7, 9));
            spaceList.Add(new Space(7, 10));
            spaceList.Add(new Space(7, 11));

            sut = new Placement(spaceList);

            var expected = new List<Space> { new Space(7, 7, 'p'), new Space(7, 8, 'q') };
            //Act
            result = sut.GetAnchors(newGame);

            //Assert
            Assert.That(result.Select(s => s.GetString()).ToList(), Is.Not.EquivalentTo(expected.Select(s => s.GetString()).ToList()));
        }

        [Test]
        [Category("GetAnchors")]
        public void GetAnchors_HorizontalTwoSingleGaps()
        {
            Placement sut;
            List<Space> result;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('a')));
            tupleList.Add(Tuple.Create(new Space(10, 7), new Tile('b')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(6, 7));
            spaceList.Add(new Space(5, 7));
            spaceList.Add(new Space(9, 7));
            spaceList.Add(new Space(8, 7));
            spaceList.Add(new Space(11, 7));

            sut = new Placement(spaceList);
            List<Space> expected = new List<Space> { new Space(7, 7, 'a'), new Space(10, 7, 'b') };
            
            //Act
            result = sut.GetAnchors(newGame);            

            //Assert
            Assert.That(result.Select(s => s.GetString()).ToList(), Is.EquivalentTo(expected.Select(s => s.GetString()).ToList()));
        }
        [Test]
        [Category("GetAnchors")]
        public void GetAnchors_VerticalTwoSingleGaps()
        {
            Placement sut;
            List<Space> result;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('a')));
            tupleList.Add(Tuple.Create(new Space(7, 10), new Tile('b')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(7, 6));
            spaceList.Add(new Space(7, 5));
            spaceList.Add(new Space(7, 9));
            spaceList.Add(new Space(7, 8));
            spaceList.Add(new Space(7, 11));

            sut = new Placement(spaceList);
            List<Space> expected = new List<Space> { new Space(7, 7, 'a'), new Space(7, 10, 'b') };

            //Act
            result = sut.GetAnchors(newGame);

            //Assert
            Assert.That(result.Select(s => s.GetString()).ToList(), Is.EquivalentTo(expected.Select(s => s.GetString()).ToList()));
        }

        [Test]
        [Category("GetAnchors")]
        public void GetAnchors_VerticalMultiple()
        {
            Placement sut;
            List<Space> result;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(2, 1), new Tile('a')));
            tupleList.Add(Tuple.Create(new Space(2, 4), new Tile('b')));
            tupleList.Add(Tuple.Create(new Space(2, 5), new Tile('c')));
            tupleList.Add(Tuple.Create(new Space(2, 6), new Tile('d')));
            tupleList.Add(Tuple.Create(new Space(2, 8), new Tile('e')));
            tupleList.Add(Tuple.Create(new Space(2, 11), new Tile('f')));
            tupleList.Add(Tuple.Create(new Space(2, 12), new Tile('g')));
            tupleList.Add(Tuple.Create(new Space(2, 13), new Tile('h')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(2, 2));
            spaceList.Add(new Space(2, 3));
            spaceList.Add(new Space(2, 7));
            spaceList.Add(new Space(2, 9));
            spaceList.Add(new Space(2, 10));

            sut = new Placement(spaceList);
            List<Space> expected = new List<Space> {
                new Space(2, 1, 'a'),
                new Space(2, 4, 'b'),
                new Space(2, 5, 'c'),
                new Space(2, 6, 'd'),
                new Space(2, 8, 'e'),
                new Space(2, 11, 'f'),
                new Space(2, 12, 'g'),
                new Space(2, 13, 'h')
            };

            //Act
            result = sut.GetAnchors(newGame);

            //Assert
            Assert.That(result.Select(s => s.GetString()).ToList(), Is.EquivalentTo(expected.Select(s => s.GetString()).ToList()));
        }
        [Test]
        [Category("GetAnchors")]
        public void GetAnchors_HorizontalMultiple()
        {
            Placement sut;
            List<Space> result;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(1, 14), new Tile('a')));
            tupleList.Add(Tuple.Create(new Space(4, 14), new Tile('b')));
            tupleList.Add(Tuple.Create(new Space(5, 14), new Tile('c')));
            tupleList.Add(Tuple.Create(new Space(6, 14), new Tile('d')));
            tupleList.Add(Tuple.Create(new Space(8, 14), new Tile('e')));
            tupleList.Add(Tuple.Create(new Space(11, 14), new Tile('f')));
            tupleList.Add(Tuple.Create(new Space(12, 14), new Tile('g')));
            tupleList.Add(Tuple.Create(new Space(13, 14), new Tile('h')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(2, 14));
            spaceList.Add(new Space(3, 14));
            spaceList.Add(new Space(7, 14));
            spaceList.Add(new Space(9, 14));
            spaceList.Add(new Space(10, 14));

            sut = new Placement(spaceList);
            List<Space> expected = new List<Space> {
                new Space(1, 14, 'a'),
                new Space(4, 14, 'b'),
                new Space(5, 14, 'c'),
                new Space(6, 14, 'd'),
                new Space(8, 14, 'e'),
                new Space(11, 14, 'f'),
                new Space(12, 14, 'g'),
                new Space(13, 14, 'h')
            };

            //Act
            result = sut.GetAnchors(newGame);

            //Assert
            Assert.That(result.Select(s => s.GetString()).ToList(), Is.EquivalentTo(expected.Select(s => s.GetString()).ToList()));
        }

        [Test]
        [Category("GetAnchors")]
        public void GetAnchors_Corner0000()
        {
            Placement sut;
            List<Space> result;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(0, 0), new Tile('a')));
            tupleList.Add(Tuple.Create(new Space(2, 0), new Tile('b')));
            tupleList.Add(Tuple.Create(new Space(3, 0), new Tile('c')));
            tupleList.Add(Tuple.Create(new Space(6, 0), new Tile('d')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(1, 0));
            spaceList.Add(new Space(4, 0));
            spaceList.Add(new Space(5, 0));

            sut = new Placement(spaceList);
            List<Space> expected = new List<Space> {
                new Space(0, 0, 'a'),
                new Space(2, 0, 'b'),
                new Space(3, 0, 'c'),
                new Space(6, 0, 'd'),
 
            };

            //Act
            result = sut.GetAnchors(newGame);

            //Assert
            Assert.That(result.Select(s => s.GetString()).ToList(), Is.EquivalentTo(expected.Select(s => s.GetString()).ToList()));
        }
        [Test]
        [Category("GetAnchors")]
        public void GetAnchors_Corner1400()
        {
            Placement sut;
            List<Space> result;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(11, 0), new Tile('a')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(14, 0));
            spaceList.Add(new Space(13, 0));
            spaceList.Add(new Space(12, 0));

            sut = new Placement(spaceList);
            List<Space> expected = new List<Space> {
                new Space(11, 0, 'a')
            };

            //Act
            result = sut.GetAnchors(newGame);

            //Assert
            Assert.That(result.Select(s => s.GetString()).ToList(), Is.EquivalentTo(expected.Select(s => s.GetString()).ToList()));
        }
        [Test]
        [Category("GetAnchors")]
        public void GetAnchors_Corner1414()
        {
            Placement sut;
            List<Space> result;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(14, 14), new Tile('a')));
            tupleList.Add(Tuple.Create(new Space(14, 12), new Tile('b')));
            tupleList.Add(Tuple.Create(new Space(14, 10), new Tile('c')));
            tupleList.Add(Tuple.Create(new Space(14, 9), new Tile('d')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(14, 13));
            spaceList.Add(new Space(14, 11));
          
            sut = new Placement(spaceList);
            List<Space> expected = new List<Space> {
                new Space(14, 14, 'a'),
                new Space(14, 12, 'b'),
                new Space(14, 10, 'c'),
                new Space(14, 9, 'd'),

            };

            //Act
            result = sut.GetAnchors(newGame);

            //Assert
            Assert.That(result.Select(s => s.GetString()).ToList(), Is.EquivalentTo(expected.Select(s => s.GetString()).ToList()));
        }
        [Test]
        [Category("GetAnchors")]
        public void GetAnchors_Corner0014()
        {
            Placement sut;
            List<Space> result;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(0, 14), new Tile('a')));
            tupleList.Add(Tuple.Create(new Space(0, 13), new Tile('b')));
            tupleList.Add(Tuple.Create(new Space(0, 10), new Tile('c')));
            tupleList.Add(Tuple.Create(new Space(0, 8), new Tile('d')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(0, 12));
            spaceList.Add(new Space(0, 11));
            spaceList.Add(new Space(0, 9));
            spaceList.Add(new Space(0, 7));

            sut = new Placement(spaceList);
            List<Space> expected = new List<Space> {
                new Space(0, 14, 'a'),
                new Space(0, 13, 'b'),
                new Space(0, 10, 'c'),
                new Space(0, 08, 'd'),

            };

            //Act
            result = sut.GetAnchors(newGame);

            //Assert
            Assert.That(result.Select(s => s.GetString()).ToList(), Is.EquivalentTo(expected.Select(s => s.GetString()).ToList()));
        }


        #endregion
    }
}
