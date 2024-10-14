using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using urban_leaf.Models;

namespace urban_leaf.Services
{
    public static class ProductService
    {
        private static readonly string FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "products.json");

        public static void SaveProducts(List<Product> products)
        {
            var json = JsonSerializer.Serialize(products);
            File.WriteAllText(FilePath, json);
        }

        public static List<Product> LoadProducts()
        {
            if (!File.Exists(FilePath))
            {
                return new List<Product>();
            }

            var json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<Product>>(json);
        }


    }
}
