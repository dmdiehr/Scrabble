﻿using System;
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
            var result = sut.SingleSubWord(new Space(7, 6, 'e'), "vertical");
            var resultWord = result.Word;
            var resultScore = result.Score;

            //Assert
            Assert.That(resultWord, Is.EqualTo("et"));      
        }
    }
}
