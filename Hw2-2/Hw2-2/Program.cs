using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hw2_2
{
    internal class Program
    {
        static char[,] revealed;
        static void RandomlyAssignGhosts(char[,] space, int ghostCount, int startX, int startY)
        {
            int m = space.GetLength(0);
            int n = space.GetLength(1);
            Random random = new Random();

            while (ghostCount > 0)
            {
                int x = random.Next(0, m);
                int y = random.Next(0, n);

                if (space[x, y] != 'X' && (x != startX || y != startY))
                {
                    space[x, y] = 'X';
                    ghostCount--;
                }
            }
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (space[i, j] != 'X')
                    {
                        int adjacentGhosts = CountAdjacentGhosts(space, i, j);
                        space[i, j] = char.Parse(adjacentGhosts.ToString());
                    }
                }
            }
        }
        static int CountAdjacentGhosts(char[,] space, int x, int y)
        {
            int count = 0;
            int m = space.GetLength(0);
            int n = space.GetLength(1);

            for (int i = Math.Max(0, x - 1); i <= Math.Min(x + 1, m - 1); i++)
            {
                for (int j = Math.Max(0, y - 1); j <= Math.Min(y + 1, n - 1); j++)
                {
                    if (space[i, j] == 'X')
                        count++;
                }
            }
            return count;
        }
        static void Main()
        {
            Console.WriteLine("設定遊戲參數");
            Console.Write("請輸入空間大小：");
            string[] dimensions = Console.ReadLine().Split(',');
            int m = int.Parse(dimensions[0]);
            int n = int.Parse(dimensions[1]);

            Console.Write("請輸入鬼的數量：");
            int ghostCount = int.Parse(Console.ReadLine());

            if (ghostCount >= m * n || ghostCount <= 0 || m <= 0 || n <= 0)
            {
                Console.WriteLine("遊戲參數錯誤 !");
                Thread.Sleep(1000);
                Environment.Exit(0);
            }

            char[,] space = new char[m, n];
       
            Console.Clear();

            Console.Write("   ");                     // 初始模板
            for (int j = 0; j < n; j++)
            {
                char label = (char)('A' + j);
                Console.Write(" " + label);
            }
            Console.WriteLine();

            for (int i = 0; i < m; i++)
            {
                Console.Write(i.ToString().PadLeft(2) + " ");
                for (int j = 0; j < n; j++)
                {
                    Console.Write(" -");
                }
                Console.WriteLine();
            }

            Console.Write("請輸入要查看的位置：");       // 初始位置
            string input = Console.ReadLine();

            string[] coordinates = input.Split(',');

            int start_y = int.Parse(coordinates[0]);
            string xStr = coordinates[1].Trim();
            int start_x = char.ToUpper(xStr[0]) - 'A';

            RandomlyAssignGhosts(space, ghostCount, start_y, start_x);    // 分配鬼

            revealed = new char[m+2, n+2];

            for (int i = 0; i < m + 2; i++)                         // 製造初始 revealed
            {
                for (int j = 0; j < n + 2; j++)
                {
                    revealed[i, j] ='-';
                }
            }

            revealed[0, 0] = ' ';

            for (int i = 0; i < n; i++)
            {
                char label = (char)('A'+i);
                revealed[0, i + 2] = label;
            }

            for (int i = 0; i < m; i++)
            {
                revealed[i + 2, 0] = i.ToString()[0];
            }

            for (int i = 0; i < m + 2; i++)
            {
                revealed[i, 1] = ' ';
            }

            for (int i = 0; i < n + 2; i++)
            {
                revealed[1, i] = ' ';
            }

            revealed[start_y + 2, start_x + 2] = space[start_y, start_x];

            int count = m * n - ghostCount-1;
            bool win = false;
            
            while (true)
            {
                Console.Clear();

                for (int i = 0; i < m + 2; ++i)
                {
                    for (int j = 0; j < n + 2; j++)
                    {
                        Console.Write(revealed[i, j]+" ");
                    }
                    Console.WriteLine();
                }

                if (count == 0)
                {
                    win = true;
                    break;
                }

                Console.Write("請輸入要查看的位置：");
                input = Console.ReadLine();
                coordinates = input.Split(',');
                int y = int.Parse(coordinates[0]);
                xStr = coordinates[1].Trim();
                int x = char.ToUpper(xStr[0]) - 'A';

                if (revealed[y + 2, x + 2] != '-')
                {
                    Console.WriteLine("無效的輸入，請再試一次");
                }
                else if (space[y, x] == 'X')
                {
                    revealed[y + 2, x + 2] = space[y, x];
                    break;
                }
                else
                {
                    revealed[y + 2, x + 2] = space[y, x];
                    count -- ;
                }
            }

            if (win == true)
            {
                Console.WriteLine("你成功躲避所有的鬼了!");
            }   
            else  Console.WriteLine("你被鬼抓到了!"); 
            
            Thread.Sleep(5000);
            Environment.Exit(0);
        }
    }
}
