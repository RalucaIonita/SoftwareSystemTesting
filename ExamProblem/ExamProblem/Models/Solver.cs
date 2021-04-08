using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ExamProblem.Models
{
    public static class Solver
    {
        public static List<Triangle> Triangles { get; set; }

        public static string LoadFileContent(string path)
        {
            var text = File.ReadAllText(path);
            return text;
        }

        public static List<Map> MapData(string text)
        {
            var maps = new List<Map>();

            var lines = text.Split("\n");

            Map map = null;
            foreach (var line in lines)
            {
                var splitLine = line.Split(" ");
                if (splitLine.Length == 1)
                {
                    if(map != null)
                        maps.Add(map);
                    map = new Map();
                }
                else
                {
                    map.Points.Add(new Point(Int32.Parse(splitLine[0]), Int32.Parse(splitLine[1])));
                }
            }
            maps.Add(map);
            return maps;
        }


        public static List<Triangle> GetTriangles(Map map)
        {
            var triangles = new List<Triangle>();

            foreach (var first in map.Points)
            {
                foreach (var second in map.Points)
                {
                    foreach (var third in map.Points)
                    {
                        var canForm = first.CanFormTriangle(second, third);
                        // Console.WriteLine(canForm);
                        if(canForm)
                            triangles.Add(new Triangle(first, second, third));
                    }
                    
                }
            }



            //
            // for(var i = 0; i < map.Points.Count - 2; i++)
            //     for(var j = i + 1; j < map.Points.Count - 1; j++)
            //         for (var k = j + 1; k < map.Points.Count; k++)
            //         {
            //             var a = map.Points[i];
            //             var b = map.Points[j];
            //             var c = map.Points[k];
            //             if (a.CanFormTriangle(b, c))
            //                 triangles.Add(new Triangle(a, b, c));
            //         }
            return triangles;
        }

        public static bool AreTranslated(Triangle t1, Triangle t2)
        {
            var origin = new Point(0, 0);
            // Console.WriteLine("\n\n\n\n\nTriangles:");
            // Console.WriteLine(t1.A.X + " " + t2.A.Y);
            // Console.WriteLine(t1.B.X + " " + t2.B.Y);
            // Console.WriteLine(t1.B.X + " " + t2.B.Y);
            // Console.WriteLine("\n");
            //
            // Console.WriteLine("Triangles:");
            // Console.WriteLine(t2.A.X + " " + t2.A.Y);
            // Console.WriteLine(t2.B.X + " " + t2.B.Y);
            // Console.WriteLine(t2.B.X + " " + t2.B.Y);
            // Console.WriteLine("\n");

            var dA = t1.A - t2.A;
            var dB = t1.B - t2.B;
            var dC = t1.C - t2.C;


            //A1-A2; B1-B2; C1-C2
            // dA.X = A1.X - A2.X;
            // dB.X = B1.X - B2.X;
            // dC.X = C1.X - C2.X;
            //
            // dA.Y = A1.Y - A2.Y;
            // dB.Y = B1.Y - B2.Y;
            // dC.Y = C1.Y - C2.Y;


            Console.WriteLine("A: " + dA.X + " " + dA.Y);
            Console.WriteLine("B: " + dB.X + " " + dB.Y);
            Console.WriteLine("C: " + dC.X + " " + dC.Y);
            if (dA == dB && dB == dC && dA != origin)
                return true;

            //A1-B2; B1-C2; C1-A2
            dA = t1.A - t2.B;
            dB = t1.B - t2.C;
            dC = t1.C - t2.A;


            Console.WriteLine("\nA: " + dA.X + " " + dA.Y);
            Console.WriteLine("B: " + dB.X + " " + dB.Y);
            Console.WriteLine("C: " + dC.X + " " + dC.Y);


            if (dA == dB && dB == dC && dA != origin)
                return true;

            //A1-C2; B1-A2; C1-B2
            dA = t1.A - t2.C;
            dB = t1.B - t2.A;
            dC = t1.C - t2.B;

            Console.WriteLine("\nA: " + dA.X + " " + dA.Y);
            Console.WriteLine("B: " + dB.X + " " + dB.Y);
            Console.WriteLine("C: " + dC.X + " " + dC.Y +"\n");

            if (dA == dB && dC == dB && dA != origin)
                return true;

            return false;
        }


        public static void WriteResultToFile(string path, string result)
        {
            File.WriteAllText(path, result);
        }
    }
}
