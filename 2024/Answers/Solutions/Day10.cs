using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Point = (int x, int y);

namespace Answers.Solutions
{
    internal class Day10 : Solution
    {
        public override int DayNum => 10;

        int[,]? HeightMap = null;
        List<Point> Trailheads = new List<Point>();

        Point[] Directions =
        {
            (0,-1),
            (1,0),
            (0,1),
            (-1,0)
        };

        protected override void Read()
        {
            string[] lines = File.ReadAllLines("../../../Data/day10.txt");
            HeightMap = new int[lines.Length, lines[0].Length];

            for (int y = 0; y < HeightMap.GetLength(0); y++)
            {
                for (int x = 0; x < HeightMap.GetLength(1); x++)
                {
                    int c = lines[y][x] - '0';
                    HeightMap[y, x] = c;
                    if (c == 0)
                    {
                        Trailheads.Add(new Point(x, y));
                    }
                }
            }
        }

        protected override void SolvePartOne()
        {
            foreach (Point p in Trailheads)
            {
                HashSet<Point> walkedPath = new HashSet<Point>();
                WalkPathPartOne(p, walkedPath);
            }
        }

        private bool WalkPathPartOne(Point currentPoint, HashSet<Point> walkedPath)
        {
            walkedPath.Add(currentPoint);

            if (HeightMap![currentPoint.y, currentPoint.x] == 9)
            {
                Solution1++;
                return true;
            }

            for (int i = 0; i < 4; i++)
            {
                Point nextPoint = (currentPoint.x + Directions[i].x, currentPoint.y + Directions[i].y);
                if (BoundsCheck(nextPoint) && !walkedPath.Contains(nextPoint) && IsValidStep(currentPoint, nextPoint))
                {
                    WalkPathPartOne(nextPoint, walkedPath);
                }
            }

            return false;
        }

        protected override void SolvePartTwo()
        {
            foreach (Point p in Trailheads)
            {
                HashSet<Point> walkedPath = new HashSet<Point>();
                WalkPathPartTwo(p);
            }
        }

        private bool WalkPathPartTwo(Point currentPoint)
        {
            if (HeightMap![currentPoint.y, currentPoint.x] == 9)
            {
                Solution2++;
                return true;
            }

            for (int i = 0; i < 4; i++)
            {
                Point nextPoint = (currentPoint.x + Directions[i].x, currentPoint.y + Directions[i].y);
                if (BoundsCheck(nextPoint) && IsValidStep(currentPoint, nextPoint))
                {
                    WalkPathPartTwo(nextPoint);
                }
            }

            return false;
        }

        private bool BoundsCheck(Point point)
        {
            return point.x > -1 && point.x < HeightMap!.GetLength(1) && point.y > -1 && point.y < HeightMap!.GetLength(0);
        }

        private bool IsValidStep(Point currentPoint, Point nextPoint)
        {
            return HeightMap![currentPoint.y, currentPoint.x] == HeightMap[nextPoint.y, nextPoint.x] - 1;
        }
    }
}
