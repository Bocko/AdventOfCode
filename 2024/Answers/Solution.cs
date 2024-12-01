using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Answers
{
    public abstract class Solution
    {
        private string Divider = "##############################";

        protected abstract void Read();
        protected abstract void SolvePartOne();
        protected abstract void SolvePartTwo();
        protected abstract void Display();

        protected void PrintSolution(int dayNum, string solution1, string solution2)
        {
            Console.WriteLine($"Day {dayNum}.1: ");
            Console.WriteLine($"    Solution: {solution1}");
            Console.WriteLine(Divider);
            Console.WriteLine($"Day {dayNum}.2: ");
            Console.WriteLine($"    Solution: {solution2}");
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
