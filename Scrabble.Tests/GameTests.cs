using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Scrabble.Tests
{
    [TestFixture]
    public class GameTests
    {
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
            Assert.That(result[0].GetSpaceListString(), Is.EqualTo(new Placement(new Space(7, 7)).GetSpaceListString()));
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
                Tuple.Create(new Space(7,7), new Tile('a')),
                Tuple.Create(new Space(7,8), new Tile('b')),
                Tuple.Create(new Space(7,9), new Tile('c'))
            };
            sut.SetBoard(tupleList);

            List<Placement> expected = new List<Placement>
            {
                new Placement(new Space(6, 7)),
                new Placement(new Space(6, 8)),
                new Placement(new Space(6, 9)),
                new Placement(new Space(7, 10)),
                new Placement(new Space(7, 6)),
                new Placement(new Space(8, 7)),
                new Placement(new Space(8, 8)),
                new Placement(new Space(8, 9)),
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

            foreach (var item in result)
            {
                Debug.WriteLine(item.GetSpaceListString());
            }

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
                Tuple.Create(new Space(7,7), new Tile('a')),
                Tuple.Create(new Space(7,8), new Tile('b'))
            };
            sut.SetBoard(tupleList);

            ////Act
            result = sut.PossiblePlacements();

            foreach (Placement placement in result)
            {
                Debug.WriteLine(placement.GetSpaceListString());
            }

            //Assert
            Assert.That(result.Count, Is.EqualTo(25));

        }
    }
}