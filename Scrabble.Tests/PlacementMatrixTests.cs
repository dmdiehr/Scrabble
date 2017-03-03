using NUnit.Framework;
using Scrabble;
using System.Collections.Generic;
using System.Linq;

namespace ProjectName.Tests
{
    [TestFixture]
    public class PlacementMatrixTests
    {
        #region //Array Construction
        [Test]
        [Category("PlacementMatrix Array Construction")]
        public void BoolArray_EmptyBoard_NoSubWords()
        {
            //Arrange

            Game game = new Game("ABCDEFG");

            List<Space> placementList = new List<Space>();
            placementList.Add(new Space(7, 7));
            placementList.Add(new Space(7, 8));
            placementList.Add(new Space(7, 9));
            placementList.Add(new Space(7, 10));

            Placement placement = new Placement(placementList, game);

            PlacementMatrix sut = new PlacementMatrix(placement);
            bool[,] sutArray = sut.ExclusionArray;

            //Act
            int trueCount = 0;
            foreach (bool item in sutArray)
            {
                if (item)
                    trueCount++;
            }

            int xLength = sutArray.GetLength(0);
            int yLength = sutArray.GetLength(1);

            //Assert
            Assert.That(trueCount, Is.EqualTo(28));
            Assert.That(xLength, Is.EqualTo(4));
            Assert.That(yLength, Is.EqualTo(7));
        }

        [Test]
         [Category("PlacementMatrix Array Construction")]
        public void BoolArray_ParallelPlacement_AllFalse()
        {
            //Arrange

            Game game = new Game("QZJX");

            List<Space> boardList = new List<Space>();
            boardList.Add(new Space(7, 7, 't'));
            boardList.Add(new Space(7, 8, 'e'));
            boardList.Add(new Space(7, 9, 's'));
            boardList.Add(new Space(7, 10, 't'));

            game.SetBoard(boardList);

            List<Space> placementList = new List<Space>();
            placementList.Add(new Space(6, 7));
            placementList.Add(new Space(6, 8));
            placementList.Add(new Space(6, 9));
            placementList.Add(new Space(6, 10));

            Placement placement = new Placement(placementList, game);

            PlacementMatrix sut = new PlacementMatrix(placement);
            bool[,] sutArray = sut.ExclusionArray;

            //Act
            int falseCount = 0;
            foreach (bool item in sutArray)
            {
                if (!item)
                    falseCount++;
            }

            int xLength = sutArray.GetLength(0);
            int yLength = sutArray.GetLength(1);

            //Assert
            Assert.That(falseCount, Is.EqualTo(16));
            Assert.That(xLength, Is.EqualTo(4));
            Assert.That(yLength, Is.EqualTo(4));
        }

        [Test]
         [Category("PlacementMatrix Array Construction")]
        public void BoolArray_ParallelPlacement_OneTrue()
        {
            //Arrange

            Game game = new Game("QZJX");

            List<Space> boardList = new List<Space>();
            boardList.Add(new Space(7, 7, 't'));
            boardList.Add(new Space(7, 8, 'e'));
            boardList.Add(new Space(7, 9, 's'));
            boardList.Add(new Space(7, 10, 't'));

            game.SetBoard(boardList);

            List<Space> placementList = new List<Space>();
            placementList.Add(new Space(8, 7));
            placementList.Add(new Space(8, 8));
            placementList.Add(new Space(8, 9));
            placementList.Add(new Space(8, 10));

            Placement placement = new Placement(placementList, game);

            PlacementMatrix sut = new PlacementMatrix(placement);
            bool[,] sutArray = sut.ExclusionArray;

            //Act
            int trueCount = 0;
            foreach (bool item in sutArray)
            {
                if (item == true)
                    trueCount++;
            }

            int xLength = sutArray.GetLength(0);
            int yLength = sutArray.GetLength(1);

            //Assert
            Assert.That(trueCount, Is.EqualTo(1));
            Assert.That(sutArray[1,3], Is.True);
            Assert.That(xLength, Is.EqualTo(4));
            Assert.That(yLength, Is.EqualTo(4));
        }

        [Test]
         [Category("PlacementMatrix Array Construction")]
        public void BoolArray_WithAnchors_NoSubWords()
        {
            //Arrange

            Game game = new Game("QZAS");

            List<Space> boardList = new List<Space>();
            boardList.Add(new Space(5, 7, 't'));
            boardList.Add(new Space(6, 7, 'h'));
            boardList.Add(new Space(7, 7, 'e'));
            boardList.Add(new Space(8, 7, 'i'));
            boardList.Add(new Space(9, 7, 's'));
            boardList.Add(new Space(10, 7, 't'));

            game.SetBoard(boardList);

            List<Space> placementList = new List<Space>();
            placementList.Add(new Space(4, 7));
            placementList.Add(new Space(11, 7));

            Placement placement = new Placement(placementList, game);

            PlacementMatrix sut = new PlacementMatrix(placement);
            bool[,] sutArray = sut.ExclusionArray;

            //Act
            int trueCount = 0;
            foreach (bool item in sutArray)
            {
                if (item == true)
                    trueCount++;
            }

            int xLength = sutArray.GetLength(0);
            int yLength = sutArray.GetLength(1);

            //Assert
            Assert.That(trueCount, Is.EqualTo(8));
            Assert.That(xLength, Is.EqualTo(2));
            Assert.That(yLength, Is.EqualTo(4));
        }

        [Test]
         [Category("PlacementMatrix Array Construction")]
        public void BoolArray_Parallel_2SubWords_Mixed()
        {
            //Arrange

            Game game = new Game("QZAS");

            List<Space> boardList = new List<Space>();
            boardList.Add(new Space(5, 7, 't'));
            boardList.Add(new Space(6, 7, 'h'));
            boardList.Add(new Space(7, 7, 'e'));
            boardList.Add(new Space(8, 7, 'i'));
            boardList.Add(new Space(9, 7, 's'));
            boardList.Add(new Space(10, 7, 't'));

            game.SetBoard(boardList);

            List<Space> placementList = new List<Space>();
            placementList.Add(new Space(5, 6));
            placementList.Add(new Space(6, 6));

            Placement placement = new Placement(placementList, game);

            PlacementMatrix sut = new PlacementMatrix(placement);
            bool[,] sutArray = sut.ExclusionArray;

            //Act
            int trueCount = 0;
            foreach (bool item in sutArray)
            {
                if (item == true)
                    trueCount++;
            }

            int xLength = sutArray.GetLength(0);
            int yLength = sutArray.GetLength(1);

            //Assert
            Assert.That(trueCount, Is.EqualTo(3));
            Assert.That(sutArray[0,2], Is.True);
            Assert.That(sutArray[1,2], Is.True);
            Assert.That(sutArray[1,3], Is.True);
            Assert.That(xLength, Is.EqualTo(2));
            Assert.That(yLength, Is.EqualTo(4));
        }
        #endregion

        #region //Exclusions



        #endregion


    }
}