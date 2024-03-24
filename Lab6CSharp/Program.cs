using System;
using System.Collections.Generic;

// Базовий клас "Друковане видання"
public abstract class PrintedPublication
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Year { get; set; }

    public PrintedPublication(string title, string author, int year)
    {
        Title = title;
        Author = author;
        Year = year;
    }

    public virtual void Show()
    {
        Console.WriteLine($"Назва: {Title}");
        Console.WriteLine($"Автор: {Author}");
        Console.WriteLine($"Рік видання: {Year}");
    }
}

// Похідний клас "Журнал"
public class Magazine : PrintedPublication
{
    public string Issue { get; set; }

    public Magazine(string title, string author, int year, string issue)
        : base(title, author, year)
    {
        Issue = issue;
    }

    public override void Show()
    {
        Console.WriteLine("Журнал:");
        base.Show();
        Console.WriteLine($"Випуск: {Issue}");
    }
}

// Похідний клас "Книга"
public class Book : PrintedPublication
{
    public int Pages { get; set; }

    public Book(string title, string author, int year, int pages)
        : base(title, author, year)
    {
        Pages = pages;
    }

    public override void Show()
    {
        Console.WriteLine("Книга:");
        base.Show();
        Console.WriteLine($"Сторінок: {Pages}");
    }
}

// Похідний клас "Підручник"
public class Textbook : Book
{
    public string Subject { get; set; }

    public Textbook(string title, string author, int year, int pages, string subject)
        : base(title, author, year, pages)
    {
        Subject = subject;
    }

    public override void Show()
    {
        Console.WriteLine("Підручник:");
        base.Show();
        Console.WriteLine($"Предмет: {Subject}");
    }
}

/// //////////////////////////////////////////////////////////////////////////////////////////

// Інтерфейс Товар
public interface IProduct
{
    string Name { get; set; }
    decimal Price { get; set; }
    DateTime ProductionDate { get; set; }
    DateTime ExpiryDate { get; set; }

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
        Console.WriteLine($"Назва: {Name}");
        Console.WriteLine($"Ціна: {Price}");
        Console.WriteLine($"Дата виробництва: {ProductionDate.ToShortDateString()}");
        Console.WriteLine($"Строк придатності: {ExpiryDate.ToShortDateString()}");
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
        Console.WriteLine($"Назва: {Name}");
        Console.WriteLine($"Ціна за одиницю: {Price}");
        Console.WriteLine($"Кількість: {Quantity}");
        Console.WriteLine($"Дата виробництва: {ProductionDate.ToShortDateString()}");
        Console.WriteLine($"Строк придатності: {ExpiryDate.ToShortDateString()}");
    }

    public bool IsExpired()
    {
        return DateTime.Now > ExpiryDate;
    }
}

// Клас Комплект
public class Set : IProduct
{
    private List<string> list;
    private decimal v;
    private DateTime dateTime1;
    private DateTime dateTime2;

    public string Name { get; set; }
    public decimal Price { get; set; }
    public DateTime ProductionDate { get; set; }
    public DateTime ExpiryDate { get; set; }
    public List<Product> Products { get; set; }

    public Set(string name, decimal price, DateTime productionDate, DateTime expiryDate, List<Product> products)
    {
        Name = name;
        Price = price;
        ProductionDate = productionDate;
        ExpiryDate = expiryDate;
        Products = products;
    }

    public Set(List<string> list, decimal v, DateTime dateTime1, DateTime dateTime2, List<Product> products)
    {
        this.list = list;
        this.v = v;
        this.dateTime1 = dateTime1;
        this.dateTime2 = dateTime2;
        Products = products;
    }

    public void ShowInfo()
    {
        Console.WriteLine($"Назва комплекту: {Name}");
        Console.WriteLine($"Загальна ціна: {Price}");
        Console.WriteLine($"Дата виробництва: {ProductionDate.ToShortDateString()}");
        Console.WriteLine($"Строк придатності: {ExpiryDate.ToShortDateString()}");
        Console.WriteLine("Склад:");
        foreach (var product in Products)
        {
            product.ShowInfo();
            Console.WriteLine("----------------------");
        }
    }

    public bool IsExpired()
    {
        foreach (var product in Products)
        {
            if (product.IsExpired())
                return true;
        }
        return DateTime.Now > ExpiryDate;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("\t\t\tTask 1 ");
        Magazine magazine = new Magazine("Моя Україна", "Василь Петров", 2022, "Січень");
        magazine.Show();

        Console.WriteLine();

        Book book = new Book("Таємниці природи", "Олена Коваленко", 2020, 320);
        book.Show();

        Console.WriteLine();

        Textbook textbook = new Textbook("Математика 10 клас", "Іван Сидоренко", 2019, 400, "Математика");
        textbook.Show();


        /////////////////////////////////////////////////////////////////////////////////////

        Console.WriteLine("\n\t\t\tTask 2 ");
        int n = 3;  // кількість товарів

        List<IProduct> products = new List<IProduct>
        {
            new Product("Молоко", 25.50m, new DateTime(2024, 3, 10), new DateTime(2024, 4, 10)),
            new Batch("Яблука", 15.00m, 10, new DateTime(2024, 3, 1), new DateTime(2024, 4, 1)),
            new Set(new List<string>{"Хліб", "Масло"}, 40.00m, new DateTime(2024, 3, 5), new DateTime(2024, 3, 15), new List<Product>
            {
                new Product("Хліб", 20.00m, new DateTime(2024, 3, 5), new DateTime(2024, 3, 15)),
                new Product("Масло", 20.00m, new DateTime(2024, 3, 3), new DateTime(2024, 3, 20))
            })
        };

        // Виведення інформації про всі товари
        foreach (var product in products)
        {
            product.ShowInfo();
            Console.WriteLine("\n ");
        }

        // Пошук прострочених товарів
        Console.WriteLine("Прострочені товари:");
        foreach (var product in products)
        {
            if (product.IsExpired())
            {
                product.ShowInfo();
                Console.WriteLine("\n ");
            }
        }
    }
}
