using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace Answers.Solutions
{
    internal class Day6 : Solution
    {
        protected override int DayNum => 6;

        char[,]? AreaMatrix = null;

        int StartPosX = 0;
        int StartPosY = 0;

        int CurrentPosX = 0;
        int CurrentPosY = 0;

        readonly int[,] Dirs = new int[,] 
        { 
            { 0, -1 }, 
            { 1, 0 }, 
            { 0, 1 }, 
            { -1, 0 } 
        };

        int StartDir = 0;
        int CurrentDir = 0;

        List<int[]> WalkedPath = new List<int[]>();

        protected override void Read()
        {
            string[] lines = File.ReadAllLines("../../../Data/day6.txt");

            AreaMatrix = new char[lines.Length, lines[0].Length];

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    AreaMatrix[i, j] = lines[i][j];
                    if (lines[i][j] == '^')
                    {
                        StartPosX = j;
                        StartPosY = i;
                    }
                }
            }
        }

        protected override void SolvePartOne()
        {
            bool isTurn = false;
            Reset();
            do
            {
                if (!isTurn)
                {
                    if (!IsCurrentPosDup())
                    {
                        WalkedPath.Add([CurrentPosX, CurrentPosY]);
                    }
                }
            }
            while (!Step(out isTurn));

            Solution1 = WalkedPath.Count;
        }

        protected override void SolvePartTwo()
        {
            int stepLimit = 10000;

            foreach (int[] point in WalkedPath)
            {
                int stepCount = 0;
                Reset();

                int newObstructionX = point[0];
                int newObstructionY = point[1];

                char oldChar = AreaMatrix![newObstructionY, newObstructionX];
                AreaMatrix![newObstructionY, newObstructionX] = '#';

                while (!Step(out _) && stepCount < stepLimit)
                {
                    stepCount++;
                }

                if (stepCount == stepLimit)
                {
                    Solution2++;
                }

                AreaMatrix![newObstructionY, newObstructionX] = oldChar;
            }
        }

        private bool Step(out bool isTurn)
        {
            isTurn = false;

            int xDir = Dirs[CurrentDir, 0];
            int yDir = Dirs[CurrentDir, 1];

            int newCurrentPosX = CurrentPosX + xDir;
            int newCurrentPosY = CurrentPosY + yDir;

            if (newCurrentPosX < 0 || newCurrentPosX > AreaMatrix!.GetLength(1) - 1)
            {
                return true;
            }

            if (newCurrentPosY < 0 || newCurrentPosY > AreaMatrix!.GetLength(0) - 1)
            {
                return true;
            }

            char nextField = AreaMatrix![newCurrentPosY, newCurrentPosX];

            if (nextField == '#')
            {
                Turn();
                isTurn = true;
                return false;
            }

            CurrentPosX = newCurrentPosX;
            CurrentPosY = newCurrentPosY;

            return false;
        }

        private void Turn()
        {
            CurrentDir++;
            CurrentDir %= 4;
        }

        private bool IsCurrentPosDup()
        {
            foreach (int[] point in WalkedPath)
            {
                if (point[0] == CurrentPosX && point[1] == CurrentPosY)
                {
                    return true;
                }
            }

            return false;
        }

        private void Reset()
        {
            CurrentPosX = StartPosX;
            CurrentPosY = StartPosY;
            CurrentDir = StartDir;
        }
    }
}
