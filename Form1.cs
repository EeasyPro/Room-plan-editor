//04.12.17
using System;
using System.Drawing;
using System.Windows.Forms;
using oop_7;
using System.IO;

namespace oop6
{
    public partial class Form1 : Form
    {
        private Bitmap bmp;
        private Graphics graphics;       
        int xDD = 0;
        int yDD = 0;
        int xDM = 0;
        int yDM = 0;
        int xS = 0;
        int yS = 0;
        int xF = 0;
        int yF = 0;
        int x1 = 0;
        int y1 = 0;
        int x2 = 0;
        int y2 = 0;
        private Storage<Shape> storage;

        bool savepicture = false;
        PointF pointX;
        PointF pointY;
        bool distall = true;       
        Font font = new Font("Arial", 15);
        Brush brush = Brushes.Black;
        bool resizex = true;
        bool resizey = false;
        bool constX = true;
        bool constY = false;
        bool checks = false;
        bool room = true;
        bool wall = false;
        bool window = false;
        bool door = false;
        bool arrow = false;
        Pen RoomsPen = new Pen(Color.Black, 5);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
             pictureBox.Image = pictureBox.BackgroundImage;
            storage = new Storage<Shape>();
            graphics = pictureBox.CreateGraphics();
            pictureBox.MouseWheel += ResizeCurrent;
            bmp = new Bitmap(pictureBox.BackgroundImage,pictureBox.Width, pictureBox.Height);            
        }
        private void ResizeCurrent(object Sender, MouseEventArgs e)
        {
            if (storage.GetCapacity() == 0)
                return;
            var obj = storage.Current.Data;            
                int radDeg = ((int) nud_rotatedeg.Value);
            if (e.Delta > 0)
            {               
                if (resizex)
                    obj.resizeX(radDeg, radDeg);
                else obj.resizeY(radDeg, radDeg);
            }
            if (e.Delta < 0)
                if (resizex)
                obj.resizeX(-radDeg, -radDeg);
            else obj.resizeY(-radDeg, -radDeg);
            Paint();
        }
        public new void Paint()
        {            
            label1.Text = "Количество фигур: " + storage.GetCapacity();
            graphics = Graphics.FromImage(bmp);
            graphics.Clear(pictureBox.BackColor);           
            if(!checkBox4.Checked)
            {
               // pictureBox.BackColor = Color.White;
               // bmp = new Bitmap(pictureBox.BackgroundImage,pictureBox.Width, pictureBox.Height);
            }
            if (savepicture)
            {
                checkBox2.Checked = false;
                savepicture = false;
                if (checkBox4.Checked == true)   
                {
                    checkBox4.Checked = false;
                    int a = 0;
                    while (a < pictureBox.Width)
                    {
                        graphics.DrawLine(new Pen(Color.LightGray, 3), a, 0, a, pictureBox.Height);
                        graphics.DrawLine(new Pen(Color.LightGray, 3), 0, a, pictureBox.Width, a);
                        a += 25;
                    }
                    a = 0;
                    while (a < pictureBox.Width)
                    {
                        graphics.DrawLine(new Pen(Color.LightGray, 1), a, 0, a, pictureBox.Height);
                        graphics.DrawLine(new Pen(Color.LightGray, 1), 0, a, pictureBox.Width, a);
                        a += 5;
                    }
                    a = 0;
                    Font font1 = new Font("Arial", 8);
                    Brush brush1 = Brushes.Black;
                    while (a < pictureBox.Width)
                    {
                        a += 50;
                        graphics.DrawLine(new Pen(Color.DimGray, 3), a, 0, a, 5);
                        if (a < 100)
                            pointX = new PointF(a - 8, 5);
                        if ((a < 1000) && (a >= 100))
                            pointX = new PointF(a - 10, 5);
                        if (a >= 1000)
                            pointX = new PointF(a - 12, 5);
                        graphics.DrawString(a.ToString(), font1, brush1, pointX);
                        pointY = new PointF(5, a - 7);
                        graphics.DrawLine(new Pen(Color.DimGray, 3), 0, a, 5, a);
                        if (a == 750)
                            continue;
                        graphics.DrawString(a.ToString(), font1, brush1, pointY);
                    }
                }
                else
                    pictureBox.BackColor = Color.White;
            }
            if (storage.GetCapacity() != 0)
            {
                for (storage.IFirst(); !storage.IIsEOL(); storage.INext())
                {                    
                    if (storage.IObject == storage.Current)
                    {
                        storage.IObject.Data.Draw(graphics);
                        if(checkBox2.Checked)
                        storage.IObject.Data.DrawFrame(graphics);
                        if (checks == false)
                        {
                            label8.Text = "X1 = " + storage.IObject.Data.X.ToString();
                            label3.Text = "Y1 = " + storage.IObject.Data.Y.ToString();
                            label9.Text = "X2 = " + storage.IObject.Data.X1.ToString();
                            label7.Text = "Y2 = " + storage.IObject.Data.Y1.ToString();
                        }
                        if ((storage.IObject.Data.X.ToString() == storage.IObject.Data.X1.ToString())||(storage.IObject.Data.Y.ToString() == storage.IObject.Data.Y1.ToString()))
                        {
                            radioButton7.Checked = false;
                            radioButton7.Enabled = false;
                            radioButton6.Checked = true;
                        }
                        else
                        {
                            radioButton7.Enabled = true;
                        }
                    }
                    else
                    {
                        storage.IObject.Data.Draw(graphics);
                    }
                    if (storage.IObject.isDistinguishVertex)
                    {
                        storage.IObject.Data.Fill(graphics);                        
                    }
                }
            }            
            pictureBox.Image = bmp;
        }
        private Storage<Shape>.Node getObjectOn(int x, int y)
        {
            for (storage.IFirst(); storage.GetCapacity() != 0 && !storage.IIsEOL(); storage.INext())
            {
                var obj = storage.IObject;
                var dx = x;
                var dy = y;
                //if (Math.Sqrt(dx*dx + dy*dy) < obj.Data.Radius)
                if ((((dx < storage.IObject.data.X1 + 3) && (dx > storage.IObject.data.X - 3)) && ((dy < storage.IObject.data.Y + 3) && (dy > storage.IObject.data.Y - 3))) ||
                        (((dx < storage.IObject.data.X1 + 3) && (dx > storage.IObject.data.X - 3)) && ((dy < storage.IObject.data.Y1 + 3) && (dy > storage.IObject.data.Y1 - 3))) ||
                        (((dx < storage.IObject.data.X + 3) && (dx > storage.IObject.data.X - 3)) && ((dy < storage.IObject.data.Y1 + 3) && (dy > storage.IObject.data.Y - 3))) ||
                        (((dx < storage.IObject.data.X1 + 3) && (dx > storage.IObject.data.X1 - 3)) && ((dy < storage.IObject.data.Y1 + 3) && (dy > storage.IObject.data.Y - 3))))
                    return obj;
            }
            return null;
        }
        private void очиститьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            storage.Clear();
            Paint();
        }               
        private void управлениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Переход к следующей фигуре \tСтрелка вправо\n" +
            "Переход к предыдущей фигуре \tСтрелка влево\n" +
            "Уменьшить размер \t\t\tКолесико миши вниз\n" +
            "Увеличить размер \t\t\tКолесико миши вверх\n" +           
            "Удалить текущую \t\t\tDelete\n" +
            "Фиксация для рисования квадрата \tLeftShift\n" +
            "Переместить вправо \t\t\"D\"\n" +
            "Переместить вниз \t\t\t\"S\"\n" +
            "Переместить влево \t\t\t\"A\"\n" +
            "Переместить вверх \t\t\t\"W\"\n" +
            "Выделить фигуру \t\t\tПробел\n" +
            "Выделить все \t\t\t\"-\"\n" +
            "Создать композицию \t\t\"+\"\n",
            "Элементы управления");
        }        
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!pictureBox.Focused && e.KeyCode == Keys.Escape)
                pictureBox.Focus();
            //Фокус на поле ввода - что-то вводят => ничего не трогаем
           // else if (!pictureBox.Focused) return;
            switch (e.KeyCode)
            {
                case Keys.ShiftKey:
                    {
                        if (checkBox1.Checked)
                            checkBox1.Checked = false;
                        else
                            checkBox1.Checked = true;                                                                    
                        break;
                    }
            }
            int d = (int) nud_dxdy.Value;
            int b = (int)nud_rotatedeg.Value;
            if (storage.GetCapacity() == 0)
                return;
            var obj = storage.Current.Data;
            switch (e.KeyCode)
            {                
                case Keys.A:
                    //Влево
                    obj.Move(-d, 0);
                    break;
                case Keys.S:
                    //Вниз
                    obj.Move(0, d);
                    break;
                case Keys.D:
                    //Вправо
                    obj.Move(d, 0);
                    break;
                case Keys.W:
                    //Вверх
                    obj.Move(0, -d);
                    break;               
                case Keys.Oemplus:
                    {
                        удалитьВыделенныеToolStripMenuItem1_Click( sender,  e);
                    }
                    break;
                case Keys.OemMinus:
                    {
                        if (distall)
                        {
                            выделитьВсеToolStripMenuItem_Click(sender, e);
                            distall = false;
                        }
                        else
                        {
                            сброситьВыделениеToolStripMenuItem_Click(sender, e);
                            distall = true;
                        }
                        break;
                    }                   
                case Keys.Delete:
                    {
                        if (storage.Current.Data.N == 10)
                        {
                            Shape obj1 = storage.Current.Data;
                            for (int i = 0;i< obj1.getCount(); i++)
                               storage.Add( obj.name());
                        }
                        storage.DeleteDistinguished();
                    }
                    break;
                case Keys.Left:
                    storage.goBack();
                    break;
                case Keys.Right:
                    storage.goFront();
                    break;
                case Keys.Space:
                    storage.Current.isDistinguishVertex = !storage.Current.isDistinguishVertex;
                    break;
            }                     
            Paint();
        }      
        private void сброситьВыделениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (storage.IFirst(); !storage.IIsEOL(); storage.INext())
                storage.IObject.isDistinguishVertex = false;
            Paint();
        }
        private void удалитьВыделенныеToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int X2 = -10000;
            int Y2 = -10000;
            int X1 = 10000;
            int Y1 = 10000;
            for (storage.IFirst(); !storage.IIsEOL(); storage.INext())
            {
                if (storage.IObject.isDistinguishVertex)
                {
                    if (storage.IObject.Data.X - storage.IObject.Data.Radius > X2)
                        X2 = storage.IObject.Data.X ;
                    if (storage.IObject.Data.Y - storage.IObject.Data.Radius > Y2)
                        Y2 = storage.IObject.Data.Y ;
                    if (storage.IObject.Data.X - storage.IObject.Data.Radius < X1)
                        X1 = storage.IObject.Data.X ;
                    if (storage.IObject.Data.Y - storage.IObject.Data.Radius < Y1)
                        Y1 = storage.IObject.Data.Y ;
                }
            }
            Shape obj;
            int Radius = 40;
            int n = 10;
            int degree = 0;            
           obj = new Composite(X1,Y1, X2, Y2, Radius, n, degree);
            for (storage.IFirst(); !storage.IIsEOL(); storage.INext())
            {                
                if (storage.IObject.isDistinguishVertex)
                {
                    obj.addComponent(storage.IObject.Data, storage.IObject.Data.X, storage.IObject.Data.Y, storage.IObject.Data.Radius,
                        storage.IObject.Data.Fcolor, storage.IObject.Data.Scolor, storage.IObject.Data.N, storage.IObject.Data.Degree,  X1, Y1,X2,Y2);
                }
            }
            storage.Add(obj);
            storage.Current = storage.Tail;
            storage.DeleteDistinguished();
            Paint();
        }          
        private void выделитьВсеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (storage.GetCapacity() == 0)
                return;
            for (storage.IFirst(); !storage.IIsEOL(); storage.INext())
                storage.IObject.isDistinguishVertex = true;
            Paint();
        }
        private void информацияОПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Название работы: Редактор плана помещения\nВариант работы: 1\nГруппа: ПРО - 215\nСтудент: Акушев Антон\nВерсия: 16.01.18", "Расчетно графическая работа");
        }       
        private void удалитьВыделенныеToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            storage.DeleteDistinguished();
            Paint();
        }        
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //save            
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {
                    myStream.Close();
                    storage.Save(saveFileDialog1.FileName);
                }
            }
        }
        private void pep(Shape obj)
        {
            obj = storage.IObject.Data;
            for (int i = 0; i < obj.Degree; i++)
            {
                storage.INext();
                obj.addComponent(storage.IObject.Data, storage.IObject.Data.X - storage.IObject.Data.Radius, storage.IObject.Data.Y - storage.IObject.Data.Radius, storage.IObject.Data.Radius,
                    storage.IObject.Data.Fcolor, storage.IObject.Data.Scolor, storage.IObject.Data.N, storage.IObject.Data.Degree, storage.IObject.Data.X1, storage.IObject.Data.Y1, storage.IObject.Data.X2, storage.IObject.Data.Y2);
                storage.IObject.isDistinguishVertex = true;
                if (storage.IObject.Data.N == 10)
                    pep(storage.IObject.Data);
            }
        }
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            bool check = false;
            //load       
            Stream myStream;
            OpenFileDialog openFileDialog = new OpenFileDialog();           
            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
           
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                storage.Clear();
                if ((myStream = openFileDialog.OpenFile()) != null)
                {                    
                    myStream.Close();                   
                    storage.Load(openFileDialog.FileName, "oop_7");                    
                }
            }
            if (storage.GetCapacity() == 0)
                return;
            for (storage.IFirst(); !storage.IIsEOL(); storage.INext())
                if (storage.IObject.Data.N == 10)
                {
                    pep(storage.IObject.Data);
                    check = true;
                }           
            Paint();
            if (check == true)
            storage.DeleteDistinguished();
            выделитьВсеToolStripMenuItem_Click(sender, e);           
            сброситьВыделениеToolStripMenuItem_Click(sender, e);
            Paint();
        }
        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
             room = true;
             wall = false;
             window = false;
             door = false;
             arrow = false;
        }
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
             room = false;
             wall = true;
             window = false;
             door = false;
             arrow = false;
        }
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
             room = false;
             wall = false;
             window = true;
             door = false;
             arrow = false;
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
             room = false;
             wall = false;
             window = false;
             door = true;
             arrow = false;
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
             room = false;
             wall = false;
             window = false;
             door = false;
             arrow = true;
        }
        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox.Focus();
            label8.Text = "X1 = " + (e.X / 5 * 5).ToString();
            label3.Text = "Y1 = " + (e.Y / 5 * 5).ToString();
            label9.Text = "X2 = 0";
            label7.Text = "Y2 = 0";
            checks = true;
            xS = e.X/5*5;
            yS = e.Y/5*5;
            pictureBox.Focus();
            var obj = getObjectOn(e.X, e.Y);
            if (e.Button == MouseButtons.Left)
                if (obj != null)
                    storage.Current = obj;

            if (e.Button == MouseButtons.Right)
                if (obj != null)
                    obj.isDistinguishVertex = !obj.isDistinguishVertex;
            Paint();
        }
        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            xF = e.X/5*5;
            yF = e.Y/5*5;
            if (checkBox3.Checked)
            {
                label5.Enabled = true;
                label6.Enabled = true;
                label5.Text = "X = " + e.X.ToString();
                label6.Text = "Y = " + e.Y.ToString();
                label9.Text = "X2 = " + (e.X/5*5).ToString();
                label7.Text = "Y2 = " + (e.Y/5*5).ToString();
            }
            else
            {
                label5.Enabled = false;
                label6.Enabled = false;
            }
            Paint();
            if (checkBox5.Checked)
            {
                graphics.DrawLine(new Pen(Color.Black, 1), e.X, 9999, e.X, -9999);
                graphics.DrawLine(new Pen(Color.Black, 1), 9999, e.Y, -9999, e.Y);
            }            
            if (checks)
            {
                if (checkBox6.Checked)
                {
                    if (Math.Abs(xS - xF) < Math.Abs(yS - yF))
                    {
                        radioButton9.Checked = true;
                        radioButton10.Checked = false;
                    }
                    else
                    {
                        radioButton9.Checked = false;
                        radioButton10.Checked = true;
                    }                    
                }                
                if (room == true)
                {
                    if (xF < xS)
                    {
                        x1 = xF;
                        x2 = xS;
                    }
                    else
                    {
                        x1 = xS;
                        x2 = xF;
                    }

                    if (yF < yS)
                    {
                        y1 = yF;
                        y2 = yS;
                    }
                    else
                    {
                        y1 = yS;
                        y2 = yF;
                    }
                    if (checkBox1.Checked)
                    {
                        graphics.DrawRectangle(RoomsPen, x1, y1, Math.Abs(x2 - x1), Math.Abs(x2 - x1));
                        pointY = new PointF(x1 + Math.Abs(x2 - x1) + 25, y1 + Math.Abs(x2 - x1) - 30);
                        graphics.DrawString(Math.Abs(x2 - x1).ToString(), font, brush, pointY);
                        pointX = new PointF(x1 + Math.Abs(x2 - x1) - 30, y1 + Math.Abs(x2 - x1) + 25);
                        graphics.DrawString(Math.Abs(x2 - x1).ToString(), font, brush, pointX);
                    }
                    else
                    {
                        graphics.DrawRectangle(RoomsPen, x1, y1, Math.Abs(x2 - x1), Math.Abs(y2 - y1));
                        pointY = new PointF(x2 + 25, y2 - 30);
                        graphics.DrawString(Math.Abs(y1 - y2).ToString(), font, brush, pointY);
                        pointX = new PointF(x2 - 30, y2 + 25);
                        graphics.DrawString(Math.Abs(x1 - x2).ToString(), font, brush, pointX);
                    }
                }

                if (wall == true)
                {                    
                    if (constX == true)
                    {
                        graphics.DrawLine(RoomsPen, xS, yS, xF, yS);
                    }
                    if (constY == true)
                    {
                        graphics.DrawLine(RoomsPen, xS, yS, xS, yF);
                    }
                }
                if (window == true)
                {
                    if (constX == true)
                    {
                        graphics.DrawLine(new Pen(Color.LightBlue, 5), xS, yS, xF, yS);
                    }
                    if (constY == true)
                    {
                        graphics.DrawLine(new Pen(Color.LightBlue, 5), xS, yS, xS, yF);
                    }
                }
                if (door == true)
                {
                    if (constX == true)
                    {
                        graphics.DrawLine((new Pen(Color.White, 5)), xS, yS, xF, yS);
                        double ugol = Math.Atan2(xS - xF, yS - yS);
                        int X = Convert.ToInt32(xF +  Math.Sin(ugol));
                        int Y = Convert.ToInt32(yS +  Math.Cos(ugol));
                        graphics.DrawLine((new Pen(Color.LightBlue, 3)), X, Y, Convert.ToInt32(X + (Math.Abs(xS-xF)) * Math.Sin(0.3 + ugol)), Convert.ToInt32(Y + (Math.Abs(xS - xF)) * Math.Cos(0.3 + ugol)));
                    }
                    if (constY == true)
                    {
                        graphics.DrawLine((new Pen(Color.White, 5)), xS, yS, xS, yF);
                        double ugol = Math.Atan2(xS - xS, yS - yF);
                        int X = Convert.ToInt32(xS + Math.Sin(ugol));
                        int Y = Convert.ToInt32(yF + Math.Cos(ugol));
                        graphics.DrawLine((new Pen(Color.LightBlue, 3)), X, Y, Convert.ToInt32(X + (Math.Abs(yS - yF)) * Math.Sin(0.3 + ugol)), Convert.ToInt32(Y + (Math.Abs(yS - yF)) * Math.Cos(0.3 + ugol)));
                    }
                }
                 if (arrow == true)
                {
                    if (constX == true)
                    {                        
                        double ugol = Math.Atan2(xS - xF, yS - yS);
                        int X = Convert.ToInt32(xF +  Math.Sin(ugol));
                        int Y = Convert.ToInt32(yS +  Math.Cos(ugol));
                        int X1 = Convert.ToInt32(xS + Math.Sin(ugol));
                        int Y1 = Convert.ToInt32(yS + Math.Cos(ugol));
                        graphics.DrawLine((new Pen(Color.Black, 3)), X, Y, Convert.ToInt32(X + 30 * Math.Sin(0.3 + ugol)), Convert.ToInt32(Y + 30 * Math.Cos(0.3 + ugol)));
                        graphics.DrawLine((new Pen(Color.Black, 3)), X, Y, Convert.ToInt32(X + 30 * Math.Sin(-0.3 + ugol)), Convert.ToInt32(Y + 30 * Math.Cos(-0.3 + ugol)));
                        graphics.DrawLine((new Pen(Color.Black, 3)), X1, Y1, Convert.ToInt32(X1 + 30 * Math.Sin(2.85 + ugol)), Convert.ToInt32(Y1 + 30 * Math.Cos(2.85 + ugol)));
                        graphics.DrawLine((new Pen(Color.Black, 3)), X1, Y1, Convert.ToInt32(X1 + 30 * Math.Sin(-2.85 + ugol)), Convert.ToInt32(Y1 + 30 * Math.Cos(-2.85 + ugol)));
                        graphics.DrawLine((new Pen(Color.Black, 3)), xS, yS, xF, yS);
                        if (xS < xF)
                            pointX = new PointF((xS + (Math.Abs(xS-xF))/2), yS + 5);
                        else pointX = new PointF((xS - (Math.Abs(xS - xF)) / 2) , yS + 5);
                        graphics.DrawString(Math.Abs(xS - xF).ToString(), font, brush, pointX);
                    }
                    if (constY == true)
                    {
                        double ugol = Math.Atan2(xS - xS, yS - yF);
                        int X = Convert.ToInt32(xS + Math.Sin(ugol));
                        int Y = Convert.ToInt32(yF + Math.Cos(ugol));
                        int X1 = Convert.ToInt32(xS + Math.Sin(ugol));
                        int Y1 = Convert.ToInt32(yS + Math.Cos(ugol));
                        graphics.DrawLine((new Pen(Color.Black, 3)), X, Y, Convert.ToInt32(X + 30 * Math.Sin(0.3 + ugol)), Convert.ToInt32(Y + 30 * Math.Cos(0.3 + ugol)));
                        graphics.DrawLine((new Pen(Color.Black, 3)), X, Y, Convert.ToInt32(X + 30 * Math.Sin(-0.3 + ugol)), Convert.ToInt32(Y + 30 * Math.Cos(-0.3 + ugol)));
                        graphics.DrawLine((new Pen(Color.Black, 3)), X1, Y1, Convert.ToInt32(X1 + 30 * Math.Sin(2.85 + ugol)), Convert.ToInt32(Y1 + 30 * Math.Cos(2.85 + ugol)));
                        graphics.DrawLine((new Pen(Color.Black, 3)), X1, Y1, Convert.ToInt32(X1 + 30 * Math.Sin(-2.85 + ugol)), Convert.ToInt32(Y1 + 30 * Math.Cos(-2.85 + ugol)));
                        graphics.DrawLine((new Pen(Color.Black, 3)), xS, yS, xS, yF);
                        if(yS<yF)
                        pointY = new PointF(xS + 5,(yS + (Math.Abs(yS - yF)) / 2));
                        else pointY = new PointF(xS + 5, (yS - (Math.Abs(yS - yF)) / 2));
                        graphics.DrawString(Math.Abs(yS - yF).ToString(), font, brush, pointY);
                    }
                }                
            }
        }
        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            label9.Text = "X2 = " + (e.X / 5 * 5).ToString();
            label7.Text = "Y2 = " + (e.Y / 5 * 5).ToString();
            checks = false;
            xF = e.X/5*5;
            yF = e.Y/5*5;
            if ((radioButton10.Checked) && (xS == xF))
                return;
            if ((radioButton9.Checked) && (yS == yF))
                return;
            if ((radioButton5.Checked) && (yS == yF))
                return;
            int a = 0;
            if ((xF < xS)&& (radioButton10.Checked))
            {
                a = xS;
                xS = xF;
                xF = a;
            }
            if ((yF < yS)&& (radioButton9.Checked))
            {
                a = yS;
                yS = yF;
                yF = a;
            }

            {
                Shape obj;
                if (room == true)
                {
                    if (checkBox1.Checked)
                        y2 = y1 + (Math.Abs(x1 - x2));
                    obj = new Square(x1, y1, x2, y2, 20, 0, 10);
                    storage.Add(obj);
                    storage.Current = storage.Tail;
                    return;
                }
                int R;
                if (wall == true)
                {
                    obj = new Line(xS, yS, xF, yF, 0, -1, 10);
                    if (constX == true)
                    {
                        yF = yS;
                        obj = new Line(xS, yS, xF, yF, 1, -1, 10);
                    }
                    if (constY == true)
                    {
                        xF = xS;
                        obj = new Line(xS, yS, xF, yF, -1, -1, 10);
                    }
                    storage.Add(obj);
                    storage.Current = storage.Tail;
                }

                if (window == true)
                {
                    obj = new Line(xS, yS, xF, yF, 0, 0, 10);
                    if (constX == true)
                    {
                        yF = yS;
                        obj = new Line(xS, yS, xF, yF, 1, 0, 10);
                    }
                    if (constY == true)
                    {
                        xF = xS;
                        obj = new Line(xS, yS, xF, yF, -1, 0, 10);
                    }
                    storage.Add(obj);
                    storage.Current = storage.Tail;
                }
                if (door == true)
                {
                    obj = new Line(xS, yS, xF, yF, 0, 1, -1);
                    if (constX == true)
                    {
                        yF = yS;
                        obj = new Line(xS, yS, xF, yF, 1, 1, -1);
                    }
                    if (constY == true)
                    {
                        xF = xS;
                        obj = new Line(xS, yS, xF, yF, -1, 1, 1);
                    }
                    storage.Add(obj);
                    storage.Current = storage.Tail;
                }
                if (arrow == true)
                {
                    obj = new Line(xS, yS, xF, yF, 0, 1, -1);
                    if (constX == true)
                    {
                        yF = yS;
                        obj = new Line(xS, yS, xF, yF, 1, 3, -1);
                    }
                    if (constY == true)
                    {
                        xF = xS;
                        obj = new Line(xS, yS, xF, yF, -1, 3, 1);
                    }                    
                    storage.Add(obj);
                    storage.Current = storage.Tail;
                }
                Paint();
            }
               
            //graphics.DrawRectangle(RoomsPen, xS, yS, Math.Abs(xF - xS),Math.Abs(yF - yS));
            //Paint();
        }
        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
             constX = true;
             constY = false;
        }
        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
             constX = false;
             constY = true;
        }
        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
             resizex = true;
             resizey = false;
        }
        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            resizex = false;
            resizey = true;
        }
        private void сохранитьПланПомещенияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            savepicture = true;
            Paint();
            if (pictureBox.Image != null)
            {
                SaveFileDialog savedialog = new SaveFileDialog();
                savedialog.Title = "Сохранить картинку как...";
                savedialog.OverwritePrompt = true;
                savedialog.CheckPathExists = true;
                savedialog.Filter = "Image Files(*.BMP)|*.BMP|Image Files(*.JPG)|*.JPG|Image Files(*.GIF)|*.GIF|Image Files(*.PNG)|*.PNG|All files (*.*)|*.*";
                savedialog.ShowHelp = true;
                if (savedialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        pictureBox.Image.Save(savedialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    catch
                    {
                        MessageBox.Show("Невозможно сохранить изображение", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            
        }
            checkBox4.Checked = true;
            checkBox2.Checked = true;
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            Paint();
        }
        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
           if(!checkBox4.Checked)
                pictureBox.BackColor = Color.White;
           else pictureBox.BackColor = Color.Transparent;
            Paint();
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked)
            {
                radioButton9.Enabled = false;
                radioButton10.Enabled = false;
            }
            else
            {
                radioButton9.Enabled = true;
                radioButton10.Enabled = true;
            }
        }

    }
}