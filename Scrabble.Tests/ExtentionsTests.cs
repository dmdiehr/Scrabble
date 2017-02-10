using ExtentionMethods;
using NUnit.Framework;
using Scrabble;
using System;
using System.Collections.Generic;

namespace Scrabble.Tests
{
    [TestFixture]
    public class ClassTests
    {
        [Test]
        [Category("WordFind")]
        public void WordFind_NoAnchors_1Word()
        {
            //Arrange

            Game game = new Game("eiuqszz");


            //Act
            List<string> result = game.GetDictionary().WordFind(game.GetTrayString(), 7);

            //Assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0], Is.EqualTo("quizzes"));
        }

        [Test]
        [Category("WordFind")]
        public void WordFind_WithAnchors_1Word()
        {
            //Arrange

            Game game = new Game("eiuzz");


            //Act
            List<string> result = game.GetDictionary().WordFind(game.GetTrayString(), 7, new List<Tuple<int, char>> { Tuple.Create(0, 'q'), Tuple.Create(6, 's') });

            //Assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0], Is.EqualTo("quizzes"));
        }

        [Test]
        [Category("WordFind")]
        public void WordFind_WithAnchors_2Word()
        {
            //Arrange

            Game game = new Game("ar");


            //Act
            List<string> result = game.GetDictionary().WordFind(game.GetTrayString(), 4, new List<Tuple<int, char>> { Tuple.Create(0, 'w'), Tuple.Create(3, 'p') });

            //Assert
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result, Is.EquivalentTo(new List<string> { "wrap", "warp" }));
        }
    }
}