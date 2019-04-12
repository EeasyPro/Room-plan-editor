using System;
using System.Drawing;

namespace oop_7
{
    public class Shape {
        public Color fcolor;
        public Color scolor;
        public int _figureWidth = 5;
        public Brush fillBrush;
        public Pen figurePen;
        public Font font = new Font("Arial", 15);
        public Brush brush = Brushes.Black;

        public Color Fcolor {
            get {
                return fcolor;
            }

            set {
                fcolor = value;
                if(fillBrush != null)
                    fillBrush.Dispose();
                fillBrush = new SolidBrush(Fcolor);
            }
        }
        public Color Scolor {
            get {
                return scolor;
            }

            set {
                scolor = value;
                if (figurePen != null)
                    figurePen.Dispose();
                figurePen = new Pen(scolor);
            }
        }
        public int FigureWidth {
            get {
                return _figureWidth;
            }
            set {
                _figureWidth = value;
                if (figurePen == null || figurePen.Width != _figureWidth)
                    figurePen = new Pen(scolor, _figureWidth);
            }
        }
        public Brush FillBrush {
            get {
                if (fillBrush == null)
                    return fillBrush = new SolidBrush(Fcolor);
                return fillBrush;
            }

            set {
                fillBrush = value;
            }
        }
        public Pen FigurePen {
            get {
                if (figurePen == null)
                    return figurePen = new Pen(scolor, _figureWidth);
                return figurePen;
            }
            set {
                figurePen = value;
            }
        }

       
        public Shape() {
            X = y =  0;
        }
        public Shape(int x, int y) {
            this.y = y;
        }
        public Shape(int x, int y, int radius,int n, int degree) {
            this.X = x;
            this.Y = y;
            _radius = radius;
            _n = n;
            _degree = degree;

        }

        public Shape(int x, int y, int radius)
        {
            this.X = x;
            this.Y = y;            
            _radius = radius;
        }

        private int  x, y, _radius, _n,x1,y1,x2,y2,_degree;

        virtual public int Degree
        {
            get
            {
                return _degree;
            }

            set
            {
                _degree = value;
            }
        }

        virtual public int N
        {
            get
            {
                return _n;
            }

            set
            {
                _n = value;
            }
        }
        virtual public int X {
            get {
                return x;
            }

            set {
                x = value;
            }
        }
        virtual public int Y {
            get {
                return y;
            }

            set {
                y = value;
            }
        }

        virtual public void MResize() { }
        virtual public void PResize() { }
        virtual public int Radius {
            get {
                return _radius;
            }

            set {
                _radius = value;
            }
        }

        virtual public int X1
        {
            get
            {
                return x1;
            }

            set
            {
                x1 = value;
            }
        }
        virtual public int Y1
        {
            get
            {
                return y1;
            }

            set
            {
                y1 = value;
            }
        }
        virtual public int X2
        {
            get
            {
                return x2;
            }

            set
            {
                x2 = value;
            }
        }
        virtual public int Y2
        {
            get
            {
                return y2;
            }

            set
            {
                y2 = value;
            }
        }
        virtual public void resizeX(int dx, int dy)
        {
            
        }
        virtual public void resizeY(int dx, int dy)
        {
           
        }
        virtual public void Draw(Graphics graphics) {}
        virtual public void Fill(Graphics graphics) {}
        public virtual void DrawFrame(Graphics graphics) {
            graphics.DrawRectangle(Pens.Chocolate, X - _radius-1, y - _radius-1, _radius * 2+2, _radius * 2+2);
        }

        virtual public void Rotate(double degree) { }
        virtual public void addComponent(Shape shapesx, int x, int y, int r, Color fcolor, Color scolor, int n, double degree, int x1, int y1, int x2, int y2) { }
        public virtual void Move(int dx, int dy)
        {
            X += dx;
            y += dy;
        }
        
        virtual public void deletcurrent() { }

        virtual public void Print()
        {
            Console.WriteLine("Shape!!");
        }
        virtual public String GetData()
        {
            return "kek";
        }

        virtual public Shape name()
        {
            return null;
        }

        virtual public int getCount()
        {
            return 0;
        }
    }
}