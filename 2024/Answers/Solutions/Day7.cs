using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Answers.Solutions
{
    internal class Day7 : Solution
    {
        protected override int DayNum => 7;

        private enum Operators
        {
            Add,
            Multiply,
            Concat
        }

        private struct Calibration
        {
            public long TestValue;
            public long[] SubValues;
        }

        private List<Calibration> Calibrations = new List<Calibration>();

        static readonly Operators[] PartOneOperators = [Operators.Add, Operators.Multiply];
        const int PartOneOperatorCount = 2;
        static readonly Operators[] PartTwoOperators = [Operators.Add, Operators.Multiply, Operators.Concat];
        const int PartTwoOperatorCount = 3;

        protected override void Read()
        {
            string[] lines = File.ReadAllLines("../../../Data/day7.txt");
            foreach (string line in lines)
            {
                string[] splitLine = line.Split(':');
                long testValue = long.Parse(splitLine[0]);
                long[] subValues = splitLine[1].Trim().Split(' ').Select(l => long.Parse(l)).ToArray();
                Calibrations.Add(new Calibration { TestValue = testValue, SubValues = subValues });
            }
        }

        protected override void SolvePartOne()
        {
            foreach (Calibration calibration in Calibrations)
            {
                int operationCount = calibration.SubValues.Length - 1;
                int combinationCount = (int)Math.Pow(PartOneOperatorCount, operationCount);

                Operators[,] operations = GenerateOperators(PartOneOperators, operationCount, combinationCount);

                if (CheckOperation(operations, calibration, combinationCount))
                {
                    Solution1 += calibration.TestValue;
                }
            }
        }

        protected override void SolvePartTwo()
        {
            foreach (Calibration calibration in Calibrations)
            {
                int operationCount = calibration.SubValues.Length - 1;
                int combinationCount = (int)Math.Pow(PartTwoOperatorCount, operationCount);

                Operators[,] operations = GenerateOperators(PartTwoOperators, operationCount, combinationCount);

                if (CheckOperation(operations, calibration, combinationCount))
                {
                    Solution2 += calibration.TestValue;
                }
            }
        }

        private Operators[,] GenerateOperators(Operators[] operators, int operationCount, long combinationCount)
        {
            Operators[,] operations = new Operators[combinationCount, operationCount];

            int nextOp = 0;

            for (int i = 0; i < combinationCount; i++)
            {
                string binary = GetBase(nextOp, operators.Length);

                for (int j = 0; j < operationCount; j++)
                {
                    if (j >= binary.Length)
                    {
                        operations[i, j] = operators[0];
                    }
                    else
                    {
                        operations[i, j] = operators[(int)char.GetNumericValue(binary, j)];
                    }
                }

                nextOp++;
            }

            return operations;
        }

        private string GetBase(int number, int baseNum)
        {
            StringBuilder result = new StringBuilder();

            string validChars = "0123456789ABCDEF";

            do
            {
                result.Append(validChars[number % baseNum]);
                number /= baseNum;
            }
            while (number > 0);

            return result.ToString();
        }

        private bool CheckOperation(Operators[,] operations, Calibration calibration, int combinationCount)
        {
            for (int i = 0; i < combinationCount; i++)
            {
                long value = 0;

                for (int j = 0; j < calibration.SubValues.Length - 1; j++)
                {
                    long partOne = 0;
                    long partTwo = 0;

                    if (value == 0)
                    {
                        partOne = calibration.SubValues[j];
                        partTwo = calibration.SubValues[j + 1];
                    }
                    else
                    {
                        partOne = value;
                        partTwo = calibration.SubValues[j + 1];
                    }

                    value = RunOperation(partOne, partTwo, operations[i, j]);
                }

                if (value == calibration.TestValue)
                {
                    return true;
                }
            }

            return false;
        }

        private long RunOperation(long partOne, long partTwo, Operators operation)
        {
            switch (operation)
            {
                case Operators.Add:
                {
                    return partOne + partTwo;
                }
                case Operators.Multiply:
                {
                    return partOne * partTwo;
                }
                case Operators.Concat:
                {
                    return long.Parse($"{partOne}{partTwo}");
                }
                default:
                {
                    return 0;
                }
            }
        }
    }
}
