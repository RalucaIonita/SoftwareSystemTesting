using System.Collections.Generic;
using ExamProblem;
using ExamProblem.Models;
using Xunit;
using Xunit.Abstractions;

namespace Tests.Tests.CategoryAnalysis
{
    public class SolverTests
    {
        //categories ->
        // T -> T in [1, 5] and T not in [1, 5]
        // N -> N in [1, 400] and N not in [1, 400]
        // Coordinates -> coordinates in [0, 109] and not in [0, 109]
        // Identical points -> are in map, are not in map
        // Plagiarism -> is and is not

        private readonly ITestOutputHelper _testOutputHelper;

        public SolverTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        //T
        [Fact]
        public void MapData_ReturnsErrorIfNumberOfMapsNotInInterval() // T not in interval
        {
            var input = "0";
            var result = Solver.MapData(input);
            _testOutputHelper.WriteLine(result.Item2);
            Assert.True(result.Item1 == null);
            Assert.True(result.Item2 == Errors.NumberOfMapsTooSmall || result.Item2 == Errors.NumberOfMapsTooBig);
        }

        [Fact]
        public void MapData_ReturnsOkIfNumberOfMapsOk() // T in interval
        {
            var input = "2\n3\n1 1\n2 2\n3 3\n3\n1 1\n2 2\n3 3";
            var result = Solver.MapData(input);
            Assert.False(result.Item1 == null);
            Assert.True(result.Item1.GetType() == typeof(List<Map>));
            Assert.True(result.Item1.Count == 2);
            Assert.True(result.Item2 == null);
        }

        //N
        [Fact]
        public void MapData_ReturnsErrorIfNumberOfStarsNotOk() // N not in interval
        {
            var input = "2\n0\n3\n1 1\n2 2\n3 3";
            var result = Solver.MapData(input);
            Assert.True(result.Item1 == null);
            Assert.True(result.Item2 == Errors.NumberOfStarsTooSmall || result.Item2 == Errors.NumberOfStarsTooBig);
        }

        [Fact]
        public void MapData_ReturnsOkIfNumberOfStarsOk() // N in interval
        {
            var input = "2\n3\n1 1\n2 2\n3 3\n3\n1 1\n2 2\n3 3";
            var result = Solver.MapData(input);
            Assert.False(result.Item1 == null);
            Assert.True(result.Item1.GetType() == typeof(List<Map>));
            Assert.True(result.Item1.Count == 2);
            Assert.True(result.Item2 == null);
        }

        [Fact]
        public void MapData_ReturnsErrorIfCoordinatesNotOk() // coordinates not in interval
        {
            var input = "2\n3\n1 1\n-2 2\n3 3\n3\n1 1\n2 2\n3 3";
            var result = Solver.MapData(input);
            _testOutputHelper.WriteLine(result.Item2);
            Assert.True(result.Item1 == null);
            Assert.True(result.Item2 == Errors.CoordinateTooSmall);
        }

        [Fact]
        public void MapData_ReturnsOkIfCoordinatesOk() // coordinates in interval
        {
            var input = "2\n3\n1 1\n2 2\n3 3\n3\n1 1\n2 2\n3 3";
            var result = Solver.MapData(input);
            Assert.False(result.Item1 == null);
            Assert.True(result.Item1.GetType() == typeof(List<Map>));
            Assert.True(result.Item1.Count == 2);
            Assert.True(result.Item2 == null);
        }

        //identical points
        [Fact]
        public void MapData_ReturnsErrorIfTwoPointsAreIdentical() // are identical
        {
            var input = "2\n3\n1 1\n1 1\n3 3\n3\n1 1\n2 2\n3 3";
            var result = Solver.MapData(input);
            Assert.True(result.Item1 == null);
            Assert.True(result.Item2 == Errors.TwoIdenticalPoints);
        }

        [Fact]
        public void MapData_ReturnsOkIfNoPointsAreIdentical() // are not identical
        {
            var input = "2\n3\n1 1\n2 2\n3 3\n3\n1 1\n2 2\n3 3";
            var result = Solver.MapData(input);
            Assert.False(result.Item1 == null);
            Assert.True(result.Item1.GetType() == typeof(List<Map>));
            Assert.True(result.Item1.Count == 2);
            Assert.True(result.Item2 == null);
        }

        //plagiarism
        [Fact]
        public void AreTranslated_ReturnsTrueIfAreTranslated() // is plagiarism
        {
            var p1 = new Point(1, 1);
            var p2 = new Point(2, 1);
            var p3 = new Point(1, 3);

            var p4 = new Point(2, 2);
            var p5 = new Point(3, 2);
            var p6 = new Point(2, 4);

            var firstTriangle = new Triangle(p1, p2, p3);
            var secondTriangle = new Triangle(p4, p5, p6);

            var result = Solver.AreTranslated(firstTriangle, secondTriangle);

            Assert.True(result.Item1);
            Assert.True(result.Item2 == null);
        }

        [Fact]
        public void AreTranslated_ReturnsTrueIfAreNotTranslated() // is not plagiarism
        {
            var p1 = new Point(1, 1);
            var p2 = new Point(2, 1);
            var p3 = new Point(1, 4);

            var p4 = new Point(2, 2);
            var p5 = new Point(3, 2);
            var p6 = new Point(2, 4);

            var firstTriangle = new Triangle(p1, p2, p3);
            var secondTriangle = new Triangle(p4, p5, p6);

            var result = Solver.AreTranslated(firstTriangle, secondTriangle);

            Assert.False(result.Item1);
            Assert.True(result.Item2 == null);
        }

    }
}
