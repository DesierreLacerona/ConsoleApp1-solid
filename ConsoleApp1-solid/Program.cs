using System;
using System.Collections.Generic;

namespace ConsoleApp1_solid
{
    // SRP: Book class holds book details (Single Responsibility Principle)
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public bool IsBorrowed { get; private set; }

        public void Borrow()
        {
            IsBorrowed = true;
        }

        public void Return()
        {
            IsBorrowed = false;
        }
    }

    // SRP: User class manages user details (Single Responsibility Principle)
    public class User
    {
        public string Name { get; set; }
    }

    // OCP: LibraryManager follows Open-Closed Principle - can be extended without modifying existing code
    public class LibraryManager
    {
        private readonly List<Book> _books = new List<Book>();

        public LibraryManager()
        {
            // Adding some default books
            _books.Add(new Book { Title = "The Hobbit", Author = "J.R.R. Tolkien" });
            _books.Add(new Book { Title = "Pride and Prejudice", Author = "Jane Austen" });
            _books.Add(new Book { Title = "Moby-Dick", Author = "Herman Melville" });
        }

        public void AddBook(Book book)
        {
            _books.Add(book);
            Console.WriteLine($"Book added: {book.Title} by {book.Author}");
        }

        public void BorrowBook(string title, User user)
        {
            Book book = _books.Find(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            if (book == null)
            {
                Console.WriteLine($"Book '{title}' not found.");
                return;
            }

            if (!book.IsBorrowed)
            {
                book.Borrow();
                Console.WriteLine($"{user.Name} borrowed '{book.Title}'");
            }
            else
            {
                Console.WriteLine($"'{book.Title}' is already borrowed.");
            }
        }

        public void ReturnBook(string title, User user)
        {
            Book book = _books.Find(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            if (book == null)
            {
                Console.WriteLine($"Book '{title}' not found.");
                return;
            }

            if (book.IsBorrowed)
            {
                book.Return();
                Console.WriteLine($"{user.Name} returned '{book.Title}'");
            }
            else
            {
                Console.WriteLine($"'{book.Title}' was not borrowed.");
            }
        }

        public void ListBooks()
        {
            Console.WriteLine("Available books:");
            foreach (var book in _books)
            {
                string status = book.IsBorrowed ? "(Borrowed)" : "(Available)";
                Console.WriteLine($"- {book.Title} by {book.Author} {status}");
            }
        }
    }

    // DIP
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("=============================");
            Console.WriteLine("   Welcome to the Library   ");
            Console.WriteLine("=============================\n");

            LibraryManager library = new LibraryManager();

            Console.Write("Enter your name: ");
            string userName = Console.ReadLine();
            User user = new User { Name = userName };

            while (true)
            {
                Console.WriteLine("\nOptions:");
                Console.WriteLine("1. List Books");
                Console.WriteLine("2. Borrow a Book");
                Console.WriteLine("3. Return a Book");
                Console.WriteLine("4. Add a Book");
                Console.WriteLine("5. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        library.ListBooks();
                        break;
                    case "2":
                        Console.Write("Enter book title to borrow: ");
                        string borrowTitle = Console.ReadLine();
                        library.BorrowBook(borrowTitle, user);
                        break;
                    case "3":
                        Console.Write("Enter book title to return: ");
                        string returnTitle = Console.ReadLine();
                        library.ReturnBook(returnTitle, user);
                        break;
                    case "4":
                        Console.Write("Enter book title: ");
                        string newTitle = Console.ReadLine();
                        Console.Write("Enter author: ");
                        string newAuthor = Console.ReadLine();
                        library.AddBook(new Book { Title = newTitle, Author = newAuthor });
                        break;
                    case "5":
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }
        }
    }
}
