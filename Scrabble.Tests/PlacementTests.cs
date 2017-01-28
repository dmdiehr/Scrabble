using NUnit.Framework;
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
    }
}
