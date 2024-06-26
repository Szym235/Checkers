using System;
using System.Drawing;
using System.Windows.Forms;

namespace Checkers
{
    public partial class Form1 : Form
    {
        private void kingClick(object sender, EventArgs e)
        {
            if (attacking) return;
            hideMarks();
            var pawn = sender as PictureBox;
            choosedPawnXOnArray = pawn.Name[0] - '0';
            choosedPawnYOnArray = pawn.Name[1] - '0';
            checkCanKingMove(sender, e);
            if (isBlueTurn && pawn.BackColor == Color.Blue) checkCanHit();
            else if (!isBlueTurn && pawn.BackColor == Color.Red) checkCanHit();
        }

        
        private void checkCanKingMove(object sender, EventArgs e)
        {
            var pawn = sender as PictureBox;
            if ((pawn.BackColor == Color.Blue && !isBlueTurn)) return;
            if ((pawn.BackColor == Color.Red && isBlueTurn)) return;
            if (choosedPawnXOnArray != 0 && choosedPawnYOnArray != 0 && boardArray[choosedPawnXOnArray - 1, choosedPawnYOnArray - 1] == null)
                {
                    createMark(choosedPawnXOnArray - 1, choosedPawnYOnArray - 1, 0);
                }
                if (choosedPawnXOnArray != 7 && choosedPawnYOnArray != 0 && boardArray[choosedPawnXOnArray + 1, choosedPawnYOnArray - 1] == null)
                {
                    createMark(choosedPawnXOnArray + 1, choosedPawnYOnArray - 1, 0);
                }
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


}