using ExtentionMethods;
using NUnit.Framework;
using Scrabble;
using System;
using System.Collections.Generic;
using System.Diagnostics;

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

            foreach (var item in result)
            {
                Debug.WriteLine("+++++++++" + item + "^^^^^^^^^^^^^^^^");
            }

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

        [Test]
        [Category("WordFind")]
        public void WordFind_Unused_Blank()
        {
            //Arrange

            Game game = new Game("AI?");


            //Act
            List<string> result = game.GetDictionary().WordFind(game.GetTrayString(), 3, new List<Tuple<int, char>> { Tuple.Create(0, 'Z'), Tuple.Create(2, 'G') });
            
            //Assert
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result, Is.EquivalentTo(new List<string> { "ZAG", "ZIG" }));
        }

        [Test]
        [Category("WordFind")]
        public void WordFind_Two_Blanks()
        {
            //Arrange

            Game game = new Game("Q??");


            //Act
            List<string> result1 = game.GetDictionary().WordFind(game.GetTrayString(), 3);
            List<string> result2 = game.GetDictionary().WordFind(game.GetTrayString(), 2);
            List<string> result3 = game.GetDictionary().WordFind(game.GetTrayString(), 3, new List<Tuple<int, char>> {Tuple.Create(2, 'Q') });

            //foreach (var item in result2)
            //{
            //    Debug.WriteLine("*" + item + "* ");
            //}


            //Assert
            Assert.That(result1, Is.EquivalentTo(new List<string> { "QAT", "QIS", "QUA", "SUQ" }));
            Assert.That(result2.Count, Is.EqualTo(101));
            Assert.That(result3, Is.EquivalentTo(new List<string> { "SUQ" }));

        }
    }
}