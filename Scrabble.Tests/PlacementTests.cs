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
        Game game = new Game();   
        #region//HasNoDuplicates

        [Test]
        [Category("HasNoDuplicates")]
        public void HasNoDuplicates_Single()
        {
            Placement sut;
            bool result = false;

            //Arrang

            sut = new Placement(new Space(7,7), game);
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
            sut = new Placement(spaceList, game);
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
            sut = new Placement(spaceList, game);
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

            List<Space> spaceList = new List<Space>() {
                new Space(1, 1),
                new Space(6, 7),
                new Space(1, 7),
                new Space(7, 13),
                new Space(7, 8),
                new Space(7, 7),
                new Space(7, 7)
            };
            sut = new Placement(spaceList, game);
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

            List<Space> spaceList = new List<Space>() {
                new Space(1, 1),
                new Space(6, 7),
                new Space(1, 7),
                new Space(7, 13),
                new Space(7, 8),
                new Space(7, 7),
                new Space(7, 9)
            };
            sut = new Placement(spaceList, game);
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
            sut = new Placement(new Space(7, 7), game);

            //Act
            result = sut.IsFirstMove();

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
            sut = new Placement(new List<Space>() { new Space(7, 7), new Space(7, 8), new Space(7, 9), new Space(7, 6)}, game);

            //Act
            result = sut.IsFirstMove();

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
            tupleList.Add(Tuple.Create(new Space(10, 7), new Tile('A')));
            tupleList.Add(Tuple.Create(new Space(10, 8), new Tile('B')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            sut = new Placement(new Space(7, 7), newGame);

            //Act
            result = sut.IsFirstMove();

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
            sut = new Placement(new List<Space>() { new Space(8, 7), new Space(8, 8), new Space(8, 9), new Space(7, 8) }, newGame);

            //Act
            result = sut.IsFirstMove();

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
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('A')));
            tupleList.Add(Tuple.Create(new Space(7, 8), new Tile('B')));
            tupleList.Add(Tuple.Create(new Space(7, 9), new Tile('C')));
            tupleList.Add(Tuple.Create(new Space(7, 10), new Tile('D')));
            tupleList.Add(Tuple.Create(new Space(7, 11), new Tile('E')));
            tupleList.Add(Tuple.Create(new Space(7, 12), new Tile('F')));
            tupleList.Add(Tuple.Create(new Space(7, 13), new Tile('G')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(7, 6));
            sut = new Placement(spaceList, newGame);

            //Act
            result = sut.IsAdjacent();

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
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('A')));
            tupleList.Add(Tuple.Create(new Space(7, 8), new Tile('B')));
            tupleList.Add(Tuple.Create(new Space(7, 9), new Tile('C')));
            tupleList.Add(Tuple.Create(new Space(7, 10), new Tile('D')));
            tupleList.Add(Tuple.Create(new Space(7, 11), new Tile('E')));
            tupleList.Add(Tuple.Create(new Space(7, 12), new Tile('F')));
            tupleList.Add(Tuple.Create(new Space(7, 13), new Tile('G')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(7, 14));
            sut = new Placement(spaceList, newGame);

            //Act
            result = sut.IsAdjacent();

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
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('A')));
            tupleList.Add(Tuple.Create(new Space(7, 8), new Tile('B')));
            tupleList.Add(Tuple.Create(new Space(7, 9), new Tile('C')));
            tupleList.Add(Tuple.Create(new Space(7, 10), new Tile('D')));
            tupleList.Add(Tuple.Create(new Space(7, 11), new Tile('E')));
            tupleList.Add(Tuple.Create(new Space(7, 12), new Tile('F')));
            tupleList.Add(Tuple.Create(new Space(7, 13), new Tile('G')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(6, 10));
            sut = new Placement(spaceList, newGame);

            //Act
            result = sut.IsAdjacent();

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
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('A')));
            tupleList.Add(Tuple.Create(new Space(7, 8), new Tile('B')));
            tupleList.Add(Tuple.Create(new Space(7, 9), new Tile('C')));
            tupleList.Add(Tuple.Create(new Space(7, 10), new Tile('D')));
            tupleList.Add(Tuple.Create(new Space(7, 11), new Tile('E')));
            tupleList.Add(Tuple.Create(new Space(7, 12), new Tile('F')));
            tupleList.Add(Tuple.Create(new Space(7, 13), new Tile('G')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(8, 10));
            sut = new Placement(spaceList, newGame);

            //Act
            result = sut.IsAdjacent();

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
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('A')));
            tupleList.Add(Tuple.Create(new Space(7, 8), new Tile('B')));
            tupleList.Add(Tuple.Create(new Space(7, 9), new Tile('C')));
            tupleList.Add(Tuple.Create(new Space(7, 10), new Tile('D')));
            tupleList.Add(Tuple.Create(new Space(7, 11), new Tile('E')));
            tupleList.Add(Tuple.Create(new Space(7, 12), new Tile('F')));
            tupleList.Add(Tuple.Create(new Space(7, 13), new Tile('G')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(8, 10));
            spaceList.Add(new Space(8, 11));
            spaceList.Add(new Space(8, 12));
            spaceList.Add(new Space(8, 13));
            spaceList.Add(new Space(8, 14));

            sut = new Placement(spaceList, newGame);

            //Act
            result = sut.IsAdjacent();

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
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('A')));
            tupleList.Add(Tuple.Create(new Space(7, 8), new Tile('B')));
            tupleList.Add(Tuple.Create(new Space(7, 9), new Tile('C')));
            tupleList.Add(Tuple.Create(new Space(7, 10), new Tile('D')));
            tupleList.Add(Tuple.Create(new Space(7, 11), new Tile('E')));
            tupleList.Add(Tuple.Create(new Space(7, 12), new Tile('F')));
            tupleList.Add(Tuple.Create(new Space(7, 13), new Tile('G')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(7, 6));
            spaceList.Add(new Space(4, 6));
            spaceList.Add(new Space(5, 6));
            spaceList.Add(new Space(6, 6));
            spaceList.Add(new Space(8, 6));

            sut = new Placement(spaceList, newGame);

            //Act
            result = sut.IsAdjacent();

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
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('A')));
            tupleList.Add(Tuple.Create(new Space(7, 8), new Tile('B')));
            tupleList.Add(Tuple.Create(new Space(7, 9), new Tile('C')));
            tupleList.Add(Tuple.Create(new Space(7, 10), new Tile('D')));
            tupleList.Add(Tuple.Create(new Space(7, 11), new Tile('E')));
            tupleList.Add(Tuple.Create(new Space(7, 12), new Tile('F')));
            tupleList.Add(Tuple.Create(new Space(7, 13), new Tile('G')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(10, 12));

            sut = new Placement(spaceList, newGame);

            //Act
            result = sut.IsAdjacent();

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
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('A')));
            tupleList.Add(Tuple.Create(new Space(7, 8), new Tile('B')));
            tupleList.Add(Tuple.Create(new Space(7, 9), new Tile('C')));
            tupleList.Add(Tuple.Create(new Space(7, 10), new Tile('D')));
            tupleList.Add(Tuple.Create(new Space(7, 11), new Tile('E')));
            tupleList.Add(Tuple.Create(new Space(7, 12), new Tile('F')));
            tupleList.Add(Tuple.Create(new Space(7, 13), new Tile('G')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(2, 6));
            spaceList.Add(new Space(2, 5));
            spaceList.Add(new Space(2, 4));
            spaceList.Add(new Space(2, 3));
            spaceList.Add(new Space(2, 2));

            sut = new Placement(spaceList, newGame);

            //Act
            result = sut.IsAdjacent();

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
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('A')));
            tupleList.Add(Tuple.Create(new Space(7, 8), new Tile('B')));
            tupleList.Add(Tuple.Create(new Space(7, 9), new Tile('C')));
            tupleList.Add(Tuple.Create(new Space(7, 10), new Tile('D')));
            tupleList.Add(Tuple.Create(new Space(7, 11), new Tile('E')));
            tupleList.Add(Tuple.Create(new Space(7, 12), new Tile('F')));
            tupleList.Add(Tuple.Create(new Space(7, 13), new Tile('G')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            Placement sut1 = new Placement(new Space(0, 0), newGame);
            Placement sut2 = new Placement(new Space(0, 14), newGame);
            Placement sut3 = new Placement(new Space(14, 0), newGame);
            Placement sut4 = new Placement(new Space(0, 14), newGame);

            //Act
            result1 = sut1.IsAdjacent();
            result2 = sut2.IsAdjacent();
            result3 = sut3.IsAdjacent();
            result4 = sut4.IsAdjacent();

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

            sut = new Placement(space, game);
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

            sut = new Placement(new List<Space>() { space1, space2 }, game);
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

            sut = new Placement(spaceList, game);

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

            sut = new Placement(spaceList, game);

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

            sut = new Placement(spaceList, game);

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

            sut = new Placement(space, game);
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

            sut = new Placement(spaceList, game);

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
            spaceList.Add(new Space(6, 14));

            sut = new Placement(spaceList, game);

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

            sut = new Placement(spaceList, game);

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

            sut = new Placement(spaceList, game);

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

            sut = new Placement(space, game);
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

            sut = new Placement(spaceList, game);

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

            sut = new Placement(spaceList, game);
            //Act
            result = sut.IsContiguous();

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

            sut = new Placement(spaceList, game);
            //Act
            result = sut.IsContiguous();

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

            sut = new Placement(spaceList, game);
            //Act
            result = sut.IsContiguous();

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

            sut = new Placement(spaceList, game);
            //Act
            result = sut.IsContiguous();

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
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('A')));
            tupleList.Add(Tuple.Create(new Space(7, 8), new Tile('B')));
            tupleList.Add(Tuple.Create(new Space(7, 9), new Tile('C')));
            tupleList.Add(Tuple.Create(new Space(7, 10), new Tile('D')));
            tupleList.Add(Tuple.Create(new Space(7, 11), new Tile('E')));
            tupleList.Add(Tuple.Create(new Space(7, 12), new Tile('F')));
            tupleList.Add(Tuple.Create(new Space(7, 13), new Tile('G')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(5, 7));
            spaceList.Add(new Space(6, 7));
            spaceList.Add(new Space(8, 7));
            spaceList.Add(new Space(9, 7));
            spaceList.Add(new Space(10, 7));

            sut = new Placement(spaceList, newGame);
            //Act
            result = sut.IsContiguous();

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
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('A')));
            tupleList.Add(Tuple.Create(new Space(8, 7), new Tile('B')));
            tupleList.Add(Tuple.Create(new Space(9, 7), new Tile('C')));
            tupleList.Add(Tuple.Create(new Space(10, 7), new Tile('D')));
            tupleList.Add(Tuple.Create(new Space(11, 7), new Tile('E')));
            tupleList.Add(Tuple.Create(new Space(12, 7), new Tile('F')));
            tupleList.Add(Tuple.Create(new Space(13, 7), new Tile('G')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(8, 6));
            spaceList.Add(new Space(8, 10));
            spaceList.Add(new Space(8, 9));
            spaceList.Add(new Space(8, 8));
            spaceList.Add(new Space(8, 11));

            sut = new Placement(spaceList, newGame);
            //Act
            result = sut.IsContiguous();

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
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('A')));
            tupleList.Add(Tuple.Create(new Space(7, 8), new Tile('B')));         

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(7, 6));
            spaceList.Add(new Space(7, 5));
            spaceList.Add(new Space(7, 9));
            spaceList.Add(new Space(7, 10));
            spaceList.Add(new Space(7, 11));

            sut = new Placement(spaceList, newGame);
            //Act
            result = sut.IsContiguous();

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
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('A')));
            tupleList.Add(Tuple.Create(new Space(8, 7), new Tile('B')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(6, 7));
            spaceList.Add(new Space(5, 7));
            spaceList.Add(new Space(9, 7));
            spaceList.Add(new Space(10, 7));
            spaceList.Add(new Space(11, 7));

            sut = new Placement(spaceList, newGame);
            //Act
            result = sut.IsContiguous();

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
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('A')));
            tupleList.Add(Tuple.Create(new Space(7, 10), new Tile('B')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(7, 6));
            spaceList.Add(new Space(7, 5));
            spaceList.Add(new Space(7, 9));
            spaceList.Add(new Space(7, 8));
            spaceList.Add(new Space(7, 11));

            sut = new Placement(spaceList, newGame);
            //Act
            result = sut.IsContiguous();

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
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('A')));
            tupleList.Add(Tuple.Create(new Space(10, 7), new Tile('B')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(6, 7));
            spaceList.Add(new Space(5, 7));
            spaceList.Add(new Space(9, 7));
            spaceList.Add(new Space(8, 7));
            spaceList.Add(new Space(11, 7));

            sut = new Placement(spaceList, newGame);
            //Act
            result = sut.IsContiguous();

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
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('A')));
            tupleList.Add(Tuple.Create(new Space(7, 8), new Tile('B')));
            tupleList.Add(Tuple.Create(new Space(7, 9), new Tile('C')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(7, 6));
            spaceList.Add(new Space(7, 5));
            spaceList.Add(new Space(7, 10));
            spaceList.Add(new Space(7, 12));
            spaceList.Add(new Space(7, 11));

            sut = new Placement(spaceList, newGame);
            //Act
            result = sut.IsContiguous();

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
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('A')));
            tupleList.Add(Tuple.Create(new Space(8, 7), new Tile('B')));
            tupleList.Add(Tuple.Create(new Space(9, 7), new Tile('C')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(6, 7));
            spaceList.Add(new Space(5, 7));
            spaceList.Add(new Space(10, 7));
            spaceList.Add(new Space(4, 7));
            spaceList.Add(new Space(11, 7));

            sut = new Placement(spaceList, newGame);
            //Act
            result = sut.IsContiguous();

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
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('A')));
            tupleList.Add(Tuple.Create(new Space(7, 8), new Tile('B')));
            tupleList.Add(Tuple.Create(new Space(7, 9), new Tile('C')));
            tupleList.Add(Tuple.Create(new Space(7, 10), new Tile('D')));
            tupleList.Add(Tuple.Create(new Space(7, 11), new Tile('E')));
            tupleList.Add(Tuple.Create(new Space(7, 12), new Tile('F')));
            tupleList.Add(Tuple.Create(new Space(7, 13), new Tile('G')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(1, 7));
            spaceList.Add(new Space(2, 7));
            spaceList.Add(new Space(4, 7));
            spaceList.Add(new Space(5, 7));
            spaceList.Add(new Space(6, 7));

            sut = new Placement(spaceList, newGame);
            //Act
            result = sut.IsContiguous();

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
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('A')));
            tupleList.Add(Tuple.Create(new Space(8, 7), new Tile('B')));
            tupleList.Add(Tuple.Create(new Space(9, 7), new Tile('C')));
            tupleList.Add(Tuple.Create(new Space(10, 7), new Tile('D')));
            tupleList.Add(Tuple.Create(new Space(11, 7), new Tile('E')));
            tupleList.Add(Tuple.Create(new Space(12, 7), new Tile('F')));
            tupleList.Add(Tuple.Create(new Space(13, 7), new Tile('G')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(7, 1));
            spaceList.Add(new Space(7, 2));
            spaceList.Add(new Space(7, 4));
            spaceList.Add(new Space(7, 5));
            spaceList.Add(new Space(7, 6));

            sut = new Placement(spaceList, newGame);
            //Act
            result = sut.IsContiguous();

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
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('A')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(7, 6));
            spaceList.Add(new Space(7, 5));
            spaceList.Add(new Space(7, 9));
            spaceList.Add(new Space(7, 10));
            spaceList.Add(new Space(7, 11));

            sut = new Placement(spaceList, newGame);
            //Act
            result = sut.IsContiguous();

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
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('A')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(6, 7));
            spaceList.Add(new Space(5, 7));
            spaceList.Add(new Space(9, 7));
            spaceList.Add(new Space(10, 7));
            spaceList.Add(new Space(11, 7));

            sut = new Placement(spaceList, newGame);
            //Act
            result = sut.IsContiguous();

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
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('A')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(7, 6));
            spaceList.Add(new Space(7, 5));
            spaceList.Add(new Space(7, 9));
            spaceList.Add(new Space(7, 8));
            spaceList.Add(new Space(7, 11));

            sut = new Placement(spaceList, newGame);
            //Act
            result = sut.IsContiguous();

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
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('A')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(6, 7));
            spaceList.Add(new Space(5, 7));
            spaceList.Add(new Space(9, 7));
            spaceList.Add(new Space(8, 7));
            spaceList.Add(new Space(11, 7));

            sut = new Placement(spaceList, newGame);
            //Act
            result = sut.IsContiguous();

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
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('A')));
 
            tupleList.Add(Tuple.Create(new Space(7, 9), new Tile('C')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(7, 6));
            spaceList.Add(new Space(7, 5));
            spaceList.Add(new Space(7, 10));
            spaceList.Add(new Space(7, 12));
            spaceList.Add(new Space(7, 11));

            sut = new Placement(spaceList, newGame);
            //Act
            result = sut.IsContiguous();

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
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('A')));
            tupleList.Add(Tuple.Create(new Space(7, 8), new Tile('B')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(7, 6));
            spaceList.Add(new Space(7, 5));
            spaceList.Add(new Space(7, 10));
            spaceList.Add(new Space(7, 12));
            spaceList.Add(new Space(7, 11));

            sut = new Placement(spaceList, newGame);
            //Act
            result = sut.IsContiguous();

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
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('A')));
            tupleList.Add(Tuple.Create(new Space(8, 7), new Tile('B')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(6, 7));
            spaceList.Add(new Space(5, 7));
            spaceList.Add(new Space(10, 7));
            spaceList.Add(new Space(4, 7));
            spaceList.Add(new Space(11, 7));

            sut = new Placement(spaceList, newGame);
            //Act
            result = sut.IsContiguous();

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
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('A')));
            tupleList.Add(Tuple.Create(new Space(9, 7), new Tile('C')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(6, 7));
            spaceList.Add(new Space(5, 7));
            spaceList.Add(new Space(10, 7));
            spaceList.Add(new Space(4, 7));
            spaceList.Add(new Space(11, 7));

            sut = new Placement(spaceList, newGame);
            //Act
            result = sut.IsContiguous();

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
            tupleList.Add(Tuple.Create(new Space(1, 7), new Tile('A')));
            tupleList.Add(Tuple.Create(new Space(3, 7), new Tile('C')));
            tupleList.Add(Tuple.Create(new Space(10, 7), new Tile('J')));
            tupleList.Add(Tuple.Create(new Space(12, 7), new Tile('L')));


            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(2, 7));
            spaceList.Add(new Space(11, 7));

            sut = new Placement(spaceList, newGame);
            //Act
            result = sut.IsContiguous();

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
            tupleList.Add(Tuple.Create(new Space(1, 7), new Tile('A')));
            tupleList.Add(Tuple.Create(new Space(3, 7), new Tile('C')));
            tupleList.Add(Tuple.Create(new Space(10, 7), new Tile('J')));
            tupleList.Add(Tuple.Create(new Space(12, 7), new Tile('L')));


            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(2, 7));
            spaceList.Add(new Space(4, 7));
            spaceList.Add(new Space(11, 7));

            sut = new Placement(spaceList, newGame);
            //Act
            result = sut.IsContiguous();

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
            tupleList.Add(Tuple.Create(new Space(1, 7), new Tile('A')));
            tupleList.Add(Tuple.Create(new Space(3, 7), new Tile('C')));
            tupleList.Add(Tuple.Create(new Space(10, 7), new Tile('J')));
            tupleList.Add(Tuple.Create(new Space(12, 7), new Tile('L')));


            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(2, 7));
            spaceList.Add(new Space(4, 7));
            spaceList.Add(new Space(9, 7));
            spaceList.Add(new Space(11, 7));

            sut = new Placement(spaceList, newGame);
            //Act
            result = sut.IsContiguous();

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
            tupleList.Add(Tuple.Create(new Space(7, 1), new Tile('A')));
            tupleList.Add(Tuple.Create(new Space(7, 3), new Tile('C')));
            tupleList.Add(Tuple.Create(new Space(7, 10), new Tile('J')));
            tupleList.Add(Tuple.Create(new Space(7, 12), new Tile('L')));


            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(7, 2));
            spaceList.Add(new Space(7, 11));

            sut = new Placement(spaceList, newGame);
            //Act
            result = sut.IsContiguous();

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
            tupleList.Add(Tuple.Create(new Space(7, 1), new Tile('A')));
            tupleList.Add(Tuple.Create(new Space(7, 3), new Tile('C')));
            tupleList.Add(Tuple.Create(new Space(7, 10), new Tile('J')));
            tupleList.Add(Tuple.Create(new Space(7, 12), new Tile('L')));


            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(7, 2));
            spaceList.Add(new Space(7, 4));
            spaceList.Add(new Space(7, 11));

            sut = new Placement(spaceList, newGame);
            //Act
            result = sut.IsContiguous();

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
            tupleList.Add(Tuple.Create(new Space(7, 1), new Tile('A')));
            tupleList.Add(Tuple.Create(new Space(7, 3), new Tile('C')));
            tupleList.Add(Tuple.Create(new Space(7, 10), new Tile('J')));
            tupleList.Add(Tuple.Create(new Space(7, 12), new Tile('L')));


            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(7, 2));
            spaceList.Add(new Space(7, 4));
            spaceList.Add(new Space(7, 9));
            spaceList.Add(new Space(7, 11));

            sut = new Placement(spaceList, newGame);
            //Act
            result = sut.IsContiguous();

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
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('A')));
            tupleList.Add(Tuple.Create(new Space(7, 8), new Tile('B')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(7, 6));
            spaceList.Add(new Space(7, 5));
            spaceList.Add(new Space(7, 9));
            spaceList.Add(new Space(7, 10));
            spaceList.Add(new Space(7, 11));

            sut = new Placement(spaceList, newGame);

            var expected = new List<Space>{ new Space(7, 8, 'B'), new Space(7, 7, 'A') };
            
            //Act
            result = sut.Anchors;

            //Assert
            
            Assert.That(result.Except(expected, SpaceTileEqualityComparer.Instance).Count(), Is.EqualTo(0));
        }

        [Test]
        [Category("GetAnchors")]
        public void GetAnchors_Horizontal_DoubleMiddleGap()
        {
            Placement sut;
            List<Space> result = null;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('A')));
            tupleList.Add(Tuple.Create(new Space(8, 7), new Tile('B')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(6, 7));
            spaceList.Add(new Space(5, 7));
            spaceList.Add(new Space(9, 7));
            spaceList.Add(new Space(10, 7));
            spaceList.Add(new Space(11, 7));

            sut = new Placement(spaceList, newGame);

            var expected = new List<Space> { new Space(7, 7, 'A'), new Space(8, 7, 'B') };
            //Act
            result = sut.Anchors;

            //Assert            
            Assert.That(result.Except(expected, SpaceTileEqualityComparer.Instance).Count(), Is.EqualTo(0));
        }

        [Test]
        [Category("GetAnchors")]
        public void GetAnchors_DoubleMiddleGap_WrongLetters()
        {
            Placement sut;
            List<Space> result = null;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('A')));
            tupleList.Add(Tuple.Create(new Space(7, 8), new Tile('B')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(7, 6));
            spaceList.Add(new Space(7, 5));
            spaceList.Add(new Space(7, 9));
            spaceList.Add(new Space(7, 10));
            spaceList.Add(new Space(7, 11));

            sut = new Placement(spaceList, newGame);

            var expected = new List<Space> { new Space(7, 7, 'P'), new Space(7, 8, 'Q') };
            //Act
            result = sut.Anchors;

            //Assert            
            Assert.That(result.Except(expected, SpaceTileEqualityComparer.Instance).Count(), Is.EqualTo(2));
        }

        [Test]
        [Category("GetAnchors")]
        public void GetAnchors_HorizontalTwoSingleGaps()
        {
            Placement sut;
            List<Space> result;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('A')));
            tupleList.Add(Tuple.Create(new Space(10, 7), new Tile('B')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(6, 7));
            spaceList.Add(new Space(5, 7));
            spaceList.Add(new Space(9, 7));
            spaceList.Add(new Space(8, 7));
            spaceList.Add(new Space(11, 7));

            sut = new Placement(spaceList, newGame);
            List<Space> expected = new List<Space> { new Space(7, 7, 'A'), new Space(10, 7, 'B') };
            
            //Act
            result = sut.Anchors;            

            //Assert            
            Assert.That(result.Except(expected, SpaceTileEqualityComparer.Instance).Count(), Is.EqualTo(0));
        }
        [Test]
        [Category("GetAnchors")]
        public void GetAnchors_VerticalTwoSingleGaps()
        {
            Placement sut;
            List<Space> result;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('A')));
            tupleList.Add(Tuple.Create(new Space(7, 10), new Tile('B')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(7, 6));
            spaceList.Add(new Space(7, 5));
            spaceList.Add(new Space(7, 9));
            spaceList.Add(new Space(7, 8));
            spaceList.Add(new Space(7, 11));

            sut = new Placement(spaceList, newGame);
            List<Space> expected = new List<Space> { new Space(7, 7, 'A'), new Space(7, 10, 'B') };

            //Act
            result = sut.Anchors;

            //Assert            
            Assert.That(result.Except(expected, SpaceTileEqualityComparer.Instance).Count(), Is.EqualTo(0));
        }

        [Test]
        [Category("GetAnchors")]
        public void GetAnchors_VerticalMultiple()
        {
            Placement sut;
            List<Space> result;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(2, 1), new Tile('A')));
            tupleList.Add(Tuple.Create(new Space(2, 4), new Tile('B')));
            tupleList.Add(Tuple.Create(new Space(2, 5), new Tile('C')));
            tupleList.Add(Tuple.Create(new Space(2, 6), new Tile('D')));
            tupleList.Add(Tuple.Create(new Space(2, 8), new Tile('E')));
            tupleList.Add(Tuple.Create(new Space(2, 11), new Tile('F')));
            tupleList.Add(Tuple.Create(new Space(2, 12), new Tile('G')));
            tupleList.Add(Tuple.Create(new Space(2, 13), new Tile('H')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(2, 2));
            spaceList.Add(new Space(2, 3));
            spaceList.Add(new Space(2, 7));
            spaceList.Add(new Space(2, 9));
            spaceList.Add(new Space(2, 10));

            sut = new Placement(spaceList, newGame);
            List<Space> expected = new List<Space> {
                new Space(2, 1, 'A'),
                new Space(2, 4, 'B'),
                new Space(2, 5, 'C'),
                new Space(2, 6, 'D'),
                new Space(2, 8, 'E'),
                new Space(2, 11, 'F'),
                new Space(2, 12, 'G'),
                new Space(2, 13, 'H')
            };

            //Act
            result = sut.Anchors;

            //Assert            
            Assert.That(result.Except(expected, SpaceTileEqualityComparer.Instance).Count(), Is.EqualTo(0));
        }
        [Test]
        [Category("GetAnchors")]
        public void GetAnchors_HorizontalMultiple()
        {
            Placement sut;
            List<Space> result;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(1, 14), new Tile('A')));
            tupleList.Add(Tuple.Create(new Space(4, 14), new Tile('B')));
            tupleList.Add(Tuple.Create(new Space(5, 14), new Tile('C')));
            tupleList.Add(Tuple.Create(new Space(6, 14), new Tile('D')));
            tupleList.Add(Tuple.Create(new Space(8, 14), new Tile('E')));
            tupleList.Add(Tuple.Create(new Space(11, 14), new Tile('F')));
            tupleList.Add(Tuple.Create(new Space(12, 14), new Tile('G')));
            tupleList.Add(Tuple.Create(new Space(13, 14), new Tile('H')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(2, 14));
            spaceList.Add(new Space(3, 14));
            spaceList.Add(new Space(7, 14));
            spaceList.Add(new Space(9, 14));
            spaceList.Add(new Space(10, 14));

            sut = new Placement(spaceList, newGame);
            List<Space> expected = new List<Space> {
                new Space(1, 14, 'A'),
                new Space(4, 14, 'B'),
                new Space(5, 14, 'C'),
                new Space(6, 14, 'D'),
                new Space(8, 14, 'E'),
                new Space(11, 14, 'F'),
                new Space(12, 14, 'G'),
                new Space(13, 14, 'H')
            };

            //Act
            result = sut.Anchors;

            //Assert            
            Assert.That(result.Except(expected, SpaceTileEqualityComparer.Instance).Count(), Is.EqualTo(0));
        }

        [Test]
        [Category("GetAnchors")]
        public void GetAnchors_Corner0000()
        {
            Placement sut;
            List<Space> result;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(0, 0), new Tile('A')));
            tupleList.Add(Tuple.Create(new Space(2, 0), new Tile('B')));
            tupleList.Add(Tuple.Create(new Space(3, 0), new Tile('C')));
            tupleList.Add(Tuple.Create(new Space(6, 0), new Tile('D')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(1, 0));
            spaceList.Add(new Space(4, 0));
            spaceList.Add(new Space(5, 0));

            sut = new Placement(spaceList, newGame);
            List<Space> expected = new List<Space> {
                new Space(0, 0, 'A'),
                new Space(2, 0, 'B'),
                new Space(3, 0, 'C'),
                new Space(6, 0, 'D'),
 
            };

            //Act
            result = sut.Anchors;

            //Assert            
            Assert.That(result.Except(expected, SpaceTileEqualityComparer.Instance).Count(), Is.EqualTo(0));
        }
        [Test]
        [Category("GetAnchors")]
        public void GetAnchors_Corner1400()
        {
            Placement sut;
            List<Space> result;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(11, 0), new Tile('A')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(14, 0));
            spaceList.Add(new Space(13, 0));
            spaceList.Add(new Space(12, 0));

            sut = new Placement(spaceList, newGame);
            List<Space> expected = new List<Space> {
                new Space(11, 0, 'A')
            };

            //Act
            result = sut.Anchors;

            //Assert           
            Assert.That(result.Except(expected, SpaceTileEqualityComparer.Instance).Count(), Is.EqualTo(0));
        }
        [Test]
        [Category("GetAnchors")]
        public void GetAnchors_Corner1414()
        {
            Placement sut;
            List<Space> result;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(14, 14), new Tile('A')));
            tupleList.Add(Tuple.Create(new Space(14, 12), new Tile('B')));
            tupleList.Add(Tuple.Create(new Space(14, 10), new Tile('C')));
            tupleList.Add(Tuple.Create(new Space(14, 9), new Tile('D')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(14, 13));
            spaceList.Add(new Space(14, 11));
          
            sut = new Placement(spaceList, newGame);
            List<Space> expected = new List<Space> {
                new Space(14, 14, 'A'),
                new Space(14, 12, 'B'),
                new Space(14, 10, 'C'),
                new Space(14, 9, 'D'),

            };

            //Act
            result = sut.Anchors;

            //Assert            
            Assert.That(result.Except(expected, SpaceTileEqualityComparer.Instance).Count(), Is.EqualTo(0));
        }
        [Test]
        [Category("GetAnchors")]
        public void GetAnchors_Corner0014()
        {
            Placement sut;
            List<Space> result;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(0, 14), new Tile('A')));
            tupleList.Add(Tuple.Create(new Space(0, 13), new Tile('B')));
            tupleList.Add(Tuple.Create(new Space(0, 10), new Tile('C')));
            tupleList.Add(Tuple.Create(new Space(0, 8), new Tile('D')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(0, 12));
            spaceList.Add(new Space(0, 11));
            spaceList.Add(new Space(0, 9));
            spaceList.Add(new Space(0, 7));

            sut = new Placement(spaceList, newGame);
            List<Space> expected = new List<Space> {
                new Space(0, 14, 'A'),
                new Space(0, 13, 'B'),
                new Space(0, 10, 'C'),
                new Space(0, 08, 'D'),

            };

            //Act
            result = sut.Anchors;

            //Assert            
            Assert.That(result.Except(expected, SpaceTileEqualityComparer.Instance).Count(), Is.EqualTo(0));
        }

        [Test]
        [Category("GetAnchors")]
        public void GetAnchors_NoAnchorsFirstMove()
        {
            Placement sut;
            List<Space> result;

            //Arrange
            Game newGame = new Game();            

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(7, 7));
            spaceList.Add(new Space(7, 8));
            spaceList.Add(new Space(7, 9));
            spaceList.Add(new Space(7, 10));

            sut = new Placement(spaceList, newGame);


            //Act
            result = sut.Anchors;

            //Assert
            Assert.That(result.Count(), Is.EqualTo(0));
        }
        [Test]
        [Category("GetAnchors")]
        public void GetAnchors_NoAnchorsParallelVertical()
        {
            Placement sut;
            List<Space> result;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('A')));
            tupleList.Add(Tuple.Create(new Space(7, 8), new Tile('B')));
            tupleList.Add(Tuple.Create(new Space(7, 9), new Tile('C')));
            tupleList.Add(Tuple.Create(new Space(7, 10), new Tile('D')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(6, 7));
            spaceList.Add(new Space(6, 8));
            spaceList.Add(new Space(6, 9));
            spaceList.Add(new Space(6, 6));

            sut = new Placement(spaceList, newGame);


            //Act
            result = sut.Anchors;

            //Assert
            Assert.That(result.Count(), Is.EqualTo(0));
        }

        [Test]
        [Category("GetAnchors")]
        public void GetAnchors_NoAnchorsParallelHorizontal()
        {
            Placement sut;
            List<Space> result;

            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('A')));
            tupleList.Add(Tuple.Create(new Space(8, 7), new Tile('B')));
            tupleList.Add(Tuple.Create(new Space(9, 7), new Tile('C')));
            tupleList.Add(Tuple.Create(new Space(10, 7), new Tile('D')));

            Game newGame = new Game();
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(6, 8));
            spaceList.Add(new Space(7, 8));
            spaceList.Add(new Space(8, 8));
            spaceList.Add(new Space(9, 8));

            sut = new Placement(spaceList, newGame);


            //Act
            result = sut.Anchors;

            //Assert
            Assert.That(result.Count(), Is.EqualTo(0));
        }
        #endregion
        #region //ValidPlays

        [Test]
        [Category("ValidPlays")]
        public void ValidPlays_SingleTilePlays()
        {
            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('A')));
            tupleList.Add(Tuple.Create(new Space(8, 7), new Tile('B')));
            tupleList.Add(Tuple.Create(new Space(9, 7), new Tile('B')));
            tupleList.Add(Tuple.Create(new Space(10, 7), new Tile('A')));

            Game newGame = new Game("ZAQ");
            newGame.SetBoard(tupleList);

            //play1 expect 0 plays

            List<Space> spaceList1 = new List<Space>();
            spaceList1.Add(new Space(6, 7));


            var placement1 = new Placement(spaceList1, newGame);

            //play2 expect 2 plays, "aa" and "za"

            List<Space> spaceList2 = new List<Space>();
            spaceList2.Add(new Space(7, 6));

            var placement2 = new Placement(spaceList2, newGame);

            //play3 expect 1 plays, "ab"

            List<Space> spaceList3 = new List<Space>();
            spaceList3.Add(new Space(8, 6));

            var placement3 = new Placement(spaceList3, newGame);




            //Act
            var plays1 = placement1.ValidPlays();
            var plays2 = placement2.ValidPlays();
            var plays3 = placement3.ValidPlays();

            //Assert
            Assert.That(plays1.Count, Is.EqualTo(0));
            Assert.That(plays2.Count, Is.EqualTo(2));
            Assert.That(plays3.Count, Is.EqualTo(1));

        }

        [Test]
        [Category("ValidPlays")]
        public void ValidPlays_MultiTileNoPlays()
        {
            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('A')));
            tupleList.Add(Tuple.Create(new Space(8, 7), new Tile('B')));
            tupleList.Add(Tuple.Create(new Space(9, 7), new Tile('B')));
            tupleList.Add(Tuple.Create(new Space(10, 7), new Tile('A')));

            Game newGame = new Game("QVJC");
            newGame.SetBoard(tupleList);

            //play1 expect 0 plays

            List<Space> spaceList1 = new List<Space>();
            spaceList1.Add(new Space(6, 7));
            spaceList1.Add(new Space(5, 7));
            spaceList1.Add(new Space(4, 7));
            spaceList1.Add(new Space(3, 7));

            var placement1 = new Placement(spaceList1, newGame);

            
            //Act
            var plays1 = placement1.ValidPlays();

            //Assert

            Assert.That(plays1.Count, Is.EqualTo(0));

        }

        [Test]
        [Category("ValidPlays")]
        public void ValidPlays_MultiTiles_TrayPlacementMatch_OnePlay()
        {
            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('O')));
            tupleList.Add(Tuple.Create(new Space(8, 7), new Tile('U')));
            tupleList.Add(Tuple.Create(new Space(9, 7), new Tile('T')));
 

            Game newGame = new Game("LAY");
            newGame.SetBoard(tupleList);

            //play1 expect 0 plays

            List<Space> spaceList1 = new List<Space>();
            spaceList1.Add(new Space(6, 7));
            spaceList1.Add(new Space(5, 7));
            spaceList1.Add(new Space(4, 7));


            var placement1 = new Placement(spaceList1, newGame);


            //Act
            var plays1 = placement1.ValidPlays();

            //Assert

            Assert.That(plays1.Count, Is.EqualTo(1));

        }

        [Test]
        [Category("ValidPlays")]
        public void ValidPlays_ManyPlays()
        {
            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(6, 7), new Tile('A')));
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('B')));
            tupleList.Add(Tuple.Create(new Space(8, 7), new Tile('B')));
            tupleList.Add(Tuple.Create(new Space(9, 7), new Tile('A')));

            Game newGame = new Game("ZADE");
            newGame.SetBoard(tupleList);

            //play1 expect 2 plays, "DAZE" and "ADZE"

            List<Space> spaceList1 = new List<Space>();
            spaceList1.Add(new Space(6, 8));
            spaceList1.Add(new Space(5, 8));
            spaceList1.Add(new Space(4, 8));
            spaceList1.Add(new Space(3, 8));

            var placement1 = new Placement(spaceList1, newGame);

            //play2 expect 5 plays, "AE", "ZA", "AD", "ED", "DE"

            List<Space> spaceList2 = new List<Space>();
            spaceList2.Add(new Space(6, 8));
            spaceList2.Add(new Space(5, 8));


            var placement2 = new Placement(spaceList2, newGame);

            //play3 expect 0 plays

            List<Space> spaceList3 = new List<Space>();
            spaceList3.Add(new Space(5, 7));


            var placement3 = new Placement(spaceList3, newGame);

            //play4 expect 1 play, "AB"

            List<Space> spaceList4 = new List<Space>();
            spaceList4.Add(new Space(7, 6));


            var placement4 = new Placement(spaceList4, newGame);


            //Act
            var plays1 = placement1.ValidPlays();
            var plays2 = placement2.ValidPlays();
            var plays3 = placement3.ValidPlays();
            var plays4 = placement4.ValidPlays();

            //Assert

            Assert.That(plays1.Count, Is.EqualTo(2));
            Assert.That(plays2.Count, Is.EqualTo(5));
            Assert.That(plays3.Count, Is.EqualTo(0));
            Assert.That(plays4.Count, Is.EqualTo(1));
        }

        [Test]
        [Category("ValidPlays")]
        public void ValidPlays_MultiTilesWithBlanks_ManyPlays()
        {
            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('A')));
            tupleList.Add(Tuple.Create(new Space(8, 7), new Tile('N')));
            tupleList.Add(Tuple.Create(new Space(9, 7), new Tile('D')));


            Game newGame = new Game("LAY??");
            newGame.SetBoard(tupleList);

            List<Space> spaceList = new List<Space>();
            spaceList.Add(new Space(7, 6));
            spaceList.Add(new Space(8, 6));
            


            var placement1 = new Placement(spaceList, newGame);


            //Act
            var plays1 = placement1.ValidPlays();

            foreach (var item in plays1)
            {
                Debug.WriteLine("Are Words Valid: " + item.AreWordsValid() + " Play: " + item.GetPlayString() + " Word: " + item.GetSubWords()[0].Word);
            }


            //Assert

            Assert.That(plays1.Count, Is.EqualTo(60));

        }


        #endregion
    }
}
