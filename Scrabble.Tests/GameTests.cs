using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Scrabble.Tests
{
    [TestFixture]
    public class GameTests
    {
        [Test]
        [Category("SingleSubWord")]
        public void SingleSubWord_Simple()
        {
            //Arrange
            List<Space> spaceList = new List<Space> {
                new Space(9,7,'S'),
                new Space(8,7,'E'),
                new Space(7,7,'T'),
                new Space(10,7,'T')
            };
            Game game = new Game();
            game.SetBoard(spaceList);
   
            //Act
            var result = game.SingleSubWord(Tuple.Create(new Space(7, 6), new Tile('E')), "vertical");
            var resultWord = result.Word;
            var resultScore = result.Score;

            //Assert
            Assert.That(resultWord, Is.EqualTo("ET"));
        }

        #region //PossiblePlacements
        [Test]
        [Category("PossiblePlacements")]
        public void PossiblePlacements_SingleTile_EmptyBoard()
        {
            //Arrange

            Game sut = new Game("a");
            List<Placement> result;

            ////Act
            result = sut.PossiblePlacements();

            //Assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].GetSpaceListString(), Is.EqualTo(new Placement(new Space(7, 7), sut).GetSpaceListString()));
        }

        [Test]
        [Category("PossiblePlacements")]
        public void PossiblePlacements_SingleTile_SimpleBoard()
        {
            //Arrange
            Game sut = new Game("a");
            List<Placement> result;

            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>
            {
                Tuple.Create(new Space(7,7), new Tile('A')),
                Tuple.Create(new Space(7,8), new Tile('B')),
                Tuple.Create(new Space(7,9), new Tile('C'))
            };
            sut.SetBoard(tupleList);

            List<Placement> expected = new List<Placement>
            {
                new Placement(new Space(6, 7), sut),
                new Placement(new Space(6, 8), sut),
                new Placement(new Space(6, 9), sut),
                new Placement(new Space(7, 10), sut),
                new Placement(new Space(7, 6), sut),
                new Placement(new Space(8, 7), sut),
                new Placement(new Space(8, 8), sut),
                new Placement(new Space(8, 9), sut),
            };
            ////Act
            result = sut.PossiblePlacements();

            //Assert
            Assert.That(result.Count, Is.EqualTo(8));
            Assert.That(result, Is.EquivalentTo(expected));
        }
        [Test]
        [Category("PossiblePlacements")]
        public void PossiblePlacements_SevenTile_EmptyBoard()
        {
            //Arrange
            Game sut = new Game("abcdefg");
            List<Placement> result;
            ////Act
            result = sut.PossiblePlacements();

            //Assert
            Assert.That(result.Count, Is.EqualTo(55));
        }

        [Test]
        [Category("PossiblePlacements")]
        public void PossiblePlacements_2tray_2board()
        {
            //Arrange
            Game sut = new Game("cd");
            List<Placement> result;

            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>
            {
                Tuple.Create(new Space(7,7), new Tile('A')),
                Tuple.Create(new Space(7,8), new Tile('B'))
            };
            sut.SetBoard(tupleList);

            ////Act
            result = sut.PossiblePlacements();

            //Assert
            Assert.That(result.Count, Is.EqualTo(25));

        }

        [Test]
        [Category("PossiblePlacements")]
        public void PossiblePlacements_EmptyTray()
        {
            //Arrange
            Game sut = new Game("");
            List<Placement> result;

            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>
            {
                Tuple.Create(new Space(7,7), new Tile('A')),
                Tuple.Create(new Space(7,8), new Tile('B'))
            };
            sut.SetBoard(tupleList);

            ////Act
            result = sut.PossiblePlacements();

            //Assert
            Assert.That(result.Count, Is.EqualTo(0));

        }

        [Test]
        [Category("Lots of Results")]       
        public void LotsOfPlacements()
        {
            //Arrange
            Game game = new Game("ABCDEFG");

            List<Space> boardList = new List<Space>
            {
                new Space(3, 3, 'V'),
                new Space(4, 3, 'O'),
                new Space(5, 3, 'X'),

                new Space(1, 7, 'P'),
                new Space(2, 7, 'I'),
                new Space(3, 7, 'S'),
                new Space(4, 7, 'H'),
                new Space(5, 7, 'O'),
                new Space(6, 7, 'G'),
                new Space(7, 7, 'E'),


                new Space(7, 8, 'F'),
                new Space(8, 8, 'A'),
                new Space(9, 8, 'Z'),
                new Space(10, 8, 'E'),

                new Space(4, 2, 'D'),
                new Space(4, 4, 'V'),
                new Space(4, 5, 'I'),
                new Space(4, 6, 'S'),

            };
            game.SetBoard(boardList);

            //Act
            List<Placement> allPlacements = game.PossiblePlacements();

            //Assert
            Assert.That(allPlacements.Count, Is.EqualTo(708));
        }
        #endregion

        #region //FindAllPlays

        [Test]
        [Category("FindAllPlays")]
        public void FindAllPlays_NoSecondarySubwords()
        {
            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(6, 7), new Tile('A')));
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('B')));
            tupleList.Add(Tuple.Create(new Space(8, 7), new Tile('B')));
            tupleList.Add(Tuple.Create(new Space(9, 7), new Tile('A')));

            Game newGame = new Game("ZP");
            newGame.SetBoard(tupleList);

            //Act
            List<Play> result = newGame.FindAllPlays();

            //Assert
            Assert.That(result.Count, Is.EqualTo(6));
        }

        [Test]
        [Category("FindAllPlays")]
        public void FindAllPlays_ManyPlays()
        {
            //Arrange
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>();
            tupleList.Add(Tuple.Create(new Space(6, 7), new Tile('A')));
            tupleList.Add(Tuple.Create(new Space(7, 7), new Tile('B')));
            tupleList.Add(Tuple.Create(new Space(8, 7), new Tile('B')));
            tupleList.Add(Tuple.Create(new Space(9, 7), new Tile('A')));

            Game newGame = new Game("ZADE");
            newGame.SetBoard(tupleList);

            //Act
            List<Play> result = newGame.FindAllPlays();

            //Assert
            Assert.That(result.Count, Is.EqualTo(68));
        }
        #endregion

        [Test]
        [Category("Lots of Results")]
        //[Ignore("Takes forever")]
        public void LotsOfPlays()
        {
            //Arrange
            Game game = new Game("ABCDEFG");
            List<Space> boardList = new List<Space>
            {
                new Space(3, 3, 'V'),
                new Space(4, 3, 'O'),
                new Space(5, 3, 'X'),

                new Space(1, 7, 'P'),
                new Space(2, 7, 'I'),
                new Space(3, 7, 'S'),
                new Space(4, 7, 'H'),
                new Space(5, 7, 'O'),
                new Space(6, 7, 'G'),
                new Space(7, 7, 'E'),


                new Space(7, 8, 'F'),
                new Space(8, 8, 'A'),
                new Space(9, 8, 'Z'),
                new Space(10, 8, 'E'),

                new Space(4, 2, 'D'),
                new Space(4, 4, 'V'),
                new Space(4, 5, 'I'),
                new Space(4, 6, 'S'),

            };
            game.SetBoard(boardList);

            //Act
            List<Play> plays1 = game.FindAllPlays();

            game.SetTray("ABCDEF?");
            List<Play> plays2 = game.FindAllPlays();

            game.SetTray("AE??");
            List<Play> plays3 = game.FindAllPlays();
            
            //Assert
            Assert.That(plays1.Count, Is.EqualTo(384));
            Assert.That(plays2.Count, Is.EqualTo(4723));
            Assert.That(plays3.Count, Is.EqualTo(7562));
        }

        [Test]
        [Category("Lots of Results")]
        //[Ignore("Redundant")]
        public void TwoBlanks()
        {
            //Arrange
            Game game = new Game("?");
            List<Space> boardList = new List<Space>
            {

                new Space(7, 7, 'Z'),
                new Space(8, 7, 'A')

            };
            game.SetBoard(boardList);

            //Act
            //int placements = 
            List<Play> plays1 = game.FindAllPlays();

            //Assert
            Assert.That(plays1.Count, Is.EqualTo(33));

        }
    }
}