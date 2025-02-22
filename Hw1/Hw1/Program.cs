using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hw1
{
    internal class Program
    {
        static List<string> shoppingCart = new List<string>();
        static void ProductList()
        {
            Console.WriteLine("商品列表：");
            Console.WriteLine("商品名稱            商品單價");
            Console.WriteLine("1. 潛水相機防丟繩    TWD 199");
            Console.WriteLine("2. 潛水配重帶       TWD 460");
            Console.WriteLine("3. 潛水作業指北針    TWD 1100");
        }
        static void Addproduct()
        {
            bool check = true;
            string product = "";
            Console.WriteLine("(1)潛水相機防丟繩 (2)潛水配重帶 (3)潛水作業指北針");
            while (check == true)
            {
                Console.Write("輸入數字選擇商品： ");
                int productcode = int.Parse(Console.ReadLine());
                if (productcode >= 1 && productcode <= 3)
                {
                    switch (productcode)
                    {
                        case 1:
                            product = "1.潛水相機防丟繩";
                            break;
                        case 2:
                            product = "2.潛水配重帶";
                            break;
                        case 3:
                            product = "3.潛水作業指北針";
                            break;
                    }
                    check = false;
                }
                else
                {
                    Console.WriteLine("輸入錯誤! 重新輸入!");
                    break;
                }
            }
            while (check == false)
            {
                Console.Write("輸入數量： ");
                int quantity = int.Parse(Console.ReadLine());
                if (quantity >= 1 && quantity <= 5)
                {
                    for (int i = 0; i < quantity; i++)
                    {
                        shoppingCart.Add(product);
                    }
                    Console.WriteLine($"{quantity} 個 {product} 已經被新增至購物車。");
                    check = true;
                }
                else
                {
                    Console.WriteLine("輸入錯誤! 重新輸入!");
                    break;
                }
            }
        }
        static void Removeproduct()
        {
            bool check = true;
            string product = "";
            if (shoppingCart.Count == 0)
            {
                Console.WriteLine("購物車是空的!");
            }
            else
            {
                Console.WriteLine("購物車內容：");
                Dictionary<string, int> productQuantities = new Dictionary<string, int>();
                Dictionary<string, int> productPrices = new Dictionary<string, int>
                {
                    { "1.潛水相機防丟繩", 199 },
                    { "2.潛水配重帶", 460 },
                    { "3.潛水作業指北針", 1100 }
                };

                foreach (var item in shoppingCart)
                {
                    if (productQuantities.ContainsKey(item))
                    {
                        productQuantities[item]++;
                    }
                    else
                    {
                        productQuantities[item] = 1;
                    }
                }
                foreach (var productQuantity in productQuantities)
                {
                    string productName = productQuantity.Key;
                    int quantity = productQuantity.Value;
                    int itemPrice = productPrices.ContainsKey(productName) ? productPrices[productName] : 0;
                    int itemTotalPrice = itemPrice * quantity;

                    Console.WriteLine($"{productName} - 單價：{itemPrice:C}, 數量：{quantity}, 小計：{itemTotalPrice:C}");
                }

                while (check == true)
                {
                    Console.Write("請輸入要刪除的商品： ");
                    int productcode = int.Parse(Console.ReadLine());
                    if (productcode >= 1 && productcode <= 3)
                    {
                        switch (productcode)
                        {
                            case 1:
                                product = "1.潛水相機防丟繩";
                                break;
                            case 2:
                                product = "2.潛水配重帶";
                                break;
                            case 3:
                                product = "3.潛水作業指北針";
                                break;
                        }
                        check = false;
                    }
                    else
                    {
                        Console.WriteLine("輸入錯誤! 重新輸入!");
                        break;
                    }
                }
                while (check == false)
                {
                    Console.Write("輸入數量： ");
                    int removeQuantity = int.Parse(Console.ReadLine());
                    int currentQuantity = productQuantities.ContainsKey(product) ? productQuantities[product] : 0;
                    if (removeQuantity > 0 && removeQuantity <= currentQuantity)
                    {
                        if (currentQuantity == 0)
                        {
                            Console.WriteLine($"購物車中沒有 {product}。");
                            break; 
                        }
                        for (int i = 0; i < removeQuantity; i++)
                        {
                            shoppingCart.Remove(product);
                        }
                        Console.WriteLine($"{removeQuantity} 個 {product} 已從購物車中刪除。");
                        check = true;
                    }
                    else 
                    {
                        Console.WriteLine("輸入錯誤！請輸入有效的數量!");
                        break;
                    }
                }
            }           
        }
        static void View()
        {
            Console.WriteLine("購物車內容：");
            Dictionary<string, int> productQuantities = new Dictionary<string, int>();
            Dictionary<string, int> productPrices = new Dictionary<string, int>
            {
                { "1.潛水相機防丟繩", 199 },
                { "2.潛水配重帶", 460 },
                { "3.潛水作業指北針", 1100 }
            };

            foreach (var productName in productPrices.Keys)
            {
                productQuantities[productName] = 0;
            }

            foreach (var item in shoppingCart)
            {
                productQuantities[item]++;
            }

            foreach (var productPrice in productPrices)
            {
                string productName = productPrice.Key;
                int itemPrice = productPrice.Value;
                int quantity = productQuantities.ContainsKey(productName) ? productQuantities[productName] : 0;
                int itemTotalPrice = itemPrice * quantity;

                Console.WriteLine($"{productName} - 單價：{itemPrice:C}, 數量：{quantity}, 小計：{itemTotalPrice:C}");
            }
        }
        static void Calculate()
        {
            Console.WriteLine("訂單商品：");

            int total=0;
            Dictionary<string, int> productQuantities = new Dictionary<string, int>();
            Dictionary<string, int> productPrices = new Dictionary<string, int>
                {
                    { "1.潛水相機防丟繩", 199 },
                    { "2.潛水配重帶", 460 },
                    { "3.潛水作業指北針", 1100 }
                };

            foreach (var item in shoppingCart)
            {
                if (productQuantities.ContainsKey(item))
                {
                    productQuantities[item]++;
                }
                else
                {
                    productQuantities[item] = 1;
                }
            }
            foreach (var productQuantity in productQuantities)
            {
                string productName = productQuantity.Key;
                int quantity = productQuantity.Value;
                int itemPrice = productPrices.ContainsKey(productName) ? productPrices[productName] : 0;
                int itemTotalPrice = itemPrice * quantity;

                Console.WriteLine($"{productName} - 單價：{itemPrice:C}, 數量：{quantity}, 小計：{itemTotalPrice:C}");
                total += itemTotalPrice;
            }
            Console.WriteLine($"總價： {total:C}");
        }    
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("(1)商品列表 (2)新增至購物車 (3)自購物車刪除 (4)查看購物車 (5)計算總金額 (6)退出網站");
                Console.Write("輸入數字選擇功能： ");
                string choice;
                choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ProductList();
                        Console.WriteLine();
                        break;
                    case "2":
                        Addproduct();
                        Console.WriteLine();
                        break;
                    case "3":
                        Removeproduct();
                        Console.WriteLine();
                        break;
                    case "4":
                        View();
                        Console.WriteLine();
                        break;
                    case "5":
                        Calculate();
                        Console.WriteLine();
                        break;
                    case "6":
                        Console.WriteLine("感謝使用，退出網站。");
                        Thread.Sleep(1000);
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("輸入錯誤! 重新輸入!");
                        Console.WriteLine();
                        break;
                }  
            }
        }
    }
}
