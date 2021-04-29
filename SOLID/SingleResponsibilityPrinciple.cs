using System;
using System.IO;

namespace SOLID
{
    ///What is solid
    /// Wikipedia definition: 
    /// "SOLID is a mnemonic acronym for five design principles intended to make software designs more understandable, flexible, and maintainable.
    /// The principles are a subset of many principles promoted by American software engineer and instructor Robert C. Martin" 
    /// <summary>
    /// 1. Single responsibility principle
    /// A class should not have more than 1 responsibilities 
    /// </summary>
    public class Book
    {
        public string Name, Author;
        public DateTime PublishingDate;
        public int Copies;

        public Book(string name, string author, DateTime publishingDate, int copies)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Author = author ?? throw new ArgumentNullException(nameof(author));
            PublishingDate = publishingDate;
            Copies = copies;
        }

        //public override string ToString()
        //{
        //    return $"Title: {Name}" +
        //           $"{Environment.NewLine}" +
        //           $"Author: {Author}" +
        //           $"{Environment.NewLine}" +
        //           $"Published at : {PublishingDate: MMMM dd yyyy} " +
        //           $"{Environment.NewLine}" +
        //           $"Copies: {Copies}" +
        //           $"{Environment.NewLine}";
        //}

        //breaks the SRP 
        //public void SaveBook(string fileName)
        //{
        //    File.WriteAllText(fileName, ToString());
        //}

        //public Book LoadDataFromUrl(Uri uri)
        //{
        //    //get the data
        //    return new Book("Lord of the Flies", "William Golding", new DateTime(1954, 9, 17), 100);
        //}
    }

    public class BookPersistence
    {
        public void SaveBookToFile(string file, Book book)
        {
            if (!File.Exists(file))
            {
                File.WriteAllText(file, book.ToString());
            }
            else
            {
                File.AppendAllText(file, book.ToString());
            }
            //throw new Exception("Could not write to file");
        }

    }

    public class SingleResponsibilityPrinciple
    {
        public static void Main(string[] args)
        {
            var book = new Book("Wuthering Heights", "Emily Bronte", new DateTime(1847, 12, 1), 10);
            var filename = @"C:\Projects\SOLID\books.txt";
            //book.SaveBook(filename);
            //Console.WriteLine(book.ToString());

            var bookP = new BookPersistence();
            bookP.SaveBookToFile(filename, book);
        }
    }
}
