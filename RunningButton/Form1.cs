using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RunningButton
{
    public partial class Form1 : Form
    {
        Random rand;
        Point prevButtonLocation;
        Point newButtonLocation;
        Point prevMousePosition;
        Size prevFormSize;
        double mouseDirToButtonDist;

        public Form1()
        {
            InitializeComponent();
            rand = new Random();
            newButtonLocation = new Point();
            prevMousePosition = new Point(MousePosition.X, MousePosition.Y);
            prevFormSize = this.Size;
            prevButtonLocation = button1.Location;

            //radioButton1.Location = new Point(button1.Location.X + button1.Size.Width / 2, button1.Location.Y + button1.Size.Height / 2);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            button1.Location = new Point(Convert.ToInt32(Math.Round((float)(prevButtonLocation.X * this.Size.Width / prevFormSize.Width))), 
                Convert.ToInt32(Math.Round((float)(prevButtonLocation.Y * this.Size.Height / prevFormSize.Height))));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Size == prevFormSize)
            {
                Point p0 = new Point(button1.Location.X + button1.Size.Width / 2 + this.Location.X + 10, button1.Location.Y + button1.Size.Height / 2 + this.Location.Y + 30);
                Point p1 = new Point(prevMousePosition.X, prevMousePosition.Y);
                Point p2 = new Point(MousePosition.X, MousePosition.Y);

                mouseDirToButtonDist = (Math.Abs((p2.Y - p1.Y) * p0.X - (p2.X - p1.X) * p0.Y + p2.X * p1.Y - p2.Y * p1.X)) /
                    (Math.Sqrt(Math.Pow(p2.Y - p1.Y, 2) + Math.Pow(p2.X - p1.X, 2)));

                if ((Math.Sqrt(Math.Pow(p1.Y - p0.Y, 2) + Math.Pow(p1.X - p0.X, 2))) > (Math.Sqrt(Math.Pow(p2.Y - p0.Y, 2) + Math.Pow(p2.X - p0.X, 2))) && (mouseDirToButtonDist < 100))
                {
                    newButtonLocation.X = button1.Location.X + (p2.X - p1.X);

                    if (newButtonLocation.X < 5)
                    {
                        newButtonLocation.X = 5;
                    }
                    if (newButtonLocation.X > this.Size.Width - button1.Size.Width - 20)
                    {
                        newButtonLocation.X = this.Size.Width - button1.Size.Width - 20;
                    }

                    newButtonLocation.Y = button1.Location.Y + (p2.Y - p1.Y);

                    if (newButtonLocation.Y < 5)
                    {
                        newButtonLocation.Y = 5;
                    }
                    if (newButtonLocation.Y > this.Size.Height - button1.Size.Height - 40)
                    {
                        newButtonLocation.Y = this.Size.Height - button1.Size.Height - 40;
                    }

                    button1.Location = newButtonLocation;

                }

                prevMousePosition = MousePosition;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            prevFormSize = this.Size;
            prevButtonLocation = button1.Location;
        }

        partial class Form2 : Form
        {
            private System.Windows.Forms.Label label1;

            public Form2()
            {
                label1 = new Label();

                label1.AutoSize = true;
                label1.Location = new Point(60, 30);
                label1.Name = "label1";
                label1.Size = new Size(35, 13);
                label1.TabIndex = 1;
                label1.Text = "You touched the button";
                label1.Font = new Font("Microsoft Sans Serif", 12F);

                AutoScaleDimensions = new SizeF(6F, 13F);
                AutoScaleMode = AutoScaleMode.Font;
                ClientSize = new Size(300, 100);
                Controls.Add(this.label1);
                Name = "Form2";
                Text = "Congrats!";
                ResumeLayout(false);
                PerformLayout();
            }
        }

        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
            Form2 finalDialog = new Form2();

            finalDialog.ShowDialog();
        }
    }
}
