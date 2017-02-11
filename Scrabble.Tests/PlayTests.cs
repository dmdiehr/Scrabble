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
            game.SetMultiplier(7, 7, "word", 2);


            Play sut = new Play(new List<Tuple<Space, Tile>> { Tuple.Create(new Space(11, 7), new Tile('s')), Tuple.Create(new Space(11, 8), new Tile('u')), Tuple.Create(new Space(11, 9), new Tile('m')) }, game);



            //Act
            var result = sut.SingleSubWord(Tuple.Create(new Space(11, 7), new Tile('s')), "horizontal");
            var resultWord = result.Word;
            var resultScore = result.Score;

            //Assert
            Assert.That(resultWord, Is.EqualTo("tests"));
            Assert.That(resultScore, Is.EqualTo(10));
        }
    }
}
