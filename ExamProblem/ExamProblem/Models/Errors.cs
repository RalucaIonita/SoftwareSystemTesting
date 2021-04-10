using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamProblem.Models
{
    public static class Errors
    {
        public static string FileNotFound = "File not found";
        public static string FileDoesNotContainOnlyDigits = "File does not contain only digits.";
        public static string WrongNumberOfMaps = "Number of given maps is not equal with declared number of maps.";
        public static string WrongNumberOfPoints = "Number of given points is not equal with declared number of points.";
        public static string NotEnoughPoints = "Not enough points to form a triangle.";

        public static string EmptyFile = "File given is empty.";
    }
}
