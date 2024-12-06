

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
        ];

        public static void Main(string[] arg)
        {
            RunSolutions();
        }

        private static void RunSolutions()
        {
            foreach (Solution solution in Solutions)
            {
                solution.Run();
            }
        }
    }
}