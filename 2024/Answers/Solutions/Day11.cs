using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Answers.Solutions
{
    internal class Day11 : Solution
    {
        public override int DayNum => 11;

        Dictionary<long, long> StonesPartOne = new Dictionary<long, long>();
        Dictionary<long, long> StonesPartTwo = new Dictionary<long, long>();

        protected override void Read()
        {
            string lines = File.ReadAllText("../../../Data/day11.txt");
            foreach (string stone in lines.Split(' '))
            {
                StonesPartOne.TryAdd(long.Parse(stone), 1);
                StonesPartTwo.TryAdd(long.Parse(stone), 1);
            }
        }

        protected override void SolvePartOne()
        {
            for (int i = 0; i < 25; i++)
            {
                Dictionary<long, long> NewStones = new Dictionary<long, long>();

                foreach (var (stone, count) in StonesPartOne)
                {
                    Blink(stone, count, NewStones);
                }

                StonesPartOne = NewStones;
            }

            Solution1 = StonesPartOne.Values.Sum();
        }

        protected override void SolvePartTwo() 
        {
            for (int i = 0; i < 75; i++)
            {
                Dictionary<long, long> NewStones = new Dictionary<long, long>();

                foreach (var (stone, count) in StonesPartTwo)
                {
                    Blink(stone, count, NewStones);
                }

                StonesPartTwo = NewStones;
            }

            Solution2 = StonesPartTwo.Values.Sum();
        }

        private void Blink(long stone, long count, Dictionary<long, long> newStones)
        {
            string number = stone.ToString();
            if (stone == 0)
            {
                newStones.TryAdd(1, 0);
                newStones[1] += count;
            }
            else if (number.Length % 2 == 0)
            {
                int numberHalfPoint = number.Length / 2;
                long stone1 = long.Parse(number.AsSpan(new Range(0, numberHalfPoint)));
                long stone2 = long.Parse(number.AsSpan(new Range(numberHalfPoint, number.Length)));

                newStones.TryAdd(stone1, 0);
                newStones[stone1] += count;
                newStones.TryAdd(stone2, 0);
                newStones[stone2] += count;
            }
            else
            {
                long newStone = stone * 2024;
                newStones.TryAdd(newStone, 0);
                newStones[newStone] += count;
            }
        }
    }
}
