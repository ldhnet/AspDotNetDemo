using System;
using System.Collections.Generic;
using System.Linq;

namespace HelperConsole
{
    public static class LookHelper
    {
        private static List<Product> GetProductList()
        {
            var products = new List<Product>
                               {
                                   new Product {Id = 1, Category = "Electronics", Value = 15.0},
                                   new Product {Id = 2, Category = "Groceries", Value = 40.0},
                                   new Product {Id = 3, Category = "Garden", Value = 210.3},
                                   new Product {Id = 4, Category = "Pets", Value = 2.1},
                                   new Product {Id = 5, Category = "Electronics", Value = 19.95},
                                   new Product {Id = 6, Category = "Pets", Value = 21.25},
                                   new Product {Id = 7, Category = "Pets", Value = 5.50},
                                   new Product {Id = 8, Category = "Garden", Value = 13.0},
                                   new Product {Id = 9, Category = "Automotive", Value = 10.0},
                                   new Product {Id = 10, Category = "Electronics", Value = 250.0},
                               };
            return products;
        }

        public static void funclookup()
        {
            var products = GetProductList();
            var groups = products.GroupBy(p => p.Category);
            //删除所有属于Garden的产品
            products.RemoveAll(p => p.Category == "Garden");

            foreach (var group in groups)
            {
                Console.WriteLine(group.Key);
                foreach (var item in group)
                {
                    Console.WriteLine("\t" + item);
                }
            }
        }

        public static void funclookup_2()
        {
            var products = GetProductList();
            var productsByCategory = products.ToLookup(p => p.Category);
            foreach (var group in productsByCategory)
            {
                Console.WriteLine(group.Key);
                foreach (var item in group)
                {
                    Console.WriteLine("\t" + item);
                }
            }
            Console.WriteLine("\t" + "Start...");
            foreach (var group in productsByCategory)
            {
                PrintCategory(productsByCategory, group.Key);
            }
            Console.WriteLine("\t" + "End...");
        }

        private static void PrintCategory(ILookup<string, Product> productsByCategory, string categoryName)
        {
            foreach (var item in productsByCategory[categoryName])
            {
                Console.WriteLine(item);
            }
        }
    }

    public sealed class Product
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public double Value { get; set; }

        public override string ToString()
        {
            return string.Format("[{0}: {1} - {2}]", Id, Category, Value);
        }
    }
}