using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Checkers
{
    public partial class Form1 : Form
    {
        private void pawnClick(object sender, EventArgs e)
        {
            if (attacking) return;
            hideMarks();
            var pawn = sender as PictureBox;
            choosedPawnXOnArray = pawn.Name[0] - '0';
            choosedPawnYOnArray = pawn.Name[1] - '0';
            checkCanMove(sender, e);
            if (isBlueTurn && pawn.BackColor == Color.Blue) checkCanHit();
            else if (!isBlueTurn && pawn.BackColor == Color.Red) checkCanHit();
        }

        private void checkCanMove(object sender, EventArgs e)
        {
            var pawn = sender as PictureBox;
            if (isBlueTurn && pawn.BackColor == Color.Blue)
            {
                if (choosedPawnXOnArray != 0 && choosedPawnYOnArray != 0 && boardArray[choosedPawnXOnArray - 1, choosedPawnYOnArray - 1] == null)
                {
                    createMark(choosedPawnXOnArray - 1, choosedPawnYOnArray - 1, 0);
                }
                if (choosedPawnXOnArray != 7 && choosedPawnYOnArray != 0 && boardArray[choosedPawnXOnArray + 1, choosedPawnYOnArray - 1] == null)
                {
                    createMark(choosedPawnXOnArray + 1, choosedPawnYOnArray - 1, 0);
                }
            }
            else if (!isBlueTurn && pawn.BackColor == Color.Red)
            {
                if (choosedPawnXOnArray != 0 && choosedPawnYOnArray != 7 && boardArray[choosedPawnXOnArray - 1, choosedPawnYOnArray + 1] == null)
                {
                    createMark(choosedPawnXOnArray - 1, choosedPawnYOnArray + 1, 0);
                }
                if (choosedPawnXOnArray != 7 && choosedPawnYOnArray != 7 && boardArray[choosedPawnXOnArray + 1, choosedPawnYOnArray + 1] == null)
                {
                    createMark(choosedPawnXOnArray + 1, choosedPawnYOnArray + 1, 0);
                }
            }
        }

        private bool checkCanHit()
        {
            bool canHit = false;
            //left up
            if (choosedPawnXOnArray > 1 && choosedPawnYOnArray > 1 && boardArray[choosedPawnXOnArray - 1, choosedPawnYOnArray - 1] != null &&
                boardArray[choosedPawnXOnArray - 1, choosedPawnYOnArray - 1].Name != "mark" && 
                boardArray[choosedPawnXOnArray - 1, choosedPawnYOnArray - 1].BackColor != boardArray[choosedPawnXOnArray, choosedPawnYOnArray].BackColor &&
                boardArray[choosedPawnXOnArray - 2, choosedPawnYOnArray - 2] == null)
            {
                createMark(choosedPawnXOnArray - 2, choosedPawnYOnArray - 2, 1);
                canHit = true;
            }
            //left down
            if (choosedPawnXOnArray > 1 && choosedPawnYOnArray < 6 && boardArray[choosedPawnXOnArray - 1, choosedPawnYOnArray + 1] != null &&
                boardArray[choosedPawnXOnArray - 1, choosedPawnYOnArray + 1].Name != "mark" &&
                boardArray[choosedPawnXOnArray - 1, choosedPawnYOnArray + 1].BackColor != boardArray[choosedPawnXOnArray, choosedPawnYOnArray].BackColor &&
                boardArray[choosedPawnXOnArray - 2, choosedPawnYOnArray + 2] == null)
            {
                createMark(choosedPawnXOnArray - 2, choosedPawnYOnArray + 2, 1);
                canHit = true;
            }
            //right down
            if (choosedPawnXOnArray < 6 && choosedPawnYOnArray < 6 && boardArray[choosedPawnXOnArray + 1, choosedPawnYOnArray + 1] != null &&
                boardArray[choosedPawnXOnArray + 1, choosedPawnYOnArray + 1].Name != "mark" &&
                boardArray[choosedPawnXOnArray + 1, choosedPawnYOnArray + 1].BackColor != boardArray[choosedPawnXOnArray, choosedPawnYOnArray].BackColor &&
                boardArray[choosedPawnXOnArray + 2, choosedPawnYOnArray + 2] == null)
            {
                createMark(choosedPawnXOnArray + 2, choosedPawnYOnArray + 2, 1);
                canHit = true;
            }
            //right up
            if (choosedPawnXOnArray < 6 && choosedPawnYOnArray > 1 && boardArray[choosedPawnXOnArray + 1, choosedPawnYOnArray - 1] != null &&
                boardArray[choosedPawnXOnArray + 1, choosedPawnYOnArray - 1].Name != "mark" &&
                boardArray[choosedPawnXOnArray + 1, choosedPawnYOnArray - 1].BackColor != boardArray[choosedPawnXOnArray, choosedPawnYOnArray].BackColor &&
                boardArray[choosedPawnXOnArray + 2, choosedPawnYOnArray - 2] == null)
            {
                createMark(choosedPawnXOnArray + 2, choosedPawnYOnArray - 2, 1);
                canHit = true;
            }
            return canHit;
        }

        private void moveMarkClick(object sender, EventArgs e)
        {
            PictureBox mark = sender as PictureBox;
            int markX = (mark.Location.X - square1.Location.X) / 81;
            int markY = (mark.Location.Y - square1.Location.Y) / 81;

            this.Controls.Remove(boardArray[markX, markY]);
            boardArray[markX, markY] = null;
            boardArray[markX, markY] = boardArray[choosedPawnXOnArray, choosedPawnYOnArray];
            boardArray[markX, markY].Location = new Point(mark.Location.X + 7, mark.Location.Y + 7);
            boardArray[markX, markY].BringToFront();
            boardArray[markX, markY].Name = "" + markX + markY;
            boardArray[choosedPawnXOnArray, choosedPawnYOnArray] = null;
            checkCanBecomeKing(markX, markY);
            nextTurn(sender, e);
        }

        private void hitMarkClick(object sender, EventArgs e)
        {
            PictureBox mark = sender as PictureBox;
            int markX = (mark.Location.X - square1.Location.X) / 81;
            int markY = (mark.Location.Y - square1.Location.Y) / 81;

            this.Controls.Remove(boardArray[markX, markY]);
            boardArray[markX, markY] = null;
            boardArray[markX, markY] = boardArray[choosedPawnXOnArray, choosedPawnYOnArray];
            boardArray[markX, markY].Location = new Point(mark.Location.X + 7, mark.Location.Y + 7);
            boardArray[markX, markY].BringToFront();
            boardArray[markX, markY].Name = "" + markX + markY;
            boardArray[choosedPawnXOnArray, choosedPawnYOnArray] = null;
            this.Controls.Remove(boardArray[(choosedPawnXOnArray + markX) / 2, (choosedPawnYOnArray + markY) / 2]);
            boardArray[(choosedPawnXOnArray + markX) / 2, (choosedPawnYOnArray + markY) / 2] = null;
            checkCanBecomeKing(markX, markY);
            hideMarks();
            countPawns();
            choosedPawnXOnArray = markX;
            choosedPawnYOnArray = markY;
            if (!checkCanHit())
            {
                nextTurn(sender, e);
                attacking = false;
            }
            else
            {
                attacking = true;
                pictureBoxNextTurn.Visible = true;
                labelNextTurn.Visible = true;
                labelNextTurn.BringToFront();
            }
        }

        private void createMark(int x, int y, int type)
        {
            boardArray[x, y] = new PictureBox
            {
                Name = "mark",
                BackColor = Color.Black,
                Size = new Size(75, 75),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Location = new Point(square1.Location.X + x * 81, square1.Location.Y + y * 81),
            };
            if (type == 0)
            {
                boardArray[x, y].Click += moveMarkClick;
                boardArray[x, y].Image = Checkers.Properties.Resources.moveMarker;
            } else if(type == 1)
            {
                boardArray[x, y].Click += hitMarkClick;
                boardArray[x, y].Image = Checkers.Properties.Resources.hitMarker;
            }
            this.Controls.Add(boardArray[x, y]);
            boardArray[x, y].BringToFront();
        }

        private void checkCanBecomeKing(int x, int y)
        {
            if (y == 0 && boardArray[x, y].BackColor == Color.Blue || y == 7 && boardArray[x, y].BackColor == Color.Red)
            {
                boardArray[x, y].Click -= pawnClick;
                boardArray[x, y].Click += kingClick;
                if (boardArray[x, y].BackColor == Color.Red) boardArray[x, y].Image = Checkers.Properties.Resources.kingRed;
                if (boardArray[x, y].BackColor == Color.Blue) boardArray[x, y].Image = Checkers.Properties.Resources.kingBlue;
            }
        }
    }
}
