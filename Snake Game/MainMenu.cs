using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake_Game
{
    class MainMenu
    {
        Main Form { get; set; }
        Panel Menu { get; set; }
        public Button StartGame { get; set; }
        public Label Title { get; set; }

        public MainMenu(Main main_form)
        {
            Form = main_form;
        }

        public void Init()
        {
            // create panel
            Menu = new Panel();
            Menu.Width = Form.Size.Width;
            Menu.Height = Form.Size.Height;
            //Menu.BackColor = Color.DarkGreen;
            Menu.BackgroundImageLayout = ImageLayout.Tile;
            Menu.BackgroundImage = Image.FromFile(@"Images/grass-pattern.jpg");
            Menu.Visible = false;

            // add start button
            StartGame = new Button();
            StartGame.Text = "Start";
            StartGame.BackColor = Color.White;
            StartGame.Height = 30;
            StartGame.Width = 270;
            StartGame.Location = new Point(
                (Form.Size.Width / 2) - (StartGame.Width / 2),
                (Form.Size.Height / 2) - (StartGame.Height / 2)
            );
            StartGame.Click += new EventHandler(Form.StartGame_Click);
            Menu.Controls.Add(StartGame);

            // add some text
            Title = new Label();
            Title.Text = "Snake Game";
            Title.BackColor = Color.Transparent;
            Title.ForeColor = Color.White;
            Title.Font = new Font(Title.Font.FontFamily, 36);
            Title.AutoSize = true;
            //title.TextAlign = ContentAlignment.MiddleCenter;
            //title.Dock = DockStyle.Fill;
            Title.Location = new Point(
                Form.Size.Width / 2 - Title.Size.Width / 2 - 100,
                Form.Height / 2 - (StartGame.Height * 3)
            );
            Menu.Controls.Add(Title);

            Form.Controls.Add(Menu);
        }

        public void Show()
        {
            Menu.BringToFront();
            Menu.Visible = true;
        }

        public void Hide()
        {
            Menu.SendToBack();
            Menu.Visible = false;
            Form.gameBox.Focus();
            Form.gameBox.BringToFront();
        }
    }
}
