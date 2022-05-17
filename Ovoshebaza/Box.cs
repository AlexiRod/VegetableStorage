using System;
using System.Collections.Generic;
using System.Text;

namespace Ovoshebaza
{
    public class Box
    {

        public string Id { get; private set; }
        public int Mass { get; private set; }
        public decimal Price { get; private set; }

        public Box(string id, int mass, decimal price)
        {
            Id = id;
            Mass = mass;
            Price = price;
        }

        // Метод для изменения свойства с приватным типом доступа.
        public void ChangePrice(decimal newPrice)
        {
            Price = newPrice;
        }
        public void ChangeMass(int mass)
        {
            Mass = mass;
        }

        // Пересчет цены с повреждением.
        public decimal GetPriceWithDamage(double damage)
        {
            return Price * Mass * (1 - (decimal)damage);
        }

        public override string ToString()
        {
            return $"Id: {Id}, Масса: {Mass} кг, Цена за кг: {Price:c2}";
        }
        public string GetInfo()
        {
            return $"{Id}; Масса: {Mass} кг, Цена за кг: {Price:c2} (Рыночная цена (масса * цена): {GetPriceWithDamage(0):c2})";
        }
    }
}
