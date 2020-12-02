using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingProjectLab_2048
{
    public class BlockManager
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
            int numEmptyBlocks = BlockManager.CountEmptyBlocks(blks);
            var ran = new Random();
            int position = ran.Next(0, numEmptyBlocks);
            //generated value is 2 or 4
            int value = ran.Next(1, 2) * 2;

            int i = -1;
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (blks[row, col].num == 0)
                    {
                        i++;
                        //generate only one new block each time
                        if (i == position)
                        {
                            blks[row, col].num = value;
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
                        //add the position just combined
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
            int emptyBlockIndex = -1;
            for (int i = 0; i < 4; i++)
            {
                if (list[i] == 0)
                {
                    if (emptyBlockIndex == -1)
                        emptyBlockIndex = i;
                }
                else
                {
                    if (emptyBlockIndex != -1)
                    {
                        if (changedList.Contains(i))
                        {
                            changedList[changedList.IndexOf(i)] = emptyBlockIndex;
                        }
                        list[emptyBlockIndex] = list[i];
                        list[i] = 0;
                        emptyBlockIndex = -1;
                        i = emptyBlockIndex + 1;
                        changedList.Add(-1);
                    }
                }
            }

            return changedList;
        }
        public static bool MoveUp(ref Block[,] blks, ref Block[,] OldBlocks, ref int score)
        {
            Block[,] temp = new Block[4, 4];
            InitBlocks(ref temp);
            Copy(ref temp, ref blks);

            bool BlocksChanged = false;
            //on vertical direction, each column forms a list
            for (int col = 0; col < 4; col++)
            {
                //each column forms a list-ls
                List<int> ls = new List<int>();
                for (int row = 0; row < 4; row++)
                {
                    ls.Add(blks[row, col].num);
                    blks[row, col].NewBlock = false;
                    blks[row, col].Combined = false;  
                }
                List<int> ChangedList = CheckChangedBlocks(ref ls, ref score);
                if (ChangedList.Count != 0) 
                {
                    BlocksChanged = true;
                    int l_ls = ChangedList.Count;
                    for (int i = 0; i < l_ls; i++)
                    {
                        if (ChangedList[i] != -1)
                            blks[ChangedList[i], col].Combined = true;
                    }
                    int ls_k = 0;
                    for (int row = 0; row < 4; row++) 
                    {
                        blks[row, col].num = ls[ls_k];
                        ls_k++;
                    }
                    ls.Clear();
                }
            }

            if (BlocksChanged == true)
            {
                Copy(ref OldBlocks, ref temp);
            }
            return BlocksChanged;

        }
        public static bool MoveDown(ref Block[,] blks, ref Block[,] OldBlocks, ref int score)
        {
            Block[,] temp = new Block[4, 4];
            InitBlocks(ref temp);
            Copy(ref temp, ref blks);

            bool BlocksChanged = false;
            //on vertical direction, each column forms a list
            for (int col = 0; col < 4; col++)
            {
                //each column forms a list-ls
                List<int> ls = new List<int>();
                for (int row = 3; row >= 0; row--)
                {
                    ls.Add(blks[row, col].num);
                    blks[row, col].NewBlock = false;
                    blks[row, col].Combined = false; 
                }
                List<int> ChangedList = CheckChangedBlocks(ref ls, ref score);
                if (ChangedList.Count != 0) 
                {
                    BlocksChanged = true;
                    int l_ls = ChangedList.Count;
                    for (int i = 0; i < l_ls; i++)
                    {
                        if (ChangedList[i] != -1)
                            blks[3 - ChangedList[i], col].Combined = true;
                    }
                    int ls_k = 0;
                    for (int row = 3; row >= 0; row--)
                    {
                        blks[row, col].num = ls[ls_k];
                        ls_k++;
                    }
                }
                ls.Clear();
            }

            if (BlocksChanged == true)
            {
                Copy(ref OldBlocks, ref temp);
            }
            return BlocksChanged;
        }
        public static bool MoveLeft(ref Block[,] blks, ref Block[,] OldBlocks, ref int score)
        {
            Block[,] temp = new Block[4, 4];
            InitBlocks(ref temp);
            Copy(ref temp, ref blks);

            bool BlocksChanged = false;
            //on horizontal direction, each row forms a list
            for (int row = 0; row < 4; row++)
            {
                //each row forms a list-ls
                List<int> ls = new List<int>();
                for (int col = 0; col < 4; col++)
                {
                    ls.Add(blks[row, col].num);
                    blks[row, col].NewBlock = false;
                    blks[row, col].Combined = false;
                }
                List<int> ChangedList = CheckChangedBlocks(ref ls, ref score);
                if (ChangedList.Count != 0) 
                {
                    BlocksChanged = true;
                    int l_ls = ChangedList.Count;
                    for (int i = 0; i < l_ls; i++)
                    {
                        if (ChangedList[i] != -1)
                            blks[row, ChangedList[i]].Combined = true;
                    }
                    int ls_k = 0;
                    for (int col = 0; col < 4; col++)
                    {
                        blks[row, col].num = ls[ls_k];
                        ls_k++;
                    }
                    ls.Clear();
                }
            }

            if (BlocksChanged == true)
            {
                Copy(ref OldBlocks, ref temp);
            }
            return BlocksChanged;
        }
        public static bool MoveRight(ref Block[,] blks, ref Block[,] OldBlocks, ref int score)
        {
            Block[,] temp = new Block[4, 4];
            InitBlocks(ref temp);
            Copy(ref temp, ref blks);

            bool BlocksChanged = false;
            //on horizontal direction, each row forms a list
            for (int row = 0; row < 4; row++)
            {
                //each row forms a list-ls
                List<int> ls = new List<int>();
                for (int col = 3; col >= 0; col--)
                {
                    ls.Add(blks[row, col].num);
                    blks[row, col].NewBlock = false;
                    blks[row, col].Combined = false;
                }
                List<int> ChangedList = CheckChangedBlocks(ref ls, ref score);
                if (ChangedList.Count != 0) 
                {
                    BlocksChanged = true;
                    int num = ChangedList.Count;
                    for (int i = 0; i < num; i++)
                    {
                        if (ChangedList[i] != -1)
                            blks[row, 3 - ChangedList[i]].Combined = true;
                    }
                    int j = 0;
                    for (int col = 3; col >= 0; col--)
                    {
                        blks[row, col].num = ls[j];
                        j++;
                    }
                    ls.Clear();
                }
            }

            if (BlocksChanged == true)
            {
                Copy(ref OldBlocks, ref temp);
            }
            return BlocksChanged;
        }




    }
}
