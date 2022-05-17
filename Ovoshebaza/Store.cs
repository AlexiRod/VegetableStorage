using System;
using System.Collections.Generic;
using System.Text;

namespace Ovoshebaza
{
    class Store
    {
        public static readonly Random random = new Random();


        public static List<Container> containers = new List<Container>();
        public static int MaxCount { get; private set; }

        public static decimal Price { get; private set; }

        static Store()
        {
            // По умолчанию цена за контейнер и максимальное их количество определяю я. 
            MaxCount = 10;
            Price = 50;
        }

        // Методы для обращения к закрытым статическим полям.
        public static void ChangeCount(int newCount)
        {
            MaxCount = newCount;
        }
        public static void ChangePrice(decimal newPrice)
        {
            Price = newPrice;
        }

        /// <summary>
        /// Удаление лишних контейнеров.
        /// </summary>
        /// <returns></returns>
        public static List<Container> RecalculateContainers()
        {
            List<Container> excessContainers = new List<Container>();
            foreach (Container container in containers)
                if (container.GetPrice() < Price)
                {
                    excessContainers.Add(container);
                }
            int i = 0;
            int countOfContainers = containers.Count;
            while (countOfContainers > MaxCount)
            {
                excessContainers.Add(containers[i]);
                countOfContainers--;
                i++;
            }

            foreach (Container con in excessContainers)
                containers.Remove(con);
            return excessContainers;
        }

        /// <summary>
        /// Добавление контейнера и вывод сообщения в случае переполнения.
        /// </summary>
        /// <param name="container"></param>
        /// <param name="messgae"></param>
        /// <returns></returns>
        public static bool TryToAddContainer(Container container, ref string messgae)
        {
            if (container.GetPrice() >= Price)
            {
                // Если нужно заменить контейнер, выберем случайный и удалим его.
                if (MaxCount == containers.Count)
                {
                    // По условию заменить нужно любой не последний. Выберем его случайно, удалим,
                    // а новый контейнер добавим в конец - он как раз станет самым поздним добавленным.
                    int ind = random.Next(0, containers.Count);
                    messgae = $"Количество контейнеров на складе достигло максимума. Добавленный контейнер {container.Id} " +
                        $"заменил раннее добавленный контейнер {containers[ind].Id}. ";
                    containers.RemoveAt(ind);
                }
                containers.Add(container);
                return true;
            }
            return false; 
        }

        /// <summary>
        /// Удаление контейнера.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool TryToDeleteContainer(string id) 
        {
            bool isFound = false;
            foreach (Container container in containers)
                if (container.Id == id)
                {
                    isFound = true;
                    containers.Remove(container);
                    break;
                }
            return isFound;
        }

        public static string GetInfo(int i)
        {
            string ret = "Контейнеры на складе:\n";
            if (i != 3 && i != 1)
                ret = $"Вместимость склада: {MaxCount} контейнеров, Информация обо всех контейнерах на складе:\n";
            if (i != 3 && i == 1)
                ret = $"Вместимость склада: {MaxCount} контейнеров, Плата за хранение одного: {Price:c2}, Информация обо всех контейнерах на складе:\n";
            if (i == 1)
                foreach (Container container in containers)
                    ret += container + Environment.NewLine;
            if (i == 2 || i == 3)
                foreach (Container container in containers)
                    ret += $"\n* Контейнер {container.Id} Содержимое:\n" + container.GetInfo(0) + Environment.NewLine;
            return ret;
        }
    }
}
