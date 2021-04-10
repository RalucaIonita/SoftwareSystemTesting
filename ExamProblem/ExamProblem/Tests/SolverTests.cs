using ExamProblem.Models;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace ExamProblem.Tests
{
    public class SolverTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public SolverTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void LoadText_ShouldReturnError()
        {
            var path = "dasrgawezsthb";
            var result = Solver.LoadFileContent(path);

            Assert.True(result == Errors.FileNotFound);
        }

        [Fact]
        public void LoadText_ShouldReturnString()
        {
            var path = "..\\..\\..\\..\\ExamProblem\\plagiat.in";
            var text = Solver.LoadFileContent(path);

            var expected = "2\r\n5\r\n1 1\r\n2 2\r\n0 0\r\n1 2\r\n100 105\r\n5\r\n1 1\r\n2 2\r\n1 2\r\n0 0\r\n0 1";
            Assert.True(text == expected);
        }

        [Fact]
        public void MapData_ShouldReturnErrorForInputWithLetters()
        {
            var input = "dsfbhsdrga\n\nwsreg";
            var map = Solver.MapData(input);
            Assert.True(map.Item2 == Errors.FileDoesNotContainOnlyDigits);
            Assert.True(map.Item1 == null);
        }

        [Fact]
        public void MapData_ShouldReturnErrorForWrongPoints()
        {
            var input = "2\n3\n1 1\n2 2\n3 3\n5\n1 2\n";
            var map = Solver.MapData(input);
            Assert.True(map.Item2 == Errors.WrongNumberOfPoints);
            Assert.True(map.Item1 == null);
        }

        [Fact]
        public void MapData_ShouldReturnErrorForWrongMaps()
        {
            var input = "3\n5\n1 1\n2 2\n0 0\n1 2\n100 105\n5\n1 1\n2 2\n1 2\n0 0\n0 1";
            var map = Solver.MapData(input);
            Assert.True(map.Item2 == Errors.WrongNumberOfMaps);
            Assert.True(map.Item1 == null);
        }

        [Fact]
        public void MapData_ReturnsErrorIfEmptyFile()
        {
            var input = "";
            var map = Solver.MapData(input);
            Assert.True(map.Item2 == Errors.EmptyFile);
            Assert.True(map.Item1 == null);
        }

        [Fact]
        public void MapData_ReturnsListOfMapsIfDataOk()
        {
            var input = "2\n5\n1 1\n2 2\n0 0\n1 2\n100 105\n5\n1 1\n2 2\n1 2\n0 0\n0 1";
            var map = Solver.MapData(input);
            Assert.True(map.Item2 == null);
            Assert.True(map.Item1.GetType() == typeof(List<Map>));
            Assert.True(map.Item1.Count == 2);
        }

        [Fact]
        public void GetTriangles_ReturnsErrorIfNotEnoughPoints()
        {
            var map = new Map
            {
                Points = new List<Point> {new Point(4, 5), new Point(4, 8)}
            };

            var result = Solver.GetTriangles(map);
            Assert.True(result.Item2 == Errors.NotEnoughPoints);
            Assert.True(result.Item1 == null);
        }

        [Fact]
        public void AreTranslated_ReturnsTrueIfTranslated()
        {
            var t1 = new Triangle
            {
                A = new Point(1, 1),
                B = new Point(1, 3),
                C = new Point(4, 2),
            };

            var t2 = new Triangle
            {
                A = new Point(2, 2),
                B = new Point(2, 4),
                C = new Point(5, 3),
            };

            var result = Solver.AreTranslated(t1, t2);
            Assert.True(result);

        }

        [Fact]
        public void AreTranslated_ReturnsFalseeIfNotTranslated()
        {
            var t1 = new Triangle
            {
                A = new Point(1, 1),
                B = new Point(1, 3),
                C = new Point(4, 3),
            };

            var t2 = new Triangle
            {
                A = new Point(2, 2),
                B = new Point(2, 4),
                C = new Point(5, 3),
            };

            var result = Solver.AreTranslated(t1, t2);
            Assert.False(result);

        }
    }
}
