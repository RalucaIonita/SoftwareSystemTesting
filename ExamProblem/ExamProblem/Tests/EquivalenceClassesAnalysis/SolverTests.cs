using System.Collections.Generic;
using ExamProblem.Models;
using Xunit;
using Xunit.Abstractions;

namespace ExamProblem.Tests.EquivalenceClassesAnalysis
{
    public class SolverTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public SolverTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }


        //classes ->
        // C1 = {path | path exists}
        // C2 = {path | path does not exist}


        [Fact]
        public void LoadText_ShouldReturnString() //Class 1
        {
            var path = "..\\..\\..\\..\\ExamProblem\\plagiat.in";
            var text = Solver.LoadFileContent(path);

            var expected = "2\r\n5\r\n1 1\r\n2 2\r\n0 0\r\n1 2\r\n100 105\r\n5\r\n1 1\r\n2 2\r\n1 2\r\n0 0\r\n0 1";
            Assert.True(text == expected);
        }

        [Fact]
        public void LoadText_ShouldReturnError() //Class 2
        {
            var path = "dasrgawezsthb";
            var result = Solver.LoadFileContent(path);

            Assert.True(result == Errors.FileNotFound);
        }

        
        //Classes
        // C1 = {text | text contains letters}
        // C2 = {text | text does not contain the number of points declared}
        // C3 = {text | text does not contain the number of maps declared}
        // C4 = {text | text is empty string}
        // C5 = {text | text is valid}
        // C6 = {text | text does not contain two identical points}


        [Fact]
        public void MapData_ShouldReturnErrorForInputWithLetters() //Class 1
        {
            var input = "dsfbhsdrga\n\nwsreg";
            var map = Solver.MapData(input);
            Assert.True(map.Item2 == Errors.FileDoesNotContainOnlyDigits);
            Assert.True(map.Item1 == null);
        }

        [Fact]
        public void MapData_ShouldReturnErrorForWrongPoints() //Class 2
        {
            var input = "2\n3\n1 1\n2 2\n3 3\n5\n1 2\n";
            var map = Solver.MapData(input);
            Assert.True(map.Item2 == Errors.WrongNumberOfPoints);
            Assert.True(map.Item1 == null);
        }

        [Fact]
        public void MapData_ShouldReturnErrorForWrongMaps() //Class 3
        {
            var input = "3\n5\n1 1\n2 2\n0 0\n1 2\n100 105\n5\n1 1\n2 2\n1 2\n0 0\n0 1";
            var map = Solver.MapData(input);
            Assert.True(map.Item2 == Errors.WrongNumberOfMaps);
            Assert.True(map.Item1 == null);
        }

        [Fact]
        public void MapData_ReturnsErrorIfEmptyFile() //Class 4
        {
            var input = "";
            var map = Solver.MapData(input);
            Assert.True(map.Item2 == Errors.EmptyFile);
            Assert.True(map.Item1 == null);
        }

        [Fact]
        public void MapData_ReturnsListOfMapsIfDataOk() //Class 5
        {
            var input = "2\n5\n1 1\n2 2\n0 0\n1 2\n100 105\n5\n1 1\n2 2\n1 2\n0 0\n0 1";
            var map = Solver.MapData(input);
            Assert.True(map.Item2 == null);
            Assert.True(map.Item1.GetType() == typeof(List<Map>));
            Assert.True(map.Item1.Count == 2);
        }

        [Fact]
        public void MapData_ReturnsErrorIfFoundTwoIdenticalPoints() //Class 6
        {
            var input = "2\n5\n1 1\n1 1\n0 0\n1 2\n100 105\n5\n1 1\n2 2\n1 2\n0 0\n0 1";
            var map = Solver.MapData(input);
            Assert.True(map.Item1 == null);
            Assert.True(map.Item2 == Errors.TwoIdenticalPoints);
        }


        //classes ->
        // C1 = {map | map does not have 3 or more points}
        // C2 = {map | map does has 3 or more points}



        [Fact]
        public void GetTriangles_ReturnsErrorIfNotEnoughPoints() //Class 1
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
        public void GetTriangles_ReturnsOkIfEnoughPoints() //Class 2
        {
            var map = new Map
            {
                Points = new List<Point> { new Point(4, 5), new Point(4, 8), new Point(5, 9) }
            };

            var result = Solver.GetTriangles(map);
            Assert.True(result.Item2 == null);
            Assert.True(result.Item1.GetType() == typeof(List<Triangle>));
        }


        //classes -> 
        // C1 = {points | triangles formed of points are translated}
        // C2 = {points | triangles formed of points are not translated}
        // C3 = {points | one or both of the triangles are null}


        [Fact]
        public void AreTranslated_ReturnsTrueIfTranslated() //Class 1
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
            Assert.True(result.Item1);
            Assert.True(result.Item2 == null);

        }

        [Fact]
        public void AreTranslated_ReturnsFalseIfNotTranslated() //Class 2
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
            Assert.False(result.Item1);
            Assert.True(result.Item2 == null);

        }


        [Fact]
        public void AreTranslated_ReturnsErrorIfOneOrBothDoNotExist() //Class 3
        {
            var t1 = new Triangle
            {
                A = new Point(1, 1),
                B = new Point(1, 3),
                C = new Point(4, 3),
            };
            
            var result = Solver.AreTranslated(t1, null);
            Assert.True(result.Item1 == null);
            Assert.True(result.Item2 == Errors.NullTriangle);

        }
    }
}
