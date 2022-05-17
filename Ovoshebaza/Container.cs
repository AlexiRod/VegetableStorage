using System;
using System.Collections.Generic;
using System.Text;

namespace Ovoshebaza
{
    public class Container
    {
        private readonly Random random = new Random();

        public List<Box> boxes = new List<Box>();

        public string Id { get; private set; }
        public int MaxMass { get; }
        public double Damage { get; }

        public Container(List<Box> list, string id,  bool isWriting)
        {
            MaxMass = random.Next(50, 1001);
            Id = id;
            Damage = random.NextDouble() * 0.5;

            // Если будут лишние ящики, они добавятся в этот список.
            List<Box> excessBoxes = new List<Box>();
            int mass = 0;
            foreach (Box box in list)
            {
                if (mass + box.Mass <= MaxMass)
                    boxes.Add(box);
                else excessBoxes.Add(box);
            }

            // Если нужно, выведем сообщение о том, что имеются лишние ящики.
            if (isWriting && excessBoxes.Count != 0)
            {
                Console.WriteLine("---------\n");
                Console.WriteLine("Произошло превышение допустимой массы контейнера. Ящики, которые не были добавлены: ");
                foreach (Box box in excessBoxes)
                    Console.WriteLine(box);
                Console.WriteLine("\n---------");
            }
        }

        public Container(Box box, string id)
        {
            Id = id;
            boxes.Add(box);
        }
        public Container(List<Box> list, string id)
        {
            boxes = list;
            Id = id;
        }

        /// <summary>
        /// Цена контейнера.
        /// </summary>
        /// <returns></returns>
        public decimal GetPrice()
        {
            decimal price = 0;
            foreach (Box box in boxes)
                price += box.GetPriceWithDamage(Damage);

            return price;
        }

        /// <summary>
        /// Информация №1.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string ret = $"Id: {Id},{Environment.NewLine}Вместимость: {MaxMass} кг,{Environment.NewLine}" +
                $"Коэффициент повреждения: {Damage:f2},{Environment.NewLine}Цена: {GetPrice():c2},{Environment.NewLine}" +
                $"Информация о ящиках данного контейнера:{Environment.NewLine}";
            foreach (Box box in boxes)
                ret += box + $"{Environment.NewLine}";
            return ret; 
        }

        /// <summary>
        /// Информация №2.
        /// </summary>
        /// <returns></returns>
        public string GetInfo(int i)
        {
            if (i == 0)
                return $"{boxes[0].GetInfo()}";
            string ret = "";
            foreach (Box box in boxes)
                ret += box + $"{Environment.NewLine}";
            return ret;
        }

    }
}
