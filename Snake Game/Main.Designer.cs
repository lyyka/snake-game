namespace Snake_Game
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.moving_timer = new System.Windows.Forms.Timer(this.components);
            this.gameBox = new System.Windows.Forms.PictureBox();
            this.points_lb = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gameBox)).BeginInit();
            this.SuspendLayout();
            // 
            // moving_timer
            // 
            this.moving_timer.Interval = 200;
            this.moving_timer.Tick += new System.EventHandler(this.Moving_timer_Tick);
            // 
            // gameBox
            // 
            this.gameBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gameBox.Location = new System.Drawing.Point(1, 57);
            this.gameBox.Name = "gameBox";
            this.gameBox.Size = new System.Drawing.Size(645, 600);
            this.gameBox.TabIndex = 0;
            this.gameBox.TabStop = false;
            this.gameBox.Paint += new System.Windows.Forms.PaintEventHandler(this.GameBox_Paint);
            // 
            // points_lb
            // 
            this.points_lb.AutoSize = true;
            this.points_lb.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.points_lb.Location = new System.Drawing.Point(12, 9);
            this.points_lb.Name = "points_lb";
            this.points_lb.Size = new System.Drawing.Size(129, 31);
            this.points_lb.TabIndex = 1;
            this.points_lb.Text = "Points: 0";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(647, 658);
            this.Controls.Add(this.points_lb);
            this.Controls.Add(this.gameBox);
            this.Name = "Main";
            this.Text = "Snake Game";
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gameBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.PictureBox gameBox;
        public System.Windows.Forms.Label points_lb;
        public System.Windows.Forms.Timer moving_timer;
    }
}

