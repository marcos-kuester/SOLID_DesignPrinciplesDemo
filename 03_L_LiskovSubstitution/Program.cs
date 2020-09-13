using System;
using static System.Console;

namespace _03_L_LiskovSubstitution
{
    public class Rectangle
    {
        public virtual int Width { get; set; }   // add virtual
        public virtual int Height { get; set; }  // add virtual

        public Rectangle()
        {
        }

        public Rectangle(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public override string ToString()
        {
            return $"Width: {Width} Height: {Height} Area: {Width * Height}";
        }
    }

    public class Square : Rectangle
    {
        // must use override (not new)
        public override int Width
        {
            set { base.Width = base.Height = value; }
        }

        // must use override (not new)
        public override int Height { get => base.Height; set => base.Height = base.Width = value; }

        public Square()
        { }

        public Square(int side)
        {
            Width = side;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Rectangle rec = new Rectangle(3, 2);
            WriteLine(rec.ToString());

            Square square1 = new Square();
            square1.Width = 3;
            WriteLine(square1.ToString());

            Rectangle square2 = new Square(4);
            WriteLine(square2.ToString());

            // Result
            // ---------------------------
            // Width: 3 Height: 2 Area: 6
            // Width: 3 Height: 3 Area: 9
            // Width: 4 Height: 4 Area: 16
        }
    }
}
