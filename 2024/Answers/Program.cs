

using Answers.Solutions;
using System.Runtime.CompilerServices;

namespace Answers
{
    public class Program
    {
        private static readonly Solution[] Solutions =
        [
            new Day1(),
            new Day2(),
            new Day3()
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