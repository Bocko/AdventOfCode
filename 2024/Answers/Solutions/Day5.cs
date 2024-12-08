using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Answers.Solutions
{
    internal class Day5 : Solution
    {
        public override int DayNum => 5;

        readonly Dictionary<int, List<int>> PageRules = new Dictionary<int, List<int>>();
        readonly List<int[]> Pages = new List<int[]>();
        readonly List<int[]> InvalidPages = new List<int[]>();

        protected override void Read()
        {
            string[] lines = File.ReadAllLines("../../../Data/day5.txt");

            bool scanPageRules = true;

            foreach (string line in lines)
            {
                if (line.Equals(""))
                {
                    scanPageRules = false;
                    continue;
                }

                if (scanPageRules)
                {
                    string[] rules = line.Split('|');
                    if (!PageRules.TryAdd(int.Parse(rules[0]), new List<int> { int.Parse(rules[1]) }))
                    {
                        PageRules[int.Parse(rules[0])].Add(int.Parse(rules[1]));
                    }
                }
                else
                {
                    int[] pages = line.Split(',').Select(p => int.Parse(p)).ToArray();
                    Pages.Add(pages);
                }
            }
        }

        protected override void SolvePartOne()
        {
            foreach (int[] page in Pages)
            {
                bool pageIsValid = true;
                for (int i = 0; i < page.Length; i++)
                {
                    int currentPage = page[i];
                    for (int j = i + 1; j < page.Length; j++)
                    {
                        int checkedPage = page[j];
                        if (PageRules.TryGetValue(checkedPage, out var rules))
                        {
                            if (rules.Contains(currentPage))
                            {
                                pageIsValid = false;
                            }
                        }
                    }
                }

                if (pageIsValid)
                {
                    int middleIndex = page.Length / 2;
                    int middleItem = page[middleIndex];
                    Solution1 += middleItem;
                }
                else
                {
                    InvalidPages.Add(page);
                }
            }
        }

        protected override void SolvePartTwo()
        {
            foreach (int[] page in InvalidPages)
            {
                while (!PageChecker(page, out int firstIndex, out int secondIndex))
                {
                    int temp = page[firstIndex];
                    page[firstIndex] = page[secondIndex];
                    page[secondIndex] = temp;
                }

                int middleIndex = page.Length / 2;
                int middleItem = page[middleIndex];
                Solution2 += middleItem;
            }
        }

        private bool PageChecker(int[] page, out int firstIndex, out int secondIndex)
        {
            firstIndex = 0;
            secondIndex = 0;

            for (int i = 0; i < page.Length; i++)
            {
                int currentPage = page[i];
                for (int j = i + 1; j < page.Length; j++)
                {
                    int checkedPage = page[j];
                    if (PageRules.TryGetValue(checkedPage, out var rules))
                    {
                        if (rules.Contains(currentPage))
                        {
                            firstIndex = i;
                            secondIndex = j;
                            return false;
                        }
                    }
                }
            }

            return true;
        }
    }
}
