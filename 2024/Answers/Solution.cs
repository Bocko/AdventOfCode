using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Answers
{
    public abstract class Solution
    {
        protected abstract int DayNum
        {
            get;
        }

        protected int Solution1 = 0;
        protected int Solution2 = 0;

        private const string Divider = "##############################";

        private long TimeElapsed = 0;

        protected abstract void Read();
        protected abstract void SolvePartOne();
        protected abstract void SolvePartTwo();

        private void Display()
        {
            Console.WriteLine(Divider);
            Console.WriteLine($"Day {DayNum}:");
            Console.WriteLine($"\tTime: {TimeElapsed}ms");
            Console.WriteLine(Divider);
            Console.WriteLine($"\tPart 1:");
            Console.WriteLine($"\tSolution: {Solution1}");
            Console.WriteLine(Divider);
            Console.WriteLine($"\tPart 2:");
            Console.WriteLine($"\tSolution: {Solution2}");
        }

        public void Run()
        {
            Stopwatch sw = Stopwatch.StartNew();
            Read();
            SolvePartOne();
            SolvePartTwo();
            sw.Stop();
            TimeElapsed = sw.ElapsedMilliseconds;
            Display();
        }
    }
}
