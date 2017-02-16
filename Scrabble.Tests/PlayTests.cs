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
                new Space(9,7,'S'),
                new Space(8,7,'E'),
                new Space(7,7,'T'),
                new Space(10,7,'T')
            };
            Game game = new Game();
            game.SetBoard(spaceList);
            Play sut = new Play(new List<Tuple<Space,Tile>>{ Tuple.Create(new Space(7, 6), new Tile('E')) }, game);



            //Act
            var result = sut.SingleSubWord(Tuple.Create(new Space(7, 6), new Tile('E')), "vertical");
            var resultWord = result.Word;
            var resultScore = result.Score;

            //Assert
            Assert.That(resultWord, Is.EqualTo("ET"));
        }

        [Test]
        [Category("GetSubWords")]
        public void GetSubWords_FirstPlay_Horizontal()
        {
            //Arrange
            Game game = new Game();
            List<Tuple<Space, Tile>> tupleList = new List<Tuple<Space, Tile>>
            {
                Tuple.Create(new Space(7, 7), new Tile('T')),
                Tuple.Create(new Space(8, 7), new Tile('E')),
                Tuple.Create(new Space(9, 7), new Tile('S')),
                Tuple.Create(new Space(10, 7), new Tile('T'))
            };

            //Act
            Play play = new Play(tupleList, game);

            //Assert
            Assert.That(play.GetSubWords().Count, Is.EqualTo(1));
            Assert.That(play.GetSubWords()[0].Word, Is.EqualTo("TEST"));
        }

        [Test]
        [Category("GetSubWords")]
        public void GetSubWords_ParallelPlay_Vertical()
        {
            //Arrange
            Game game = new Game();

            List<Space> boardList = new List<Space>
            {
                new Space(7, 7, 'A'),
                new Space(7, 8, 'B'),
                new Space(7, 9, 'C'),
                new Space(7, 10, 'D')
            };
            game.SetBoard(boardList);

            List<Tuple<Space, Tile>> playList = new List<Tuple<Space, Tile>>
            {
                Tuple.Create(new Space(8, 7), new Tile('T')),
                Tuple.Create(new Space(8, 9), new Tile('S')),
                Tuple.Create(new Space(8, 8), new Tile('E')),
                Tuple.Create(new Space(8, 10), new Tile('T'))
            };

            List<string> expected = new List<string> { "TEST", "AT", "BE", "CS", "DT" };
            
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
                new Space(7, 7, 'A'),
                new Space(8, 7, 'B'),
                new Space(9, 7, 'C'),
                new Space(10, 7, 'D'),

                new Space(10, 8, 'E'),
                new Space(10, 9, 'F'),
                new Space(10, 10, 'G'),

                new Space(9, 9, 'H'),
                new Space(11, 9, 'I')
            };
            game.SetBoard(boardList);

            List<Tuple<Space, Tile>> playList = new List<Tuple<Space, Tile>>
            {
                Tuple.Create(new Space(9, 8), new Tile('T')),
                Tuple.Create(new Space(11, 8), new Tile('S')),
                Tuple.Create(new Space(12, 8), new Tile('T'))
            };

            List<string> expected = new List<string> { "TEST", "CTH", "SI" };

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
                new Space(7, 7, 'A'),
                new Space(7, 8, 'R'),
                new Space(7, 9, 'T'),
                new Space(7, 10, 'Y'),

                new Space(8, 8, 'A'),
                new Space(9, 8, 'I'),
                new Space(10, 8, 'E'),

                new Space(9, 7, 'H'),
                new Space(9, 9, 'S')
            };
            game.SetBoard(boardList);

            List<Tuple<Space, Tile>> playList = new List<Tuple<Space, Tile>>
            {
                Tuple.Create(new Space(8, 9), new Tile('E')),
                Tuple.Create(new Space(10, 9), new Tile('T')),
                Tuple.Create(new Space(11, 9), new Tile('S'))
            };

            List<string> expected = new List<string> { "TESTS", "AE", "ET" };

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
            Assert.That(play.AreWordsValid, Is.True);
        }

    }
}
