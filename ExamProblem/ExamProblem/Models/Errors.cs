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
        public static string NullTriangle = "One or both triangles are null";
        public static string TwoIdenticalPoints = "Found two identical points in the same map.";

        public static string NumberOfMapsTooSmall = "Given number of maps is smaller than 1";
        public static string NumberOfMapsTooBig = "Given number of maps is greater than 5.";
    }
}
