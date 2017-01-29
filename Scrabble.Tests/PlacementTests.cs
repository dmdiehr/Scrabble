﻿using NUnit.Framework;
using Scrabble;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrabble.Tests
{
    [TestFixture]
    public class PlacementTests
    {

        //IsOnBoard Tests
        [Test]
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

        //IsAvailable Tests

        [Test]
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

        //HasNoDuplicates Test
        [Test]
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
        public void HasNoDuplicates_OneDupe()
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

        //IsSingle Tests
        [Test]
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
        
        //IsHorizontal

        [Test]
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
        
        //IsVertical

        [Test]
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
    }
}
