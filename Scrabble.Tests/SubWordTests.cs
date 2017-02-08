using System;
using System.Collections.Generic;
using NUnit.Framework;
namespace Scrabble.Tests
{
    [TestFixture]
    class SubWordTests
    {
        [Test]
        [Category("Constructor")]
        public void Constructor_NewVertInOrder()
        {
            SubWord sut;
            string resultWord = "";
            int resultScore = 0;

            //Arrange

            List<Space> pairs = new List<Space> {
                new Space(7,7,'t'),
                new Space(7,8,'e'),
                new Space(7,9,'s'),
                new Space(7,10,'t')
             };
            

            sut = new SubWord(pairs);
            //Act
            resultWord = sut.Word;
            resultScore = sut.Score;

            //Assert
            Assert.That(resultWord, Is.EqualTo("test"));
            Assert.That(resultScore, Is.EqualTo(4));
        }

        [Test]
        [Category("Constructor")]
        public void Constructor_NewVertOutOfOrder()
        {
            SubWord sut;
            string resultWord = "";
            int resultScore = 0;

            //Arrange

            List<Space> pairs = new List<Space> {
                new Space(7,9,'s'),
                new Space(7,8,'e'),
                new Space(7,7,'t'),
                new Space(7,10,'t')
            };


            sut = new SubWord(pairs);
            //Act
            resultWord = sut.Word;
            resultScore = sut.Score;

            //Assert
            Assert.That(resultWord, Is.EqualTo("test"));
            Assert.That(resultScore, Is.EqualTo(4));
        }

        [Test]
        [Category("Constructor")]
        public void Constructor_NewHorizontalInOrder()
        {
            SubWord sut;
            string resultWord = "";
            int resultScore = 0;

            //Arrange

            List<Space> pairs = new List<Space> {
                new Space(7,7, 't'),
                new Space(8,7, 'e'),
                new Space(9,7, 's'),
                new Space(10,7, 't')
            };


            sut = new SubWord(pairs);
            //Act
            resultWord = sut.Word;
            resultScore = sut.Score;

            //Assert
            Assert.That(resultWord, Is.EqualTo("test"));
            Assert.That(resultScore, Is.EqualTo(4));
        }

        [Test]
        [Category("Constructor")]
        public void Constructor_NewHorizontalOutOfOrder()
        {
            SubWord sut;
            string resultWord = "";
            int resultScore = 0;

            //Arrange

            List<Space> pairs = new List<Space> {
                new Space(9,7,'s'),
                new Space(8,7,'e'),
                new Space(7,7,'t'),
                new Space(10,7,'t')
            };


            sut = new SubWord(pairs);
            //Act
            resultWord = sut.Word;
            resultScore = sut.Score;

            //Assert
            Assert.That(resultWord, Is.EqualTo("test"));
            Assert.That(resultScore, Is.EqualTo(4));
        }
    }
}
