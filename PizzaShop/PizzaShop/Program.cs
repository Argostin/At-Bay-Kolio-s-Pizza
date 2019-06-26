using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaShop
{
    class Program
    {
        /// <summary>
        /// //////////////////////////////   INPUT HELPER
        /// </summary>
        static class Inputs
        {
            public static int Int(string S, int n, int m)
            {
                int r;
                do
                {
                    Console.Write(S);
                }
                while (!int.TryParse(Console.ReadLine(), out r) || m<=r || n>r);
                Console.Clear();
                return r;
            }

            public static double Double(string S)
            {
                double r;
                do
                {
                    Console.Write(S);
                }
                while (!double.TryParse(Console.ReadLine(), out r));
                Console.Clear();
                return r;
            }

            public static string String(string S)
            {
                Console.Write(S);
                string r= Console.ReadLine();
                Console.Clear();
                return r;
            }

        }

        /// <summary>
        /// /////////////////////////////////        MENUS
        /// </summary>
        static class Menu
        {
            public static void LoginMenu(ItemList[] itemLists, List<Order> orderList)
            {
                int Z = -1;
                while (Z != 0)
                {
                    Console.WriteLine("1. Командно меню.");
                    Console.WriteLine("2. Меню за работници.");
                    Console.WriteLine("0. Изход");
                    Z = Inputs.Int("Въведи цяло число: ",0,3);

                    switch (Z)
                    {
                        case 1: AdminMenu(itemLists,orderList); break;
                        case 2: WorkerMenu(itemLists, orderList); break;
                        case 0: return;
                    }
                }
            }



            private static void SellMenu(ItemList[] itemLists, List<Order> orderList)
            {
                int Z = -1;
                while (Z != 0)
                {
                    Console.WriteLine("1. Пици.");
                    Console.WriteLine("2. Десерти.");
                    Console.WriteLine("3. Напитки.");
                    Console.WriteLine("4. Добавки.");
                    Console.WriteLine("0. Изход");
                    Z = Inputs.Int("Въведи цяло число: ",0,5);

                    switch (Z)
                    {
                        case 1: itemLists[0].MakeSell(orderList); break;
                        case 2: itemLists[1].MakeSell(orderList); break;
                        case 3: itemLists[2].MakeSell(orderList); break;
                        case 4: itemLists[3].MakeSell(orderList); break;
                        case 0: return;
                    }

                }
            }

            public static void WorkerMenu(ItemList[] itemLists, List<Order> orderList)
            {
                int Z = -1;
                while (Z != 0)
                {
                    Console.WriteLine("1. Пръчка.");
                    Console.WriteLine("2. Лист с сделките.");
                    Console.WriteLine("0. Назад.");
                    Z = Inputs.Int("Въведи цяло число: ",0,3);

                    switch (Z)
                    {
                        case 1: SellMenu(itemLists, orderList); break;
                        case 2: Order.DisplayList(orderList); break;
                        case 0: return;
                    }
                }
            }

            private static void AdminMenu(ItemList[] itemLists, List<Order> orderList)
            {
                int Z = -1;
                while (Z != 0)
                {
                    Console.WriteLine("1. Списъци с продукти.");
                    Console.WriteLine("2. Налични пари.");
                    Console.WriteLine("3. Сделки.");
                    Console.WriteLine("4. Продукти на свършване.");
                    Console.WriteLine("5. Добави пари.");
                    Console.WriteLine("0. Назад");
                    Z = Inputs.Int("Въведи цяло число: ",0,6);
                    switch (Z)
                    {
                        case 1: ProductTypes(itemLists, orderList); break;
                        case 2: Bujet.Display(); break;
                        case 3: Order.DisplayList(orderList); break;
                        case 4: for(int i = 0; i < 4; i++)itemLists[i].CheckCount();  break;
                        case 5: Bujet.AddToMoney(Inputs.Double("Количество пари: ")); break;
                        case 0: return;
                    }
                }
            }

            

            private static void ProductTypes(ItemList[] itemLists, List<Order> orderList)
            {
                int Z = -1;
                while (Z != 0)
                {
                    Console.WriteLine("1. Пици.");
                    Console.WriteLine("2. Десерти.");
                    Console.WriteLine("3. Напитки.");
                    Console.WriteLine("4. Добавки.");
                    Console.WriteLine("0. Изход");
                    Z = Inputs.Int("Въведи цяло число: ",0,5);

                    switch (Z)
                    {
                        case 1: itemLists[0].Menu(orderList); break;
                        case 2: itemLists[1].Menu(orderList); break;
                        case 3: itemLists[2].Menu(orderList); break;
                        case 4: itemLists[3].Menu(orderList); break;
                        case 0: return;
                    }
                    
                }
            }
        }

        /// <summary>
        /// //////////////////////////////////     PRODUCTS
        /// </summary>

        class Item
        {
            private double buyPrice, sellPrice;
            private string name, description;
            private int count;

            public Item()
            {
                
                name = Inputs.String("Име на продукта:");
                buyPrice = Inputs.Double("Цена на изкупуване: ");
                sellPrice = Inputs.Double("Цена на продаване: ");
                count = Inputs.Int("Брой: ",0,30000);
                description = Inputs.String("Описание на продукта: ");
            }

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
                Z = Inputs.Int("Въведи цяло число: ",0,6);

                switch (Z)
                {
                    case 1: name = Inputs.String("Въведи име: "); break;
                    case 2: buyPrice = Inputs.Double("Въведи цена:"); break;
                    case 3: sellPrice = Inputs.Double("Въведи цена:"); break;
                    case 4: count = Inputs.Int("Въведи брой:",0,30000); break;
                    case 5: description = Inputs.String("Въведи описание:"); break;
                    case 0: return; 
                }
            }

            public void Buy(List<Order> orderList)
            {
                int Z = Inputs.Int("Брой: ",0,30000);
                if (Bujet.TakeFromMoney(Z * buyPrice))
                {
                    count += Z;
                    Order order = new Order(DateTime.Now,name,buyPrice,Z,"Покупка",Z*buyPrice);
                    orderList.Add(order);
                }
                else Console.WriteLine("Няма достатъчно пари за покупката.");
            }
            public void Sell(List<Order> orderList)
            {
                int Z = Inputs.Int("Брой: ",0,30000);
                if (Z<=count)
                {
                    Bujet.AddToMoney(Z * sellPrice);
                    count -= Z;
                    Order order = new Order(DateTime.Now, name, sellPrice, Z, "Продажба", Z * sellPrice);
                    orderList.Add(order);
                    if (count < 6) Console.WriteLine("Продукта е на свършване!");
                    Console.ReadKey();
                }
                else Console.WriteLine("Няма достатъчно продукти за сделката.");
            }

            public void Fira(List<Order> orderList)
            {
                int Z = Inputs.Int("Брой: ",0,30000);
                if (Z <= count)
                {
                    count -= Z;
                    Order order = new Order(DateTime.Now, name, -buyPrice, Z, "Фира", Z * -buyPrice);
                    orderList.Add(order);
                    if (count < 6) Console.WriteLine("Продукта е на свършване!");
                    Console.ReadKey();
                }
                else Console.WriteLine("Грешно въведени продукти.");
            }

            public int GetCount()
            {
                return count;
            }


        }
        /// <summary>
        /// ////////////////////////                 LISTS OF PRODUCTS
        /// </summary>
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

            private void BuyProduct(int Z,List<Order> orderList)
            {
                items[Z].Buy(orderList);
            }

            private void SellProduct(int Z,List<Order> orderList)
            {
                items[Z].Sell(orderList);
            }

            public void Menu(List<Order> orderList)
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
                    Z = Inputs.Int("Въведи цяло число: ",0,6);

                    switch (Z)
                    {
                        case 1: DisplayList(); break;
                        case 2: AddItem(); break;
                        case 3: ChangeItem(Inputs.Int("ID на продукта: ",0,lenght)); break;
                        case 4: RemoveItem(Inputs.Int("ID на продукта: ", 0, lenght)); break;
                        case 5: BuyProduct(Inputs.Int("ID на продукта: ", 0, lenght), orderList); break;
                        case 0: return;
                    }
                }
            }

            public void MakeSell(List<Order> orderList)
            {
                int ID,type;
                
                DisplayList();

                ID = Inputs.Int("ID на продукта: ",0, lenght);
                type = Inputs.Int("1. Продажба.\n2.Фира.\n0. Отказ.\n", 0, 3);

                if (type == 1) items[ID].Sell(orderList);
                if (type == 2) items[ID].Fira(orderList);

            }
            

            public void CheckCount()
            {
                for(int i = 0; i < lenght; i++)
                {
                    if(items[i].GetCount()<=5) items[i].Display();
                }
            }
        }
        /// <summary>
        /// /////////////////////////                  MONEY MANAGER
        /// </summary>
        static class Bujet
        {
            private static double money;

            public static void AddToMoney(double X)
            {
                money += X;
            }

            public static bool TakeFromMoney(double X)
            {
                if (money > X) { money -= X; return true; }
                else return false;
            }

            public static void SetMoney(double X)
            {
                money = X;
            }

            public static double GetMoney()
            {
                return money;
            }

            public static void Display()
            {
                Console.WriteLine("Пари: {0}лв.", money);
            }


        }
        /// <summary>
        /// //////////////////////        ORDER
        /// </summary>

        class Order
        {
            DateTime date;
            string name, type;
            double price, value;
            int count;
            static int lenght=0;

            public Order(DateTime a, string b, double c, int d, string e, double f)
            {
                date = a;
                name = b;
                price = c;
                count = d;
                type = e;
                value = f;
                lenght++;
            }

            public DateTime GetDate()
            {
                return date;
            }

            public string GetName()
            {
                return name;
            }

            public string GettType()//вече GetType е използвано някъде ;-; затова има второ t
            {
                return type;
            }

            public double GetPrice()
            {
                return price;
            }

            public double GetValue()
            {
                return value;
            }

            public int GetCount()
            {
                return count;
            }

            private void Display()
            {
                Console.WriteLine("Дата:{0};  Име:{1};  Цена:{2}лв;  Брой:{3};  Вид:{4};  Стойност:{5}лв;",date,name,price,count,type,value);
            }

            public static void DisplayList(List<Order> orderList)
            {
                for(int i = 0; i < lenght; i++)
                {
                    orderList[i].Display();
                }
            }
        }

        /// <summary>
        /// ////////////////////             MAIN
        /// </summary>
        static void Main(string[] args)
        {
            List<Order> orderList = new List<Order>();

            ItemList[] itemLists = new ItemList[4];

            itemLists[0] = new ItemList();//pizza
            itemLists[1] = new ItemList();//dessert
            itemLists[2] = new ItemList();//drink
            itemLists[3] = new ItemList();//dobavki

			//test

            Menu.LoginMenu(itemLists,orderList);
        }
    }
}
