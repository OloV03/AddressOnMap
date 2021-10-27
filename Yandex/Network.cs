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
    public class Network
    {
        public void Inputvalue(ChromeDriver chromeDriver, string value)
        {

            List<IWebElement> webElements = chromeDriver.FindElementsByClassName("input__control").ToList();
            foreach (var item in webElements)
            {
                if (!item.Displayed)
                    continue;
                if (item.GetAttribute("placeholder") != "Поиск мест и адресов")
                    continue;
                item.Clear();
                item.SendKeys(value);
            }

            Thread.Sleep(1000);
            IWebElement button = chromeDriver.FindElementByXPath("/html/body/div[1]/div[2]/div[3]/div/div/div/div/form/div[3]/button");
            button.Click();
            Thread.Sleep(1000);

            IWebElement webElement = chromeDriver.FindElementByClassName("toponym-card-title-view__coords-badge");

            double xi = Convert.ToDouble(webElement.Text.Substring(0, 9));

            double yi = Convert.ToDouble(webElement.Text.Substring(10, 10));

            Console.WriteLine("Координаты (" + value + "): " + webElement.Text);

            double x = 55.752692;
            double y = 37.620902;

            double xC = 55.776049; // х Севера
            double xY = 55.729335; // х Юга

            double yZ = 37.581097; // y Запада 
            double yV = 37.660707; // y Востока

            if (xi <= xC && xi >= xY)
            {
                if (yi <= yV && yi >= yZ) Console.WriteLine("Центр " + value);
            }

            else if (xi > xC)
            {
                if (yi > y) Console.WriteLine("Северо-Восток " + value);
                else Console.WriteLine("Северо-Запад " + value);
            }

            else if (xi < xY)
            {
                if (yi > y) Console.WriteLine("Юго-Восток " + value);
                else Console.WriteLine("Юго-Запад " + value);
            }

            else if (yi > yV)
            {
                if (xi > x) Console.WriteLine("Северо-Восток " + value);
                else Console.WriteLine("Юго-Восток " + value);
            }

            else if (yi < yZ)
            {
                if (xi > x) Console.WriteLine("Северо-Запад " + value);
                else Console.WriteLine("Юго-Запад " + value);
            }


            Console.WriteLine("Готово");
        }

        public void valueOnMap(ChromeDriver chrome, string[] values)
        {
            int i = 0;
            chrome.Navigate().GoToUrl("https://yandex.ru/maps/213/moscow/?ll=37.612038%2C55.733991&mode=routes&rtext=&rtt=auto&z=11.88");
            List<IWebElement> webElements = chrome.FindElementsByTagName("input").ToList();
            List<IWebElement> webElements1 = chrome.FindElementsByTagName("span").ToList();

            foreach (var item in webElements)
            {
                if (!item.Displayed)
                    continue;
                if (item.GetAttribute("class") != "input__control")
                    continue;
                if (item.GetAttribute("placeholder") != "Откуда")
                    continue;
                item.SendKeys(values[i] + "\n");
                i++;
            }

            foreach (var item in webElements)
            {
                if (!item.Displayed)
                    continue;
                if (item.GetAttribute("class") != "input__control")
                    continue;
                if (item.GetAttribute("placeholder") != "Куда")
                    continue;
                item.SendKeys(values[i] + "\n");
                i++;
            }

            for (int j = 0; j < values.Length - 2; j++)
            {
                // жмем кнопку добавить точку
                webElements = chrome.FindElementsByClassName("route-form-view__add").ToList();
                foreach (var item in webElements)
                {
                    if (!item.Displayed)
                        continue;
                    if (item.Text != "Добавить точку")
                        continue;
                    item.Click();
                    break;
                }

                webElements = chrome.FindElementsByTagName("input").ToList();
                foreach (var item in webElements)
                {
                    if (!item.Displayed)
                        continue;
                    if (item.GetAttribute("class") != "input__control")
                        continue;
                    if (item.GetAttribute("placeholder") != "Куда")
                        continue;
                    item.SendKeys(values[i] + "\n");
                    i++;
                }
            }
        }
    }
}
