using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Answers.Solutions
{
    internal class Day9 : Solution
    {
        public override int DayNum => 9;

        private enum ItemType
        {
            File,
            FreeSpace
        }

        private struct DiskItem
        {
            public int Id;
            public ItemType Type;

            public override string ToString()
            {
                return Type == ItemType.File ? Id.ToString() : ".";
            }
        }

        private struct FileItem
        {
            public int StartIndex;
            public int Length;
        }

        readonly List<FileItem> FileItems = new List<FileItem>();

        readonly List<DiskItem> DiskItems = new List<DiskItem>();

        protected override void Read()
        {
            string line = File.ReadAllText("../../../Data/day9.txt");

            int id = 0;
            bool isFile = true;
            int expandedIndex = 0;

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];

                int count = c - '0';
                if (isFile)
                {
                    FileItems.Add(new FileItem() { StartIndex = expandedIndex, Length = count});

                    for (int j = 0; j < count; j++)
                    {
                        DiskItems.Add(new DiskItem() { Id = id, Type = ItemType.File });
                    }

                    id++;
                }
                else
                {
                    for (int j = 0; j < count; j++)
                    {
                        DiskItems.Add(new DiskItem() { Id = -1, Type = ItemType.FreeSpace });
                    }
                }

                expandedIndex += count;
                isFile = !isFile;
            }
        }

        protected override void SolvePartOne()
        {
            DiskItem[] diskItems = [.. DiskItems];

            int fileIndex = diskItems.Length - 1;

            for (int freeIndex = 0; freeIndex < diskItems.Length; freeIndex++)
            {
                if (fileIndex == freeIndex)
                {
                    break;
                }

                DiskItem freeSpaceItem = diskItems[freeIndex];
                if (freeSpaceItem.Type != ItemType.FreeSpace)
                {
                    continue;
                }

                bool fileFound = false;

                while (!fileFound)
                {
                    DiskItem fileItem = diskItems[fileIndex];
                    if (fileItem.Type != ItemType.File)
                    {
                        fileFound = false;
                        fileIndex--;
                        continue;
                    }

                    DiskItem temp = diskItems[freeIndex];
                    diskItems[freeIndex] = diskItems[fileIndex];
                    diskItems[fileIndex] = temp;

                    fileIndex--;
                    fileFound = true;
                }

            }

            for (int i = 0; i <= fileIndex; i++)
            {
                Solution1 += i * diskItems[i].Id;
            }
        }

        protected override void SolvePartTwo()
        {
            DiskItem[] diskItems = [.. DiskItems];

            for (int fileIndex = FileItems.Count - 1; fileIndex > -1; fileIndex--)
            {
                int fileLength = FileItems[fileIndex].Length;
                int startingIndex = FreeSpaceSearch(diskItems, fileLength);

                if (startingIndex < 0 || startingIndex >= FileItems[fileIndex].StartIndex)
                {
                    continue;
                }

                for (int index = 0; index < fileLength; index++)
                {
                    int freeSpaceIndex = startingIndex + index;
                    int currentFileIndex = FileItems[fileIndex].StartIndex + index;

                    DiskItem temp = diskItems[freeSpaceIndex];
                    diskItems[freeSpaceIndex] = diskItems[currentFileIndex];
                    diskItems[currentFileIndex] = temp;
                }
            }

            for (int i = 0; i < diskItems.Length; i++)
            {
                DiskItem diskItem = diskItems[i];
                if (diskItem.Type != ItemType.File)
                {
                    continue;
                }

                Solution2 += i * diskItem.Id;
            }
        }

        private int FreeSpaceSearch(DiskItem[] diskItems, int length)
        {
            int startingIndex = 0;

            int freeSpaceCount = 0;

            for (int i = 0; i < diskItems.Length; i++)
            {
                DiskItem item = diskItems[i];
                if (item.Type != ItemType.FreeSpace)
                {
                    freeSpaceCount = 0;
                    continue;
                }

                if (freeSpaceCount == 0)
                {
                    startingIndex = i;
                }

                freeSpaceCount++;

                if (freeSpaceCount == length)
                {
                    return startingIndex;
                }
            }

            return -1;
        }
    }
}
