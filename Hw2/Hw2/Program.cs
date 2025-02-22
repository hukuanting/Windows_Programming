using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hw2
{
    internal class Program
    {
        static string[] shopping_cart = { "潛水相機防丟繩", "潛水配重帶", "潛水作業指北針" };
        static int[] price = { 199, 460, 1100 };
        static int[] quantity = { 0, 0, 0 };
        static string[] coin = { "(TWD)", "(USD)", "(CNY)", "(JPY)" };
        static double[] ratio = { 1, 0.031, 0.23, 4.59 };

        static void ProductList(int selectedCoinCode)
        {
            Console.WriteLine("商品列表：");
            Console.WriteLine("商品名稱             商品單價");
            Console.WriteLine("1. 潛水相機防丟繩 {0} {1}", coin[selectedCoinCode], price[0]* ratio[selectedCoinCode]);
            Console.WriteLine("2. 潛水配重帶    {0} {1}", coin[selectedCoinCode], price[1] * ratio[selectedCoinCode]);
            Console.WriteLine("3. 潛水作業指北針 {0} {1}", coin[selectedCoinCode], price[2] * ratio[selectedCoinCode]);
        }
        static void Addproduct()
        {
            Console.WriteLine("(1)潛水相機防丟繩 (2)潛水配重帶 (3)潛水作業指北針");
            Console.Write("輸入數字選擇商品： ");
            int productcode = int.Parse(Console.ReadLine());
            if (productcode >= 1 && productcode <= 3)
            {
                Console.Write("輸入數量： ");
                int count = int.Parse(Console.ReadLine());
                if (count >= 1 && count <= 5)
                {
                    switch (productcode)
                    {
                        case 1:
                            quantity[0]+= count;
                            break;
                        case 2:
                            quantity[1]+= count;
                            break;
                        case 3:
                            quantity[2]+= count;
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("輸入錯誤! 重新輸入!");
                }
            }
            else
            {
                Console.WriteLine("輸入錯誤! 重新輸入!");
            }
        }
        static void Removeproduct(int selectedCoinCode)
        {
            Console.WriteLine("購物車內容：");
            Console.WriteLine("商品               單價    數量    小計");
            Console.WriteLine("1.潛水相機防丟繩 {0}{1}      {2}      {3} ", coin[selectedCoinCode], price[0] * ratio[selectedCoinCode], quantity[0], quantity[0]* price[0] * ratio[selectedCoinCode]);
            Console.WriteLine("2.潛水配重帶    {0}{1}      {2}      {3} ", coin[selectedCoinCode], price[1] * ratio[selectedCoinCode], quantity[1], quantity[1] * price[1] * ratio[selectedCoinCode]);
            Console.WriteLine("3.潛水作業指北針 {0}{1}      {2}      {3} ", coin[selectedCoinCode], price[2] * ratio[selectedCoinCode], quantity[2], quantity[2] * price[2] * ratio[selectedCoinCode]);

            Console.Write("輸入數字選擇商品: ");
            int productcode = int.Parse(Console.ReadLine());
            if (productcode == 1 || productcode == 2 || productcode == 3)
            {
                Console.Write("輸入數量: ");
                int count = int.Parse(Console.ReadLine());
                if (count>=1 && count <= 4)
                {
                    if (productcode == 1)
                    {
                        if (count <= quantity[0])
                        {
                            quantity[0] = quantity[0] - count;
                        }
                        else
                        {
                            Console.WriteLine("輸入錯誤! 請重新輸入!");
                        }
                    }
                    else if (productcode == 2)
                    {
                        if (count <= quantity[1])
                        {
                            quantity[1] = quantity[1] - count;
                        }
                        else
                        {
                            Console.WriteLine("輸入錯誤! 請重新輸入!");
                        }
                    }
                    else
                    {
                        if (count <= quantity[2])
                        {
                            quantity[2] = quantity[2] - count;
                        }
                        else
                        {
                            Console.WriteLine("輸入錯誤! 請重新輸入!");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("輸入錯誤! 請重新輸入!");
                }
            }
            else
            {
                Console.WriteLine("輸入錯誤! 請重新輸入!");
            }
        }
        static void View(int selectedCoinCode)
        {
            Console.WriteLine("購物車內容：");
            Console.WriteLine("商品               單價     數量     小計");
            Console.WriteLine("1.潛水相機防丟繩 {0}{1}      {2}      {3} ", coin[selectedCoinCode], price[0] * ratio[selectedCoinCode], quantity[0], quantity[0] * price[0] * ratio[selectedCoinCode]);
            Console.WriteLine("2.潛水配重帶    {0}{1}      {2}      {3} ", coin[selectedCoinCode], price[1] * ratio[selectedCoinCode], quantity[1], quantity[1] * price[1] * ratio[selectedCoinCode]);
            Console.WriteLine("3.潛水作業指北針 {0}{1}      {2}      {3} ", coin[selectedCoinCode], price[2] * ratio[selectedCoinCode], quantity[2], quantity[2] * price[2] * ratio[selectedCoinCode]);
        }
        static void Pay(int selectedCoinCode)
        {
            int Camera = 1;
            int Belt = 2;
            int Compass = 1;
            double total = 0;

            Console.WriteLine("訂單商品：");
            Console.WriteLine("商品                   單價    數量    小計");
            if (quantity[0] > 0)
            {
                Console.WriteLine("1.潛水相機防丟繩 {0}{1}     {2}     {3} ", coin[selectedCoinCode], price[0] * ratio[selectedCoinCode], quantity[0], quantity[0] * price[0] * ratio[selectedCoinCode]);
            }
            if (quantity[1] > 0)
            {
                Console.WriteLine("1.潛水相機防丟繩 {0}{1}     {2}      {3} ", coin[selectedCoinCode], price[1] * ratio[selectedCoinCode], quantity[1], quantity[1] * price[1] * ratio[selectedCoinCode]);
            }
            if (quantity[2] > 0)
            {
                Console.WriteLine("1.潛水相機防丟繩 {0}{1}     {2}     {3} ", coin[selectedCoinCode], price[2] * ratio[selectedCoinCode], quantity[2], quantity[2] * price[2] * ratio[selectedCoinCode]);
            }
            total = (quantity[0] * price[0] * ratio[selectedCoinCode]) + (quantity[1] * price[1] * ratio[selectedCoinCode]) + (quantity[2] * price[2] * ratio[selectedCoinCode]);

            Console.WriteLine("總價： {0}", total);
            Console.Write(" *是否要結帳(Y/N)* ： ");
            string userInput = Console.ReadLine();
            if (userInput.ToLower() == "y")
            {
                if (quantity[0] > Camera || quantity[1] > Belt || quantity[2] > Compass)
                {
                    if (quantity[0] > Camera)
                    {
                        Console.WriteLine("潛水相機防丟繩 庫存不足! 剩餘數量1!");
                    }
                    if (quantity[1] > Belt)
                    {
                        Console.WriteLine("潛水配重帶 庫存不足! 剩餘數量2!");
                    }
                    if (quantity[2] > Compass)
                    {
                        Console.WriteLine("潛水作業指北針 庫存不足! 剩餘數量1!");
                    }
                }
                else
                {
                    Console.Write("*選擇結帳方式(1.線上支付 2.貨到付款)： ");
                    int pay_way = int.Parse(Console.ReadLine());
                    if(pay_way == 1 || pay_way == 2)
                    {
                        Console.Write("*折扣碼(若無折扣碼則輸入N)： ");
                        string discount = Console.ReadLine();
                        if (discount == "1111" || discount.ToLower() == "n")
                        {
                            Console.WriteLine();
                            Console.WriteLine("訂單狀態：");
                            Console.WriteLine("商品               單價    數量    小計");
                            if (quantity[0] > 0)
                            {
                                Console.WriteLine("1.潛水相機防丟繩 {0}{1}    {2}    {3} ", coin[selectedCoinCode], price[0] * ratio[selectedCoinCode], quantity[0], quantity[0] * price[0] * ratio[selectedCoinCode]);
                            }
                            if (quantity[1] > 0)
                            {
                                Console.WriteLine("1.潛水相機防丟繩 {0}{1}    {2}    {3} ", coin[selectedCoinCode], price[1] * ratio[selectedCoinCode], quantity[1], quantity[1] * price[1] * ratio[selectedCoinCode]);
                            }
                            if (quantity[2] > 0)
                            {
                                Console.WriteLine("1.潛水相機防丟繩 {0}{1}    {2}    {3} ", coin[selectedCoinCode], price[2] * ratio[selectedCoinCode], quantity[2], quantity[2] * price[2] * ratio[selectedCoinCode]);
                            }


                            if (discount == "1111")
                            {
                                Console.WriteLine("總價(折扣後)： {0}", total*0.95);
                                if (pay_way == 1)
                                {
                                    Console.WriteLine("狀態： 已付款");
                                }
                                else if (pay_way == 2)
                                {
                                    Console.WriteLine("狀態： 未付款");
                                }
                                else
                                {
                                    Console.WriteLine("輸入錯誤! 請重新輸入!");
                                }
                            }

                            else if (discount.ToLower() == "n")
                            {
                                Console.WriteLine("總價(折扣後)： {0}", total);
                                if (pay_way == 1)
                                {
                                    Console.WriteLine("狀態： 未付款");
                                }
                                else if (pay_way == 2)
                                {
                                    Console.WriteLine("狀態： 已付款");
                                }
                                else
                                {
                                    Console.WriteLine("輸入錯誤! 請重新輸入!");
                                }
                            }
                            else
                            {
                                Console.WriteLine("輸入錯誤! 請重新輸入!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("輸入錯誤! 請重新輸入!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("輸入錯誤! 請重新輸入!");
                    }
                }     
            }
            else if (userInput.ToLower() == "n")
            {

            }
            else
            {
                Console.WriteLine("輸入錯誤! 請重新輸入!");
            }
        }
        static int Change()
        {
            Console.WriteLine("選擇貨幣 1.TWD 2.USD 3.CNY 4.JPY");
            Console.Write("輸入數字選擇功能: ");
       
            int coincode = int.Parse(Console.ReadLine());
            return coincode-1;
        }
        static void Main(string[] args)
        {
            int selectedCoinCode = 0;
            while (true)
            {
                Console.WriteLine("(1)商品列表 (2)新增至購物車 (3)自購物車刪除 (4)查看購物車 (5)結帳 (6)轉換幣值 (7)退出網站");
                Console.Write("輸入數字選擇功能： ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        ProductList(selectedCoinCode);
                        Console.WriteLine();
                        break;
                    case "2":
                        Addproduct();
                        Console.WriteLine();
                        break;
                    case "3":
                        Removeproduct(selectedCoinCode);
                        Console.WriteLine();
                        break;
                    case "4":
                        View(selectedCoinCode);
                        Console.WriteLine();
                        break;
                    case "5":
                        Pay(selectedCoinCode);
                        Console.WriteLine();
                        break;
                    case "6":
                        selectedCoinCode = Change();
                        Console.WriteLine();
                        break;
                    case "7":
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
