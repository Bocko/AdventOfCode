using System;
using System.Collections.Generic;
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

        protected abstract void Read();
        protected abstract void SolvePartOne();
        protected abstract void SolvePartTwo();

        private void Display()
        {
            Console.WriteLine($"Day {DayNum}.1: ");
            Console.WriteLine($"    Solution: {Solution1}");
            Console.WriteLine(Divider);
            Console.WriteLine($"Day {DayNum}.2: ");
            Console.WriteLine($"    Solution: {Solution2}");
            Console.WriteLine(Divider);
        }

        public void Run()
        {
            Read();
            SolvePartOne();
            SolvePartTwo();
            Display();
        }
    }
}
