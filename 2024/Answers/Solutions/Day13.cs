using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Point = (long x, long y);

namespace Answers.Solutions
{
    internal class Day13 : Solution
    {
        public override int DayNum => 13;

        const int AButtonPrice = 3;
        const int BButtonPrice = 1;

        const long Offset = 10000000000000;

        struct Machine
        {
            public Point ButtonA;
            public Point ButtonB;
            public Point Prize;
        }

        readonly List<Machine> Machines = new List<Machine>();

        protected override void Read()
        {
            string[] lines = File.ReadAllLines("../../../Data/day13.txt");

            for (int i = 0; i < lines.Length; i += 4)
            {
                string btnAStr = lines[i];
                string btnBStr = lines[i + 1];
                string prizeStr = lines[i + 2];

                Point buttonA = PointExtraction(btnAStr, false);
                Point buttonB = PointExtraction(btnBStr, false);
                Point prize = PointExtraction(prizeStr, true);

                Machines.Add(new Machine { ButtonA = buttonA, ButtonB = buttonB, Prize = prize });
            }
        }

        private Point PointExtraction(string line, bool prize)
        {
            int xStart = line.IndexOf(prize ? "X=" : "X+") + 2;
            int xEnd = line.IndexOf(',') - xStart;

            int yStart = line.IndexOf(prize ? "Y=" : "Y+") + 2;
            int yEnd = line.Length - yStart;

            return (int.Parse(line.AsSpan(xStart, xEnd)), int.Parse(line.AsSpan(yStart, yEnd)));
        }

        protected override void SolvePartOne()
        {
            // https://www.reddit.com/r/adventofcode/comments/1hd7irq/2024_day_13_an_explanation_of_the_mathematics/
            foreach (Machine machine in Machines)
            {
                Point a = machine.ButtonA;
                Point b = machine.ButtonB;
                Point p = machine.Prize;

                long A = (p.x * b.y - p.y * b.x) / (a.x * b.y - a.y * b.x);
                long B = (a.x * p.y - a.y * p.x) / (a.x * b.y - a.y * b.x);

                if (A * a.x + B * b.x == p.x && A * a.y + B * b.y == p.y)
                {
                    Solution1 += A * AButtonPrice;
                    Solution1 += B * BButtonPrice;
                }
            }
        }

        protected override void SolvePartTwo()
        {
            foreach (Machine machine in Machines)
            {
                Point a = machine.ButtonA;
                Point b = machine.ButtonB;
                Point p = (machine.Prize.x + Offset, machine.Prize.y + Offset);

                long A = (p.x * b.y - p.y * b.x) / (a.x * b.y - a.y * b.x);
                long B = (a.x * p.y - a.y * p.x) / (a.x * b.y - a.y * b.x);

                if (A * a.x + B * b.x == p.x && A * a.y + B * b.y == p.y)
                {
                    Solution2 += A * AButtonPrice;
                    Solution2 += B * BButtonPrice;
                }
            }
        }
    }
}
