using System;
using System.Collections.Generic;
using NUnit.Framework;
namespace Scrabble.Tests
{
    [TestFixture]
    class SubWordTests
    {
        [Test]
        [Category("SubWord Constructor")]
        public void Constructor_NewVertInOrder()
        {
            SubWord sut;
            string resultWord = "";
            int resultScore = 0;
            Game game = new Game();

            //Arrange

            List <Tuple<Space, Tile>> pairs = new List<Tuple<Space, Tile>> {
                Tuple.Create(new Space(7,7), new Tile('T')),
                Tuple.Create(new Space(7,8), new Tile('E')),
                Tuple.Create(new Space(7,9), new Tile('S')),
                Tuple.Create(new Space(7,10), new Tile('T')),
             };
            

            sut = new SubWord(pairs, game);
            //Act
            resultWord = sut.Word;
            resultScore = sut.SubWordScore();

            //Assert
            Assert.That(resultWord, Is.EqualTo("TEST"));           
        }

        [Test]
        [Category("SubWord Constructor")]
        public void Constructor_NewVertOutOfOrder()
        {
            SubWord sut;
            string resultWord = "";
            int resultScore = 0;
            Game game = new Game();

            //Arrange

            List<Tuple<Space, Tile>> pairs = new List<Tuple<Space, Tile>> {
                Tuple.Create(new Space(7,8), new Tile('E')),
                Tuple.Create(new Space(7,7), new Tile('T')),   
                Tuple.Create(new Space(7,9), new Tile('S')),
                Tuple.Create(new Space(7,10), new Tile('T')),
             };


            sut = new SubWord(pairs, game);
            //Act
            resultWord = sut.Word;
            resultScore = sut.SubWordScore();

            //Assert
            Assert.That(resultWord, Is.EqualTo("TEST"));           
        }

        [Test]
        [Category("SubWord Constructor")]
        public void Constructor_NewHorizontalInOrder()
        {
            SubWord sut;
            string resultWord = "";           
            Game game = new Game();

            //Arrange

            List<Tuple<Space, Tile>> pairs = new List<Tuple<Space, Tile>> {
                Tuple.Create(new Space(7,8), new Tile('E')),
                Tuple.Create(new Space(7,7), new Tile('T')),
                Tuple.Create(new Space(7,9), new Tile('S')),
                Tuple.Create(new Space(7,10), new Tile('T')),
             };


            sut = new SubWord(pairs, game);
            //Act
            resultWord = sut.Word;            

            //Assert
            Assert.That(resultWord, Is.EqualTo("TEST"));           
        }

        [Test]
        [Category("SubWord Constructor")]
        public void Constructor_NewHorizontalOutOfOrder()
        {
            SubWord sut;
            string resultWord = "";
            Game game = new Game();

            //Arrange

            List<Tuple<Space, Tile>> pairs = new List<Tuple<Space, Tile>> {
                Tuple.Create(new Space(7,8), new Tile('E')),
                Tuple.Create(new Space(7,7), new Tile('T')),
                Tuple.Create(new Space(7,9), new Tile('S')),
                Tuple.Create(new Space(7,10), new Tile('T')),
             };


            sut = new SubWord(pairs, game);
            //Act
            resultWord = sut.Word;

            //Assert
            Assert.That(resultWord, Is.EqualTo("TEST"));            
        }
    }
}
