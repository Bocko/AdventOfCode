using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Answers.Solutions
{
    internal class Day2 : Solution
    {
        public override int DayNum => 2;

        readonly List<int[]> Reports = new List<int[]>();
        readonly List<int[]> FailedReports = new List<int[]>();

        protected override void Read()
        {
            string[] rows = File.ReadAllLines("../../../Data/day2.txt");
            foreach (string row in rows)
            {
                int[] newReport = row.Split(' ').Select(int.Parse).ToArray();
                Reports.Add(newReport);
            }
        }

        protected override void SolvePartOne()
        {
            foreach (int[] report in Reports)
            {
                if (ReportChecker(report))
                {
                    Solution1++;
                }
                else
                {
                    FailedReports.Add(report);
                }
            }
        }

        protected override void SolvePartTwo()
        {
            Solution2 += Solution1;

            foreach (int[] report in FailedReports)
            {
                for (int i = 0; i < report.Length; i++)
                {
                    int[] splitReport = new int[report.Length - 1];
                    int leftStart = 0;
                    int leftEnd = i;
                    int rightStart = i + 1;
                    int rightEnd = report.Length;

                    int[] left = report.AsSpan(new Range(leftStart, leftEnd)).ToArray();
                    Array.Copy(left, splitReport, left.Length);
                    int[] right = report.AsSpan(new Range(rightStart, rightEnd)).ToArray();
                    Array.Copy(right, 0, splitReport, left.Length, right.Length);

                    if (ReportChecker(splitReport))
                    {
                        Solution2++;
                        break;
                    }
                }
            }
        }

        private bool ReportChecker(int[] report)
        {
            bool isIncreasing = false;
            bool safe = true;
            for (int leadingIndex = 0; leadingIndex < report.Length - 1; leadingIndex++)
            {
                int trailingIndex = leadingIndex + 1;
                int leadingValue = report[leadingIndex];
                int trailingValue = report[trailingIndex];
                int diff = leadingValue - trailingValue;

                if (diff == 0 || diff > 3 || diff < -3)
                {
                    safe = false;
                    break;
                }

                if (leadingIndex == 0)
                {
                    isIncreasing = diff > 0;
                }

                if (isIncreasing != IsIncreasing(diff))
                {
                    safe = false;
                    break;
                }
            }

            return safe;
        }

        private bool IsIncreasing(int diff)
        {
            return diff > 0;
        }
    }
}
