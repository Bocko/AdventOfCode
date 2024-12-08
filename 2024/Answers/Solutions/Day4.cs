using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Answers.Solutions
{
    internal class Day4 : Solution
    {
        public override int DayNum => 4;

        char[,]? XmasMatrix = null;

        readonly char[] Xmas = ['X', 'M', 'A', 'S'];

        protected override void Read()
        {
            string[] lines = File.ReadAllLines("../../../Data/day4.txt");

            XmasMatrix = new char[lines.Length, lines[0].Length];

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    XmasMatrix[i, j] = lines[i][j];
                }
            }
        }

        protected override void SolvePartOne()
        {
            CheckHorizontal();
            CheckVertical();
            CheckDiagonal();
        }

        private void CheckHorizontal()
        {
            for (int y = 0; y < XmasMatrix!.GetLength(0); y++)
            {
                char[] row = new char[XmasMatrix.GetLength(1)];

                for (int x = 0; x < XmasMatrix!.GetLength(1); x++)
                {
                    row[x] = XmasMatrix[y, x];
                }

                Solution1 += CheckRow(row);
            }
        }

        private void CheckVertical()
        {
            for (int x = 0; x < XmasMatrix!.GetLength(1); x++)
            {
                char[] row = new char[XmasMatrix.GetLength(0)];

                for (int y = 0; y < XmasMatrix!.GetLength(0); y++)
                {
                    row[y] = XmasMatrix[y, x];
                }

                Solution1 += CheckRow(row);
            }
        }

        private void CheckDiagonal()
        {
            // top right -> bottom left
            int max = XmasMatrix!.GetLength(0);
            for (int i = -max + 1; Math.Abs(i) < max; i++)
            {
                char[] charRow = new char[max - Math.Abs(i)];
                for (int j = 0; j <= max - Math.Abs(i) - 1; j++)
                {
                    int col = i > 0 ? j : (Math.Abs(i) + j);
                    int row = i < 0 ? j : i + j;
                    charRow[j] = XmasMatrix[row, col];
                }

                Solution1 += CheckRow(charRow);
            }

            // top left -> bottom right

            for (int i = 0; i < max * 2; i++)
            {
                char[] charRow = new char[i + 1];
                for (int j = 0; j <= i; j++)
                {
                    int row = j;
                    int col = i - j;
                    if (col < max && row < max)
                    {
                        charRow[j] = XmasMatrix[row, col];
                    }
                }

                Solution1 += CheckRow(charRow);
            }
        }

        private int CheckRow(char[] row)
        {
            if (row.Length < 4)
            {
                return 0;
            }

            int numberOfXmas = 0;

            int nextForwardIndex = 0;
            int nextReverseIndex = 3;

            for (int i = 0; i < row.Length; i++)
            {
                char currentChar = row[i];
                if (currentChar == Xmas[nextForwardIndex])
                {
                    if (nextForwardIndex < 3)
                    {
                        nextForwardIndex++;
                    }
                    else
                    {
                        numberOfXmas++;
                        nextForwardIndex = 0;
                    }
                }
                else
                {
                    nextForwardIndex = 0;

                    if (currentChar == Xmas[nextForwardIndex])
                    {
                        nextForwardIndex++;
                    }
                }

                if (currentChar == Xmas[nextReverseIndex])
                {
                    if (nextReverseIndex > 0)
                    {
                        nextReverseIndex--;
                    }
                    else
                    {
                        numberOfXmas++;
                        nextReverseIndex = 3;
                    }
                }
                else
                {
                    nextReverseIndex = 3;

                    if (currentChar == Xmas[nextReverseIndex])
                    {
                        nextReverseIndex--;
                    }
                }
            }

            return numberOfXmas;
        }

        protected override void SolvePartTwo()
        {
            for (int x = 0; x < XmasMatrix!.GetLength(0); x++)
            {
                for (int y = 0; y < XmasMatrix!.GetLength(1); y++)
                {
                    char currentChar = XmasMatrix![x, y];

                    if (currentChar != 'A')
                    {
                        continue;
                    }

                    char upperLeft = '_';
                    char upperRight = '_';
                    char lowerLeft = '_';
                    char lowerRight = '_';

                    int newX = x - 1;
                    int newY = y - 1;
                    if (IsValidIndex(newX, newY, XmasMatrix.GetLength(0)))
                    {
                        upperLeft = XmasMatrix[newX, newY];
                    }
                    else
                    {
                        continue;
                    }

                    newX = x - 1;
                    newY = y + 1;
                    if (IsValidIndex(newX, newY, XmasMatrix.GetLength(0)))
                    {
                        upperRight = XmasMatrix[newX, newY];
                    }
                    else
                    {
                        continue;
                    }

                    newX = x + 1;
                    newY = y - 1;
                    if (IsValidIndex(newX, newY, XmasMatrix.GetLength(0)))
                    {
                        lowerLeft = XmasMatrix[newX, newY];
                    }
                    else
                    {
                        continue;
                    }

                    newX = x + 1;
                    newY = y + 1;
                    if (IsValidIndex(newX, newY, XmasMatrix.GetLength(0)))
                    {
                        lowerRight = XmasMatrix[newX, newY];
                    }
                    else
                    {
                        continue;
                    }

                    if (IsValidCharacter(upperLeft) && IsValidCharacter(upperRight) && IsValidCharacter(lowerLeft) && IsValidCharacter(lowerRight))
                    {
                        if (upperLeft != lowerRight && upperRight != lowerLeft)
                        {
                            Solution2++;
                        }
                    }
                }
            }
        }

        private bool IsValidIndex(int x, int y, int max)
        {
            return x > -1 && y > -1 && x < max && y < max;
        }

        private bool IsValidCharacter(char currentChar)
        {
            return currentChar == 'M' || currentChar == 'S';
        }
    }
}
