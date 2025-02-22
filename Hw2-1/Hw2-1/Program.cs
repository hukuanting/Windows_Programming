using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Hw2_1
{
    internal class Program
    {
        static List<string> transcript = new List<string>();
        static string Get_level(int score)
        {
            string level;
            if (score < 60)
            {
                level = "F";
            }
            else if (score >= 60 && score < 63)
            {
                level = "C-";
            }
            else if (score >= 63 && score < 67)
            {
                level = "C";
            }
            else if (score >= 67 && score < 70)
            {
                level = "C+";
            }
            else if (score >= 70 && score < 73)
            {
                level = "B-";
            }
            else if (score >= 73 && score < 77)
            {
                level = "B";
            }
            else if (score >= 77 && score < 80)
            {
                level = "B+";
            }
            else if (score >= 80 && score < 85)
            {
                level = "A-";
            }
            else if (score >= 85 && score < 90)
            {
                level = "A";
            }
            else
            {
                level = "A+";
            }
            return level;
        }
        static int Get_olg_gpa(int score)
        {
            int gpa;
            if (score < 50)
            {
                gpa = 0;
            }
            else if (score < 60 && score >= 50)
            {
                gpa = 1;
            }
            else if (score < 70 && score >= 60)
            {
                gpa = 2;
            }
            else if (score < 80 && score >= 70)
            {
                gpa = 3;
            }
            else
            {
                gpa = 3;
            }
            return gpa;
        }
        static double Get_new_gpa(string level)
        {
            double gpa;
            if (level == "F")
            {
                gpa = 0;
            }
            else if (level == "C-")
            {
                gpa = 1.7;
            }
            else if (level == "C")
            {
                gpa = 2.0;
            }
            else if (level == "C+")
            {
                gpa = 2.3;
            }
            else if (level == "B-")
            {
                gpa = 2.7;
            }
            else if (level == "B")
            {
                gpa = 3.0;
            }
            else if (level == "B+")
            {
                gpa = 3.3;
            }
            else if (level == "A-")
            {
                gpa = 3.7;
            }
            else if (level == "A")
            {
                gpa = 4.0;
            }
            else
            {
                gpa = 4.3;
            }
            return gpa;
        }
        static void Create(string input)
        {
            int score;
            string level;
            string new_input;
            bool check = false;
            string[] parts = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 4)
            {   
                int credit = Convert.ToInt32(parts[3]);
                score = Convert.ToInt32(parts[2]);
                if (score >= 0 && score <= 100)
                {
                    if (credit <= 10 && credit >= 0)
                    {
                        level = Get_level(score);
                        new_input = parts[1] + " " + parts[2] + " " + level + " " + parts[3];

                        for (int i = transcript.Count - 1; i >= 0; i--)
                        {
                            string subject = transcript[i];
                            if (subject.StartsWith(parts[1]))
                            {
                                check = true;
                            }
                        }
                        if (check == true)
                        {
                            Console.WriteLine("科目已存在");
                        }
                        else
                        {
                            transcript.Add(new_input);
                            Console.WriteLine("科目已新增");
                        }
                    }
                    else
                    {
                        Console.WriteLine("學分數異常! 請重新輸入!");
                    }
                }      
                else
                {
                    Console.WriteLine("成績分數異常! 請重新輸入!");
                }
            }
            else
            {
                Console.WriteLine("指令格式不符! 請重新輸入!");
            }
        }
        static void Delete(string input)
        {   
            bool check = false;
            string[] parts = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 2)
            {
                for (int i = transcript.Count - 1; i >= 0; i--)
                {
                    string subject = transcript[i];
                    if (subject.StartsWith(parts[1]))
                    {
                        transcript.Remove(subject);
                        Console.WriteLine("科目已刪除");
                        check = true;
                    }
                }
                if (check == false)
                {
                    Console.WriteLine("科目不存在");
                }
            }
            else
            {
                Console.WriteLine("指令格式不符! 請重新輸入!");
            }
        }
        static void Update(string input)
        {
            int score;
            string level;
            string new_input;
            bool check = false;
            string[] parts = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 4)
            {
                int credit = Convert.ToInt32(parts[3]);
                score = Convert.ToInt32(parts[2]);
                if (score >= 0 && score <= 100)
                {
                    if (credit <= 10 && credit >= 0)
                    {
                        level = Get_level(score);
                        new_input = parts[1] + " " + parts[2] + " " + level + " " + parts[3];
                        for (int i = transcript.Count - 1; i >= 0; i--)
                        {
                            string subject = transcript[i];
                            if (subject.StartsWith(parts[1]))
                            {
                                transcript.Remove(subject);
                                check = true;
                            }
                        }
                        if (check == true)
                        {
                            Console.WriteLine("科目已更新");
                            transcript.Add(new_input);
                        }
                        else
                        {
                            Console.WriteLine("科目不存在");
                        }
                    }
                    else
                    {
                        Console.WriteLine("學分數異常! 請重新輸入!");
                    }
                }
                else
                {
                    Console.WriteLine("成績分數異常! 請重新輸入!");
                }
            }
            else
            {
                Console.WriteLine("指令格式不符! 請重新輸入!");
            }
        }
        static void Print()
        {
            List<string> sortedTranscript = new List<string>(transcript);
            sortedTranscript = transcript.OrderByDescending(line =>
            {
                string[] parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length >= 3)
                {
                    int score = Convert.ToInt32(parts[1]);
                    return score;
                }
                return 0;
            }).ToList();

            Console.WriteLine("我的成績單");
            Console.WriteLine("編號  科目代碼  分數  等第  學分數");
            int i = 1;
            int totalScore = 0;
            int total_credit = 0;
            string code;
            int total_old_gpa = 0;
            double total_new_gpa = 0;
            int got_credit = 0;

            for (int j = sortedTranscript.Count - 1; j >= 0; j--)
            {
                string record = transcript[j];
                code = Convert.ToString(i);
                Console.WriteLine(code + "      " + record);
                i++;

                string[] parts = record.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 4)
                {
                    int score = Convert.ToInt32(parts[1]);
                    int credit = Convert.ToInt32(parts[3]);
                    string level = parts[2];

                    int old_gpa = Get_olg_gpa(score);  
                    double new_gpa = Get_new_gpa(level);

                    total_old_gpa += old_gpa;
                    total_new_gpa += new_gpa;
                    totalScore += score*credit;
                    if (score >= 60)
                    {
                        got_credit+= credit;
                    }

                    total_credit += credit;
                }
            }
            double averageScore = (double) totalScore / total_credit;
            double new_gpa_Score = (double)total_new_gpa / total_credit;
            double old_gpa_Score = (double)total_old_gpa / total_credit;
            Console.WriteLine("總平均分數： {0}", averageScore);
            Console.WriteLine("GPA： {0}/4.0 (舊制), {1}/4.3 (新制)", old_gpa_Score, new_gpa_Score);
            Console.WriteLine("實拿學分數/總學分數：{0}/{1}", got_credit, total_credit);
        }

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("##  成績計算器  ##");
                Console.WriteLine("1. 新增科目(create)");
                Console.WriteLine("2. 刪除科目(delete)");
                Console.WriteLine("3. 更新科目(update)");
                Console.WriteLine("4. 列印成績單(print)");
                Console.WriteLine("5. 退出選單(exit)");
                Console.Write("輸入要執行的指令操作： ");
                string input = Console.ReadLine();
                string[] parts = input.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length > 0)
                {
                    string mode = parts[0];
                    if (mode == "create" || mode == "delete" || mode == "update" || mode == "print" || mode == "exit")
                    {
                        switch (mode)
                        {
                            case "create":
                                Create(input);
                                break;
                            case "delete":
                                Delete(input);
                                break;
                            case "update":
                                Update(input);
                                break;
                            case "print":
                                Print();
                                break;
                            case "exit":
                                Console.WriteLine("Bye");
                                Thread.Sleep(1000);
                                Environment.Exit(0);
                                break;
                            default:
                                Console.WriteLine("指令格式不符! 請重新輸入!");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("指令格式不符! 請重新輸入!");
                    }
                }
                else
                {
                    Console.WriteLine("指令格式不符! 請重新輸入!");
                }
            }
        }
    }
}
