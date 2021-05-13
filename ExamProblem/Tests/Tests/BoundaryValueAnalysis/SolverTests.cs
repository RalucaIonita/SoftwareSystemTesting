using System.Collections.Generic;
using ExamProblem;
using ExamProblem.Models;
using Xunit;
using Xunit.Abstractions;

namespace Tests.Tests.BoundaryValueAnalysis
{
    public class SolverTests
    {
        //boundaries ->
        // T has values between 1 and 5
        // N has values between 1 and 400
        // Coordinates are between 0 and 109

        private readonly ITestOutputHelper _testOutputHelper;

        public SolverTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        //T
        [Fact]
        public void MapData_ReturnsErrorIfNumberOfMapsTooSmall() // maps count < 1
        {
            var input = "0";
            var result = Solver.MapData(input);
            _testOutputHelper.WriteLine(result.Item2);
            Assert.True(result.Item1 == null);
            Assert.True(result.Item2 == Errors.NumberOfMapsTooSmall);
        }

        [Fact]
        public void MapData_ReturnsErrorOkIfNumberOfMapsOk() // maps count >= 1 && maps count <= 5
        {
            var input = "2\n3\n1 1\n2 2\n3 3\n3\n1 1\n2 2\n3 3";
            var result = Solver.MapData(input);
            Assert.False(result.Item1 == null);
            Assert.True(result.Item1.GetType() == typeof(List<Map>));
            Assert.True(result.Item1.Count == 2);
            Assert.True(result.Item2 == null);
        }

        [Fact]
        public void MapData_ReturnsErrorIfNumberOfMapsTooBig() // maps count > 5
        {
            var input = "6\n3\n1 1\n2 2\n3 3\n3\n1 1\n2 2\n3 3\n3\n1 1\n2 2\n3 3\n3\n1 1\n2 2\n3 3\n3\n1 1\n2 2\n3 3\n3\n1 1\n2 2\n3 3";
            var result = Solver.MapData(input);
            Assert.True(result.Item1 == null);
            Assert.True(result.Item2 == Errors.NumberOfMapsTooBig);
        }

        //N
        [Fact]
        public void MapData_ReturnsErrorIfNumberOfStarsTooSmall() // stars count < 1
        {
            var input = "2\n0\n3\n1 1\n2 2\n3 3";
            var result = Solver.MapData(input);
            Assert.True(result.Item1 == null);
            Assert.True(result.Item2 == Errors.NumberOfStarsTooSmall);
        }

        [Fact]
        public void MapData_ReturnsOkIfNumberOfStarsOk() // stars count >= 1 && stars count <= 400
        {
            var input = "2\n3\n1 1\n2 2\n3 3\n3\n1 1\n2 2\n3 3";
            var result = Solver.MapData(input);
            Assert.False(result.Item1 == null);
            Assert.True(result.Item1.GetType() == typeof(List<Map>));
            Assert.True(result.Item1.Count == 2);
            Assert.True(result.Item2 == null);
        }

        [Fact]
        public void MapData_ReturnsErrorIfNumberOfStarsTooBig() // stars count > 400
        {
            var input = "2\n800\n3\n1 1\n2 2\n3 3";
            var result = Solver.MapData(input);
            Assert.True(result.Item1 == null);
            Assert.True(result.Item2 == Errors.NumberOfStarsTooBig);
        }

        //Coordinate
        [Fact]
        public void MapData_ReturnsErrorIfCoordinatesTooSmall() // coordinate < 1
        {
            var input = "2\n3\n1 1\n-2 2\n3 3\n3\n1 1\n2 2\n3 3";
            var result = Solver.MapData(input);
            _testOutputHelper.WriteLine(result.Item2);
            Assert.True(result.Item1 == null);
            Assert.True(result.Item2 == Errors.CoordinateTooSmall);
        }

        [Fact]
        public void MapData_ReturnsOkIfCoordinatesOk() // coordinate >= 1 && coordinate <= 109
        {
            var input = "2\n3\n1 1\n2 2\n3 3\n3\n1 1\n2 2\n3 3";
            var result = Solver.MapData(input);
            Assert.False(result.Item1 == null);
            Assert.True(result.Item1.GetType() == typeof(List<Map>));
            Assert.True(result.Item1.Count == 2);
            Assert.True(result.Item2 == null);
        }

        [Fact]
        public void MapData_ReturnsErrorIfCoordinatesTooBig() // coordinate > 109
        {
            var input = "2\n3\n1 1\n200 2\n3 3\n3\n1 1\n2 2\n3 3";
            var result = Solver.MapData(input);
            Assert.True(result.Item1 == null);
            Assert.True(result.Item2 == Errors.CoordinateTooBig);
        }


    }
}
