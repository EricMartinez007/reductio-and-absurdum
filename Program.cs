List<ProductType> productTypes = new List<ProductType>()
{
    new ProductType()
    {
        Id = 1,
        Name = "Apparel"
    },
    new ProductType()
    {
        Id = 2, 
        Name = "Potions"
    },
    new ProductType()
    {
        Id = 3,
        Name = "Enchanted Objects"
    },
    new ProductType()
    {
        Id = 4, 
        Name = "Wands"
    }
};

List<Product> products = new List<Product>()
{
    new Product()
    {
        Id = 1,
        Name = "Ravenclaw Robe",
        Price = 49.99M,
        IsAvailable = true,
        DateAdded = new DateTime(2024, 3, 12),
        ProductTypeId = 1
    },
    new Product()
    {
        Id = 2,
        Name = "Polyjuice Potion",
        Price = 199.99M,
        IsAvailable = true,
        DateAdded = new DateTime(2025, 1, 5),
        ProductTypeId = 2
    },
    new Product()
    {
        Id = 3,
        Name = "Voldi Horcrux",
        Price = 999.99M,
        IsAvailable = false,
        DateAdded = new DateTime(1991, 12, 25),
        ProductTypeId = 3
    },
    new Product()
    {
        Id = 4,
        Name = "Elder Wand",
        Price = 1000000.99M,
        IsAvailable = true,
        DateAdded = new DateTime(1803, 8, 1),
        ProductTypeId = 4
    }
};

Console.WriteLine("Welcome to Reductio & Absurdum!");
Console.WriteLine("EST. 1025 A.D.");

string choice = null;
while (choice != "0")
{
    Console.WriteLine(@"
Choose an option:
0. Exit
1. View All Products
2. View Products by Category
3. Add a Product
4. Delete a Product
5. Update a Product");
    
    choice = Console.ReadLine();

    if (choice == "0")
    {
        Console.WriteLine("You are hereby banished and you smell like cheese! I hope your spells misfire, mudblood!");
    }
    else if (choice == "1")
    {
        ViewAllProducts();
    }
    else if (choice == "2")
    {
        ViewProductsByCategory();
    }
    else if (choice == "3")
    {
        AddProduct();
    }
    else if (choice == "4")
    {
        DeleteProduct();
    }
    else if (choice == "5")
    {
        UpdateProduct();
    }
    else
    {
        Console.WriteLine("Please choose a valid option, mudblood");
    }
}

void ViewAllProducts()
{
    Console.WriteLine("All Products:");
    Console.WriteLine(string.Join("\n", products.Select((p, i) =>
        $"{i + 1}. {p.Name} - ${p.Price} - {(p.IsAvailable ? "Available" : "Not Available")} - {p.DaysOnShelf} days on shelf")));
}

void ViewProductsByCategory()
{
    Console.WriteLine("Product Categories:");
    Console.WriteLine(string.Join("\n", productTypes.Select((pt, i) =>
        $"{i + 1}. {pt.Name}")));

    ProductType chosenType = null;
    while (chosenType == null)
    {
        Console.WriteLine("Choose a category: ");
        try
        {
            int response = int.Parse(Console.ReadLine().Trim());
            chosenType = productTypes[response - 1];
        }
        catch (FormatException)
        {
            Console.WriteLine("Please type only integers!");
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("Please choose an existing category only!");
        }
    }

    List<Product> filteredProducts = products.Where(p => p.ProductTypeId == chosenType.Id).ToList();

    if (filteredProducts.Count == 0)
    {
        Console.WriteLine($"No products found in {chosenType.Name}");
        return;
    }

    Console.WriteLine($"{chosenType.Name}:");
    Console.WriteLine(string.Join("\n", filteredProducts.Select((p, i) =>
        $"{i + 1}. {p.Name} - ${p.Price} - {(p.IsAvailable ? "Available" : "Not Available")} - {p.DaysOnShelf} days on shelf")));
}

// Needed help from my dawg Claude, have not been exposed to adding a product in C# before
void AddProduct()
{
    Console.WriteLine("Enter the product name: ");
    string name = Console.ReadLine().Trim();

    decimal price = 0;
    while (price == 0)
    {
        Console.WriteLine("Enter the product price: ");
        try
        {
            price = decimal.Parse(Console.ReadLine().Trim());
        }
        catch (FormatException)
        {
            Console.WriteLine("Please enter a valid price!");
        }
    }

    Console.WriteLine("Product Categories:");
    Console.WriteLine(string.Join("\n", productTypes.Select((pt, i) =>
        $"{i + 1}. {pt.Name}")));

    ProductType chosenType = null;
    while (chosenType == null)
    {
        Console.WriteLine("Choose a category: ");
        try
        {
            int response = int.Parse(Console.ReadLine().Trim());
            chosenType = productTypes[response - 1];
        }
        catch (FormatException)
        {
            Console.WriteLine("Please type only integers!");
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("Please choose an existing category only!");
        }
    }

    int newId = products.Count > 0 ? products.Max(p => p.Id) + 1 : 1;

    products.Add(new Product()
    {
        Id = newId,
        Name = name,
        Price = price,
        IsAvailable = true,
        DateAdded = DateTime.Now,
        ProductTypeId = chosenType.Id
    });

    Console.WriteLine($"{name} has been added to the inventory!");
}

void DeleteProduct()
{
    ViewAllProducts();

    Product chosenProduct = null;
    while (chosenProduct == null)
    {
        Console.WriteLine("Enter the number of the product to delete: ");
        try
        {
            int response = int.Parse(Console.ReadLine().Trim());
            chosenProduct = products[response - 1];
        }
        catch (FormatException)
        {
            Console.WriteLine("Please type only integers!");
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("Please choose an existing product!");
        }
    }

    Console.WriteLine($"Are you sure you want to delete {chosenProduct.Name}? (y/n)");
    string confirm = Console.ReadLine().Trim().ToLower();

    if (confirm == "y")
    {
        products.Remove(chosenProduct);
        Console.WriteLine($"{chosenProduct.Name} has been removed from the inventory!");
    }
    else
    {
        Console.WriteLine("Delete cancelled.");
    }
}

void UpdateProduct()
{
    ViewAllProducts();

    Product chosenProduct = null;
    while (chosenProduct == null)
    {
        Console.WriteLine("Enter the number of the product to update: ");
        try
        {
            int response = int.Parse(Console.ReadLine().Trim());
            chosenProduct = products[response - 1];
        }
        catch (FormatException)
        {
            Console.WriteLine("Please type only integers!");
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("Please choose an existing product!");
        }
    }

    Console.WriteLine($"Editing {chosenProduct.Name}");

    decimal newPrice = 0;
    while (newPrice == 0)
    {
        Console.WriteLine($"Enter a new price (current price: ${chosenProduct.Price}): ");
        try
        {
            newPrice = decimal.Parse(Console.ReadLine().Trim());
        }
        catch (FormatException)
        {
            Console.WriteLine("Please enter a valid price!");
        }
    }

    Console.WriteLine($"Is this product available? (current availability: {(chosenProduct.IsAvailable ? "Available" : "Not Available")})");
    Console.WriteLine("1. Available");
    Console.WriteLine("2. Not Available");

    bool validAvailability = false;
    while (!validAvailability)
    {
        try
        {
            int response = int.Parse(Console.ReadLine().Trim());
            if (response == 1)
            {
                chosenProduct.IsAvailable = true;
                validAvailability = true;
            }
            else if (response == 2)
            {
                chosenProduct.IsAvailable = false;
                validAvailability = true;
            }
            else
            {
                Console.WriteLine("Please choose 1 or 2!");
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Please type only integers!");
        }
    }

    chosenProduct.Price = newPrice;
    Console.WriteLine($"{chosenProduct.Name} has been updated!");
}