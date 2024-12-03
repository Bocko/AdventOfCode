﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Answers.Solutions
{
    internal class Day3 : Solution
    {
        int Solution1 = 0;
        int Solution2 = 0;

        string Instructions = "";
        string MulPattern = @"mul\((?'first'\d{1,3}),(?'second'\d{1,3})\)";
        string OnOffPattern = @"mul\((?'first'\d{1,3}),(?'second'\d{1,3})\)|do\(\)|don't\(\)";

        protected override void Read()
        {
            Instructions = File.ReadAllText("../../../Data/day3.txt");
        }

        protected override void SolvePartOne()
        {
            foreach (Match m in Regex.Matches(Instructions, MulPattern))
            {
                int first = int.Parse(m.Groups["first"].Value);
                int second = int.Parse(m.Groups["second"].Value);
                Solution1 += first * second;
            }
        }

        protected override void SolvePartTwo()
        {
            bool enabled = true;

            foreach (Match m in Regex.Matches(Instructions, OnOffPattern))
            {
                Console.WriteLine(m.Value);

                if (m.Value.Equals("do()"))
                {
                    enabled = true;
                }
                else if (m.Value.Equals("don't()"))
                {
                    enabled = false;
                }
                else
                {
                    if (!enabled)
                    {
                        continue;
                    }

                    int first = int.Parse(m.Groups["first"].Value);
                    int second = int.Parse(m.Groups["second"].Value);
                    Solution2 += first * second;
                }
            }
        }

        protected override void Display()
        {
            PrintSolution(3, Solution1.ToString(), Solution2.ToString());
        }
    }
}