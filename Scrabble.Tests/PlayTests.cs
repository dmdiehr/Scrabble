using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Scrabble.Tests
{
    [TestFixture]
    class PlayTests
    {

        [Test]
        [Category("SingleSubWord")]
        public void SingleSubWord_Simple()
        {            
            //Arrange
            List<Space> spaceList = new List<Space> {
                new Space(9,7,'s'),
                new Space(8,7,'e'),
                new Space(7,7,'t'),
                new Space(10,7,'t')
            };
            Game game = new Game();
            game.SetBoard(spaceList);
            Play sut = new Play(new List<Tuple<Space,Tile>>{ Tuple.Create(new Space(7, 6), new Tile('e')) }, game);



            //Act
            var result = sut.SingleSubWord(Tuple.Create(new Space(7, 6), new Tile('e')), "vertical");
            var resultWord = result.Word;
            var resultScore = result.Score;

            //Assert
            Assert.That(resultWord, Is.EqualTo("et"));
            Assert.That(resultScore, Is.EqualTo(2));
        }

        [Test]
        [Category("SingleSubWord")]
        public void SingleSubWord_BonusSquares()
        {
            //Arrange

            List<Space> spaceList = new List<Space> {
                new Space(7, 7, 't'),
                new Space(9,7,'s'),
                new Space(8,7,'e'),
                new Space(10,7,'t')
            };
            Game game = new Game();
            game.SetBoard(spaceList);
            game.SetMultiplier(11, 7, "word", 2);


            Play sut = new Play(new List<Tuple<Space, Tile>> { Tuple.Create(new Space(11, 7), new Tile('s')), Tuple.Create(new Space(11, 8), new Tile('u')), Tuple.Create(new Space(11, 9), new Tile('m')) }, game);



            //Act
            var result = sut.SingleSubWord(Tuple.Create(new Space(11, 7), new Tile('s')), "horizontal");
            var resultWord = result.Word;
            var resultScore = result.Score;

            //Assert
            Assert.That(resultWord, Is.EqualTo("tests"));
            Assert.That(resultScore, Is.EqualTo(10));
        }

        [Test]
        [Category("GetSubWords")]
        public void GetSubWords_FirstPlay_Horizontal()
        {
            //Arrange
            Game game = new Game();
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>
            {
                Tuple.Create(new Space(7, 7), new Tile('t')),
                Tuple.Create(new Space(8, 7), new Tile('e')),
                Tuple.Create(new Space(9, 7), new Tile('s')),
                Tuple.Create(new Space(10, 7), new Tile('t'))
            };

            //Act
            Play play = new Play(tupleList, game);

            //Assert
            Assert.That(play.GetSubWords().Count, Is.EqualTo(1));
            Assert.That(play.GetSubWords()[0].Word, Is.EqualTo("test"));
        }

        [Test]
        [Category("GetSubWords")]
        public void GetSubWords_ParallelPlay_Vertical()
        {
            //Arrange
            Game game = new Game();

            List<Space> boardList = new List<Space>
            {
                new Space(7, 7, 'a'),
                new Space(7, 8, 'b'),
                new Space(7, 9, 'c'),
                new Space(7, 10, 'd')
            };
            game.SetBoard(boardList);

            List<Tuple<Space, Tile>> playList = new List<Tuple<Space, Tile>>
            {
                Tuple.Create(new Space(8, 7), new Tile('t')),
                Tuple.Create(new Space(8, 9), new Tile('s')),
                Tuple.Create(new Space(8, 8), new Tile('e')),
                Tuple.Create(new Space(8, 10), new Tile('t'))
            };

            List<string> expected = new List<string> { "test", "at", "be", "cs", "dt" };
            
            //Act
            Play play = new Play(playList, game);
            List<string> result = new List<string>();
            foreach (SubWord subword in play.GetSubWords())
            {
                result.Add(subword.Word);
            }

            //Assert
            Assert.That(play.GetSubWords().Count, Is.EqualTo(5));
            Assert.That(result, Is.EquivalentTo(expected));
        }

        [Test]
        [Category("GetSubWords")]
        public void GetSubWords_Complicated_Horizontal()
        {
            //Arrange
            Game game = new Game();

            List<Space> boardList = new List<Space>
            {
                new Space(7, 7, 'a'),
                new Space(8, 7, 'b'),
                new Space(9, 7, 'c'),
                new Space(10, 7, 'd'),

                new Space(10, 8, 'e'),
                new Space(10, 9, 'f'),
                new Space(10, 10, 'g'),

                new Space(9, 9, 'h'),
                new Space(11, 9, 'i')
            };
            game.SetBoard(boardList);

            List<Tuple<Space, Tile>> playList = new List<Tuple<Space, Tile>>
            {
                Tuple.Create(new Space(9, 8), new Tile('t')),
                Tuple.Create(new Space(11, 8), new Tile('s')),
                Tuple.Create(new Space(12, 8), new Tile('t'))
            };

            List<string> expected = new List<string> { "test", "cth", "si" };

            //Act
            Play play = new Play(playList, game);
            List<string> result = new List<string>();
            foreach (SubWord subword in play.GetSubWords())
            {
                result.Add(subword.Word);
            }

            //Assert
            Assert.That(play.GetSubWords().Count, Is.EqualTo(3));
            Assert.That(result, Is.EquivalentTo(expected));
            Assert.That(play.GetScore(), Is.EqualTo(14));
            Assert.That(play.AreWordsValid, Is.False);
        }

        [Test]
        [Category("GetSubWords")]
        public void GetSubWords_Complicated_Vertical()
        {
            //Arrange
            Game game = new Game();

            List<Space> boardList = new List<Space>
            {
                new Space(7, 7, 'a'),
                new Space(7, 8, 'r'),
                new Space(7, 9, 't'),
                new Space(7, 10, 'y'),

                new Space(8, 8, 'a'),
                new Space(9, 8, 'i'),
                new Space(10, 8, 'e'),

                new Space(9, 7, 'h'),
                new Space(9, 9, 's')
            };
            game.SetBoard(boardList);

            List<Tuple<Space, Tile>> playList = new List<Tuple<Space, Tile>>
            {
                Tuple.Create(new Space(8, 9), new Tile('e')),
                Tuple.Create(new Space(10, 9), new Tile('t')),
                Tuple.Create(new Space(11, 9), new Tile('s'))
            };

            List<string> expected = new List<string> { "tests", "ae", "et" };

            //Act
            Play play = new Play(playList, game);
            List<string> result = new List<string>();
            foreach (SubWord subword in play.GetSubWords())
            {
                result.Add(subword.Word);
            }

            //Assert
            Assert.That(play.GetSubWords().Count, Is.EqualTo(3));
            Assert.That(result, Is.EquivalentTo(expected));
            Assert.That(play.GetScore(), Is.EqualTo(9));
            Assert.That(play.AreWordsValid, Is.True);
        }

    }
}
