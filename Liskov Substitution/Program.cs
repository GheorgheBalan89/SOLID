using System;

namespace LiskovSubstitution
{
    /// <summary>
    /// 3. Liskov substitution principle (LSP)
    /// the child class should be perfectly substitutable for their parent class 
    /// </summary>

    //public class Rectangle
    //{
    //    public int Width { get; set; }
    //    public int Height { get; set; }

    //    public Rectangle()
    //    {

    //    }
    //    public Rectangle(int height, int width)
    //    {
    //        Height = height;
    //        Width = width;
    //    }

    //    public override string ToString()
    //    {
    //        return $"{nameof(Width)} : {Width}, {nameof(Height)}: {Height}";
    //    }
    //}

    //public class Square : Rectangle
    //{
    //    public int Width { get; set; }
    //    public int Height
    //    {
    //        set => base.Width = base.Height = value;
    //    }
    //}



    public class Rectangle
    {
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }

        public Rectangle()
        {

        }
        public Rectangle(int height, int width)
        {
            Height = height;
            Width = width;
        }

        public override string ToString()
        {
            return $"{nameof(Width)} : {Width}, {nameof(Height)}: {Height}";
        }
    }

    public class Square : Rectangle
    {
        public override int Width
        {
            set => base.Width = base.Height = value;
        }

        public override int Height
        {
            set => base.Width = base.Height = value;
        }
    }


    class Program
    {
        public static int Area(Rectangle r) => r.Width * r.Height;
        static void Main(string[] args)
        {
            Rectangle rc = new Rectangle(4, 6);
            Console.WriteLine($"{rc} has area {Area(rc)}");

            Rectangle sq = new Square();
            sq.Width = 4;
            Console.WriteLine($"{sq} has area {Area(sq)}");
        }
    }
}
