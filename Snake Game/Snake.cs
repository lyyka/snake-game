using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Game
{
    class Snake
    {
        // represents main form
        Main form;

        // The root of the linked list that represents snake
        public SnakePart Head { get; set; }

        // the gmae snake belongs to
        Game Game { get; set; }

        // Construct new snake
        // Main form is a must becase of game matrix size
        // Second argument is the starting length of a snake
        public Snake(Main main, Game game, int snake_width, int snake_height, int length = 6)
        {
            Game = game;
            // set the global form to this main
            form = main;

            int max_x = Game.Game_Matrix.GetLength(0);
            int max_y = Game.Game_Matrix.GetLength(1);

            // Starting x and y coordinates of snake
            int start_x = max_x / 2 + length;
            int start_y = max_y / 2;

            // define new part of snake
            SnakePart new_part = null;
            // Tells if head is created so just the first part is set as head
            bool head_created = false;

            // until length is zero
            while(length != 0)
            {
                // Set the previous part so we can link it with next
                // At the begining this will be null
                SnakePart previous_part = new_part;

                // Define new part on x,y coordinates
                new_part = new SnakePart(start_x, start_y);
                new_part.Width = snake_width;
                new_part.Height = snake_height;
                // Default direction will be to the right
                new_part.LastDirection = new int[2] { 1, 0 };
                new_part.MovementDirection = new int[2] { 1, 0 };
                // If the head is not created, create it
                if (!head_created)
                {
                    // set that head is created so no part after this doesn't get set as head
                    head_created = true;
                    new_part.IsHead = true;
                    Head = new_part;
                }

                // next part of current part is previous
                new_part.NextPart = previous_part;

                // if there is a previous part (every part after Head has it)
                if(previous_part != null)
                {
                    // set it's previous part to this new part
                    previous_part.PrevPart = new_part;
                }

                // Write the position to the game matrx
                Game.Game_Matrix[start_x, start_y] = 1;

                // Decrease length
                length--;
                // Generate next part on the left side of the snake
                start_x--;
            }
        }

        public void Move()
        {
            // Move the head part
            Head.Position_X += Head.MovementDirection[0];
            Head.Position_Y += Head.MovementDirection[1];

            // Adapt head to the borders
            Head.AdaptOutOfBorder(Game.Game_Matrix);
            // Check if head colided with something
            CheckNewPositionForCollisions(Head);

            // go through parts, starting from the part after Head
            SnakePart current_part = Head.PrevPart;
            while (current_part != null)
            {
                // set last direction to movement direction
                current_part.LastDirection = current_part.MovementDirection;
                // If there is next part, get it's last movement direction and set it on this part
                current_part.FollowNext();
                // Move part
                current_part.Position_X += current_part.MovementDirection[0];
                current_part.Position_Y += current_part.MovementDirection[1];
                // Adapt the part if it crosses the border
                current_part.AdaptOutOfBorder(Game.Game_Matrix);
                // Update matrix for moved part
                UpdateMatrix(current_part);

                // get next part to move
                current_part = current_part.PrevPart;
            }

            Head.LastDirection = Head.MovementDirection;
        }

        public void UpdateMatrix(SnakePart part)
        {
            // get matrix size
            int x = Game.Game_Matrix.GetLength(0);
            int y = Game.Game_Matrix.GetLength(1);

            // remove 1 from old position
            if (part.Position_X - part.MovementDirection[0] >= 0 && part.Position_X - part.MovementDirection[0] < x
                && part.Position_Y - part.MovementDirection[1] >= 0 && part.Position_Y - part.MovementDirection[1] < y)
            {
                Game.Game_Matrix[part.Position_X - part.MovementDirection[0], part.Position_Y - part.MovementDirection[1]] = 0;
            }

            // update with new position
            if (part.Position_X >= 0 && part.Position_X < x
                && part.Position_Y >= 0 && part.Position_Y < y)
            {
                Game.Game_Matrix[part.Position_X, part.Position_Y] = 1;
            }
        }

        public void CheckNewPositionForCollisions(SnakePart head)
        {
            // Get matrix size
            int x = Game.Game_Matrix.GetLength(0);
            int y = Game.Game_Matrix.GetLength(1);

            // If the position is inside the matrix
            if(head.Position_X >= 0 && head.Position_X < x
                && head.Position_Y >= 0 && head.Position_Y < y)
            {
                // check if on that position is already some part
                if (Game.Game_Matrix[head.Position_X, head.Position_Y] == 1)
                {
                    // destroy the snake
                    Destroy();
                }

                // check if food is hit
                if (Game.Game_Matrix[head.Position_X, head.Position_Y] == 2)
                {
                    Game.IncreasePoints();

                    Game.Sounds["success"].controls.play();

                    Game.Game_Matrix[head.Position_X, head.Position_Y] = 0;
                    // attach one more part to the snake
                    SnakePart prev = null;
                    SnakePart current = Head;
                    while(current != null)
                    {
                        prev = current;
                        current = current.PrevPart;
                    }

                    prev.AttachPart();

                    // spawn another food
                    Game.SpawnFood();
                }
            }
        }

        public void Destroy()
        {
            // Unable snake to move
            //CanMove = false;
            // End the game in form
            form.GameOver();
            // Detach the head
            //Head.PrevPart = null;
        }

    }
}
