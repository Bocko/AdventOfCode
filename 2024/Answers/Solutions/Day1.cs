using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Answers.Solutions
{
    public class Day1 : Solution
    {
        List<int> LeftColumn = new List<int>();
        List<int> RightColumn = new List<int>();

        Dictionary<int, int> LeftColumnNumberCount = new Dictionary<int, int>();
        Dictionary<int, int> RightColumnNumberCount = new Dictionary<int, int>();

        int Solution1 = 0;
        int Solution2 = 0;

        protected override void Read()
        {
            string[] fileInput = File.ReadAllLines("../../../Data/day1.txt");

            foreach (string line in fileInput)
            {
                string[] values = line.Split("   ");
                LeftColumn.Add(int.Parse(values[0]));
                RightColumn.Add(int.Parse(values[1]));
            }
        }

        protected override void SolvePartOne()
        {
            LeftColumn.Sort();
            RightColumn.Sort();
            for (int i = 0; i < LeftColumn.Count; i++)
            {
                Solution1 += Math.Abs(LeftColumn[i] - RightColumn[i]);
            }
        }

        protected override void SolvePartTwo()
        {
            for (int i = 0; i < LeftColumn.Count; i++)
            {
                // I could have done this in the loop of the first solution, but I wanted to keep it separate
                AddToNumberToDictionary(LeftColumnNumberCount, LeftColumn[i]);
                AddToNumberToDictionary(RightColumnNumberCount, RightColumn[i]);
            }

            foreach (int leftNumber in LeftColumnNumberCount.Keys)
            {
                if (RightColumnNumberCount.TryGetValue(leftNumber, out int numberOfTimesInRight))
                {
                    Solution2 += leftNumber * numberOfTimesInRight * LeftColumnNumberCount[leftNumber];
                }
            }
        }

        private void AddToNumberToDictionary(Dictionary<int, int> dictionary, int key)
        {
            if (!dictionary.TryAdd(key, 1))
            {
                dictionary[key]++;
            }
        }

        protected override void Display()
        {
            PrintSolution(1, Solution1.ToString(), Solution2.ToString());
        }
    }
}
