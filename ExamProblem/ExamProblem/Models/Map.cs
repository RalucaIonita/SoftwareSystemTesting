using System.Collections.Generic;

namespace ExamProblem.Models
{
    public class Map
    {
        public List<Point> Points { get; set; }

        public Map()
        {
            Points = new List<Point>();
        }

        public Map(List<Point> points)
        {
            Points = points;
        }
    }
}
