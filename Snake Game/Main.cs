using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake_Game
{
    public partial class Main : Form
    {
        Game Game;
        MainMenu MainMenu;

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            MainMenu = new MainMenu(this);
            MainMenu.Init();
            MainMenu.Show();
        }

        public void StartGame_Click(object sender, EventArgs e)
        {
            MainMenu.Hide();
            Game = new Game(this);
            Game.Start();
            moving_timer.Start();
        }

        private void GameBox_Paint(object sender, PaintEventArgs e)
        {
            if (Game != null && Game.Snake != null)
            {
                //Graphics g = e.Graphics;
                // If snake exists
                Game.RepaintSnakeParts(e.Graphics);
                if (Game.Current_Food != null)
                {
                    SolidBrush brush = new SolidBrush(Color.Red);
                    Game.Current_Food.Draw(e.Graphics, brush);
                    brush.Dispose();
                    brush = null;
                }
                //g.Dispose();
                //g = null;
            }
        }

        private void Moving_timer_Tick(object sender, EventArgs e)
        {
            // On each tick of the timer, refresh and move each part
            gameBox.Refresh();
        }

        public void GameOver()
        {
            MainMenu.Title.Text = "Game over!\n" + Game.Points + " points";
            MainMenu.Title.Location = new Point(
                Size.Width / 2 - MainMenu.Title.Size.Width / 2,
                Height / 2 - (MainMenu.StartGame.Height * 5)
            );
            MainMenu.StartGame.Text = "Try again!";
            MainMenu.Show();

            // Stop the timer so it doesn't refresh anympre
            moving_timer.Stop();
            // Delete snake object
            Game.Points = 0;
            Game.Snake = null;
            Game.Current_Food = null;
            // Unregiser keydown event so no controls work anymore
            gameBox.KeyDown -= new KeyEventHandler(Game.HandleControls);
            // Refresh to clear out the snake left
            gameBox.Refresh();
        }
    }
}
