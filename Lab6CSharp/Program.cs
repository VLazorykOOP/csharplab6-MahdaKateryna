using System;
using System.Collections;
using System.Collections.Generic;

// Інтерфейс для можливості використання оператора foreach
public interface IPrintPublication
{
    void Show();
}

// Базовий клас для всіх друкованих видань
public class PrintPublication : IPrintPublication
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Year { get; set; }

    public PrintPublication(string title, string author, int year)
    {
        Title = title;
        Author = author;
        Year = year;
    }

    public virtual void Show()
    {
        Console.WriteLine($"Title: {Title}");
        Console.WriteLine($"Author: {Author}");
        Console.WriteLine($"Year: {Year}");
    }
}

// Клас для журналів
public class Magazine : PrintPublication
{
    public string Issue { get; set; }

    public Magazine(string title, string author, int year, string issue) : base(title, author, year)
    {
        Issue = issue;
    }

    public override void Show()
    {
        base.Show();
        Console.WriteLine($"Issue: {Issue}");
    }
}

// Клас для книг
public class Book : PrintPublication
{
    public string Genre { get; set; }

    public Book(string title, string author, int year, string genre) : base(title, author, year)
    {
        Genre = genre;
    }

    public override void Show()
    {
        base.Show();
        Console.WriteLine($"Genre: {Genre}");
    }
}

// Клас для підручників
public class Textbook : Book
{
    public string Subject { get; set; }

    public Textbook(string title, string author, int year, string genre, string subject) : base(title, author, year, genre)
    {
        Subject = subject;
    }

    public override void Show()
    {
        base.Show();
        Console.WriteLine($"Subject: {Subject}");
    }
}

// Реалізація інтерфейсу IEnumerable для можливості використання оператора foreach
public class Library : IEnumerable<IPrintPublication>
{
    private List<IPrintPublication> publications = new List<IPrintPublication>();

    public void AddPublication(IPrintPublication publication)
    {
        publications.Add(publication);
    }

    public IEnumerator<IPrintPublication> GetEnumerator()
    {
        return publications.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}


// Інтерфейс Товар
public interface IProduct
{
    string Name { get; set; }
    decimal Price { get; set; }
    void ShowInfo();
    bool IsExpired();
}

// Клас Продукт
public class Product : IProduct
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public DateTime ProductionDate { get; set; }
    public DateTime ExpiryDate { get; set; }

    public Product(string name, decimal price, DateTime productionDate, DateTime expiryDate)
    {
        Name = name;
        Price = price;
        ProductionDate = productionDate;
        ExpiryDate = expiryDate;
    }

    public void ShowInfo()
    {
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Price: {Price}");
        Console.WriteLine($"Production Date: {ProductionDate.ToShortDateString()}");
        Console.WriteLine($"Expiry Date: {ExpiryDate.ToShortDateString()}");
    }

    public bool IsExpired()
    {
        return DateTime.Now > ExpiryDate;
    }
}

// Клас Партія
public class Batch : IProduct
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public DateTime ProductionDate { get; set; }
    public DateTime ExpiryDate { get; set; }

    public Batch(string name, decimal price, int quantity, DateTime productionDate, DateTime expiryDate)
    {
        Name = name;
        Price = price;
        Quantity = quantity;
        ProductionDate = productionDate;
        ExpiryDate = expiryDate;
    }

    public void ShowInfo()
    {
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Price per unit: {Price}");
        Console.WriteLine($"Quantity: {Quantity}");
        Console.WriteLine($"Production Date: {ProductionDate.ToShortDateString()}");
        Console.WriteLine($"Expiry Date: {ExpiryDate.ToShortDateString()}");
    }

    public bool IsExpired()
    {
        return DateTime.Now > ExpiryDate;
    }
}

// Клас Комплект
public class Kit : IProduct
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public List<Product> Products { get; set; }

    public Kit(string name, decimal price, List<Product> products)
    {
        Name = name;
        Price = price;
        Products = products;
    }

    public void ShowInfo()
    {
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Price: {Price}");
        Console.WriteLine("Products:");
        foreach (var product in Products)
        {
            product.ShowInfo();
        }
    }

    public bool IsExpired()
    {
        foreach (var product in Products)
        {
            if (product.IsExpired())
            {
                return true;
            }
        }
        return false;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("\t\t\tTask1\n");
        // Створення об'єктів для тестування
        Magazine magazine = new Magazine("National Geographic", "Various Authors", 2024, "April");
        Book book = new Book("To Kill a Mockingbird", "Harper Lee", 1960, "Fiction");
        Textbook textbook = new Textbook("Introduction to Computer Science", "John Smith", 2020, "Educational", "Computer Science");

        // Виклик методу Show() для кожного об'єкта
        Console.WriteLine("Magazine:");
        magazine.Show();

        Console.WriteLine("\nBook:");
        book.Show();

        Console.WriteLine("\nTextbook:");
        textbook.Show();

        Console.WriteLine("\n\t\t\tTask2\n");

        List<IProduct> products = new List<IProduct>
        {
            new Product("Milk", 25.50m, new DateTime(2024, 3, 1), new DateTime(2024, 4, 1)),
            new Batch("Apples", 15.00m, 10, new DateTime(2024, 3, 15), new DateTime(2024, 4, 15)),
            new Kit("Fruit Basket", 100.00m, new List<Product>
            {
                new Product("Banana", 10.00m, new DateTime(2024, 3, 10), new DateTime(2024, 4, 10)),
                new Product("Apple", 5.00m, new DateTime(2024, 3, 10), new DateTime(2024, 4, 10))
            })
        };

        Console.WriteLine("All products:\n");
        foreach (var product in products)
        {
            product.ShowInfo();
            Console.WriteLine($"Is Expired: {(product.IsExpired() ? "Yes" : "No")}");
            Console.WriteLine();
        }

        Console.WriteLine("Expired products:");
        foreach (var product in products)
        {
            if (product.IsExpired())
            {
                product.ShowInfo();
                Console.WriteLine();
            }
        }

        Console.WriteLine("\n\t\t\tTask3\n");

        Library library = new Library();

        // Додавання друкованих видань до бібліотеки
        library.AddPublication(new Magazine("National Geographic", "Various Authors", 2024, "April"));
        library.AddPublication(new Book("To Kill a Mockingbird", "Harper Lee", 1960, "Fiction"));
        library.AddPublication(new Textbook("Introduction to Computer Science", "John Smith", 2020, "Educational", "Computer Science"));

        // Використання оператора foreach
        foreach (var publication in library)
        {
            publication.Show();
            Console.WriteLine();
        }
    }
}
