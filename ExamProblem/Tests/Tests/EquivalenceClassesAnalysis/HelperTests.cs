using ExamProblem;
using ExamProblem.Models;
using Xunit;

namespace Tests.Tests.EquivalenceClassesAnalysis
{
    public class HelperTests
    {
        //classes ->
        // C1 = {points | points can form a triangle}
        // C2 = {points | points cannot form a triangle}
        

        [Fact]
        public void CanFormTriangle_ReturnsFalseIfPointsDoNotForm() //Class 1
        {
            var a = new Point(1, 1);
            var b = new Point(2, 1);
            var c = new Point(3, 1);

            var result = a.CanFormTriangle(b, c);
            Assert.False(result);
        }

        [Fact]
        public void CanFormTriangle_ReturnsTrueIfPointsForm() //Class 2
        {
            var a = new Point(1, 1);
            var b = new Point(2, 1);
            var c = new Point(3, 3);

            var result = a.CanFormTriangle(b, c);
            Assert.True(result);
        }


        //classes ->
        // C1 = {text | text has characters that are not digits, '\n' or ' '}
        // C2 = {text | text has characters that are only digits, '\n' or ' '}


        [Fact]
        public void EliminateWeirdCharacters_RemovesCharactersIfTheyExist() //Class 1
        {
            var str = "asdfghjnmk3456 789@R&";
            var result = str.EliminateWeirdCharacters();
            Assert.True(result == "3456 789");
        }

        [Fact]
        public void EliminateWeirdCharacters_ReturnsTextIfCharactersDoNotExist() //Class 2
        {
            var str = "56";
            var result = str.EliminateWeirdCharacters();
            Assert.True(result == str);
        }



        //classes ->
        // C1 = {text | text contains more than just digits}
        // C2 = {text | text contains only digits}

        [Fact]
        public void ContainsOnlyOkCharacters_ReturnsFalseIfNotOkCharacters() //Class 1
        {
            var str = "56dfghjkl";
            var result = str.ContainsOnlyOkCharacters();
            Assert.False(result);
        }

        [Fact]
        public void ContainsOnlyOkCharacters_ReturnsTrueIfOkCharacters() //Class 2
        {
            var str = "56567\n 89";
            var result = str.ContainsOnlyOkCharacters();
            Assert.True(result);
        }
    }
}
