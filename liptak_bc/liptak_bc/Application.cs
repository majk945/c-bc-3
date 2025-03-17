using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml;

namespace liptak_bc
{
    class Application
    {
        private XmlDocument document { get; set; }
        private List<Product> ProductsList { get; set; }

        public Application()
        {
            ShowBanner();
            this.ProductsList = new List<Product>();
            this.document = new XmlDocument();
            document.Load("C:\\Users\\majk\\Desktop\\liptak_bc\\liptak_bc\\xmlProducts.xml");
            XmlNodeList products = document.DocumentElement.ChildNodes;
            ParseData(products);
            RunMenu();
        }


        private void ParseData(XmlNodeList Products)
        {
            foreach (XmlNode product in Products)
            {
                Product newProduct = new Product();
                newProduct.SetId(int.Parse(product["id"].InnerText));
                newProduct.SetName(product["name"].InnerText);
                newProduct.SetCategory(product["category"].InnerText);
                newProduct.SetSubCategory(product["subcategory"].InnerText);
                newProduct.SetPrice(double.Parse(product["price"].InnerText));
                newProduct.SetStock(int.Parse(product["stock"].InnerText));

                var AdditionalInfoElement = product["additional_information"];
                if (AdditionalInfoElement != null)
                {
                    foreach (XmlNode AdditionalInfo in AdditionalInfoElement)
                    {
                        newProduct.GetAdditionalInfo()[AdditionalInfo.Name] = AdditionalInfo.InnerText;
                    }
                }

                this.ProductsList.Add(newProduct);
            }
        }

        private void SaveData()
        {
            XmlNode root = document.DocumentElement;
            root.RemoveAll();

            foreach (var product in ProductsList)
            {
                XmlElement productElement = document.CreateElement("product");

                XmlElement id = document.CreateElement("id");
                id.InnerText = product.GetId().ToString();
                productElement.AppendChild(id);

                XmlElement name = document.CreateElement("name");
                name.InnerText = product.GetName();
                productElement.AppendChild(name);

                XmlElement category = document.CreateElement("category");
                category.InnerText = product.GetCategory();
                productElement.AppendChild(category);

                XmlElement subcategory = document.CreateElement("subcategory");
                subcategory.InnerText = product.GetSubCategory();
                productElement.AppendChild(subcategory);

                XmlElement price = document.CreateElement("price");
                price.InnerText = product.GetPrice().ToString();
                productElement.AppendChild(price);

                XmlElement stock = document.CreateElement("stock");
                stock.InnerText = product.GetStock().ToString();
                productElement.AppendChild(stock);

                XmlElement additionalInfo = document.CreateElement("additional_information");
                foreach (var info in product.GetAdditionalInfo())
                {
                    XmlElement infoElement = document.CreateElement(info.Key);
                    infoElement.InnerText = info.Value;
                    additionalInfo.AppendChild(infoElement);
                }
                productElement.AppendChild(additionalInfo);

                root.AppendChild(productElement);
            }

            document.Save("C:\\Users\\majk\\Desktop\\liptak_bc\\liptak_bc\\xmlProducts.xml");
        }

        private void RunMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;

                Console.WriteLine("=====================================");
                Console.WriteLine("           HLAVNÉ MENU             ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("=====================================");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(" 1 ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("- Zobraziť všetky kategórie");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(" 2 ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("- Zobraziť všetky produkty");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(" 3 ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("- Hladat produkt");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(" 4 ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("- Pridať nový produkt");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(" 5 ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("- Upraviť existujúci produkt");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(" 6 ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("- Odstrániť produkt");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(" 7 ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("- Zoradiť produkty");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(" 8 ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("- Ukončiť program");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("=====================================");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("     Voľba: ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n=====================================");
                Console.ForegroundColor = ConsoleColor.Yellow;
                string choice = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.Gray;

                switch (choice)
                {
                    case "1":
                        DisplayCategories();
                        break;
                    case "2":
                        DisplayProducts();
                        break;
                    case "3":
                        SearchProducts();
                        break;
                    case "4":
                        AddNewProduct();
                        break;
                    case "5":
                        UpdateProduct();
                        break;
                    case "6":
                        DeleteProduct();
                        break;
                    case "7":
                        SortProducts();
                        break;
                    case "8":
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\n👋 Ďakujeme za používanie programu. Dovidenia!\n");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n⛔ Neplatná voľba, skúste znova.");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine("\nStlačte ENTER pre pokračovanie...");
                        Console.ReadLine();
                        break;
                }
            }
        }

        private void AddNewProduct()
        {
            Console.Clear();
            Console.WriteLine("\n===========================");
            Console.WriteLine("        PRIDAŤ NOVÝ PRODUKT      ");
            Console.WriteLine("===========================\n");

            Product newProduct = new Product();

            Console.Write("Zadajte ID: ");
            newProduct.SetId(int.Parse(Console.ReadLine()));

            Console.Write("Zadajte názov: ");
            newProduct.SetName(Console.ReadLine());

            Console.Write("Zadajte kategóriu: ");
            newProduct.SetCategory(Console.ReadLine());

            Console.Write("Zadajte podkategóriu: ");
            newProduct.SetSubCategory(Console.ReadLine());

            Console.Write("Zadajte cenu: ");
            newProduct.SetPrice(double.Parse(Console.ReadLine()));

            Console.Write("Zadajte množstvo na sklade: ");
            newProduct.SetStock(int.Parse(Console.ReadLine()));

            Console.WriteLine("Zadajte dodatočné informácie (nechajte prázdne pre ukončenie):");
            while (true)
            {
                Console.Write("Zadajte nazov informacie: ");
                string key = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(key)) break;

                Console.Write("Zadajte hodnotu informacie: ");
                string value = Console.ReadLine();
                newProduct.GetAdditionalInfo()[key] = value;

                Console.WriteLine("Chcete pridať ďalšiu dodatočnú informáciu? (ano/nie):");
                string response = Console.ReadLine().Trim().ToLower();
                if (response != "ano") break;
            }

            ProductsList.Add(newProduct);
            SaveData();

            Console.WriteLine("\n✅ Produkt bol úspešne pridaný.");
            Console.WriteLine("\nStlačte ENTER pre návrat do hlavného menu...");
            Console.ReadLine();
        }

        private void UpdateProduct()
        {
            Console.Clear();
            Console.WriteLine("\n===========================");
            Console.WriteLine("      UPRAVIŤ EXISTUJÚCI PRODUKT    ");
            Console.WriteLine("===========================\n");

            Console.Write("Zadajte ID produktu, ktorý chcete upraviť: ");
            int productId = int.Parse(Console.ReadLine());

            Product product = ProductsList.FirstOrDefault(p => p.GetId() == productId);
            if (product == null)
            {
                Console.WriteLine("\n⛔ Produkt s týmto ID neexistuje.");
                Console.WriteLine("\nStlačte ENTER pre návrat do hlavného menu...");
                Console.ReadLine();
                return;
            }

            Console.Write("Zadajte nový názov (aktuálny: {0}): ", product.GetName());
            string newName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newName)) product.SetName(newName);

            Console.Write("Zadajte novú kategóriu (aktuálna: {0}): ", product.GetCategory());
            string newCategory = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newCategory)) product.SetCategory(newCategory);

            Console.Write("Zadajte novú podkategóriu (aktuálna: {0}): ", product.GetSubCategory());
            string newSubCategory = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newSubCategory)) product.SetSubCategory(newSubCategory);

            Console.Write("Zadajte novú cenu (aktuálna: {0}): ", product.GetPrice());
            string newPrice = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newPrice)) product.SetPrice(double.Parse(newPrice));

            Console.Write("Zadajte nové množstvo na sklade (aktuálne: {0}): ", product.GetStock());
            string newStock = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newStock)) product.SetStock(int.Parse(newStock));

            Console.WriteLine("Zadajte nové dodatočné informácie (nechajte prázdne pre ukončenie):");
            while (true)
            {
                Console.Write("Zadajte nazov informacie: ");
                string key = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(key)) break;

                Console.Write("Zadajte hodnotu informacie: ");
                string value = Console.ReadLine();
                product.GetAdditionalInfo()[key] = value;

                Console.WriteLine("Chcete pridať ďalšiu dodatočnú informáciu? (ano/nie):");
                string response = Console.ReadLine().Trim().ToLower();
                if (response != "ano") break;
            }

            SaveData();

            Console.WriteLine("\n✅ Produkt bol úspešne aktualizovaný.");
            Console.WriteLine("\nStlačte ENTER pre návrat do hlavného menu...");
            Console.ReadLine();
        }

        private void DeleteProduct()
        {
            Console.Clear();
            Console.WriteLine("\n===========================");
            Console.WriteLine("        ODSTRÁNIŤ PRODUKT        ");
            Console.WriteLine("===========================\n");

            Console.Write("Zadajte ID produktu, ktorý chcete odstrániť: ");
            int productId = int.Parse(Console.ReadLine());

            Product product = ProductsList.FirstOrDefault(p => p.GetId() == productId);
            if (product == null)
            {
                Console.WriteLine("\n⛔ Produkt s týmto ID neexistuje.");
                Console.WriteLine("\nStlačte ENTER pre návrat do hlavného menu...");
                Console.ReadLine();
                return;
            }

            ProductsList.Remove(product);
            SaveData();

            Console.WriteLine("\n✅ Produkt bol úspešne odstránený.");
            Console.WriteLine("\nStlačte ENTER pre návrat do hlavného menu...");
            Console.ReadLine();
        }

        private void SortProducts()
        {
            Console.Clear();
            Console.WriteLine("\n===========================");
            Console.WriteLine("        ZORADIŤ PRODUKTY        ");
            Console.WriteLine("===========================\n");

            // Ask if sorting from all products or specific products
            Console.WriteLine("Chcete triediť zo všetkých produktov alebo zo špecifických produktov?");
            Console.WriteLine("1. Všetky produkty");
            Console.WriteLine("2. Špecifické produkty");
            Console.Write("\nVyberte možnosť (1-2): ");
            string sortChoice = Console.ReadLine();

            List<Product> productsToSort;

            if (sortChoice == "2")
            {
                // Get search filters first
                Console.WriteLine("Najprv vyhľadajte produkty podľa kritérií.");
                var filters = GetSearchFilters();
                productsToSort = FilterProducts(ProductsList, filters);

                if (productsToSort.Count == 0)
                {
                    Console.WriteLine("\nŽiadne produkty nevyhovujú zadaným kritériám.");
                    Console.WriteLine("\nStlačte ENTER pre návrat do hlavného menu...");
                    Console.ReadLine();
                    return;
                }

                // Display filtered products
                DisplaySearchResults(productsToSort);
            }
            else
            {
                productsToSort = new List<Product>(ProductsList);
            }

            // Ask for sorting criteria
            Console.WriteLine("\nZvoľte kritériá triedenia:");
            Console.WriteLine("1. Podľa ID (vzostupne/zostupne)");
            Console.WriteLine("2. Podľa názvu (vzostupne/zostupne)");
            Console.WriteLine("3. Podľa kategórie (vzostupne/zostupne)");
            Console.WriteLine("4. Podľa podkategórie (vzostupne/zostupne)");
            Console.WriteLine("5. Podľa ceny (vzostupne/zostupne)");
            Console.WriteLine("6. Podľa množstva na sklade (vzostupne/zostupne)");
            Console.WriteLine("Zadajte cislo a hodnotu (napr. 1z,3v,5z):");

            string choice = Console.ReadLine();
            var criteria = choice.Split(',').Select(c => c.Trim()).ToList();

            Comparison<Product> comparison = (p1, p2) =>
            {
                foreach (var criterion in criteria)
                {
                    string criterionKey = criterion.Substring(0, criterion.Length - 1);
                    bool ascending = criterion.EndsWith("v", StringComparison.OrdinalIgnoreCase);
                    int result = 0;

                    switch (criterionKey)
                    {
                        case "1":
                            result = p1.GetId().CompareTo(p2.GetId());
                            break;
                        case "2":
                            result = p1.GetName().CompareTo(p2.GetName());
                            break;
                        case "3":
                            result = p1.GetCategory().CompareTo(p2.GetCategory());
                            break;
                        case "4":
                            result = p1.GetSubCategory().CompareTo(p2.GetSubCategory());
                            break;
                        case "5":
                            result = p1.GetPrice().CompareTo(p2.GetPrice());
                            break;
                        case "6":
                            result = p1.GetStock().CompareTo(p2.GetStock());
                            break;
                        default:
                            Console.WriteLine("\n⛔ Neplatná voľba, skúste znova.");
                            Console.WriteLine("\nStlačte ENTER pre návrat...");
                            Console.ReadLine();
                            return 0;
                    }

                    if (result != 0)
                        return ascending ? result : -result;
                }
                return 0;
            };

            InsertionSort(productsToSort, comparison);

            Console.Clear();
            Console.WriteLine("\n===========================");
            Console.WriteLine("        VÝSLEDOK TRIEDENIA        ");
            Console.WriteLine("===========================\n");

            if (sortChoice == "2")
            {
                Console.WriteLine("Zoradené zo špecifických produktov:");
            }
            else
            {
                Console.WriteLine("Zoradené zo všetkých produktov:");
            }

            foreach (var criterion in criteria)
            {
                string criterionKey = criterion.Substring(0, criterion.Length - 1);
                string order = criterion.EndsWith("z") ? "zostupne" : "vzostupne";
                Console.WriteLine($"Podľa {criterionKey} ({order})");
            }

            DisplaySortedProducts(productsToSort);

            Console.WriteLine("\nChcete uložiť zoradené výsledky? (ano/nie):");
            string saveResponse = Console.ReadLine().Trim().ToLower();
            if (saveResponse == "ano")
            {
                SaveSortedResults(productsToSort);
            }
        }

        private void DisplaySortedProducts(List<Product> sortedProducts)
        {
            Console.Clear();
            Console.WriteLine("\n==============================================================");
            Console.WriteLine("                    ZOZNAM ZORADENÝCH PRODUKTOV                ");
            Console.WriteLine("==============================================================");

            Console.WriteLine("{0,-5} | {1,-35} | {2,-15} | {3,-20} | {4,-8} | {5,-6}",
                "ID", "Názov", "Kategória", "Podkategória", "Cena (EUR)", "Sklad");
            Console.WriteLine(new string('=', 90));

            foreach (var product in sortedProducts)
            {
                Console.WriteLine("{0,-5} | {1,-35} | {2,-15} | {3,-20} | {4,-8:F2} | {5,-6} ks",
                    product.GetId(),
                    product.GetName().Length > 35 ? product.GetName().Substring(0, 32) + "..." : product.GetName(),
                    product.GetCategory(),
                    product.GetSubCategory(),
                    product.GetPrice(),
                    product.GetStock());

                if (product.GetAdditionalInfo().Count > 0)
                {
                    Console.WriteLine("\n   Dodatočné informácie:");
                    foreach (var info in product.GetAdditionalInfo())
                    {
                        Console.WriteLine("     - {0,-15}: {1}", info.Key, info.Value);
                    }
                }

                Console.WriteLine(new string('-', 90));
            }

            Console.WriteLine("\nStlačte ENTER pre návrat do hlavného menu...");
            Console.ReadLine();
        }
        private void DisplayCategories()
        {
            HashSet<string> categories = new HashSet<string>();

            foreach (var product in ProductsList)
            {
                categories.Add(product.GetCategory());
            }

            Console.WriteLine("\nDostupné kategórie:");
            foreach (var category in categories)
            {
                Console.WriteLine($"- {category}");
            }

            while (true)
            {
                Console.WriteLine("\nVyberte kategóriu alebo napíšte 'spat' pre návrat: ");
                string selectedCategory = Console.ReadLine();

                if (selectedCategory.ToLower() == "spat")
                {
                    return;
                }
                else if (categories.Contains(selectedCategory))
                {
                    Console.WriteLine("\nChcete zobraziť:");
                    Console.WriteLine("1 - Všetky produkty v tejto kategórii");
                    Console.WriteLine("2 - Podkategórie tejto kategórie");
                    Console.Write("Voľba: ");

                    string choice = Console.ReadLine();

                    if (choice == "1")
                    {
                        DisplayProductsByCategory(selectedCategory);
                        return;
                    }
                    else if (choice == "2")
                    {
                        DisplaySubCategoryByCategory(selectedCategory);
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Neplatná voľba, skúste znova.");
                    }
                }
                else
                {
                    Console.WriteLine("Neplatná kategória, skúste znova.");
                }
            }
        }

        private void DisplayProductsByCategory(string category)
        {
            Console.Clear();
            Console.WriteLine($"\n==============================================================");
            Console.WriteLine($"            PRODUKTY V KATEGÓRII: {category.ToUpper()}");
            Console.WriteLine($"==============================================================");

            Console.WriteLine("{0,-5} | {1,-35} | {2,-20} | {3,-8} | {4,-6}",
                "ID", "Názov", "Podkategória", "Cena (EUR)", "Sklad");
            Console.WriteLine(new string('=', 80));

            bool found = false;
            foreach (var product in ProductsList)
            {
                if (product.GetCategory().Equals(category, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("{0,-5} | {1,-35} | {2,-20} | {3,-8:F2} | {4,-6} ks",
                        product.GetId(),
                        product.GetName().Length > 35 ? product.GetName().Substring(0, 32) + "..." : product.GetName(),
                        product.GetSubCategory(),
                        product.GetPrice(),
                        product.GetStock());

                    if (product.GetAdditionalInfo().Count > 0)
                    {
                        Console.WriteLine("\n   Dodatočné informácie:");
                        foreach (var info in product.GetAdditionalInfo())
                        {
                            Console.WriteLine("     - {0,-15}: {1}", info.Key, info.Value);
                        }
                    }

                    Console.WriteLine(new string('-', 80));
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("V tejto kategórii sa nenachádzajú žiadne produkty.");
            }

            Console.WriteLine("\nStlačte ENTER pre návrat...");
            Console.ReadLine();
        }

        private void DisplaySubCategoryByCategory(string category)
        {
            Console.Clear();
            Console.WriteLine($"\n==============================================================");
            Console.WriteLine($"          PODKATEGÓRIE V KATEGÓRII: {category.ToUpper()}");
            Console.WriteLine($"==============================================================");

            HashSet<string> subCategories = new HashSet<string>();

            foreach (var product in ProductsList)
            {
                if (product.GetCategory().Equals(category, StringComparison.OrdinalIgnoreCase))
                {
                    subCategories.Add(product.GetSubCategory());
                }
            }

            if (subCategories.Count == 0)
            {
                Console.WriteLine("V tejto kategórii sa nenachádzajú žiadne podkategórie.");
                Console.WriteLine("\nStlačte ENTER pre návrat...");
                Console.ReadLine();
                return;
            }

            int index = 1;
            Dictionary<int, string> selectionMap = new Dictionary<int, string>();
            foreach (var subCategory in subCategories)
            {
                Console.WriteLine($"{index}. {subCategory}");
                selectionMap[index] = subCategory;
                index++;
            }

            Console.WriteLine("\nZadajte číslo podkategórie pre zobrazenie jej produktov (alebo 0 pre návrat):");

            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > selectionMap.Count)
            {
                Console.WriteLine("Neplatná voľba, skúste znova:");
            }

            if (choice == 0)
            {
                return;
            }

            DisplayProductBySubCategory(selectionMap[choice]);
        }

        private void DisplayProductBySubCategory(string subCategory)
        {
            Console.Clear();
            Console.WriteLine($"\n==============================================================");
            Console.WriteLine($"        PRODUKTY V PODKATEGÓRII: {subCategory.ToUpper()}");
            Console.WriteLine($"==============================================================");

            Console.WriteLine("{0,-5} | {1,-35} | {2,-15} | {3,-8} | {4,-6}",
                "ID", "Názov", "Kategória", "Cena (EUR)", "Sklad");
            Console.WriteLine(new string('=', 80));

            bool found = false;
            foreach (var product in ProductsList)
            {
                if (product.GetSubCategory().Equals(subCategory, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("{0,-5} | {1,-35} | {2,-15} | {3,-8:F2} | {4,-6} ks",
                    product.GetId(),
                    product.GetName().Length > 35 ? product.GetName().Substring(0, 32) + "..." : product.GetName(),
                    product.GetCategory(),
                    product.GetPrice(),
                    product.GetStock());

                    if (product.GetAdditionalInfo().Count > 0)
                    {
                        Console.WriteLine("\n   Dodatočné informácie:");
                        foreach (var info in product.GetAdditionalInfo())
                        {
                            Console.WriteLine("     - {0,-15}: {1}", info.Key, info.Value);
                        }
                    }

                    Console.WriteLine(new string('-', 80));
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("V tejto podkategórii sa nenachádzajú žiadne produkty.");
            }

            Console.WriteLine("\nStlačte ENTER pre návrat...");
            Console.ReadLine();
        }

        private void DisplaySortedProducts()
        {
            Console.Clear();
            Console.WriteLine("\n==============================================================");
            Console.WriteLine("                    ZOZNAM ZORADENÝCH PRODUKTOV                ");
            Console.WriteLine("==============================================================");

            Console.WriteLine("{0,-5} | {1,-35} | {2,-15} | {3,-20} | {4,-8} | {5,-6}",
                "ID", "Názov", "Kategória", "Podkategória", "Cena (EUR)", "Sklad");
            Console.WriteLine(new string('=', 90));

            foreach (var product in ProductsList)
            {
                Console.WriteLine("{0,-5} | {1,-35} | {2,-15} | {3,-20} | {4,-8:F2} | {5,-6} ks",
                    product.GetId(),
                    product.GetName().Length > 35 ? product.GetName().Substring(0, 32) + "..." : product.GetName(),
                    product.GetCategory(),
                    product.GetSubCategory(),
                    product.GetPrice(),
                    product.GetStock());

                if (product.GetAdditionalInfo().Count > 0)
                {
                    Console.WriteLine("\n   Dodatočné informácie:");
                    foreach (var info in product.GetAdditionalInfo())
                    {
                        Console.WriteLine("     - {0,-15}: {1}", info.Key, info.Value);
                    }
                }

                Console.WriteLine(new string('-', 90));
            }

            Console.WriteLine("\nStlačte ENTER pre návrat do hlavného menu...");
            Console.ReadLine();
        }

        private void SaveSortedResults(List<Product> sortedProducts)
        {
            string date = DateTime.UtcNow.ToString("yyyy-MM-dd");
            string fileName = $"SortedProducts_{date}.txt";
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.WriteLine("==============================================================");
                writer.WriteLine("                    ZOZNAM ZORADENÝCH PRODUKTOV                ");
                writer.WriteLine("==============================================================");

                writer.WriteLine("{0,-5} | {1,-35} | {2,-15} | {3,-20} | {4,-8} | {5,-6}",
                    "ID", "Názov", "Kategória", "Podkategória", "Cena (EUR)", "Sklad");
                writer.WriteLine(new string('=', 90));

                foreach (var product in sortedProducts)
                {
                    writer.WriteLine("{0,-5} | {1,-35} | {2,-15} | {3,-20} | {4,-8:F2} | {5,-6} ks",
                        product.GetId(),
                        product.GetName().Length > 35 ? product.GetName().Substring(0, 32) + "..." : product.GetName(),
                        product.GetCategory(),
                        product.GetSubCategory(),
                        product.GetPrice(),
                        product.GetStock());

                    if (product.GetAdditionalInfo().Count > 0)
                    {
                        writer.WriteLine("\n   Dodatočné informácie:");
                        foreach (var info in product.GetAdditionalInfo())
                        {
                            writer.WriteLine("     - {0,-15}: {1}", info.Key, info.Value);
                        }
                    }

                    writer.WriteLine(new string('-', 90));
                }
            }

            Console.WriteLine($"\n✅ Zoradené výsledky boli uložené do súboru: {fileName}");
            Console.WriteLine("\nStlačte ENTER pre návrat do hlavného menu...");
            Console.ReadLine();
        }
        private void DisplayProducts()
        {
            Console.Clear();
            Console.WriteLine("\n==============================================================");
            Console.WriteLine("                    ZOZNAM VŠETKÝCH PRODUKTOV                ");
            Console.WriteLine("==============================================================");

            Console.WriteLine("{0,-5} | {1,-35} | {2,-15} | {3,-20} | {4,-8} | {5,-6}",
                "ID", "Názov", "Kategória", "Podkategória", "Cena (EUR)", "Sklad");
            Console.WriteLine(new string('=', 90));

            foreach (var product in ProductsList)
            {
                Console.WriteLine("{0,-5} | {1,-35} | {2,-15} | {3,-20} | {4,-8:F2} | {5,-6} ks",
                    product.GetId(),
                    product.GetName().Length > 35 ? product.GetName().Substring(0, 32) + "..." : product.GetName(),
                    product.GetCategory(),
                    product.GetSubCategory(),
                    product.GetPrice(),
                    product.GetStock());

                if (product.GetAdditionalInfo().Count > 0)
                {
                    Console.WriteLine("\n   Dodatočné informácie:");
                    foreach (var info in product.GetAdditionalInfo())
                    {
                        Console.WriteLine("     - {0,-15}: {1}", info.Key, info.Value);
                    }
                }

                Console.WriteLine(new string('-', 90));
            }

            Console.WriteLine("\nStlačte ENTER pre návrat do hlavného menu...");
            Console.ReadLine();
        }

        private void SearchProducts()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n===========================");
                Console.WriteLine("        HĽADANIE PRODUKTOV        ");
                Console.WriteLine("===========================\n");

                var filters = GetSearchFilters();

                var filteredProducts = FilterProducts(ProductsList, filters);

                DisplaySearchResults(filteredProducts);

                Console.WriteLine("\nMožnosti:");
                Console.WriteLine("1. Nové vyhľadávanie");
                Console.WriteLine("2. Návrat do hlavného menu");
                Console.Write("\nVyberte možnosť (1-2): ");

                string choice = Console.ReadLine();
                if (choice != "1")
                    break;
            }
        }

        private SearchFilters GetSearchFilters()
        {
            var filters = new SearchFilters();

            Console.Write("Zadajte názov produktu (nechajte prázdne pre ignorovanie): ");
            filters.NameFilter = Console.ReadLine()?.Trim().ToLower() ?? "";

            Console.Write("Zadajte kategóriu (nechajte prázdne pre ignorovanie): ");
            filters.CategoryFilter = Console.ReadLine()?.Trim().ToLower() ?? "";

            Console.Write("Zadajte podkategóriu (nechajte prázdne pre ignorovanie): ");
            filters.SubCategoryFilter = Console.ReadLine()?.Trim().ToLower() ?? "";

            Console.Write("Zadajte minimálnu cenu (nechajte prázdne pre ignorovanie): ");
            string minPriceInput = Console.ReadLine()?.Trim() ?? "";
            filters.MinPrice = string.IsNullOrWhiteSpace(minPriceInput) ? 0 : double.TryParse(minPriceInput, out double minPrice) ? minPrice : 0;

            Console.Write("Zadajte maximálnu cenu (nechajte prázdne pre ignorovanie): ");
            string maxPriceInput = Console.ReadLine()?.Trim() ?? "";
            filters.MaxPrice = string.IsNullOrWhiteSpace(maxPriceInput) ? double.MaxValue : double.TryParse(maxPriceInput, out double maxPrice) ? maxPrice : double.MaxValue;



            return filters;
        }

        private List<Product> FilterProducts(List<Product> products, SearchFilters filters)
        {
            return products.Where(product =>
                (string.IsNullOrWhiteSpace(filters.NameFilter) || product.GetName().ToLower().Contains(filters.NameFilter)) &&
                (string.IsNullOrWhiteSpace(filters.CategoryFilter) || product.GetCategory().ToLower().Equals(filters.CategoryFilter)) &&
                (string.IsNullOrWhiteSpace(filters.SubCategoryFilter) || product.GetSubCategory().ToLower().Equals(filters.SubCategoryFilter)) &&
                (product.GetPrice() >= filters.MinPrice && product.GetPrice() <= filters.MaxPrice) &&
                (!filters.FilterInStock || product.GetStock() > 0)
            ).ToList();
        }

        private void DisplaySearchResults(List<Product> filteredProducts)
        {
            Console.Clear();
            Console.WriteLine("\n===========================");
            Console.WriteLine("        VÝSLEDKY HĽADANIA       ");
            Console.WriteLine("===========================\n");

            if (filteredProducts.Count == 0)
            {
                Console.WriteLine("Žiadne produkty nevyhovujú zadaným kritériám.");
                return;
            }

            Console.WriteLine("{0,-5} | {1,-35} | {2,-15} | {3,-20} | {4,-10} | {5,-8}",
                "ID", "Názov", "Kategória", "Podkategória", "Cena (€)", "Množstvo");
            Console.WriteLine(new string('-', 105));

            foreach (var product in filteredProducts)
            {
                Console.WriteLine("{0,-5} | {1,-35} | {2,-15} | {3,-20} | {4,-10:F2} | {5,-8} ks",
                    product.GetId(),
                    TruncateString(product.GetName(), 35),
                    product.GetCategory(),
                    product.GetSubCategory(),
                    product.GetPrice(),
                    product.GetStock());

                if (product.GetAdditionalInfo().Count > 0)
                {
                    Console.WriteLine("\n   Dodatočné informácie:");
                    foreach (var info in product.GetAdditionalInfo())
                    {
                        Console.WriteLine("     - {0,-15}: {1}", info.Key, info.Value);
                    }
                }

                Console.WriteLine(new string('-', 105));
            }

            Console.WriteLine($"\nNájdených produktov: {filteredProducts.Count}");
        }

        private void InsertionSort<T>(List<T> list, Comparison<T> comparison)
        {
            for (int i = 1; i < list.Count; i++)
            {
                T key = list[i];
                int j = i - 1;

                while (j >= 0 && comparison(list[j], key) > 0)
                {
                    list[j + 1] = list[j];
                    j--;
                }
                list[j + 1] = key;
            }
        }

        private class SearchFilters
        {
            public string NameFilter { get; set; } = "";
            public string CategoryFilter { get; set; } = "";
            public string SubCategoryFilter { get; set; } = "";
            public double MinPrice { get; set; } = 0;
            public double MaxPrice { get; set; } = double.MaxValue;
            public bool FilterInStock { get; set; } = false;
        }

        private string TruncateString(string str, int maxLength)
        {
            return str.Length > maxLength ? str.Substring(0, maxLength - 3) + "..." : str;
        }

        private void ShowBanner()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;


            Console.WriteLine(@" _____ _      _    _             _   _       _     
| ____| | ___| | _| |_ _ __ ___ | | | |_   _| |__  
|  _| | |/ _ \ |/ / __| '__/ _ \| |_| | | | | '_ \ 
| |___| |  __/   <| |_| | | (_) |  _  | |_| | |_) |
|_____|_|\___|_|\_\\__|_|  \___/|_| |_|\__,_|_.__/ ");

            Console.ForegroundColor = ConsoleColor.Gray;


            SlowPrint("\n   ====== Elektronická správa produktov ======", 10);
            SlowPrint("         © 2025 Michal Lipták | Verzia 1.0", 10);

            Console.WriteLine("\n");
            SlowPrint("   Stlačte ENTER pre pokračovanie do softwaru...", 10);

            Console.ReadLine();
            Console.Clear();
        }


        private void SlowPrint(string message, int delay = 30)
        {
            foreach (char c in message)
            {
                Console.Write(c);
                System.Threading.Thread.Sleep(delay);
            }
            Console.WriteLine();
        }
    }


}