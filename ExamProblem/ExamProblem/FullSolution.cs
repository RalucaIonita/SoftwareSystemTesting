using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ExamProblem.Models;

namespace ExamProblem.Tests
{
    public static class FullSolution
    {
        static FullSolution()
        {
        }

        public static string ProblemSolution(string inPath, string outPath)
        {
            var text = "";
            try
            {
                text = File.ReadAllText(inPath);
            }
            catch (FileNotFoundException)
            {
                return Errors.FileNotFound;
            }

            if (text.Length == 0)
                return Errors.EmptyFile;
            
            Console.WriteLine(text);
            var isValid = text.ContainsOnlyOkCharacters();
            if (!isValid)
                return Errors.FileDoesNotContainOnlyDigits;

            //text = text.EliminateWeirdCharacters();

            var maps = new List<Map>();

            var lines = text.Split("\n");
            var saidCount = Int32.Parse(lines[0]);
            if (saidCount < 1)
                return Errors.NumberOfMapsTooSmall;
            if (saidCount > 5)
                return Errors.NumberOfMapsTooBig;

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
                            return Errors.WrongNumberOfPoints;
                        }

                        maps.Add(map);
                    }

                    map = new Map();
                    if (splitLine[0] != "")
                        number = Int32.Parse(splitLine[0]);
                    if (number < 1)
                        return Errors.NumberOfStarsTooSmall;
                    if (number > 400)
                        return Errors.NumberOfStarsTooBig;
                }
                else
                {
                    var x = Int32.Parse(splitLine[0]);
                    var y = Int32.Parse(splitLine[1]);
                    if (x < 0 || y < 0)
                        return Errors.CoordinateTooSmall;
                    if (x > 109 || y > 109)
                        return Errors.CoordinateTooBig;
                    var point = new Point(x, y);
                    Console.WriteLine(point.X + " -----> " + point.Y);
                    if (map?.Points.Count != 0)
                    {
                        var exists = map?.Points.FirstOrDefault(p => p.X == point.X && p.Y == point.Y);
                        if (exists != null)
                            return Errors.TwoIdenticalPoints;
                    }

                    map?.Points.Add(point);
                }
            }

            maps.Add(map);
            //check map count
            var trueCount = maps.Count;
            if (trueCount != saidCount)
                return Errors.WrongNumberOfMaps;

            var data = maps.Where(m => m.Points.Count != 0).ToList();
            var stringToWrite = "";
            foreach (var thisMap in data)
            {
                var found = false;
                if (thisMap.Points.Count < 3)
                {
                    return Errors.NotEnoughPoints;
                }

                var triangles = new List<Triangle>();

                foreach (var first in thisMap.Points)
                {
                    foreach (var second in thisMap.Points)
                    {
                        foreach (var third in thisMap.Points)
                        {
                            var canForm = first.CanFormTriangle(second, third);
                            if (canForm)
                                triangles.Add(new Triangle(first, second, third));
                        }

                    }
                }
                for (var i = 0; i < triangles.Count - 1; i++)
                for (var j = i + 1; j < triangles.Count; j++)
                {
                    var areTranslated = Solver.AreTranslated(triangles[i], triangles[j]);

                    if (areTranslated.Item1)
                    {
                        Console.WriteLine("*");
                        found = true;
                        goto End;
                    }
                }

                End:
                if (found)
                    stringToWrite += "DA ";
                else stringToWrite += "NU ";
            }
            try
            {
                Console.WriteLine("\n\nWrote solution to file plagiat.out!");
                File.WriteAllText(outPath, stringToWrite);
                Console.WriteLine("\nOk, bye!");
                return Errors.Success;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Errors.Error;
            }
        }
    }
}
