using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Ovoshebaza
{
    public class Order
    {
        private readonly Random random = new Random();
        // Возможные овощи для заказа.
        public List<Box> allBoxes = new List<Box>();
        // Овощи в самом заказе.
        public List<Box> orderBoxes = new List<Box>();

        public decimal price = 0;
        public Order(int day, string path)
        {
            int level = (int)Math.Ceiling((decimal)day / 5);
            // Инициализация возможных овощей.
            string[] str = File.ReadAllLines(path);
            foreach (string str1 in str)
            {
                string[] s = str1.Split();
                allBoxes.Add(new Box(s[0], 0, Convert.ToInt32(s[1])));
            }

            // Генерация заказа.
            int countOfOrders = random.Next((int)(level*0.5), (int)(level*1.5));
            countOfOrders = Math.Max(countOfOrders, 1);

            for (int k = 0; k < countOfOrders; k++)
            {
                Box box = allBoxes[random.Next(0, allBoxes.Count)];
                int mass = random.Next(80 + level*20, level*160);
                orderBoxes.Add(new Box(box.Id, mass, box.Price));
                price += orderBoxes[k].GetPriceWithDamage(0) * 1.2m;
            }
            price *= 1 + (decimal)random.NextDouble() * 0.5m;
        }


        public override string ToString()
        {
            string ret = $"Прибыль: {price:c2}; ";
            foreach (Box box in orderBoxes)
                ret += box.GetInfo() + "\n"; 
            return ret; 
        }

    }
}
