using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake_Game
{
    class Food
    {
        public int Position_X { get; set; }
        public int Position_Y { get; set; }
        public Food(int pos_x, int pos_y)
        {
            Position_X = pos_x;
            Position_Y = pos_y;
        }
        public void Draw(Graphics g, SolidBrush brush)
        {
            Rectangle food = new Rectangle((Position_X - 1) * 25, (Position_Y - 1) * 25, 25, 25);
            g.FillRectangle(brush, food);
        }
    }
}
