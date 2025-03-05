// Package Express Shipping Calculator
// Author: Jennifer Lee
// Version: 1.5.0
using System;
using System.Linq;

namespace PackageExpress.Calculator
{
    public interface IShippingCalculator
    {
        double CalculateShippingCost(double width, double height, double length, double weight);
    }

    public class StandardShippingCalculator : IShippingCalculator
    {
        public double CalculateShippingCost(double width, double height, double length, double weight)
        {
            return (width * height * length * weight) / 100;
        }
    }

    class Program
    {
        private static readonly IShippingCalculator _calculator = new StandardShippingCalculator();

        static void Main(string[] args)
        {
            // Display welcome message
            Console.WriteLine("Welcome to Package Express. Please follow the instructions below.");

            try
            {
                // Get package weight from user
                double weightInput = GetDimensionInput("Please enter the package weight:");

                // Check if weight exceeds limit
                if (weightInput > 50)
                {
                    Console.WriteLine("Package too heavy to be shipped via Package Express. Have a good day.");
                    return;
                }

                // Get package dimensions
                var dimensions = new[]
                {
                    GetDimensionInput("Please enter the package width:"),
                    GetDimensionInput("Please enter the package height:"),
                    GetDimensionInput("Please enter the package length:")
                };

                // Calculate total package size
                double totalSize = dimensions.Sum();

                // Check if size exceeds limit
                if (totalSize > 50)
                {
                    Console.WriteLine("Package too big to be shipped via Package Express.");
                    return;
                }

                // Calculate shipping cost using the calculator
                double shippingCost = _calculator.CalculateShippingCost(
                    dimensions[0], dimensions[1], dimensions[2], weightInput);

                // Display shipping cost
                Console.WriteLine($"Your estimated total for shipping this package is: ${shippingCost:F2}");
                Console.WriteLine("Thank you!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private static double GetDimensionInput(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                if (double.TryParse(Console.ReadLine(), out double value) && value > 0)
                {
                    return value;
                }
                Console.WriteLine("Please enter a valid positive number.");
            }
        }
    }
} 