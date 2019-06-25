using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaShop
{
    class Program
    {
        static class Inputs
        {
            public static int Int(string S, bool c)
            {
                int r;
                do
                {
                    Console.Write(S);
                }
                while (!int.TryParse(Console.ReadLine(), out r));
                if (c) Console.Clear();
                return r;
            }

            public static double Double(string S, bool c)
            {
                double r;
                do
                {
                    Console.Write(S);
                }
                while (!double.TryParse(Console.ReadLine(), out r));
                if (c) Console.Clear();
                return r;
            }

            public static string String(string S, bool c)
            {
                Console.Write(S);
                string r= Console.ReadLine();
                if(c) Console.Clear();
                return r;
            }

        }

        static class Menu
        {
            public static void LoginMenu(ItemList[] itemLists)
            {
                int Z = -1;
                while (Z != 0)
                {
                    Console.WriteLine("1. Командно меню.");
                    Console.WriteLine("2. Меню за работници.");
                    Console.WriteLine("0. Изход");
                    Z = Inputs.Int("Въведи цяло число: ", true);

                    switch (Z)
                    {
                        case 1: AdminMenu(itemLists); break;
                        case 0: return;
                    }
                }
            }

            private static void AdminMenu(ItemList[] itemLists)
            {
                int Z = -1;
                while (Z != 0)
                {
                    Console.WriteLine("1. Списъци с продукти.");
                    Console.WriteLine("2. Бюджет.");
                    Console.WriteLine("0. Назад");
                    Z = Inputs.Int("Въведи цяло число: ", true);
                    switch (Z)
                    {
                        case 1: ProductTypes(itemLists); break;
                        //case 2:
                        case 0: return;
                    }
                }
            }

            private static void ProductTypes(ItemList[] itemLists)
            {
                int Z = -1;
                while (Z != 0)
                {
                    Console.WriteLine("1. Пици.");
                    Console.WriteLine("2. Десерти.");
                    Console.WriteLine("3. Напитки.");
                    Console.WriteLine("4. Добавки.");
                    Console.WriteLine("0. Изход");
                    Z = Inputs.Int("Въведи цяло число: ", true);

                    switch (Z)
                    {
                        case 1: itemLists[0].Menu(); break;
                        case 2: itemLists[1].Menu(); break;
                        case 3: itemLists[2].Menu(); break;
                        case 4: itemLists[3].Menu(); break;
                        case 0: return;
                    }
                    
                }
            }
        }

        class Item
        {
            private double buyPrice, sellPrice;
            private string name, description;
            private int count;

            public Item()
            {
                
                name = Inputs.String("Име на продукта:", true);
                buyPrice = Inputs.Double("Цена на изкупуване: ", true);
                sellPrice = Inputs.Double("Цена на продаване: ", true);
                count = Inputs.Int("Брой: ", true);
                description = Inputs.String("Описание на продукта: ", true);
            }
            /*
            public Item(string a,double b, double c, int d, string e)
            {
                name = a;
                buyPrice = b;
                sellPrice = c;
                count = d;
                description = e;
            }
            */

            public void GetValues(out string a, out double b, out double c, out int d, out string e)
            {
                a = name;
                b = buyPrice;
                c = sellPrice;
                d = count;
                e = description;
            }

            public void Display()
            {
                Console.WriteLine("Име:{0};  Цена1:{1};  Цена2:{2};  Брой:{3};  Описание:{4};",name,buyPrice,sellPrice,count,description);
            }

            public void Change()
            {
                int Z = -1;
                Console.WriteLine("1. Име.");
                Console.WriteLine("2. Цена на изкууване.");
                Console.WriteLine("3. Цена на продаване.");
                Console.WriteLine("4. Брой.");
                Console.WriteLine("5. Описание.");
                Console.WriteLine("0. Назад.");
                Z = Inputs.Int("Въведи цяло число: ", true);

                switch (Z)
                {
                    case 1: name = Inputs.String("Въведи име: ",true); break;
                    case 2: buyPrice = Inputs.Double("Въведи цена:", true); break;
                    case 3: sellPrice = Inputs.Int("Въведи цена:", true); break;
                    case 4: count = Inputs.Int("Въведи брой:", true); break;
                    case 5: description = Inputs.String("Въведи описание:", true); break;
                    case 0: return; 
                }
            }

            public void Buy()
            {
                count += Inputs.Int("Брой: ", true);
                //money-
            }

            public void Sell()
            {
                //count--
                //money--
            }


        }

        class ItemList
        {
            List<Item> items = new List<Item>();
            private int lenght = 0;
            private void AddItem()
            {
                items.Add(new Item());
                lenght++;
            }

            private void RemoveItem(int Z)
            {
                items.RemoveAt(Z);
                lenght--;
            }

            private void ChangeItem(int Z)
            {
                items[Z].Change();
            }

            private void DisplayList()
            {

                for(int i = 0; i < lenght; i++)
                {
                    Console.Write("ID:{0};  ",i);
                    items[i].Display();
                }
            }

            private void BuyProduct(int Z)
            {
                items[Z].Buy();
            }

            private void SellProduct(int Z)
            {
                items[Z].Sell();
            }

            public void Menu()
            {
                int Z = -1;
                while (Z != 0)
                {
                    Console.WriteLine("1. Таблица с продуктите.");
                    Console.WriteLine("2. Добавяне на продукт.");
                    Console.WriteLine("3. Промяна на продукт.");
                    Console.WriteLine("4. Премахване на продукт.");
                    Console.WriteLine("5. Купуване на продукти.");
                    Console.WriteLine("0. Назад.");
                    Z = Inputs.Int("Въведи цяло число: ", true);

                    switch (Z)
                    {
                        case 1: DisplayList(); break;
                        case 2: AddItem(); break;
                        case 3: ChangeItem(Inputs.Int("ID на продукта: ", true)); break;
                        case 4: RemoveItem(Inputs.Int("ID на продукта: ", true)); break;
                        case 5: BuyProduct(Inputs.Int("ID на продукта: ", true)); break;
                        case 0: return;
                    }
                }
            }
        }

        static class Bujet
        {
            private static double money;
        }

        static void Main(string[] args)
        {
            ItemList[] itemLists = new ItemList[4];

            itemLists[0] = new ItemList();//pizza
            itemLists[1] = new ItemList();//dessert
            itemLists[2] = new ItemList();//drink
            itemLists[3] = new ItemList();//dobavki


            Menu.LoginMenu(itemLists);
        }
    }
}
