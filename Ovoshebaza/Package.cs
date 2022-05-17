using System;
using System.Collections.Generic;
using System.Text;

namespace Ovoshebaza
{
    public class Package
    {
        private readonly Random random = new Random();
        public List<Container> containeres = new List<Container>();
       
        public decimal Price { get; private set; }

        public Package(List<Box> boxes, int conCount)
        {
            decimal price = 0;
            foreach (Box box in boxes)
            {
                price += box.GetPriceWithDamage(0);
                int newMass = random.Next((int)(box.Mass * 0.8), (int)(box.Mass * 1.2));
                int count = random.Next(1, conCount + 1);
                for (int i = 0; i < count; i++)
                {
                    if (i != count-1)
                    {
                        Box b = new Box(box.Id, random.Next(1, (int)(newMass*0.9)), box.Price);
                        newMass -= b.Mass;
                        containeres.Add(new Container(b, "c#" + random.Next(0, 10000)));
                    }
                    else containeres.Add(new Container(new Box(box.Id, newMass, box.Price), "c#" + random.Next(0, 10000)));
                }
            }

            Price = random.Next((int)(price*0.8m), (int)(price * 1.3m));
        }

        public override string ToString()
        {
            string ret = "Содержимое закупки:\n";
            foreach (Container container in containeres)
                ret += $"{container.Id};\nСодержимое: " + container.GetInfo(0) + "\n";
            ret += $"Цена закупки: {Price:c2}\n";
            return ret;
        }
    }
}
