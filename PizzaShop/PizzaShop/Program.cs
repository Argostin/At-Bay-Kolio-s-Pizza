using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
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
                    Console.WriteLine("6. Създай нова база от данни.");
                    Console.WriteLine("0. Назад");
                    Z = Inputs.Int("Въведи цяло число: ",0,6);
                    switch (Z)
                    {
                        case 1: ProductTypes(itemLists, orderList); break;
                        case 2: Bujet.Display(); break;
                        case 3: Order.DisplayList(orderList); break;
                        case 4: for(int i = 0; i < 4; i++)itemLists[i].CheckCount();  break;
                        case 5: Bujet.AddToMoney(Inputs.Double("Количество пари: ")); break;
                        case 6: DataBase.Create(); break;
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

            public Item(string a = "", double b=0, double c=0, int d=0, string e="")
            {
                name = a;
                buyPrice = b;
                sellPrice = c;
                count = d;
                description = e;
                
            }

            public void UpdateDB(string nameOfList)
            {
                SQLiteConnection dbConn = new SQLiteConnection("Data Source=PizzaDataBase.sqlite;Version=3;");
                dbConn.Open();
                string sql = "insert into " + nameOfList + " (name,buyprice,sellprice,count,description) values ('" + name + "','" + buyPrice + "','" + sellPrice + "','" + count + "','" + description + "');";
                SQLiteCommand command = new SQLiteCommand(sql, dbConn);
                command.ExecuteNonQuery();
                dbConn.Close();

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
                    Order order = new Order(DateTime.Now.ToString(), name,buyPrice,Z,"Покупка",Z*buyPrice,true);
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
                    Order order = new Order(DateTime.Now.ToString(), name, sellPrice, Z, "Продажба", Z * sellPrice,true);
                    orderList.Add(order);
                    if (count < 6) { Console.WriteLine("Продукта е на свършване!"); Console.ReadKey(); }

                    }
                else Console.WriteLine("Няма достатъчно продукти за сделката.");
            }

            public void Fira(List<Order> orderList)
            {
                int Z = Inputs.Int("Брой: ",0,30000);
                if (Z <= count)
                {
                    count -= Z;
                    Order order = new Order(DateTime.Now.ToString(), name, -buyPrice, Z, "Фира", Z * -buyPrice,true);
                    orderList.Add(order);
                    if (count < 6) { Console.WriteLine("Продукта е на свършване!"); Console.ReadKey(); }

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
            private string nameOfList;

            List<Item> items = new List<Item>();
            private int lenght = 0;

            public ItemList(string n)
            {
                nameOfList = n;
            }

            public void AddItem(Item item, bool T)
            {
                string name;
                double buyPrice;
                double sellPrice;
                int count;
                string description;
                if (T)
                {
                    name = Inputs.String("Име на продукта:");
                    buyPrice = Inputs.Double("Цена на изкупуване: ");
                    sellPrice = Inputs.Double("Цена на продаване: ");
                    count = Inputs.Int("Брой: ", 0, 30000);
                    description = Inputs.String("Описание на продукта: ");
                    item = new Item(name, buyPrice, sellPrice, count, description);
                }

                items.Add(item);
                lenght++;
            }

            private void RemoveItem(int Z)
            {
                items.RemoveAt(Z);
                lenght--;
                UpdateList();
            }

            private void ChangeItem(int Z)
            {
                items[Z].Change();
                UpdateList();
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
                UpdateList();
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
                        case 2: AddItem(new Item(),true); break;
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
                UpdateList();
            }
            

            public void CheckCount()
            {
                for(int i = 0; i < lenght; i++)
                {
                    if(items[i].GetCount()<=5) items[i].Display();
                }
            }

            public void UpdateList()
            {
                SQLiteConnection dbConn = new SQLiteConnection("Data Source=PizzaDataBase.sqlite;Version=3;");
                dbConn.Open();
                string sql = "DELETE FROM " + nameOfList;
                SQLiteCommand command = new SQLiteCommand(sql, dbConn);
                command.ExecuteNonQuery();
                dbConn.Close();


                for (int i = 0; i < lenght; i++)
                {
                    items[i].UpdateDB(nameOfList);
                }

                

            }
        }
        /// <summary>
        /// /////////////////////////                  MONEY MANAGER
        /// </summary>
        static class Bujet
        {
            private static double money;

            public static void Load()
            {
                SQLiteConnection dbConn = new SQLiteConnection("Data Source=PizzaDataBase.sqlite;Version=3;");
                dbConn.Open();
                string sql = "select * from Money";
                SQLiteCommand command = new SQLiteCommand(sql, dbConn);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    money = double.Parse(reader["Money"].ToString());
                }
                dbConn.Close();
            }

            public static void Update()
            {
                SQLiteConnection dbConn = new SQLiteConnection("Data Source=PizzaDataBase.sqlite;Version=3;");
                dbConn.Open();
                string sql = "delete from Money";
                SQLiteCommand command = new SQLiteCommand(sql, dbConn);
                command.ExecuteNonQuery();

                sql = "INSERT INTO Money (Money) values ('" + money + "');";
                command = new SQLiteCommand(sql, dbConn);
                command.ExecuteNonQuery();
                dbConn.Close();
            }

            public static void AddToMoney(double X)
            {
                money += X;
                Update();
            }

            public static bool TakeFromMoney(double X)
            {
                if (money > X) { money -= X; Update(); return true; }
                else return false;
            }

            public static void SetMoney(double X)
            {
                money = X;
                Update();
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
            string date;
            string name, type;
            double price, value;
            int count;
            static int lenght=0;

            public Order(string a, string b, double c, int d, string e, double f,bool j)
            {
                date = a;
                name = b;
                price = c;
                count = d;
                type = e;
                value = f;
                lenght++;
                if(j) DataBase.AddOrder(a,b,c,d,e,f);
            }



            public string GetDate()
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
        /// //////////////////////            DATA BASE
        /// </summary>
        static class DataBase
        {
            public static void Create()
            {
                SQLiteConnection.CreateFile("PizzaDataBase.sqlite");

                SQLiteConnection dbConn = new SQLiteConnection("Data Source=PizzaDataBase.sqlite;Version=3;");
                dbConn.Open();

                string sql;
                //orders
                sql = "create table Orders (";
                sql += " 'ID' INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,";
                sql += " 'date' TEXT NOT NULL,";
                sql += " 'name'  TEXT NOT NULL,";
                sql += " 'price' NUMERIC NOT NULL,";
                sql += " 'count' INTEGER NOT NULL,";
                sql += " 'type' TEXT NOT NULL,";
                sql += " 'value' NUMERIC NOT NULL);";
                SQLiteCommand command = new SQLiteCommand(sql, dbConn);
                command.ExecuteNonQuery();
                //products

                string[] table = { "Pizzas", "Desserts", "Drinks", "Additions" };
                for(int i = 0; i < 4; i++)
                {
                    sql = "CREATE TABLE " + table[i] + " (";
                    sql += " 'ID'    INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,";
                    sql += " 'name'  TEXT NOT NULL,";
                    sql += " 'buyprice' NUMERIC NOT NULL,";
                    sql += " 'sellprice' NUMERIC NOT NULL,";
                    sql += " 'count' INTEGER NOT NULL,";
                    sql += " 'description' TEXT NOT NULL);";
                    command = new SQLiteCommand(sql, dbConn);
                    command.ExecuteNonQuery();
                }

                sql = "CREATE TABLE 'Money' ('Money' NUMERIC NOT NULL);";
                command = new SQLiteCommand(sql, dbConn);
                command.ExecuteNonQuery();

                dbConn.Close();
            }

            public static void AddOrder(string date, string name, double price, int count, string type, double value)
            {

                SQLiteConnection dbConn = new SQLiteConnection("Data Source=PizzaDataBase.sqlite;Version=3;");
                dbConn.Open();

                string sql = "INSERT INTO Orders (date,name,price,count,type,value) values ('" + name + "','" + date + "','" + price + "','" + count + "','" + type + "','" + value + "');";
                SQLiteCommand command = new SQLiteCommand(sql, dbConn);
                command.ExecuteNonQuery();

                dbConn.Close();
            }

            public static List<Order> LoadOrders()
            {
                List<Order> list = new List<Order>();
                SQLiteConnection dbConn = new SQLiteConnection("Data Source=PizzaDataBase.sqlite;Version=3;");
                dbConn.Open();

                string sql = "select * from Orders";
                SQLiteCommand command = new SQLiteCommand(sql, dbConn);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string date = reader["date"].ToString();
                    string name = reader["name"].ToString();
                    string type = reader["type"].ToString();
                    double price = double.Parse(reader["price"].ToString());
                    double value = double.Parse(reader["value"].ToString());
                    int count = int.Parse(reader["count"].ToString());
                    Order O = new Order(date, name, price, count, type, value, false);
                    list.Add(O);
                    
                }
                dbConn.Close();
                return list;
            }

            public static ItemList LoadItemList(string nameOfList)
            {
                ItemList list = new ItemList(nameOfList);

                SQLiteConnection dbConn = new SQLiteConnection("Data Source=PizzaDataBase.sqlite;Version=3;");
                dbConn.Open();

                string sql = "select *  from " + nameOfList;
                SQLiteCommand command = new SQLiteCommand(sql, dbConn);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string name = reader["name"].ToString();
                    string desctiption = reader["description"].ToString();
                    double buyprice = double.Parse(reader["buyprice"].ToString());
                    double sellprice = double.Parse(reader["sellprice"].ToString());
                    int count = int.Parse(reader["count"].ToString());

                    Item O = new Item(name, buyprice, sellprice, count, desctiption);
                    list.AddItem(O,false);

                }
                dbConn.Close();
                return list;
            }
            //DELETE FROM table_name
        }


        /// <summary>
        /// ////////////////////             MAIN
        /// </summary>
        static void Main(string[] args)
        {
            if (!File.Exists("PizzaDataBase.sqlite")) DataBase.Create();
            List<Order> orderList = DataBase.LoadOrders();
            ItemList[] itemLists = new ItemList[4];
            itemLists[0] = DataBase.LoadItemList("Pizzas");//pizza
            itemLists[1] = DataBase.LoadItemList("Desserts");//dessert
            itemLists[2] = DataBase.LoadItemList("Drinks");//drink
            itemLists[3] = DataBase.LoadItemList("Additions");//dobavki
            Bujet.Load();

            Menu.LoginMenu(itemLists,orderList);
        }
    }
}
