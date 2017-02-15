using ExtentionMethods;
using NUnit.Framework;
using Scrabble;
using System;
using System.Collections.Generic;

namespace Scrabble.Tests
{
    [TestFixture]
    public class ExtensionsTests
    {
        [Test]
        [Category("WordFind")]
        public void WordFind_NoAnchors_1Word()
        {
            //Arrange

            Game game = new Game("EIUQSZZ"); 


            //Act
            List<string> result = game.GetDictionary().WordFind(game.GetTrayString(), 7);

            //Assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0], Is.EqualTo("QUIZZES"));
        }

        [Test]
        [Category("WordFind")]
        public void WordFind_WithAnchors_1Word()
        {
            //Arrange

            Game game = new Game("EIUZZ");


            //Act
            List<string> result = game.GetDictionary().WordFind(game.GetTrayString(), 7, new List<Tuple<int, char>> { Tuple.Create(0, 'Q'), Tuple.Create(6, 'S') });

            //Assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0], Is.EqualTo("QUIZZES"));
        }

        [Test]
        [Category("WordFind")]
        public void WordFind_WithAnchors_2Word()
        {
            //Arrange

            Game game = new Game("AR");


            //Act
            List<string> result = game.GetDictionary().WordFind(game.GetTrayString(), 4, new List<Tuple<int, char>> { Tuple.Create(0, 'W'), Tuple.Create(3, 'P') });

            //Assert
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result, Is.EquivalentTo(new List<string> { "WRAP", "WARP" }));
        }
    }
}