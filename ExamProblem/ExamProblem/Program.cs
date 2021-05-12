using ExamProblem.Models;
using System;
using System.Linq;
using System.Threading;
using ExamProblem.Tests;

namespace ExamProblem
{
    class Program
    {
        private static string InPath = "..\\..\\..\\..\\ExamProblem\\plagiat.in";
        private static string OutPath = "..\\..\\..\\..\\ExamProblem\\plagiat.out";

        public static void Main(string[] args)
        {

            FullSolution.ProblemSolution(InPath, OutPath);

            // Console.WriteLine("Hello, world!\n\nI started solving the problem!");
            //
            // //load data
            // var fileContent = Solver.LoadFileContent(InPath);
            // if (fileContent == Errors.FileNotFound)
            // {
            //     Thread.Sleep(5000);
            //     return;
            // }
            // Console.WriteLine("\nLoaded data!");
            // Console.WriteLine("File data:");
            // Console.WriteLine(fileContent);
            //
            // //filter text
            // fileContent = fileContent.EliminateWeirdCharacters();
            //
            // //map data
            // var result = Solver.MapData(fileContent);
            // if(result.Item1 == null) 
            // {
            //     Console.WriteLine(result.Item2);
            //     return;
            // }
            // var data = result.Item1.Where(m => m.Points.Count != 0).ToList();
            // Console.WriteLine("\nMapped data!");
            // Console.WriteLine("Maps: " + data.Count);
            //
            // Console.WriteLine("Map 1");
            // data[0].Points.ForEach(p => Console.WriteLine(p.X + " " + p.Y));
            // Console.WriteLine("Map 2");
            // data[1].Points.ForEach(p => Console.WriteLine(p.X + " " + p.Y));
            //
            //
            // var stringToWrite = "";
            // foreach (var map in data)
            // {
            //     var found = false;
            //     Console.WriteLine("**");
            //     //get triangles
            //     var triResponse = Solver.GetTriangles(map);
            //     if (triResponse.Item1 == null)
            //     {
            //         Console.WriteLine(triResponse.Item2);
            //         return;
            //     }
            //
            //     var triangles = triResponse.Item1;
            //     Console.WriteLine(triangles.Count + "triangles.");
            //     for (var i = 0; i < triangles.Count - 1; i++)
            //         for (var j = i + 1; j < triangles.Count; j++)
            //         {
            //             var areTranslated = Solver.AreTranslated(triangles[i], triangles[j]);
            //
            //             if (areTranslated.Item1)
            //             {
            //                 Console.WriteLine("*");
            //                 found = true;
            //                 goto End;
            //             }
            //         }
            //     
            //     End:
            //     if (found)
            //         stringToWrite += "DA ";
            //     else stringToWrite += "NU ";
            // }
            //
            //
            // Console.WriteLine("\n\nWrote solution to file plagiat.out!");
            // Solver.WriteResultToFile(OutPath, stringToWrite);
            // Console.WriteLine("\nOk, bye!");
        }
    }
}
