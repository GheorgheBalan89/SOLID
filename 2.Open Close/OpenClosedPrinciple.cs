using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenClosed
{
    /// <summary>
    /// 2. The open -closed principle
    /// States that a class should be open for extensions, but closed for modifications
    /// </summary>
    public class Enums
    {
        public enum Genre
        {
            PeriodDrama, Thriller, Crime
        };

    }

    public class Book
    {
        public string Name, Author;
        public DateTime PublishingDate;
        public int Copies;
        public Enums.Genre Genre;

        public Book(string name, string author, DateTime publishingDate, int copies, Enums.Genre genre)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Author = author ?? throw new ArgumentNullException(nameof(author));
            PublishingDate = publishingDate;
            Copies = copies;
            Genre = genre;
        }

        public override string ToString()
        {
            return $"Title: {Name}" +
                   $"{Environment.NewLine}" +
                   $"Author: {Author}" +
                   $"{Environment.NewLine}" +
                   $"Published at : {PublishingDate: MMMM dd yyyy} " +
                   $"{Environment.NewLine}" +
                   $"Copies: {Copies}" +
                   $" {Environment.NewLine}" +
                   $"Genre: {Genre} {Environment.NewLine}";
        }

    }

    //Find all books in a given genre
    //Find all books by author x


    public class BookFilter
    {
        //this works - but we would need to modify the class every time we want to change a filter or add a new one
        private readonly IEnumerable<Book> _books;

        public BookFilter(IEnumerable<Book> books)
        {
            _books = books;
        }
        public IEnumerable<Book> FilterByGenre(Enums.Genre genre)
        {
            return _books.Where(x => x.Genre == genre);
        }

        public IEnumerable<Book> FilterByAuthor(string author)
        {
            return _books.Where(x => x.Author.ToLower() == author.ToLower());
        }

        public IEnumerable<Book> FilterByAuthorAndGenre(Enums.Genre genre, string author)
        {
            return _books.Where(x => x.Genre == genre && x.Author.ToLower() == author.ToLower());
        }
    }


    //open-closed principle
    public interface ICondition<T>
    {
        bool IsSatisfied(T book);
    }

    public interface IFilter<T>
    {
        IEnumerable<T> Filter(ICondition<T> spec);
    }

    public class GenreCondition : ICondition<Book>
    {
        private readonly Enums.Genre genre;

        public GenreCondition(Enums.Genre genre)
        {
            this.genre = genre;
        }
        public bool IsSatisfied(Book book)
        {
            return book.Genre == genre;
        }
    }

    public class AuthorCondition : ICondition<Book>
    {
        private readonly string authorName;

        public AuthorCondition(string authorName)
        {
            this.authorName = authorName;
        }

        public bool IsSatisfied(Book book)
        {
            return book.Author.ToLower() == authorName.ToLower();
        }
    }


    public class DoubleCondition<T> : ICondition<T>
    {
        private readonly ICondition<T> firstCondition;
        private readonly ICondition<T> secondCondition;

        public DoubleCondition(ICondition<T> firstCondition, ICondition<T> secondCondition)
        {
            this.firstCondition = firstCondition ?? throw new ArgumentNullException(nameof(firstCondition));
            this.secondCondition = secondCondition ?? throw new ArgumentNullException(nameof(secondCondition));
        }
        public bool IsSatisfied(T type)
        {
            return firstCondition.IsSatisfied(type) && secondCondition.IsSatisfied(type);
        }
    }

    public class DynamicFilter : IFilter<Book>
    {
        private readonly IEnumerable<Book> _books;
        public DynamicFilter(IEnumerable<Book> books)
        {
            _books = books;
        }
        public IEnumerable<Book> Filter(ICondition<Book> condition)
        {
            return _books.Where(condition.IsSatisfied);
        }
    }

    public class OpenClosedPrinciple
    {
        static void Main(string[] args)
        {
            var books = new List<Book>()
            {
               new Book("Wuthering Heights", "Emily Bronte", new DateTime(1847, 12, 1), 10, Enums.Genre.PeriodDrama),
               new Book("Lord of the Flies", "William Golding", new DateTime(1954, 9, 17), 100, Enums.Genre.Thriller),
               new Book("Jude the Obscure", "Thomas Hardy", new DateTime(1895, 1, 1), 20, Enums.Genre.PeriodDrama),
               new Book("A Study in Scarlet", "Sir Arthur Conan Doyle", new DateTime(1887, 1, 1),50, Enums.Genre.Crime)
            };

            //get all books in genre x

            //var dramaBooks = new BookFilter(books).FilterByGenre(Enums.Genre.PeriodDrama).ToList();
            //var output = $"genre{Environment.NewLine}";
            //foreach (var book in dramaBooks)
            //{
            //    output += book + Environment.NewLine;
            //}

            ////filter by author and genre
            //var authorAndGenre = new BookFilter(dramaBooks).FilterByAuthorAndGenre(Enums.Genre.PeriodDrama, "emily bronte").ToList();
            //output += $"author and genre books {Environment.NewLine}";
            //foreach (var book in authorAndGenre)
            //{
            //    output += book + Environment.NewLine;
            //}


            var df = new DynamicFilter(books);
            var periodDrama = df.Filter(new GenreCondition(Enums.Genre.PeriodDrama));

            var output = $"genre{Environment.NewLine}";
            foreach (var book in periodDrama)
            {
                output += book + Environment.NewLine;
            }

            var authorAndGenre = df.Filter(new DoubleCondition<Book>(new GenreCondition(Enums.Genre.PeriodDrama), new AuthorCondition("emily bronte")));

            output += $"author and genre books {Environment.NewLine}";
            foreach (var book in authorAndGenre)
            {
                output += book + Environment.NewLine;
            }

            Console.WriteLine($"{output}");


        }
    }
}
