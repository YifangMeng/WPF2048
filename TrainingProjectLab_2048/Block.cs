using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TrainingProjectLab_2048
{
    public class Block
    {
        public int num = 0;
        public bool Combined = false;
        public bool NewBlock = false;  
        //block colors 
        private static SolidColorBrush number2 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7fffd4"));
        private static SolidColorBrush number4 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7fff00"));
        private static SolidColorBrush number8 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#228b22"));
        private static SolidColorBrush number16 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8fbc8b"));
        private static SolidColorBrush number32 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#bdb76b"));
        private static SolidColorBrush number64 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f65e3b"));
        private static SolidColorBrush number128 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#edcf72"));
        private static SolidColorBrush number256 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#edcc61"));
        private static SolidColorBrush number512 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#adff2f"));
        private static SolidColorBrush number1024 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#20b2aa"));
        private static SolidColorBrush number2048 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#32cd32"));
        private static SolidColorBrush noNumber = new SolidColorBrush((Color)ColorConverter.ConvertFromString("LightGray"));

        public static SolidColorBrush GetNumberedBlocksColor(Block blk)
        {
            switch (blk.num)
            {
                case 2:
                    return number2;
                case 4:
                    return number4;
                case 8:
                    return number8;
                case 16:
                    return number16;
                case 32:
                    return number32;
                case 64:
                    return number64;
                case 128:
                    return number128;
                case 256:
                    return number256;
                case 512:
                    return number512;
                case 1024:
                    return number1024;
                case 2048:
                    return number2048;
                default:
                    return noNumber;

            }
        }
        public static SolidColorBrush GetNoNumberColor()
        {
            return noNumber;
        }

    }
}
