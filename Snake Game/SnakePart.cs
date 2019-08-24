using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Game
{
    class SnakePart
    {
        // Position represents values starting from 1
        // When drawing or calculating position, decrease it by 1 to get index values, 0,1,2,...
        public int Position_X { get; set; }
        public int Position_Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool IsHead { get; set; }
        public SnakePart NextPart { get; set; }
        public SnakePart PrevPart { get; set; }

        // value pair that says what to do with x and y coordinates
        // up [0, -1]
        // down [0, 1]
        // left [-1, 0]
        // right [1, 0]
        public int[] MovementDirection { get; set; }
        public int[] LastDirection { get; set; }

        public SnakePart(int pos_x, int pos_y)
        {
            MovementDirection = new int[2] { -1, 0 };

            Position_X = pos_x;
            Position_Y = pos_y;

            Width = 25;
            Height = 25;
        }

        public void Draw(Graphics g, SolidBrush brush)
        {
            Rectangle snake_part = new Rectangle((Position_X-1) * Width, (Position_Y-1) * Height, Width, Height);
            g.FillRectangle(brush, snake_part);
        }

        public void FollowNext()
        {
            if(NextPart != null)
            {
                MovementDirection = NextPart.LastDirection;
            }
        }

        public void AttachPart()
        {
            int start_x = Position_X - MovementDirection[0];
            int start_y = Position_Y - MovementDirection[1];

            SnakePart new_part = new SnakePart(start_x, start_y);
            new_part.Width = 25;
            new_part.Height = 25;

            new_part.LastDirection = MovementDirection;
            new_part.MovementDirection = MovementDirection;

            new_part.PrevPart = null;
            new_part.NextPart = this;

            PrevPart = new_part;
        }

        public void AdaptOutOfBorder(int[,] game_matrix)
        {
            int x = game_matrix.GetLength(0);
            int y = game_matrix.GetLength(1);

            if (Position_X > x)
            {
                Position_X = 1;
            }
            else if (Position_X < 0)
            {
                Position_X = x;
            }
            else if (Position_Y > y)
            {
                Position_Y = 1;
            }
            else if (Position_Y < 0) 
            {
                Position_Y = y;
            }
        }
    }
}
