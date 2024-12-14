using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Point = (int x, int y);

namespace Answers.Solutions
{
    internal class Day14 : Solution
    {
        public override int DayNum => 14;

        const int Width = 101;
        const int Height = 103;

        struct Robot
        {
            public Point Pos;
            public Point Vel;

            public void Step()
            {
                int newXPos = Pos.x + Vel.x;
                int newYPos = Pos.y + Vel.y;

                if (newXPos < 0)
                {
                    newXPos = Width + newXPos;
                }

                if (newYPos < 0)
                {
                    newYPos = Height + newYPos;
                }

                if (newXPos > Width - 1)
                {
                    newXPos -= Width;
                }

                if (newYPos > Height - 1)
                {
                    newYPos -= Height;
                }

                Pos.x = newXPos;
                Pos.y = newYPos;
            }
        }

        List<Robot> RobotsPartOne = new List<Robot>();
        List<Robot> RobotsPartTwo = new List<Robot>();

        protected override void Read()
        {
            string[] lines = File.ReadAllLines("../../../Data/day14.txt");

            foreach (string line in lines)
            {
                int velStart = line.IndexOf(" v=");

                string[] position = line.AsSpan(2, velStart - 2).ToString().Split(',');
                string[] velocity = line.AsSpan(velStart + 3, line.Length - (velStart + 3)).ToString().Split(',');

                RobotsPartOne.Add(new Robot
                {
                    Pos = (int.Parse(position[0]), int.Parse(position[1])),
                    Vel = (int.Parse(velocity[0]), int.Parse(velocity[1]))
                });

                RobotsPartTwo.Add(new Robot
                {
                    Pos = (int.Parse(position[0]), int.Parse(position[1])),
                    Vel = (int.Parse(velocity[0]), int.Parse(velocity[1]))
                });
            }
        }

        protected override void SolvePartOne()
        {
            for (int seconds = 1; seconds <= 100; seconds++)
            {
                for (int i = 0; i< RobotsPartOne.Count; i++)
                {
                    Robot robot = RobotsPartOne[i];
                    robot.Step();
                    RobotsPartOne[i] = robot;
                }
            }

            int quad1Count = 0; // top left
            int quad2Count = 0; // top right
            int quad3Count = 0; // bottom left
            int quad4Count = 0; // bottom right

            foreach (Robot robot in RobotsPartOne)
            {
                if (robot.Pos.y < Height / 2)
                {
                    if (robot.Pos.x < Width / 2)
                    {
                        quad1Count++;
                    }
                    else if (robot.Pos.x > Width / 2)
                    {
                        quad2Count++;
                    }
                }
                else if (robot.Pos.y > Height / 2)
                {
                    if (robot.Pos.x < Width / 2)
                    {
                        quad3Count++;
                    }
                    else if (robot.Pos.x > Width / 2)
                    {
                        quad4Count++;
                    }
                }
            }

            Solution1 = quad1Count * quad2Count * quad3Count * quad4Count;
        }

        protected override void SolvePartTwo()
        {
            StringBuilder output = new StringBuilder();

            for (int seconds = 101; seconds < 8150; seconds++)
            {
                char[,] map = new char[Height, Width];

                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        map[y, x] = '.';
                    }
                }

                for (int i = 0; i < RobotsPartOne.Count; i++)
                {
                    Robot robot = RobotsPartOne[i];
                    robot.Step();
                    RobotsPartOne[i] = robot;
                    map[robot.Pos.y, robot.Pos.x] = 'X';
                }

                output.AppendLine($"SECONDS: {seconds}");

                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        output.Append(map[y,x]);
                    }
                    output.AppendLine();
                }
            }

            // WARNING! This will generate a 80MB+ file (xd)
            File.WriteAllText("../../../Data/day14part2solution.txt", output.ToString());
        }
    }
}
