using System;

namespace Interface_Segregation
{
    /// <summary>
    /// 4. Interface segregation principle
    /// A class should not be forced to implement methods it does not need
    /// </summary>
    public class Book
    {

    }

    //breaks it
    //public interface IActions
    //{
    //    public void Lend(Book b);
    //    public void Return(Book b);
    //    public void AddToShelf(Book b);
    //    public void Display(Book b);
    //    public void Archive(Book b);
    //}

    //public class OpenBook : IActions
    //{
    //    public void Lend(Book b)
    //    {

    //    }

    //    public void Return(Book b)
    //    {

    //    }

    //    public void AddToShelf(Book b)
    //    {

    //    }

    //    public void Display(Book b)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void Archive(Book b)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    public interface ILendable
    {
        public void Lend(Book b);
        public void Return(Book b);
        public void AddToShelf(Book b);
    }

    public class LendableBook : ILendable
    {
        public void Lend(Book b)
        {

        }

        public void Return(Book b)
        {

        }

        public void AddToShelf(Book b)
        {

        }
    }

    public interface ICollectible
    {
        public void Display(Book b);
        public void Archive(Book b);
    }

    public class CollectibleBook : ICollectible
    {
        public void Display(Book b)
        {

        }

        public void Archive(Book b)
        {

        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
