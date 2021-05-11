using ExamProblem.Models;

namespace ExamProblem
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
                if (char.IsDigit(ch) || ch == ' ' || ch == '\n')
                    result += ch;
            }
            return result;
        }

        public static bool ContainsOnlyOkCharacters(this string str)
        {
            foreach (var chr in str)
            {
                if (!char.IsDigit(chr) && chr != ' ' && chr != '\n' && chr != '-')
                    return false;
            }
            return true;
        }
    }
}
