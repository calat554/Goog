using System;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace Winsim
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "goog")
                for (int i = 0; i < 10; i++)
                {
                    progressBar1.Value += 10;
                }
            else
                MessageBox.Show(
                    "Incorrect",
                    "Try again",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            if (progressBar1.Value >= 100)
            {
                string soundPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sound.wav");
                if (File.Exists(soundPath))
                {
                    using (SoundPlayer player = new SoundPlayer(soundPath))
                    {
                        player.Play();
                    }
                }

                using (DodgeMessageBox dodgeBox = new DodgeMessageBox("Goog is angry", "Error"))
                {
                    dodgeBox.ShowDialog();
                }

                Application.Exit();

                Process.Start(new ProcessStartInfo("cmd.exe", "/c timeout /t 10 /nobreak >nul & rundll32.exe user32.dll,LockWorkStation") { UseShellExecute = true, WindowStyle = ProcessWindowStyle.Hidden });
            }
        }
    }
}