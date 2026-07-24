using System;
using System.Drawing;
using System.Windows.Forms;
using FormsTimer = System.Windows.Forms.Timer;

namespace Winsim
{
    public partial class DodgeMessageBox : Form
    {
        private FormsTimer dodgeTimer;
        private int triggerDistance = 120;
        private Label lblMessage;
        private Button btnOk;

        public DodgeMessageBox(string message, string title)
        {
            InitializeComponentSetup(message, title);

            dodgeTimer = new FormsTimer();
            dodgeTimer.Interval = 40;
            dodgeTimer.Tick += DodgeTimer_Tick;
            dodgeTimer.Start();
        }

        private void InitializeComponentSetup(string message, string title)
        {
            this.Text = title;
            this.Size = new Size(300, 150);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            lblMessage = new Label();
            lblMessage.Text = message;
            lblMessage.Location = new Point(20, 20);
            lblMessage.Size = new Size(240, 50);
            this.Controls.Add(lblMessage);

            btnOk = new Button();
            btnOk.Text = "OK";
            btnOk.Location = new Point(105, 80);
            btnOk.Click += BtnOk_Click;
            this.Controls.Add(btnOk);
            this.AcceptButton = btnOk;
        }

        private void DodgeTimer_Tick(object sender, EventArgs e)
        {
            Point mousePos = Cursor.Position;
            Point formCenter = new Point(this.Location.X + this.Width / 2, this.Location.Y + this.Height / 2);

            double distance = Math.Sqrt(Math.Pow(mousePos.X - formCenter.X, 2) + Math.Pow(mousePos.Y - formCenter.Y, 2));

            if (distance < triggerDistance)
            {
                MoveFormAway(mousePos);
            }
        }

        private void MoveFormAway(Point mousePos)
        {
            Random rand = new Random();
            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;

            // Determine direction away from the mouse cursor
            int moveX = mousePos.X < this.Location.X + this.Width / 2 ? rand.Next(100, 200) : rand.Next(-200, -100);
            int moveY = mousePos.Y < this.Location.Y + this.Height / 2 ? rand.Next(100, 200) : rand.Next(-200, -100);

            int newX = this.Location.X + moveX;
            int newY = this.Location.Y + moveY;

            // Screen wrapping logic (if it hits an edge, pop out on the opposite side)
            if (newX > screenWidth)
            {
                newX = 0; // Wrap from right to left
            }
            else if (newX + this.Width < 0)
            {
                newX = screenWidth - this.Width; // Wrap from left to right
            }

            if (newY > screenHeight)
            {
                newY = 0; // Wrap from bottom to top
            }
            else if (newY + this.Height < 0)
            {
                newY = screenHeight - this.Height; // Wrap from top to bottom
            }

            this.Location = new Point(newX, newY);
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            dodgeTimer.Stop();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            dodgeTimer?.Stop();
            dodgeTimer?.Dispose();
            base.OnFormClosing(e);
        }
    }
}