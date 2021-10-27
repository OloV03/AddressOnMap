using System;
using System.IO;
using System.Threading;
using System.Linq;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading.Tasks;

namespace Yandex
{
    class Program
    {
        static void Main(string[] args)
        {
            Network net = new Network();
            string[] value = new string[] { };

            Console.Title = "Встречи Судьбы";
            Console.ForegroundColor = ConsoleColor.Cyan;
            EnterWord();

            Console.WriteLine("Доступные команды:\n-1-: локация адреса+координаты \t-2-: расстановка адресов на Яндекс Картах");

            string choice = Console.ReadLine();
            if (choice == "1")
            {
                Console.WriteLine("\nПеренесите в окно консоли файл с адресами будующих встреч");
                Console.Write("Путь к файлу: ");
                string file = Console.ReadLine();
                value = AddressToArray(file);

                ChromeDriver chrome = new ChromeDriver();
                Console.Clear();
                chrome.Navigate().GoToUrl("https://yandex.ru/maps/213/moscow/?ll=37.622504%2C55.753215&z=10");
                for (int i = 0; i < value.Length; i++)
                {
                    net.Inputvalue(chrome, value[i]);
                    chrome.Navigate().Refresh();
                }
            }

            if (choice == "2")
            {
                Console.WriteLine("\nПеренесите в окно консоли файл с адресами будующих встреч");
                Console.Write("Путь к файлу: ");
                string file = Console.ReadLine();
                value = AddressToArray(file);

                ChromeDriver chrome = new ChromeDriver(".");
                Console.Clear();

                EnterWord();

                Console.WriteLine("Finished");
                chrome.Navigate().GoToUrl("https://yandex.ru/maps/213/moscow/?ll=37.622504%2C55.753215&z=10");
                net.valueOnMap(chrome, value);
            }

            if (choice == "3")
            {
                List<string> text = new List<string>();
                while (true)
                {
                    string x = Console.ReadLine();
                    if (x != "")
                    {
                        text.Add(x);
                    }
                    else break;
                }
                foreach (var item in text)
                {

                    Console.WriteLine(item);
                }
            }
            Console.ReadKey();
        }

        static string[] AddressToArray(string values)
        {
            List<string> result = new List<string>();

            using (StreamReader sr = new StreamReader(values))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    line = line.Trim();
                    result.Add(line);
                }
            }

            return result.ToArray();
        }

        static void EnterWord()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine(" Добро пожаловать в приложение \"Встречи Судьбы\"");
            Console.WriteLine("-------------------------------------------------\n\n");
            Console.ResetColor();
        }
    }
}
