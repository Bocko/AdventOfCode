using Answers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;
using Point = (int x, int y);

namespace Answers.Solutions
{
    internal class Day12 : Solution
    {
        public override int DayNum => 12;

        char[,]? CharMap = null;

        List<HashSet<Point>> Areas = new List<HashSet<Point>>();

        Point[] Directions =
{
            (0,-1),
            (1,0),
            (0,1),
            (-1,0)
        };

        protected override void Read()
        {
            string[] lines = File.ReadAllLines("../../../Data/day12.txt");

            CharMap = new char[lines.Length, lines[0].Length];
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    CharMap[y, x] = lines[y][x];
                }
            }

            HashSet<Point> overallVisited = new HashSet<Point>();

            for (int y = 0; y < CharMap.GetLength(0); y++)
            {
                for (int x = 0; x < CharMap.GetLength(1); x++)
                {
                    HashSet<Point> visited = new HashSet<Point>();
                    DiscoverRegions((x, y), visited, overallVisited);

                    if (visited.Count > 0)
                    {
                        Areas.Add(visited);
                    }
                }
            }
        }

        private void DiscoverRegions(Point currentPoint, HashSet<Point> visitedCurrent, HashSet<Point> visitedOverall)
        {
            if (visitedOverall.Contains(currentPoint))
            {
                return;
            }

            visitedOverall.Add(currentPoint);
            visitedCurrent.Add(currentPoint);

            for (int i = 0; i < 4; i++)
            {
                Point nextPoint = (currentPoint.x + Directions[i].x, currentPoint.y + Directions[i].y);
                if (BoundsCheck(nextPoint) && !visitedCurrent.Contains(nextPoint) && IsValidStep(currentPoint, nextPoint))
                {
                    DiscoverRegions(nextPoint, visitedCurrent, visitedOverall);
                }
            }

            return;
        }

        private bool BoundsCheck(Point point)
        {
            return point.x > -1 && point.x < CharMap!.GetLength(1) && point.y > -1 && point.y < CharMap!.GetLength(0);
        }

        private bool IsValidStep(Point currentPoint, Point nextPoint)
        {
            return CharMap![currentPoint.y, currentPoint.x] == CharMap[nextPoint.y, nextPoint.x];
        }

        protected override void SolvePartOne()
        {
            foreach (HashSet<Point> regions in Areas)
            {
                int perimeter = 0;

                for (int i = 0; i < regions.Count; i++)
                {
                    int numberOfFreeSides = 4;

                    for (int j = 0; j < regions.Count; j++)
                    {
                        if (i == j)
                        {
                            continue;
                        }

                        Point currentPoint = regions.ElementAt(i);
                        Point checkingPoint = regions.ElementAt(j);

                        int xDiff = Math.Abs(currentPoint.x - checkingPoint.x);
                        int yDiff = Math.Abs(currentPoint.y - checkingPoint.y);

                        if ((xDiff < 2 && yDiff == 0) || (xDiff == 0 && yDiff < 2))
                        {
                            numberOfFreeSides--;
                        }
                    }

                    perimeter += numberOfFreeSides;
                }

                Solution1 += perimeter * regions.Count;
            }
        }

        protected override void SolvePartTwo()
        {
            foreach (HashSet<Point> regions in Areas)
            {
                int edges = 0;

                for (int i = 0; i < regions.Count; i++)
                {
                    Point currentPoint = regions.ElementAt(i);

                    Point north = (currentPoint.x, currentPoint.y - 1);
                    Point northEast = (currentPoint.x + 1, currentPoint.y - 1);
                    Point east = (currentPoint.x + 1, currentPoint.y);
                    //Point southEast = (currentPoint.x + 1, currentPoint.y + 1);
                    Point south = (currentPoint.x, currentPoint.y + 1);
                    Point southWest = (currentPoint.x - 1, currentPoint.y + 1);
                    Point west = (currentPoint.x - 1, currentPoint.y);
                    Point northWest = (currentPoint.x - 1, currentPoint.y - 1);

                    //stole the corner check from here:https://github.com/Bpendragon/AdventOfCodeCSharp/blob/05db46/AdventOfCode/Solutions/Year2024/Day12-Solution.cs
                    //I only stole the ifs everything else is mine >:(
                    //Check behind us, if regions does not contain spot behind us then we just crossed a fence, now we need to check if we've already counted it. 
                    if (!regions.Contains(west))
                    {
                        if (!regions.Contains(north))
                        {
                            edges++; //One step above does not have a plot this must be a corner. To avoid double-counting only adding 1 here.
                        }
                        else
                        {
                            if (regions.Contains(northWest)) edges++; //Internal corner, if False that would mean that we're at least 1 step down a vertical wall.
                        }
                    }

                    //Check Above us, if nothing above us then we're going along below a fence, make sure we count it, but don't double count it.
                    if (!regions.Contains(north))
                    {
                        if (!regions.Contains(west))
                        {
                            edges++; //One step behind does not have a plot this must be a corner. To avoid double-counting only adding 1 here.
                        }
                        else
                        {
                            if (regions.Contains(northWest)) edges++; //Internal corner, if False that would mean that we're at least 1 step down a vertical wall.
                        }
                    }

                    //Check In Front us, if nothing in front of us then we're about to cross out of the field, make sure we count it, but don't double count it.
                    if (!regions.Contains(east))
                    {
                        if (!regions.Contains(north))
                        {
                            edges++; //One step above does not have a plot this must be a corner. To avoid double-counting only adding 1 here.
                        }
                        else
                        {
                            if (regions.Contains(northEast)) edges++; //Internal corner, if False that would mean that we're at least 1 step down a vertical wall.
                        }
                    }

                    //Check below us, if nothing below us then we're going along above a fence, make sure we count it, but don't double count it.
                    if (!regions.Contains(south))
                    {
                        if (!regions.Contains(west))
                        {
                            edges++; //One step behind does not have a plot this must be a corner. To avoid double-counting only adding 1 here.
                        }
                        else
                        {
                            if (regions.Contains(southWest)) edges++; //Internal corner, if False that would mean that we're at least 1 step along the wall.
                        }
                    }
                }

                Solution2 += edges * regions.Count;
            }
        }
    }
}
