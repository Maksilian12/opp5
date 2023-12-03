using System;
using System.Collections.Generic;
using System.Linq;

// Клас, що представляє товар
class Product
{
    public string Name { get; set; }
    public double Price { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public int Rating { get; set; }

    public Product(string name, double price, string description, string category, int rating)
    {
        Name = name;
        Price = price;
        Description = description;
        Category = category;
        Rating = rating;
    }
}

// Клас, що представляє користувача
class User
{
    public string Username { get; set; }
    public string Password { get; set; }
    public List<Order> OrderHistory { get; }

    public User(string username, string password)
    {
        Username = username;
        Password = password;
        OrderHistory = new List<Order>();
    }
}

// Клас, що представляє замовлення
class Order
{
    public List<Product> Products { get; }
    public int Quantity { get; }
    public double TotalPrice { get; }
    public string Status { get; set; }

    public Order(List<Product> products, int quantity)
    {
        Products = products;
        Quantity = quantity;
        TotalPrice = products.Sum(p => p.Price) * quantity;
        Status = "Pending"; // Замовлення по замовчуванню має статус "Очікується"
    }
}

// Інтерфейс для пошуку товарів
interface ISearchable
{
    List<Product> SearchProducts(string category, double maxPrice, int minRating);
}

// Клас, що представляє магазин
class Shop : ISearchable
{
    private List<Product> products;
    private List<User> users;

    public Shop()
    {
        products = new List<Product>();
        users = new List<User>();
    }

    public void AddProduct(Product product)
    {
        products.Add(product);
    }

    public void RegisterUser(string username, string password)
    {
        users.Add(new User(username, password));
    }

    public void PlaceOrder(User user, List<Product> selectedProducts, int quantity)
    {
        if (users.Contains(user))
        {
            Order order = new Order(selectedProducts, quantity);
            user.OrderHistory.Add(order);
        }
        else
        {
            Console.WriteLine("User not found.");
        }
    }

    public List<Product> SearchProducts(string category, double maxPrice, int minRating)
    {
        return products.Where(p => p.Category == category && p.Price <= maxPrice && p.Rating >= minRating).ToList();
    }
}

class Program
{
    static void Main()
    {
        Shop shop = new Shop();

        // Додавання товарів до магазину
        shop.AddProduct(new Product("Laptop", 1000, "High-performance laptop", "Electronics", 4));
        shop.AddProduct(new Product("Smartphone", 500, "Latest smartphone model", "Electronics", 5));
        shop.AddProduct(new Product("Book", 20, "Bestselling novel", "Books", 4));

        // Реєстрація користувача
        shop.RegisterUser("user1", "password123");

        // Пошук товарів і розміщення замовлення
        User user = shop.users[0];
        List<Product> selectedProducts = shop.SearchProducts("Electronics", 800, 4);
        shop.PlaceOrder(user, selectedProducts, 2);

        // Виведення історії замовлень користувача
        Console.WriteLine($"Order history for {user.Username}:");
        foreach (Order order in user.OrderHistory)
        {
            Console.WriteLine($"Status: {order.Status}, Total Price: {order.TotalPrice}");
        }
    }
}
