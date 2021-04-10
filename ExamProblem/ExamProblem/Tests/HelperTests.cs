using ExamProblem.Models;
using Xunit;

namespace ExamProblem.Tests
{
    public class HelperTests
    {
        [Fact]
        public void CanFormTriangle_ReturnsFalseIfPointsDoNotForm()
        {
            var a = new Point(1, 1);
            var b = new Point(2, 1);
            var c = new Point(3, 1);

            var result = a.CanFormTriangle(b, c);
            Assert.False(result);
        }

        [Fact]
        public void CanFormTriangle_ReturnsTrueIfPointsForm()
        {
            var a = new Point(1, 1);
            var b = new Point(2, 1);
            var c = new Point(3, 3);

            var result = a.CanFormTriangle(b, c);
            Assert.True(result);
        }

        [Fact]
        public void EliminateWeirdCharacters_RemovesCharactersIfTheyExist()
        {
            var str = "asdfghjnmk3456 789@R&";
            var result = str.EliminateWeirdCharacters();
            Assert.True(result == "3456 789");
        }

        [Fact]
        public void EliminateWeirdCharacters_ReturnsTextIfCharactersDoNotExist()
        {
            var str = "56";
            var result = str.EliminateWeirdCharacters();
            Assert.True(result == str);
        }

        [Fact]
        public void ContainsOnlyOkCharacters_ReturnsFalseIfNotOkCharacters()
        {
            var str = "56dfghjkl";
            var result = str.ContainsOnlyOkCharacters();
            Assert.False(result);
        }

        [Fact]
        public void ContainsOnlyOkCharacters_ReturnsTrueIfOkCharacters()
        {
            var str = "56567\n 89";
            var result = str.ContainsOnlyOkCharacters();
            Assert.True(result);
        }
    }
}
