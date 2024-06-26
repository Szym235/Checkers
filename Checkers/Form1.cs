using System.Drawing;
using System.Windows.Forms;

namespace Checkers
{
    public partial class Form1 : Form
    {
        bool isBlueTurn = true;

        PictureBox[,] boardArray = new PictureBox[8, 8];
        int choosedPawnYOnArray;
        int choosedPawnXOnArray;
        bool attacking = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void nextTurn(object sender, System.EventArgs e)
        {
            isBlueTurn = !isBlueTurn;
            pictureBoxNextTurn.Visible = false;
            labelNextTurn.Visible = false;
            attacking = false;
            hideMarks();
            if (isBlueTurn)
            {
                pictureBoxTurnInfo.BackColor = Color.Blue;
                labelTurnColor.BackColor = Color.Blue;
                labelTurnColor.Text = "BLUE";
                labelTurn.BackColor = Color.Blue;
            }
            else
            {
                pictureBoxTurnInfo.BackColor = Color.Red;
                labelTurnColor.BackColor = Color.Red;
                labelTurnColor.Text = "RED";
                labelTurn.BackColor = Color.Red;
            }

        }

        private void countPawns()
        {
            int counterRed = 0;
            int counterBlue = 0;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (boardArray[i, j] != null)
                    {
                        if (boardArray[i, j].BackColor == Color.Red) counterRed++;
                        else if (boardArray[i, j].BackColor == Color.Blue) counterBlue++;
                    }
                }
            }

            labelRedPlayerCount.Text = counterRed.ToString();
            labelBluePlayerCount.Text = counterBlue.ToString();

            if(counterRed == 0)
            {
                pictureBoxMenuBackground.Visible = true;
                pictureBoxMenuBackground.BringToFront();
                labelMenuTitle.Text = "BLUE WON";
                labelMenuTitle.ForeColor = Color.Blue;
                labelMenuTitle.Visible = true;
                labelMenuTitle.BringToFront();
                buttonTitlePlay.Text = "Play again";
                buttonTitlePlay.Visible = true;
                buttonTitlePlay.BringToFront();
            }
            else if (counterBlue == 0)
            {
                pictureBoxMenuBackground.Visible = true;
                pictureBoxMenuBackground.BringToFront();
                labelMenuTitle.Text = "RED WON";
                labelMenuTitle.ForeColor = Color.Red;
                labelMenuTitle.Visible = true;
                labelMenuTitle.BringToFront();
                buttonTitlePlay.Text = "Play again";
                buttonTitlePlay.Visible = true;
                buttonTitlePlay.BringToFront();
            }
        }

        private void hideMarks()
        {
            for (int i = 0; i < 8;i++)
            {
                for(int j = 0; j < 8;j++)
                {
                    if (boardArray[i, j] != null && boardArray[i, j].Name == "mark")
                    {
                        this.Controls.Remove(boardArray[i, j]);
                        boardArray[i, j] = null;  
                    }
                }
            }
        }

        private void buttonTitlePlay_Click(object sender, System.EventArgs e)
        {
            buttonTitlePlay.Visible = false;
            labelMenuTitle.Visible = false;
            pictureBoxMenuBackground.Visible = false;

            for(int i = 0; i < 8; i++)
            {
                for(int j = 0;j < 8;j++)
                {
                    this.Controls.Remove(boardArray[i, j]);
                    boardArray[i, j] = null;
                }
            }

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if ((i + j) % 2 == 0)
                    {
                        boardArray[i, j] = new PictureBox()
                        {
                            Name = "" + i + j,
                            Image = Checkers.Properties.Resources.pawnRed,
                            SizeMode = PictureBoxSizeMode.StretchImage,
                            Size = new Size(61, 61),
                            Location = new Point(square1.Location.X + 7 + i * 81, square1.Location.Y + 7 + j * 81),
                            BackColor = Color.Red,
                        };
                        boardArray[i, j].Click += pawnClick;
                        this.Controls.Add(boardArray[i, j]);
                        boardArray[i, j].BringToFront();
                    }
                }
            }
            for (int i = 0; i < 8; i++)
            {
                for (int j = 5; j < 8; j++)
                {
                    if ((i + j) % 2 == 0)
                    {
                        boardArray[i, j] = new PictureBox()
                        {
                            Name = "" + i + j,
                            Image = Checkers.Properties.Resources.pawnBlue,
                            SizeMode = PictureBoxSizeMode.StretchImage,
                            Size = new Size(61, 61),
                            Location = new Point(square1.Location.X + 7 + i * 81, square1.Location.Y + 7 + j * 81),
                            BackColor = Color.Blue,
                        };
                        boardArray[i, j].Click += pawnClick;
                        this.Controls.Add(boardArray[i, j]);
                        boardArray[i, j].BringToFront();
                    }
                }
            }
            countPawns();
        }
    }
}
