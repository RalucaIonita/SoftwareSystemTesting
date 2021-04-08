using System;
using System.Diagnostics;
using ExamProblem.Models;

namespace ExamProblem
{
    class Program
    {
        private static string InPath =
            "C:\\Users\\raluc\\source\\repos\\SoftwareSystemTesting\\ExamProblem\\ExamProblem\\examen.in";
        private static string OutPath =
            "C:\\Users\\raluc\\source\\repos\\SoftwareSystemTesting\\ExamProblem\\ExamProblem\\examen.out";
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, world!\n\n I started solving the problem!");

            //load data
            var fileContent = Solver.LoadFileContent(InPath);
            Console.WriteLine("\nLoaded data!");
            Console.WriteLine("File data:");
            Console.WriteLine(fileContent);

            //map data
            var data = Solver.MapData(fileContent);
            Console.WriteLine("\nMapped data!");
            data.ForEach(d => Console.Write(d + " "));

            //get result
            var result = Solver.ComputeResult(data);
            if (result == null)
            {
                Console.WriteLine("\n\nEither solution does not exist or it in not unique.");
                Console.WriteLine("-1");
                return;
            }

            Console.WriteLine("\nWrote solution to file examen.out!");
            Solver.WriteResultToFile(OutPath, result);
            Console.WriteLine("\nOk, bye!");
        }
    }
}
