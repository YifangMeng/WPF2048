using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingProjectLab_2048
{
    class BlockManager
    {
        public static BlockManager blockManager = new BlockManager();
        public static BlockManager GetManager()
        {
            return blockManager;
        }

        public static int CountEmptyBlocks(Block[,] blks)
        {
            int count = 0;
            for (int row = 0; row < 4; row++)
                for (int col = 0; col < 4; col++)
                    if (blks[row, col].num == 0)
                        count++;
            return count;
        }

        public static void GenerateABlock(ref Block[,] blks)
        {
            int k = BlockManager.CountEmptyBlocks(blks);
            var ran = new Random();
            int RanPos = ran.Next(0, k);
            int RanNum = ran.Next(1, 2) * 2;

            int temp_count = -1;
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (blks[row, col].num == 0)
                    {
                        temp_count++;
                        if (temp_count == RanPos)
                        {
                            blks[row, col].num = RanNum;
                            blks[row, col].NewBlock = true;
                        }
                    }
                }
            }
        }
        public static void InitBlocks(ref Block[,] blks)
        {
            for (int row = 0; row < 4; row++)
                for (int col = 0; col < 4; col++)
                    blks[row, col] = new Block();
        }
        public static void InitNewGameBlocks(ref Block[,] blks)
        {
            InitBlocks(ref blks);
            GenerateABlock(ref blks);
            GenerateABlock(ref blks);
        }
        public static void Copy(ref Block[,] destination, ref Block[,] source)
        {
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    destination[row, col].num = source[row, col].num;
                    destination[row, col].Combined = source[row, col].Combined;
                    destination[row, col].NewBlock = source[row, col].NewBlock;
                }
            }
        }

        //get the indices of changed blocks
        public static List<int> CheckChangedBlocks(ref List<int> list, ref int score)  
        {
            //list is used to store the value of each block
            //changedList is used to store the changed indices
            List<int> changedList = new List<int>();
            int begin = 0, end = 1;
            while (end <= 3 && begin <= 2)
            {
                //0xxx
                if (list[begin] == 0)
                {
                    begin++;
                    end = begin + 1;
                }
                //x0xx
                else if (list[end] == 0)
                {
                    end++;
                }
                //two blocks with same value can be merged
                else
                {
                    if (list[begin] == list[end]) 
                    {
                        changedList.Add(begin);
                        list[begin] = list[begin] * 2;
                        list[end] = 0;
                        score = score + list[begin];
                        begin++;
                        end = begin + 1;
                    }
                    else
                    {
                        begin++;
                        end = begin + 1;
                    }
                }
            }

            return changedList;
        }
        public static bool MoveUp()
        {
            return true;

        }
        public static bool MoveDown()
        {
            return true;

        }
        public static bool MoveLeft()
        {
            return true;

        }
        public static bool MoveRight()
        {
            return true;

        }




    }
}
