using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace oop_7
{  
    public class Composite : Shape
    {
        public class Item
        {
            public Shape shapes;
            public Item next;
            public Item previous;
        }

        int count;        
        Item current;
        Item first;
        private int width;
        private int height;
       // private int Radius = 40;
        public override void addComponent(Shape shapesx, int x, int y, int r, Color fcolor, Color scolor, int n, double degree, int x1, int y1, int x2, int y2)
        {
            Item item2 = new Item();
            item2.shapes = shapesx;
            X = x;
            Y = y;
            X1 = x1;
            Y1 = y1;
            if (getCount() == 0)
            {
                item2.previous = item2;
                item2.next = item2;
                first = item2;
                current = first;
                //_points = new Point[n];                
                fillBrush = new SolidBrush(fcolor);
                figurePen = new Pen(scolor);
                
            }
            else
            {
                current.next.previous = item2;
                item2.next = current.next;
                item2.previous = current;
                current.next = item2;
                Next();
            }
            
            //if (current.shapes.N == 10)
            //{
            //    width += current.shapes.Radius;
            //    height += current.shapes.Radius;
            //}
        count++;
        }

        
        public override Shape name()
        {

            Shape obj = current.shapes;
            
                Next();
            
                return obj; 
        }


        public override int getCount()
        {
            return count;
        }

        public void Next()
        {
            current = current.next;
        }
        public override void PResize()
        {
            width += 6;
            height += 6;
            X -= 3;
            Y -= 3;
            Radius += 3;
            for (int i = 0; i < count; i++)
            {
                current.shapes.PResize();
                Next();
            }
        }
        public override void MResize()
        {
            width -= 6;
            height -= 6;
            X += 3;
            Y += 3;
            Radius -= 3;
            for (int i = 0; i < count; i++)
            {
                current.shapes.MResize();
                Next();
            }
        }

        public bool check = false;
        public override void Move(int dx, int dy)
        {          
                for (int i = 0; i < count; i++)
                {
                    current.shapes.Move(dx, dy);
                    Next();
                }
            X += dx;
            Y += dy;
            X1 += dx;
            Y1 += dy;
        }

        public override void Draw(Graphics graphics)
        {
            X1 = -9999;
            Y1 = -9999;
            X = 9999;
            Y = 9999;
            for (int i = 0; i < count; i++)
            {
                Shape obj = current.shapes;
                obj.figurePen = FigurePen;
                obj.fillBrush = FillBrush;
                obj.FigurePen = new Pen(Color.DarkRed, 5);
                if (X > obj.X)
                    X = obj.X;
                if (Y > obj.Y)
                    Y = obj.Y;
                if (X1 < obj.X1)
                    X1 = obj.X1;
                if (Y1 < obj.Y1)
                    Y1 = obj.Y1;
                    obj.Draw(graphics);
                Next();
            }
            Color myColor; //цвет, у которого будем менять прозрачность 
            Color red = Color.FromArgb(200, 200, 255); //красный цвет 
            int alpha = 150; // переменна типа int, которая задает прозрачность, может меняться от 255 до 0 
            myColor = Color.FromArgb(alpha, red);// заданиец цвета с использованием alpha-канала
            Pen pen = new Pen(myColor, 5);
            pen.DashStyle = DashStyle.Dash;
            graphics.DrawRectangle(pen, X, Y, Math.Abs(X - X1), Math.Abs(Y - Y1));

        }
        public override void Fill(Graphics graphics)
        {
            for (int i = 0; i < count; i++)
            {
                Shape obj = current.shapes;
                if (current.shapes.N == 10)
                    obj.Fill(graphics);
                if (current.shapes.N == 0)
                    obj.Fill(graphics);
                else
                    obj.Fill(graphics);
                Next();
            }
        }
        public override void DrawFrame(Graphics graphics)
        {            
            for (int i = 0; i < count; i++)
            {
                Shape obj = current.shapes;
                obj.figurePen = FigurePen;
                obj.fillBrush = FillBrush;
                obj.FigurePen = new Pen(Color.Chocolate, 3);
                obj.DrawFrame(graphics);
                if (X > obj.X)
                    X = obj.X;
                if (Y > obj.Y)
                    Y = obj.Y;
                if (X1 < obj.X1)
                    X1 = obj.X1;
                if (Y1 < obj.Y1)
                    Y1 = obj.Y1;
                Next();
            }
            Pen pen = new Pen(Brushes.Red, 5);
            pen.DashStyle = DashStyle.Dash;
            graphics.DrawRectangle(pen, X, Y, Math.Abs(X - X1), Math.Abs(Y - Y1));
        }
        public Composite()
        {            
        }
        
        public Composite(int x1, int y1, int x2, int y2, int radius, int n, int degree)
        {
            width = x2 - x1;
            height = y2 - y1 ;            
            current = null;
            first = null;
            count = 0;
            X = x1-5 ;
            Y = y1 -5;
            X1 = x1 +10;
            Y1 = y1+10;
            X2 = x2 ;
            Y2 = y2 ;            
            N = n;
            Degree = degree;   
            if (width < height)
                Radius = 20;
            else Radius = 20;            
        }
        
        public override String GetData()
        {
            Degree = getCount();
            string s = "";
            for (int i = 0; i < count; i++)
            {
                Shape obj = current.shapes;                
                s += ",False,False\r\n" + obj.GetData();
                Next();
            }
            return GetType().ToString().Substring(6) + "," + (X1+25) + "," + (Y1+25) + "," + (X2-25) + "," + (Y2-25) + "," + Radius + "," + N + "," + Degree +  s;
        }

       
    }
}
