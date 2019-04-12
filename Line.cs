using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace oop_7
{
    class Line : Shape
    {       
        public Line(int x1, int y1, int x2, int y2, int radius, int n, int degree)
        {
            X = x1;
            Y = y1;
            X1 = x2;
            Y1 = y2;
            Radius = radius;
            Degree = degree;
            N = n;           
        }

        public override void Print()
        {
            Console.WriteLine("RegularPolygon," + X + "," + Y + "," + Radius + "," + N + "," + Degree);
        }
        public override String GetData()
        {
            return GetType().ToString().Substring(6) + "," + X + "," + Y + "," + X1 + "," + Y1 + "," + Radius + "," + N + "," + Degree;
        }


        public override int X
        {
            get {return base.X;}
            set {base.X = value;}
        }

        public override int Y
        {
            get {return base.Y;}
            set {base.Y = value;}
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
            if((N == -1)||(N == 0) || (N == 1) || (N == 3))
            {
                Pen pen = new Pen(Brushes.Red, 3);
                pen.DashStyle = DashStyle.Dash;
                graphics.DrawLine(pen, X, Y, X1, Y1);
                return;
            }                       
        }

        

        public override void Draw(Graphics graphics)
        {
            FigurePen = new Pen(Color.Black, 5);
            if (N == -1)
            {
                graphics.DrawLine(FigurePen, X, Y, X1, Y1);
                return;
            }
            if (N == 0)
            {
                graphics.DrawLine(new Pen(Color.LightBlue, 5), X, Y, X1, Y1);
                return;
            }
            if (N == 1)
            {
                if (Degree == -1)
                {
                    graphics.DrawLine(new Pen(Color.White, 5), X, Y, X1, Y1);
                    double ugol = Math.Atan2(X - X1, Y - Y1);
                    int X2 = Convert.ToInt32(X1 + Math.Sin(ugol));
                    int Y2 = Convert.ToInt32(Y + Math.Cos(ugol));
                    graphics.DrawLine((new Pen(Color.LightBlue, 3)), X2, Y2, Convert.ToInt32(X2 + (Math.Abs(X - X1)) * Math.Sin(0.3 + ugol)), Convert.ToInt32(Y2 + (Math.Abs(X - X1)) * Math.Cos(0.3 + ugol)));
                }
                if (Degree == 1)
                {
                    graphics.DrawLine(new Pen(Color.White, 5), X, Y, X1, Y1);
                    double ugol = Math.Atan2(X - X1, Y - Y1);
                    int X2 = Convert.ToInt32(X + Math.Sin(ugol));
                    int Y2 = Convert.ToInt32(Y1 + Math.Cos(ugol));
                    graphics.DrawLine((new Pen(Color.LightBlue, 3)), X2, Y2, Convert.ToInt32(X2 + (Math.Abs(Y - Y1)) * Math.Sin(0.3 + ugol)), Convert.ToInt32(Y2 + (Math.Abs(Y - Y1)) * Math.Cos(0.3 + ugol)));                    
                }
                return;
            }
            if (N == 3)
            {
                PointF pointX;
                     PointF pointY;
                if (Degree == -1)
                {
                    double ugol = Math.Atan2(X - X1, Y - Y1);
                    int X2 = Convert.ToInt32(X1 + Math.Sin(ugol));
                    int Y2 = Convert.ToInt32(Y + Math.Cos(ugol));
                    int X3 = Convert.ToInt32(X + Math.Sin(ugol));
                    int Y3 = Convert.ToInt32(Y + Math.Cos(ugol));
                    graphics.DrawLine((new Pen(Color.Black, 3)), X2, Y2, Convert.ToInt32(X2 + 30 * Math.Sin(0.3 + ugol)), Convert.ToInt32(Y2 + 30 * Math.Cos(0.3 + ugol)));
                    graphics.DrawLine((new Pen(Color.Black, 3)), X2, Y2, Convert.ToInt32(X2 + 30 * Math.Sin(-0.3 + ugol)), Convert.ToInt32(Y2 + 30 * Math.Cos(-0.3 + ugol)));
                    graphics.DrawLine((new Pen(Color.Black, 3)), X3, Y3, Convert.ToInt32(X3 + 30 * Math.Sin(2.85 + ugol)), Convert.ToInt32(Y3 + 30 * Math.Cos(2.85 + ugol)));
                    graphics.DrawLine((new Pen(Color.Black, 3)), X3, Y3, Convert.ToInt32(X3 + 30 * Math.Sin(-2.85 + ugol)), Convert.ToInt32(Y3 + 30 * Math.Cos(-2.85 + ugol)));
                    graphics.DrawLine((new Pen(Color.Black, 3)), X, Y, X1, Y);
                    if (X<X1)
                     pointX = new PointF((X + (Math.Abs(X - X1)) / 2) , Y + 5);
                    else pointX = new PointF((X - (Math.Abs(X - X1)) / 2), Y + 5);
                    graphics.DrawString(Math.Abs(X - X1).ToString(), font, brush, pointX);
                }
                if (Degree == 1)
                {
                    double ugol = Math.Atan2(X - X1, Y - Y1);
                    int X2 = Convert.ToInt32(X + Math.Sin(ugol));
                    int Y2 = Convert.ToInt32(Y1 + Math.Cos(ugol));
                    int X3 = Convert.ToInt32(X + Math.Sin(ugol));
                    int Y3 = Convert.ToInt32(Y + Math.Cos(ugol));
                    graphics.DrawLine((new Pen(Color.Black, 3)), X2, Y2, Convert.ToInt32(X2 + 30 * Math.Sin(0.3 + ugol)), Convert.ToInt32(Y2 + 30 * Math.Cos(0.3 + ugol)));
                    graphics.DrawLine((new Pen(Color.Black, 3)), X2, Y2, Convert.ToInt32(X2 + 30 * Math.Sin(-0.3 + ugol)), Convert.ToInt32(Y2 + 30 * Math.Cos(-0.3 + ugol)));
                    graphics.DrawLine((new Pen(Color.Black, 3)), X3, Y3, Convert.ToInt32(X3 + 30 * Math.Sin(2.85 + ugol)), Convert.ToInt32(Y3 + 30 * Math.Cos(2.85 + ugol)));
                    graphics.DrawLine((new Pen(Color.Black, 3)), X3, Y3, Convert.ToInt32(X3 + 30 * Math.Sin(-2.85 + ugol)), Convert.ToInt32(Y3 + 30 * Math.Cos(-2.85 + ugol)));
                    graphics.DrawLine((new Pen(Color.Black, 3)), X, Y, X, Y1);
                    if (Y<Y1)
                     pointY = new PointF(X1 + 5,(Y + (Math.Abs(Y - Y1)) / 2) );
                    else pointY = new PointF(X1 + 5, (Y - (Math.Abs(Y - Y1)) / 2));
                    graphics.DrawString(Math.Abs(Y - Y1).ToString(), font, brush, pointY);
                }
                return;
            }


        }
        
        public override void resizeY(int dx, int dy)
        {
            if (Radius == 1)
            {
                if ((Math.Abs(X1 - X) + dx) < 5)
                    return;
                if ((X > X1) && (dx < 0))
                    return;               
                if (X1 > X)
                    X1 += dx;
                if ((X1 == X)&&(dx > 0))
                    X1 += dx;
            }
            if (Radius == -1)
            {
                if ((Math.Abs(Y1 - Y) + dy) < 5)
                    return;
                if ((Y > Y1) && (dy < 0))
                    return;                
                if (Y1 > Y)
                    Y1 += dy;
                if ((Y1 == Y) && (dy > 0))
                    Y1 += dy;
            }
        }       

        public override void Fill(Graphics graphics)
        {
            if (N == -1)
            {
                graphics.DrawLine((new Pen(Color.Green, 5)), X, Y, X1, Y1);
                return;
            }
            if (N == 0)
            {
                graphics.DrawLine(new Pen(Color.Green, 5), X, Y, X1, Y1);
                return;
            }
            if (N == 1)
            {
                if (Degree == -1)
                {
                    graphics.DrawLine(new Pen(Color.White, 5), X, Y, X1, Y1);
                    double ugol = Math.Atan2(X - X1, Y - Y1);
                    int X2 = Convert.ToInt32(X1 + Math.Sin(ugol));
                    int Y2 = Convert.ToInt32(Y + Math.Cos(ugol));
                    graphics.DrawLine((new Pen(Color.Green, 3)), X2, Y2, Convert.ToInt32(X2 + (Math.Abs(X - X1)) * Math.Sin(0.3 + ugol)), Convert.ToInt32(Y2 + (Math.Abs(X - X1)) * Math.Cos(0.3 + ugol)));
                }
                if (Degree == 1)
                {
                    graphics.DrawLine(new Pen(Color.White, 5), X, Y, X1, Y1);
                    double ugol = Math.Atan2(X - X1, Y - Y1);
                    int X2 = Convert.ToInt32(X + Math.Sin(ugol));
                    int Y2 = Convert.ToInt32(Y1 + Math.Cos(ugol));
                    graphics.DrawLine((new Pen(Color.Green, 3)), X2, Y2, Convert.ToInt32(X2 + (Math.Abs(Y - Y1)) * Math.Sin(0.3 + ugol)), Convert.ToInt32(Y2 + (Math.Abs(Y - Y1)) * Math.Cos(0.3 + ugol)));
                }
                return;
            }
            if (N == 3)
            {
                if (Degree == -1)
                {
                    double ugol = Math.Atan2(X - X1, Y - Y1);
                    int X2 = Convert.ToInt32(X1 + Math.Sin(ugol));
                    int Y2 = Convert.ToInt32(Y + Math.Cos(ugol));
                    int X3 = Convert.ToInt32(X + Math.Sin(ugol));
                    int Y3 = Convert.ToInt32(Y + Math.Cos(ugol));
                    graphics.DrawLine((new Pen(Color.Green, 3)), X2, Y2, Convert.ToInt32(X2 + 30 * Math.Sin(0.3 + ugol)), Convert.ToInt32(Y2 + 30 * Math.Cos(0.3 + ugol)));
                    graphics.DrawLine((new Pen(Color.Green, 3)), X2, Y2, Convert.ToInt32(X2 + 30 * Math.Sin(-0.3 + ugol)), Convert.ToInt32(Y2 + 30 * Math.Cos(-0.3 + ugol)));
                    graphics.DrawLine((new Pen(Color.Green, 3)), X3, Y3, Convert.ToInt32(X3 + 30 * Math.Sin(2.85 + ugol)), Convert.ToInt32(Y3 + 30 * Math.Cos(2.85 + ugol)));
                    graphics.DrawLine((new Pen(Color.Green, 3)), X3, Y3, Convert.ToInt32(X3 + 30 * Math.Sin(-2.85 + ugol)), Convert.ToInt32(Y3 + 30 * Math.Cos(-2.85 + ugol)));
                    graphics.DrawLine((new Pen(Color.Green, 3)), X, Y, X1, Y);
                }
                if (Degree == 1)
                {
                    double ugol = Math.Atan2(X - X, Y - Y1);
                    int X2 = Convert.ToInt32(X + Math.Sin(ugol));
                    int Y2 = Convert.ToInt32(Y1 + Math.Cos(ugol));
                    int X3 = Convert.ToInt32(X + Math.Sin(ugol));
                    int Y3 = Convert.ToInt32(Y + Math.Cos(ugol));
                    graphics.DrawLine((new Pen(Color.Green, 3)), X2, Y2, Convert.ToInt32(X2 + 30 * Math.Sin(0.3 + ugol)), Convert.ToInt32(Y2 + 30 * Math.Cos(0.3 + ugol)));
                    graphics.DrawLine((new Pen(Color.Green, 3)), X2, Y2, Convert.ToInt32(X2 + 30 * Math.Sin(-0.3 + ugol)), Convert.ToInt32(Y2 + 30 * Math.Cos(-0.3 + ugol)));
                    graphics.DrawLine((new Pen(Color.Green, 3)), X3, Y3, Convert.ToInt32(X3 + 30 * Math.Sin(2.85 + ugol)), Convert.ToInt32(Y3 + 30 * Math.Cos(2.85 + ugol)));
                    graphics.DrawLine((new Pen(Color.Green, 3)), X3, Y3, Convert.ToInt32(X3 + 30 * Math.Sin(-2.85 + ugol)), Convert.ToInt32(Y3 + 30 * Math.Cos(-2.85 + ugol)));
                    graphics.DrawLine((new Pen(Color.Green, 3)), X, Y, X, Y1);
                }
                return;
            }
        }
    }
}
