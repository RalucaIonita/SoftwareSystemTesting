using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ExamProblem.Models;

namespace ExamProblem
{
    public static class Solver
    {
        public static string LoadFileContent(string path)
        {
            try
            {
                var text = File.ReadAllText(path);
                return text;
            }
            catch (FileNotFoundException)
            {
                return Errors.FileNotFound;
            }
        }

        public static Tuple<List<Map>, string> MapData(string text)
        {
            if(text.Length == 0)
                return new Tuple<List<Map>, string>(null, Errors.EmptyFile);
            Console.WriteLine(text);
            var isValid = text.ContainsOnlyOkCharacters();
            if (!isValid)
            {
                return new Tuple<List<Map>, string>(null, Errors.FileDoesNotContainOnlyDigits);
            }

            var maps = new List<Map>();

            var lines = text.Split("\n");
            var saidCount = Int32.Parse(lines[0]);
            if (saidCount < 1)
                return new Tuple<List<Map>, string>(null, Errors.NumberOfMapsTooSmall);
            if(saidCount > 5)
                return new Tuple<List<Map>, string>(null, Errors.NumberOfMapsTooBig);

            var number = 0;

            Map map = null;
            foreach (var line in lines.Skip(1))
            {
                var splitLine = line.Split(" ");
                if (splitLine.Length == 1)
                {
                    if (map != null)
                    {
                        if (number != map.Points.Count)
                        {
                            return new Tuple<List<Map>, string>(null, Errors.WrongNumberOfPoints);
                        }
                        maps.Add(map);
                    }
                    map = new Map();
                    if(splitLine[0] != "")
                        number = Int32.Parse(splitLine[0]);
                    if (number < 1)
                        return new Tuple<List<Map>, string>(null, Errors.NumberOfStarsTooSmall);
                    if (number > 400)
                        return new Tuple<List<Map>, string>(null, Errors.NumberOfStarsTooBig);
                }
                else
                {
                    var x = Int32.Parse(splitLine[0]);
                    var y = Int32.Parse(splitLine[1]);
                    if (x < 0 || y < 0)
                        return new Tuple<List<Map>, string>(null, Errors.CoordinateTooSmall);
                    if (x > 109 || y > 109)
                        return new Tuple<List<Map>, string>(null, Errors.CoordinateTooBig);
                    var point = new Point(x, y);
                    if (map?.Points.Count != 0)
                    {
                        var exists = map?.Points.FirstOrDefault(p => p.X == point.X && p.Y == point.Y);
                        if (exists != null)
                            return new Tuple<List<Map>, string>(null, Errors.TwoIdenticalPoints);
                    }
                    map?.Points.Add(point);
                }
            }
            maps.Add(map);
            //check map count
            var trueCount = maps.Count;
            if (trueCount != saidCount)
                return new Tuple<List<Map>, string>(null, Errors.WrongNumberOfMaps);

            return new Tuple<List<Map>, string>(maps, null);
        }


        public static Tuple<List<Triangle>, string> GetTriangles(Map map)
        {
            if(map.Points.Count < 3)
                return new Tuple<List<Triangle>, string>(null, Errors.NotEnoughPoints);
            var triangles = new List<Triangle>();

            foreach (var first in map.Points)
            {
                foreach (var second in map.Points)
                {
                    foreach (var third in map.Points)
                    {
                        var canForm = first.CanFormTriangle(second, third);
                        if(canForm)
                            triangles.Add(new Triangle(first, second, third));
                    }
                    
                }
            }

            return new Tuple<List<Triangle>, string>(triangles, null);
        }

        public static Tuple<bool, string> AreTranslated(Triangle t1, Triangle t2)
        {
            var origin = new Point(0, 0);

            var dA = t1.A - t2.A;
            var dB = t1.B - t2.B;
            var dC = t1.C - t2.C;

            if (dA.Equals(dB) && dB.Equals(dC) && !dA.Equals(origin))
                return new Tuple<bool, string>(true, null);

            //A1-B2; B1-C2; C1-A2
            dA = t1.A - t2.B;
            dB = t1.B - t2.C;
            dC = t1.C - t2.A;

            if (dA.Equals(dB) && dB.Equals(dC) && !dA.Equals(origin))
                return new Tuple<bool, string>(true, null);

            //A1-C2; B1-A2; C1-B2
            dA = t1.A - t2.C;
            dB = t1.B - t2.A;
            dC = t1.C - t2.B;

            if (dA.Equals(dB) && dB.Equals(dC) && !dA.Equals(origin))
                return new Tuple<bool, string>(true, null);

            return new Tuple<bool, string>(false, null);
        }


        public static bool WriteResultToFile(string path, string result)
        {
            try
            {
                File.WriteAllText(path, result);
                return true;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            
        }
    }
}
