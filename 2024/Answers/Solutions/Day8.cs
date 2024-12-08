using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Point = (int x, int y);

namespace Answers.Solutions
{
    internal class Day8 : Solution
    {
        public override int DayNum => 8;

        readonly Dictionary<char, List<Point>> Antennas = new Dictionary<char, List<Point>>();
        HashSet<Point> Antinodes = new HashSet<Point>();
        HashSet<Point> AntinodesHarmonics = new HashSet<Point>();
        Point Bounds = new Point();

        protected override void Read()
        {
            string[] lines = File.ReadAllLines("../../../Data/day8.txt");
            Bounds.x = lines[0].Length;
            Bounds.y = lines.Length;
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    char c = lines[y][x];
                    if (c != '.')
                    {
                        if (!Antennas.TryAdd(c, [(x, y)]))
                        {
                            Antennas[c].Add((x, y));
                        }
                    }
                }
            }
        }

        protected override void SolvePartOne()
        {
            foreach (var antenna in Antennas.Values)
            {
                for (int currentIndex = 0; currentIndex < antenna.Count - 1; currentIndex++)
                {
                    for (int checkingIndex = currentIndex + 1; checkingIndex < antenna.Count; checkingIndex++)
                    {
                        Point currentAntenna = antenna[currentIndex];
                        Point checkingAntenna = antenna[checkingIndex];

                        Point diff = (checkingAntenna.x - currentAntenna.x, checkingAntenna.y - currentAntenna.y);

                        Point antinode1 = (currentAntenna.x - diff.x, currentAntenna.y - diff.y);
                        Point antinode2 = (checkingAntenna.x + diff.x, checkingAntenna.y + diff.y);

                        if (BoundCheck(currentAntenna.x - diff.x, currentAntenna.y - diff.y))
                        {
                            Antinodes.Add(antinode1);
                        }

                        if (BoundCheck(checkingAntenna.x + diff.x, checkingAntenna.y + diff.y))
                        {
                            Antinodes.Add(antinode2);
                        }
                    }
                }
            }

            Solution1 = Antinodes.Count;
        }

        private bool BoundCheck(int x, int y)
        {
            return x > -1 && x < Bounds.x && y > -1 && y < Bounds.y;
        }

        protected override void SolvePartTwo()
        {
            foreach (var antenna in Antennas.Values)
            {
                if (antenna.Count < 2)
                {
                    continue;
                }

                for (int currentIndex = 0; currentIndex < antenna.Count - 1; currentIndex++)
                {
                    for (int checkingIndex = currentIndex + 1; checkingIndex < antenna.Count; checkingIndex++)
                    {
                        Point currentAntenna = antenna[currentIndex];
                        Point checkingAntenna = antenna[checkingIndex];

                        AntinodesHarmonics.Add(currentAntenna);
                        AntinodesHarmonics.Add(checkingAntenna);

                        Point diff = (checkingAntenna.x - currentAntenna.x, checkingAntenna.y - currentAntenna.y);

                        Point diffNeg = diff;
                        Point diffPos = diff;

                        while (BoundCheckAndAdd(currentAntenna.x - diffNeg.x, currentAntenna.y - diffNeg.y))
                        {
                            diffNeg.x += diff.x;
                            diffNeg.y += diff.y;
                        }

                        while (BoundCheckAndAdd(checkingAntenna.x + diffPos.x, checkingAntenna.y + diffPos.y))
                        {
                            diffPos.x += diff.x;
                            diffPos.y += diff.y;
                        }
                    }
                }
            }

            Solution2 = AntinodesHarmonics.Count;
        }

        private bool BoundCheckAndAdd(int x, int y)
        {
            bool valid = x > -1 && x < Bounds.x && y > -1 && y < Bounds.y;

            if (valid)
            {
                AntinodesHarmonics.Add((x, y));
            }

            return valid;
        }
    }
}
