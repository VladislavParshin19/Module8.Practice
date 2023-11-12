using System;
using System.Collections.Generic;

namespace Module8.Practice
{
    //___1___
    /*В С # индексация начинается с нуля, 
     *но в некоторых языках программирования это не так. Например, 
     *в Turbo Pascal индексация массиве начинается с 1. Напишите класс RangeOfArray, 
     *который позволяет работать с массивом такого типа, в котором индексный диапазон устанавливается пользователем. Например, 
     *в диапазоне от 6 до 10, или от –9 до 15.*/
    class RangeOfArray
    {
        int[] arr = null;
        int nIndex;
        int vIndex;
        public RangeOfArray(int nIndex, int vIndex)
        {
            this.nIndex = nIndex;
            this.vIndex = vIndex;
            arr = new int[vIndex - nIndex + 1];

            Random random = new Random();

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = random.Next(20);
            }

        }
        public override string ToString()
            {
            string result = "";

            for(int i = 0; i < arr.Length; i++)
            {
                result += $"{arr[i]} ";
            }
            return result;
        }
        public int this[int num]
        {
            get
            {
                int aIndex = num - nIndex;
                if(aIndex < 0)
                {
                    throw new Exception();

                }
                else if(aIndex > arr.Length)
                {
                    throw new Exception();
                }
                else
                {
                    return arr[aIndex];
                }
            }
        }
    }

    //___2___
    /*Написать программу «Продуктовый супермаркет»:
     *выбирается ряд продуктов,
     *рассчитывается их сумма с учетом скидки в 5% 
     *(скидка предоставляется, если покупка сделана с 8.00 до 12.00 по текущему времени) */
    class Product
    {
        public string name { get; set; }
        public double price { get; set; }
    }

    class SuperMarket
    {
        List<Product> products = null;

        public SuperMarket()
        {
            products = new List<Product>();
            products.Add(new Product() { price = 1000, name = "moloko"});
            products.Add(new Product() { price = 3000, name = "hleb"});
            products.Add(new Product() { price = 5000, name = "smetana"});
        }
        
        public double this[string name]
        {
            get 
            {
                double sum = 0;

                TimeSpan start = new TimeSpan(8, 0, 0);
                TimeSpan end = new TimeSpan(12, 0, 0);

                foreach (Product prd in products)
                {
                    if (prd.name == name)
                        sum += prd.price;
                }              

                if (DateTime.Now.TimeOfDay > start &&
                    DateTime.Now.TimeOfDay < end)
                    return sum * 0.95;

                else
                    return sum;
            }
        }
    }

    //____3____
    /*В файле хранится информация об объеме продаж товара за пять последних месяцев. 
     *С помощью метода наименьших квадратов спрогнозировать объемы продаж на следующие три месяца. 
     *В качестве линии тренда выбрать линейную функцию.*/
    class Product2
    {
        public DateTime Date { get; set; }
        public string name { get; set; }
        public int NumberOfSold { get; set; }
    }

    class Function
    {
        List<Product2> products2 = null;

        public Function()
        {
            products2 = new List<Product2>();
            products2.Add(new Product2 { Date = new DateTime(2023, 1, 1), name = "ProductA", NumberOfSold = 100 });
            products2.Add(new Product2 { Date = new DateTime(2023, 2, 1), name = "ProductA", NumberOfSold = 90 });
            products2.Add(new Product2 { Date = new DateTime(2023, 3, 1), name = "ProductA", NumberOfSold = 104 });
            products2.Add(new Product2 { Date = new DateTime(2023, 4, 1), name = "ProductA", NumberOfSold = 150 });
            products2.Add(new Product2 { Date = new DateTime(2023, 5, 1), name = "ProductA", NumberOfSold = 120 });
        }

        public double Predict()
        {
            double sumX = 0, sumY = 0, sumXY = 0, sumX2 = 0;

            for (int i = 0; i < products2.Count; i++)
            {
                int x = i + 1;
                int y = products2[i].NumberOfSold;

                sumX += x;
                sumY += y;
                sumXY += x * y;
                sumX2 += x * x;
            }

            double den = products2.Count * sumX2 - sumX * sumX;

            if (den == 0)
            {
                throw new Exception();
            }

            double B = (products2.Count * sumXY - sumX * sumY) / den;
            double A = (sumY - B * sumX) / products2.Count;
            return A + B * (products2.Count + 1);
        }
    }
    class Program
    {

        static void Main(string[] args)
        {
            //__1__
            RangeOfArray array = new RangeOfArray(6, 10);
            Console.WriteLine(array.ToString());

            //__2__
            SuperMarket supermarket = new SuperMarket();
            Console.WriteLine(supermarket["smetana"]);

            //__3__
            Function function = new Function();
            double predictedValue = function.Predict();
            Console.WriteLine($"Прогноз на следующий месяц: {predictedValue}");

            Console.ReadLine();
        }
    }
}
