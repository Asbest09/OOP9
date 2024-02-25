using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace OOP9
{
    class Program
    {
        static void Main(string[] args)
        {
            Cart cart = new Cart();

            Console.WriteLine("Введите ваш баланс");
            int money = Convert.ToInt32(Console.ReadLine());

            cart.GettingCart();

            cart.BuyProduct(money);

            Console.ReadKey();
        }
    }

    class Cart
    {
        private const string Exit = "exit";

        private List<Product> products = new List<Product>();

        public void GettingCart()
        {
            bool isWorking = true;

            while (isWorking)
            {
                Console.WriteLine($"Введите название продукта или {Exit} для завершения ввода");
                string input = Console.ReadLine();

                if (input == Exit) 
                    isWorking = false;
                else
                {
                    Console.WriteLine("Введите цену");
                    int price = Convert.ToInt32(Console.ReadLine());

                    Product product = new Product(price, input);
                    products.Add(product);
                }
            }
        }

        public void BuyProduct(int money)
        {
            int allPrice = 0;
            int minPrice = int.MaxValue;

            for (int i = 0; i < products.Count; i++)
            {
                allPrice += products[i].Price;

                if (products[i].Price < minPrice)
                    minPrice = products[i].Price;
            }

            if (minPrice > money)
                Console.WriteLine("Вам не хватает ни на один продукт");
            else
            {
                if (allPrice > money)
                    allPrice = DeletingProducts(products, allPrice, money);

                money -= allPrice;

                Console.WriteLine("Спасибо за покупку, ваш баланс - " + money);
            }
        }

        private int DeletingProducts(List<Product> products, int allPrice, int money)
        {
            Random rnd = new Random();

            Console.WriteLine("Вам не хватает денег");

            while (allPrice > money)
            {
                int index = rnd.Next(products.Count);

                allPrice -= products[index].Price;

                Console.WriteLine($"Вы убрали из корзины {products[index].Name}, общая стоимость - {allPrice}");

                products.RemoveAt(index);

                Console.WriteLine("Для продолжения нажмите Enter");

                Console.ReadKey();
            }

            return allPrice;
        }
    }

    class Product
    {
        public int Price { get; private set; }
        public string Name { get; private set; }

        public Product(int price, string name)
        {
            Price = price;
            Name = name;
        }
    }
}
