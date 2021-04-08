using System;
using System.Collections.Generic;
using System.Text;

namespace ExamProblem.Models
{
    public static class Helper
    {
        public static bool CanFormTriangle(this Point a, Point b, Point c)
        {
            var product = a.X * (b.Y - c.Y)
                          + b.X * (c.Y - a.Y)
                          + c.X * (a.Y - b.Y);
            return product > 0;
        }

        public static string EliminateWeirdCharacters(this string str)
        {
            var result = "";
            foreach (var ch in str)
            {
                if (Char.IsDigit(ch) || ch == ' ' || ch == '\n')
                    result += ch;
            }
            return result;
        }
    }
}
