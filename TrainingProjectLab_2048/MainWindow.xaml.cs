using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TrainingProjectLab_2048
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int score, PrevScore;
        Block[,] blks = new Block[4, 4];
        Block[,] OldBlks = new Block[4, 4];
        Button[,] Btns = new Button[4, 4];
        DoubleAnimation doubleAnimation;
        Storyboard storyboard;

        public MainWindow()
        {
            InitializeComponent();
            doubleAnimation = new DoubleAnimation();
            //transition of font size
            doubleAnimation.From = 88;
            doubleAnimation.To = 40;
            doubleAnimation.Duration = TimeSpan.FromMilliseconds(280);
            storyboard = new Storyboard();
            storyboard.Children.Add(doubleAnimation);
            NewGame();
        }

        public void NewGame()
        {
            score = 0;
            BlockManager.InitNewGameBlocks(ref blks);
            BlockManager.InitBlocks(ref OldBlks);
            BlockManager.Copy(ref OldBlks, ref blks);
            Score.Text = score.ToString();
            RenderBoard();

        }
        public void BackGame()
        {
            score = PrevScore;
            BlockManager.Copy(ref blks, ref OldBlks);
            Score.Text = score.ToString();
            RenderBoard();
        }
        public void RenderBoard()
        {
            //clear current board
        }
        public bool GameOver(Block[,] blks)
        {
            //along the same row, two blocks own the same value
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (blks[row, col].num == blks[row, col + 1].num)
                        return false;
                }
            }
            //along the same column, two rows own the same value
            for (int col = 0; col < 4; col++)
            {
                for (int row = 0; row < 3; row++)
                {
                    if (blks[row + 1, col].num == blks[row, col].num)
                        return false;
                }
            }
            //there are empty blocks left
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (blks[row, col].num == 0)
                        return false;
                }
            }
            return true;
        }

        public void MoveUp()
        {
            PrevScore = score;
            if (BlockManager.MoveUp(ref blks, ref OldBlks, ref score) == true)  
            {
                BlockManager.GenerateABlock(ref blks);
                RenderBoard();
                Score.Text = score.ToString();
            }
        }
        public void MoveDown()
        {
            PrevScore = score;
            if (BlockManager.MoveDown(ref blks, ref OldBlks, ref score) == true) 
            {
                BlockManager.GenerateABlock(ref blks);
                RenderBoard();
                Score.Text = score.ToString();
            }
        }
        public void MoveLeft()
        {
            PrevScore = score;
            if (BlockManager.MoveLeft(ref blks, ref OldBlks, ref score) == true) 
            {
                BlockManager.GenerateABlock(ref blks);
                RenderBoard();
                Score.Text = score.ToString();
            }
        }
        public void MoveRight()
        {
            PrevScore = score;
            if (BlockManager.MoveRight(ref blks, ref OldBlks, ref score) == true)
            {
                BlockManager.GenerateABlock(ref blks);
                RenderBoard();
                Score.Text = score.ToString();
            }
        }
        

        private void New_Click(object sender, RoutedEventArgs e)
        {
            //todo
            NewGame();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    MoveUp();
                    break;
                case Key.Down:
                    MoveDown();
                    break;
                case Key.Right:
                    MoveRight();
                    break;
                case Key.Left:
                    MoveLeft();
                    break;
                default:
                    break;
            }
            if (GameOver(blks))
            {
                MessageBox.Show("Game over! Score: " + score.ToString(), "Notification");
            }

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            //todo
            BackGame();
        }
    }
}
