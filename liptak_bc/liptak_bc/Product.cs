using System;
using System.Collections.Generic;

namespace liptak_bc
{
    class Product
    {
        private int id { get; set; }
        private string name { get; set; }
        private string category { get; set; }
        private string subCategory { get; set; }

        private double price { get; set; }
        private int stock { get; set; }
        private Dictionary<string, string> AdditionalInfo { get; set; }

        public Product()
        {
            this.AdditionalInfo = new Dictionary<string, string>();
        }

        public int GetId() { return id; }
        public void SetId(int id) { this.id = id; }

        public string GetName() { return name; }
        public void SetName(string name) { this.name = name; }

        public string GetCategory() { return category; }
        public void SetCategory(string category) { this.category = category; }

        public string GetSubCategory() { return subCategory; }
        public void SetSubCategory(string subCategory) { this.subCategory = subCategory; }
        public double GetPrice() { return price; }
        public void SetPrice(double price) { this.price = price; }

        public int GetStock() { return stock; }
        public void SetStock(int stock) { this.stock = stock; }

        public Dictionary<string, string> GetAdditionalInfo() { return this.AdditionalInfo; }
        public void SetAdditionalInfo(Dictionary<string, string> additionalInfo) { this.AdditionalInfo = additionalInfo; }
    }
}



