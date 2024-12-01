

using Answers.Solutions;
using System.Runtime.CompilerServices;

namespace Answers
{
    public class Program
    {
        private static Solution[] Solutions = new Solution[]
        {
            new Day1()
        };

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