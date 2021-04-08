using System;
using System.Diagnostics;
using System.Linq;
using ExamProblem.Models;

namespace ExamProblem
{
    class Program
    {
        private static string InPath =
            "C:\\Users\\raluc\\source\\repos\\SoftwareSystemTesting\\ExamProblem\\ExamProblem\\plagiat.in";
        private static string OutPath =
            "C:\\Users\\raluc\\source\\repos\\SoftwareSystemTesting\\ExamProblem\\ExamProblem\\plagiat.out";
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, world!\n\n I started solving the problem!");

            //load data
            var fileContent = Solver.LoadFileContent(InPath);
            Console.WriteLine("\nLoaded data!");
            Console.WriteLine("File data:");
            Console.WriteLine(fileContent);

            //filter text
            fileContent = fileContent.EliminateWeirdCharacters();

            //map data
            var data = Solver.MapData(fileContent).Where(m => m.Points.Count != 0).ToList();
            Console.WriteLine("\nMapped data!");
            Console.WriteLine("Maps: " + data.Count);

            Console.WriteLine("Map 1");
            data[0].Points.ForEach(p => Console.WriteLine(p.X + " " + p.Y));
            Console.WriteLine("Map 2");
            data[1].Points.ForEach(p => Console.WriteLine(p.X + " " + p.Y));

            
            var stringToWrite = "";
            foreach (var map in data)
            {
                var found = false;
                Console.WriteLine("**");
                //get triangles
                var triangles = Solver.GetTriangles(map);
                Console.WriteLine(triangles.Count + "triangles.");
                for(var i = 0; i < triangles.Count - 1; i++)
                    for(var j = i + 1; j < triangles.Count; j++)
                        if (Solver.AreTranslated(triangles[i], triangles[j]))
                        {
                            Console.WriteLine("*");
                            found = true;
                            goto End;
                        }

                End:
                if (found)
                    stringToWrite += "DA ";
                else stringToWrite += "NU ";
            }

            




            Console.WriteLine("\n\nWrote solution to file examen.out!");
            Solver.WriteResultToFile(OutPath, stringToWrite);
            Console.WriteLine("\nOk, bye!");
        }
    }
}
