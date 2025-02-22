using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hw3_2
{
    internal class Program
    {
        static ArrayList members = new ArrayList();
        class Member
        {
            public string Name { get; set; }
            public string Department { get; set; }
            public string ID { get; set; }
            public int Level { get; set; } = 1;
            public string Title { get; set; } = "無";
            public string GetLevelDescription()
            {
                switch (Level)
                {
                    case 1:
                        return "盟新社員";
                    case 2:
                        return "資深社員";
                    default:
                        return "永久社員";
                }
            }
            public override string ToString()
            {
                return $"{Name}  {Department}  {ID}  {GetLevelDescription()}  {Title}";
            }

            public Member(string name, string department, string id)
            {
                Name = name;
                Department = department;
                ID = id;
            }

            public void UpgradeLevel()
            {
                Level++;
            }

            public void SetTitle(string title)
            {
                Title = title;
            }
        }

        static void Register(string name, string department, string id)
        {
            // 檢查是否已經存在相同的名字、系所、學號的成員
            bool exists = false;
            foreach (Member member in members)
            {
                if (member.Name == name && member.Department == department && member.ID == id)
                {
                    Console.WriteLine($"{name} 已成功登記。");
                    member.UpgradeLevel();
                    exists = true;
                    break;
                }
            }

            if (!exists)
            {
                Member newMember = new Member(name, department, id);
                members.Add(newMember);
                Console.WriteLine($"{name} 已成功登記。");
            }
        }

        static void Search(string query)
        {
            bool found = false;
            Console.WriteLine("搜尋結果：");
            foreach (Member member in members)
            {
                if (member.Name == query || member.Department == query || member.ID == query || member.Level.ToString() == query || member.Title == query)
                {
                    Console.WriteLine(member.ToString());
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine($"找不到 {query} 的相關資訊。");
            }
        }

        static void Entitle(string name, string department, string id, string title)
        {
            bool found = false;
            bool isPresident = title.Contains("社長");

            foreach (Member member in members)
            {
                if (member.Name == name && member.Department == department && member.ID == id)
                {
                    found = true;
                    if (isPresident)
                    {
                        Console.WriteLine("我們社長只有阿明一個人，你不要想篡位！");
                    }
                    else
                    {
                        member.SetTitle(title);
                        Console.WriteLine($"{name} 已晉升為 {title}");
                    }
                    break;
                }
            }

            if (!found)
            {
                Console.WriteLine("找不到這個人ㄝ");
            }
        }

        static void Check()
        {
            Console.WriteLine("--------------------------------------------------------------------");
            foreach (Member member in members)
            {
                Console.WriteLine($"{member.Name.PadRight(10)}  {member.Department.PadRight(15)}  {member.ID.PadRight(15)}  {member.GetLevelDescription().PadRight(15)}  {member.Title.PadRight(15)}");
            }
            Console.WriteLine("--------------------------------------------------------------------");
        }
        static void Help()
        {
            Console.WriteLine("-----------------------------------------------------------------------------");
            Console.WriteLine($"{"新增社員資訊：".PadRight(10)}" + $"{"register".PadRight(10)}" +
             $"{"name".PadRight(10)}" + $"{"department".PadRight(15)}" + $"{"ID".PadRight(10)}");

            Console.WriteLine($"{"以特定屬性查詢：".PadRight(9)}" + $"{"search".PadRight(10)}" +
             $"{"name".PadRight(10)}" + $"{"tag".PadRight(15)}" + $"{"Want_search_string".PadRight(10)}");

            Console.WriteLine($"{"授予社權職位：".PadRight(10)}" + $"{"entitle".PadRight(10)}" +
            $"{"name".PadRight(10)}" + $"{"department".PadRight(15)}" + $"{"ID".PadRight(10)}");

            Console.WriteLine($"{"所有社員列表：".PadRight(10)}" + $"{"check".PadRight(10)}");

            Console.WriteLine($"{"指令格式列表：".PadRight(10)}" + $"{"help".PadRight(10)}");

            Console.WriteLine($"{"離開此程式：".PadRight(11)}" + $"{"exit".PadRight(10)}");
            Console.WriteLine("-----------------------------------------------------------------------------");
        }

        static void Main(string[] args)
        {
            Console.WriteLine("--------------------------###   社員資料登記   ###--------------------------");
            Console.WriteLine($"{"新增社員資訊：".PadRight(10)}" + $"{"register".PadRight(10)}" +
             $"{"name".PadRight(10)}" + $"{"department".PadRight(15)}" + $"{"ID".PadRight(10)}");

            Console.WriteLine($"{"以特定屬性查詢：".PadRight(9)}" + $"{"search".PadRight(10)}" +
             $"{"name".PadRight(10)}" + $"{"tag".PadRight(15)}" + $"{"Want_search_string".PadRight(10)}");

            Console.WriteLine($"{"授予社權職位：".PadRight(10)}" + $"{"entitle".PadRight(10)}" +
            $"{"name".PadRight(10)}" + $"{"department".PadRight(15)}" + $"{"ID".PadRight(10)}");

            Console.WriteLine($"{"所有社員列表：".PadRight(10)}" + $"{"check".PadRight(10)}");

            Console.WriteLine($"{"指令格式列表：".PadRight(10)}" + $"{"help".PadRight(10)}");

            Console.WriteLine($"{"離開此程式：".PadRight(11)}" + $"{"exit".PadRight(10)}");

            while (true)
            {
                Console.WriteLine(" ");
                string input = Console.ReadLine();
                string[] part = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                switch (part[0])
                {
                    case "register":
                        if (part.Length == 4)
                        {
                            Register(part[1], part[2], part[3]);
                        }
                        else
                        {
                            Console.WriteLine("錯誤的輸入！");
                        }
                        Console.WriteLine();
                        break;
                    case "search":
                        if (part.Length == 2)
                        {
                            Search(part[1]);
                        }
                        else
                        {
                            Console.WriteLine("錯誤的輸入！");
                        }
                        Console.WriteLine();
                        break;

                    case "entitle":
                        if (part.Length == 5)
                        {
                            Entitle(part[1], part[2], part[3], part[4]);
                        }
                        else
                        {
                            Console.WriteLine("錯誤的輸入！");
                        }
                        Console.WriteLine();
                        break;
;
                    case "check":
                        Check();
                        Console.WriteLine();
                        break;
                    case "help":
                        Help();
                        Console.WriteLine();
                        break;
                    case "exit":
                        Console.WriteLine("感謝使用，退出網站。");
                        Thread.Sleep(1000);
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("不存在這個功能，有疑慮請打help");
                        Console.WriteLine();
                        break;
                }
            }
        }
    }
}
