using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ExamProblem.Models
{
    public static class Solver
    {
        public static string LoadFileContent(string path)
        {
            var text = File.ReadAllText(path);
            return text;
        }

        public static List<int> MapData(string text)
        {
            return text.Split("\n")
                .Last().Split(" ")
                .Select(s => Int32.Parse(s)).ToList();
        }

        public static List<int> ComputeResult(List<int> data)
        {
            var results = new List<List<int>>();
            var max = data.Max();
            Console.WriteLine("\nMaximum value: " + max);

            for (var i = 0; i <= max; i++)
            {
                Console.WriteLine("\n*\n");
                var result = NumberIsValid(i, data);
                if(result != null)
                    results.Add(result);
            }

            return results.Count == 1 ? results.First() : null;
        }

        public static List<int> NumberIsValid(int number, List<int> data)
        {
            var result = Enumerable.Range(0, data.Count).Select(i => 0).ToList();
            result[0] = number;

            //
            for (var i = 0; i < data.Count; i++)
            {
                if (i == data.Count - 2)
                        result[0] = data[^1] - result[i];

                else {
                    if (i == data.Count - 1)
                        result[1] = data[0] - result[i];
                    else
                    { 
                        result[i + 2] = data[i + 1] - result[i];
                    }
                }
            }
            result.ForEach(r => Console.Write(r + " "));

            if (result[0] == number)
                return result;
            return null;
        }

        public static void WriteResultToFile(string path, List<int> data)
        {
            var str = "";
            data.ForEach(d => str += d + " ");
            File.AppendAllText(path,str);
        }
    }
}
