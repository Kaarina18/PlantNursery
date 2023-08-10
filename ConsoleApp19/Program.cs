using System;
using System.Collections.Generic;
using System.Linq;
class Plant
{
    public string Name { get; set; }
    public string Species { get; set; }
    public int Age { get; set; }
    public double Height { get; set; }
    public double Price { get; set; }
}
class FloweringPlant : Plant
{
    public bool IsMonocot { get; set; }
}
class Customer
{
    public int PurchaseCount { get; set; }
}
class PlantNursery
{
    private List<Plant> plants = new List<Plant>();
    private List<Customer> customers = new List<Customer>();
    public void AddPlant(Plant plant)
    {
        plants.Add(plant);
    }
    public void RemovePlant(Plant plant)
    {
        plants.Remove(plant);
    }
    public double CalculateTotalRevenue()
    {
        return plants.Sum(plant => plant.Price);
    }
    public int CountFloweringPlants()
    {
        return plants.OfType<FloweringPlant>().Count();
    }
    public double CalculateAverageNonFloweringPrice()
    {
        var nonFloweringPlants = plants.OfType<Plant>().Where(plant => !(plant is
       FloweringPlant));
        return nonFloweringPlants.Any() ? nonFloweringPlants.Average(plant =>
       plant.Price) : 0;
    }
    public Plant FindCheapestPlant()
    {
        return plants.OrderBy(plant => plant.Price).FirstOrDefault();
    }
    public int CountPlantsBySpecies(string species)
    {
        return plants.Count(plant => plant.Species.Equals(species,
       StringComparison.OrdinalIgnoreCase));
    }
    public bool HasMonocotWithPrice(double price)
    {
        return plants.OfType<FloweringPlant>().Any(plant => plant.IsMonocot &&
       plant.Price == price);
    }
    public double CalculateBulkDiscount(double totalAmount, double certainAmount)
    {
        if (totalAmount > certainAmount)
        {
            return totalAmount * 0.1;
        }
        return 0;
    }
    public double CalculateLoyaltyDiscount(int customerPurchaseCount)
    {
        if (customerPurchaseCount >= 5)
        {
            return CalculateTotalRevenue() * 0.05;
        }
        return 0;
    }
    public void AddCustomerPurchase()
    {
        customers.Add(new Customer { PurchaseCount = 1 });
    }
    public int GetCustomerPurchaseCount()
    {
        return customers.Sum(customer => customer.PurchaseCount);
    }
    public Plant GetPlantByName(string name)
    {
        return plants.FirstOrDefault(plant => plant.Name.Equals(name,
       StringComparison.OrdinalIgnoreCase));
    }
    public double CalculateAverageAge()
    {
        if (plants.Any())
        {
            int totalAge = plants.Sum(plant => plant.Age);
            return (double)totalAge / plants.Count;
        }
        return 0;
    }
}
class Program
{
    static void Main(string[] args)
    {
        PlantNursery nursery = new PlantNursery();
        bool exitRequested = false;
        do
        {
            Console.WriteLine("===== Plant Nursery Management System =====");
            Console.WriteLine("1. Add Plant");
            Console.WriteLine("2. Calculate Statistics");
            Console.WriteLine("3. Exit");
            Console.Write("Select an option: ");
            string userInput = Console.ReadLine();
            switch (userInput)
            {
                case "1":
                    AddPlantMenu(nursery);
                    break;
                case "2":
                    ShowStatistics(nursery);
                    break;
                case "3":
                    exitRequested = true;
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        } while (!exitRequested);
    }
    static void AddPlantMenu(PlantNursery nursery)
    {
        Console.WriteLine("===== Add Plant =====");
        Console.Write("Enter plant name: ");
        string name = Console.ReadLine();
        Console.Write("Enter species: ");
        string species = Console.ReadLine();
        Console.Write("Enter age: ");
        int age = int.Parse(Console.ReadLine());
        Console.Write("Enter height: ");
        double height = double.Parse(Console.ReadLine());
        Console.Write("Enter price: ");
        double price = double.Parse(Console.ReadLine());
        Console.Write("Is the plant a flowering plant? (y/n): ");
        string floweringInput = Console.ReadLine();
        bool isFlowering = floweringInput.Equals("y",
       StringComparison.OrdinalIgnoreCase);
        if (isFlowering)
        {
            Console.Write("Is the flowering plant a monocot? (y/n): ");
            string monocotInput = Console.ReadLine();
            bool isMonocot = monocotInput.Equals("y",
           StringComparison.OrdinalIgnoreCase);
            nursery.AddPlant(new FloweringPlant
            {
                Name = name,
                Species = species,
                Age = age,
                Height = height,
                Price = price,
                IsMonocot = isMonocot
            });
        }
        else
        {
            nursery.AddPlant(new Plant
            {
                Name = name,
                Species = species,
                Age = age,
                Height = height,
                Price = price
            });
        }
        Console.WriteLine("Plant added successfully.");
    }
    static void ShowStatistics(PlantNursery nursery)
    {
        double totalRevenue = nursery.CalculateTotalRevenue();
        int floweringCount = nursery.CountFloweringPlants();
        double avgNonFloweringPrice =
       nursery.CalculateAverageNonFloweringPrice();
        Plant cheapestPlant = nursery.FindCheapestPlant();
        int rosaCount = nursery.CountPlantsBySpecies("Rosa");
        bool hasMonocot = nursery.HasMonocotWithPrice(5);
        double bulkDiscount = nursery.CalculateBulkDiscount(totalRevenue, 100);
        double loyaltyDiscount =
       nursery.CalculateLoyaltyDiscount(nursery.GetCustomerPurchaseCount());
        double avgAge = nursery.CalculateAverageAge();
        Console.WriteLine("===== Nursery Statistics =====");
        Console.WriteLine("Total Revenue: " + totalRevenue);
        Console.WriteLine("Flowering Count: " + floweringCount);
        Console.WriteLine("Average Price of Non-Flowering Plants: " +
       avgNonFloweringPrice);
        Console.WriteLine("Cheapest Plant: " + (cheapestPlant != null ?
       cheapestPlant.Name : "None"));
        Console.WriteLine("Number of Rosa Plants: " + rosaCount);
        Console.WriteLine("Has Monocot with Price $5: " + hasMonocot);
        Console.WriteLine("Bulk Discount: " + bulkDiscount);
        Console.WriteLine("Loyalty Discount: " + loyaltyDiscount);
        Console.WriteLine("Average Age of Plants: " + avgAge);
    }
}
class Nursery
{
    private static void Main(string[] args)
    {
        PlantNursery nursery = new PlantNursery();

        // Add sample plants
        nursery.AddPlant(new FloweringPlant { Name = "Rose", Species = "Rosa", Age = 6, Height = 45, Price = 60, IsMonocot = true });
        nursery.AddPlant(new FloweringPlant { Name = "Tulip", Species = "Tulipa", Age = 5, Height = 16, Price = 10, IsMonocot = false });
        nursery.AddPlant(new Plant { Name = "Fern", Species = "Pteridophyte", Age = 3, Height = 40, Price = 55 });

        // Simulate customer purchases
        for (int i = 0; i < 6; i++)
        {
            nursery.AddCustomerPurchase();
        }

        double totalRevenue = nursery.CalculateTotalRevenue();
        int floweringCount = nursery.CountFloweringPlants();
        double avgNonFloweringPrice = nursery.CalculateAverageNonFloweringPrice();
        Plant cheapestPlant = nursery.FindCheapestPlant();
        int rosaCount = nursery.CountPlantsBySpecies("Rosa");
        bool hasMonocot = nursery.HasMonocotWithPrice(5);
        double bulkDiscount = nursery.CalculateBulkDiscount(totalRevenue, 100);
        double loyaltyDiscount = nursery.CalculateLoyaltyDiscount(nursery.GetCustomerPurchaseCount());

        Console.WriteLine("Total Revenue: " + totalRevenue);
        Console.WriteLine("Flowering Count: " + floweringCount);
        Console.WriteLine("Average Price of Non-Flowering Plants: " + avgNonFloweringPrice);
        Console.WriteLine("Cheapest Plant: " + (cheapestPlant != null ? cheapestPlant.Name : "None"));
        Console.WriteLine("Number of Rosa Plants: " + rosaCount);
        Console.WriteLine("Has Monocot with Price $5: " + hasMonocot);
        Console.WriteLine("Bulk Discount: " + bulkDiscount);
        Console.WriteLine("Loyalty Discount: " + loyaltyDiscount);
        Console.WriteLine("Average Age of Plants: " + nursery.CalculateAverageAge());

        // Test GetPlantByName
        Console.WriteLine("Enter a plant name to get details:");
        string inputName = Console.ReadLine();
        Plant requestedPlant = nursery.GetPlantByName(inputName);
        if (requestedPlant != null)
        {
            Console.WriteLine($"Details of {requestedPlant.Name} - Species: {requestedPlant.Species}, Age: {requestedPlant.Age}, Height: {requestedPlant.Height}, Price: {requestedPlant.Price}");
        }
        else
        {
            Console.WriteLine("Plant not found.");
            while (true)

            {
                Console.Clear();

                Console.WriteLine("Plant Nursery Management System\n");

                Console.WriteLine("1. Display Results");

                Console.WriteLine("2. Search Plant by Name");

                Console.WriteLine("3. Exit");

                Console.Write("Select an option: ");



                string input = Console.ReadLine();



                switch (input)

                {

                    case "1":

                        DisplayResults(nursery);

                        break;

                    case "2":

                        // Code for searching plant by name

                        break;

                    case "3":

                        Environment.Exit(0);

                        break;

                    default:

                        Console.WriteLine("Invalid input. Please enter a valid option.");

                        break;

                }

            }

        }
    }

    private static void DisplayResults(PlantNursery nursery)
    {
        throw new NotImplementedException();
    }
}




