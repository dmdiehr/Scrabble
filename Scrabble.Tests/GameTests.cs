using NUnit.Framework;

namespace Scrabble.Tests
{
    [TestFixture]
    public class GameTests
    {
        [Test]
        [Category("PossiblePlacements")]
        public void PossiblePlacements_EmptyBoard()
        {
            Game sut;
            int result = 0;

            //Arrange

            //someParams = dummyUpSomeParams;
            //otherParams = dummyUpMoreParams;

            //sut = new SystemUnderTest(someParams);
            ////Act
            //result = sut.MethodName(otherParams);

            //Assert
            Assert.That(result, Is.EqualTo(5));
        }
    }
}