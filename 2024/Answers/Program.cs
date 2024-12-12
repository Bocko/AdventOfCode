

using Answers.Solutions;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Answers
{
    public class Program
    {
        private static readonly Solution[] Solutions =
        [
            new Day1(),
            new Day2(),
            new Day3(),
            new Day4(),
            new Day5(),
            new Day6(),
            new Day7(),
            new Day8(),
            new Day9(),
            new Day10(),
            new Day11(),
            new Day12(),
        ];

        public static void Main(string[] arg)
        {
            RunSolutions();
        }

        private static void RunSolutions()
        {
            DateTime now = DateTime.Now;

            Solutions.Where(s => s.DayNum == now.Day).First().Run();
        }
    }
}