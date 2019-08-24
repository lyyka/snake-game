using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake_Game
{
    class Game
    {
        Main Form { get; set; }
        public Snake Snake { get; set; }
        public int Points { get; set; }
        public int TimerDecrease { get; set; }
        public Food Current_Food { get; set; }

        // 0 - empty field
        // 1 - snake is there
        // 2 - food is there
        public int[,] Game_Matrix { get; set; }

        public Dictionary<string, WMPLib.WindowsMediaPlayer> Sounds { get; set; }

        public Game(Main form)
        {
            Form = form;

            // set up sounds
            Sounds = new Dictionary<string, WMPLib.WindowsMediaPlayer>();

            // create success sound
            WMPLib.WindowsMediaPlayer success_sound = new WMPLib.WindowsMediaPlayer();
            success_sound.settings.autoStart = false;
            success_sound.URL = @"Sounds/success.mp3";
            Sounds.Add("success", success_sound);
        }
        public void Start()
        {
            // Define width and height of the snake
            int snake_width = 30;
            int snake_height = 30;

            // Calculate matrix size based on form width and height
            int max_x = Form.gameBox.Size.Width / snake_width;
            int max_y = Form.gameBox.Size.Height / snake_height;

            // Define game matrix
            Game_Matrix = new int[max_x, max_y];

            Points = 0;
            Snake = new Snake(Form, this, snake_width, snake_height);
            Form.gameBox.KeyDown += new KeyEventHandler(HandleControls);
            TimerDecrease = Form.moving_timer.Interval / 10;

            SpawnFood();
        }

        public void CheckTable(Graphics G)
        {
            int width = 30;
            int height = 30;

            int rows = Game_Matrix.GetLength(0) - 1;
            int cols = Game_Matrix.GetLength(1) - 1;

            for(int i = 0; i <= rows; i++)
            {
                for(int y = 0; y <= cols; y++)
                {
                    Rectangle check = new Rectangle(i* width, y * height, width, height);
                    Color brush_color = Color.White;
                    if((y % 2 == 0 && i % 2 == 0) || (y % 2 == 1 && i % 2 == 1))
                    {
                        brush_color = Color.LightGray;
                    }
                    SolidBrush brush = new SolidBrush(brush_color);
                    G.FillRectangle(brush, check);
                    brush.Dispose();
                }
            }
        }

        public void SpawnFood()
        {
            int snake_x = Snake.Head.Position_X;
            int snake_y = Snake.Head.Position_Y;

            int bottom_x = 1;
            int top_x = 1;

            int bottom_y = 1;
            int top_y = 1;

            if(snake_x > Game_Matrix.GetLength(0) - snake_x)
            {
                top_x = snake_x;
            }
            else
            {
                bottom_x = snake_x;
                top_x = Game_Matrix.GetLength(0);
            }

            if(snake_y > Game_Matrix.GetLength(1) - snake_y)
            {
                top_y = snake_y;
            }
            else
            {
                bottom_y = snake_y;
                top_y = Game_Matrix.GetLength(1);
            }

            Random ran = new Random();

            // make sure that it generates on an empty field
            bool generated_truly = false;
            int food_x = 0;
            int food_y = 0;
            while (!generated_truly)
            {
                food_x = ran.Next(bottom_x, top_x);
                food_y = ran.Next(bottom_y, top_y);

                if(Game_Matrix[food_x, food_y] == 0)
                {
                    generated_truly = true;
                }
            }

            Current_Food = new Food(food_x, food_y);
            Game_Matrix[food_x, food_y] = 2;
        }

        public void RepaintSnakeParts(Graphics g)
        {
            if (Snake != null)
            {
                // Get the head and loop through the list of parts
                SnakePart start = Snake.Head;
                SolidBrush brush = null;
                while (start != null)
                {
                    Color part_color;
                    if (start.IsHead)
                    {
                        part_color = Color.DarkGreen;
                    }
                    else
                    {
                        part_color = Color.Green;
                    }
                    brush = new SolidBrush(part_color);
                    start.Draw(g, brush);
                    start = start.PrevPart;
                }
                if(brush != null)
                {
                    brush.Dispose();
                    brush = null;
                }
                Snake.Move();
            }
        }

        public void IncreasePoints()
        {
            Points++;
            Form.points_lb.Text = "Points: " + Points;

            if(Points % 10 == 0 && Form.moving_timer.Interval >= 1 + TimerDecrease)
            {
                Form.moving_timer.Interval -= TimerDecrease;
            }
        }

        public void HandleControls(object sender, KeyEventArgs e)
        {
            // Create key-value pairs
            // Key is a Keys enumerator so we can reference it's value with KeyCode from the event
            // Value is an array of movement
            // First val in array says how to change the X position
            // Second val in array says how to change Y position
            Dictionary<Keys, int[]> direction_increments = new Dictionary<Keys, int[]>();
            direction_increments.Add(Keys.Up, new int[2] { 0, -1 });
            direction_increments.Add(Keys.Down, new int[2] { 0, 1 });
            direction_increments.Add(Keys.Left, new int[2] { -1, 0 });
            direction_increments.Add(Keys.Right, new int[2] { 1, 0 });

            // Check if key and snake exist and if snake can move
            if (direction_increments[e.KeyCode] != null && Snake != null)
            {
                // set last direction of head based on current movement
                Snake.Head.LastDirection = Snake.Head.MovementDirection;
                // Change current movement
                Snake.Head.MovementDirection = direction_increments[e.KeyCode];
                // Refresh to move and draw all parts
                Form.gameBox.Refresh();
            }
        }
    }
}
