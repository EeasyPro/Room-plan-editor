using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace oop_7
{
    class Square : Shape
    {
        public Square(int x1, int y1, int x2, int y2, int radius, int n, int degree)
        {
            X = x1;
            Y = y1;
            X1 = x2;
            Y1 = y2;
            Radius = radius;
            Degree = degree;
            N = -1;
        }


        public override void Print()
        {
            Console.WriteLine("RegularPolygon," + X + "," + Y + "," + Radius + "," + N + "," + Degree);
        }
        public override String GetData()
        {
            return GetType().ToString().Substring(6) + "," + X + "," + Y + "," + X1 + "," + Y1 + "," + Radius + "," + N + "," + Degree;
        }
        public override void resizeX(int dx, int dy)
        {
            if ((Math.Abs(X1 - X) + dx) < 5)
                return;
            if ((X > X1) && (dx < 0))
                return;
            X1 += dx;           
        }
        public override void resizeY(int dx, int dy)
        {
            if ((Math.Abs(Y1 - Y) + dy) < 5)
                return;
            if ((Y > Y1) && (dy < 0))
                return;
            Y1 += dy;            
        }

        public override int X
        {
            get
            {
                return base.X;
            }

            set
            {
                base.X = value;
            }
        }

        public override int Y
        {
            get
            {
                return base.Y;
            }

            set
            {
                base.Y = value;
            }
        }

        public override void Move(int dx, int dy)
        {            
            X += dx;
            Y += dy;
            X1 += dx;
            Y1 += dy;
        }

        public override void DrawFrame(Graphics graphics)
        {
            Pen pen = new Pen(Brushes.Red, 3);
            pen.DashStyle = DashStyle.Dash;
            graphics.DrawRectangle(pen, X, Y, Math.Abs(X1 - X), Math.Abs(Y1 - Y));
        }



        public override void Draw(Graphics graphics)
        {
            FigurePen = new Pen(Color.Black, 5);
            graphics.DrawRectangle(FigurePen, X, Y, Math.Abs(X1 - X), Math.Abs(Y1 - Y));
        }

        
        public override void Fill(Graphics graphics)
        {
            if (N == -1)
            {
                graphics.DrawRectangle((new Pen(Color.Green, 5)), X, Y, Math.Abs(X1 - X), Math.Abs(Y1 - Y));
                return;
            }
        }

    }
}
