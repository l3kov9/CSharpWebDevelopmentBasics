namespace Shop
{
    using Shop.Models;
    using Shop.ShopDbContext;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;

    public class Startup
    {
        public static void Main()
        {
            using (var db = new ShopDb())
            {
                ClearDatabase(db);
                SaveSalesmen(db);
                ReadItemCommands(db);
                ProcessCommands(db);
                //PrintSalesmenWithCustomersCount(db);
                //PrintCustomersWithOrdersCountAndReviewCount(db);
                //PrintCustomerOrdersAndReviews(db);
                //PrintCustomerData(db);
                PrintOrdersWithMoreThanOneItem(db);
            }
        }

        private static void PrintOrdersWithMoreThanOneItem(ShopDb db)
        {
            var customerId = int.Parse(Console.ReadLine());

            //var customerOrderWithMoreThanOneItem = db
            //    .Customers
            //    .Where(c => c.Id == customerId)
            //    .Select(c => new
            //    {
            //        Orders=c.Orders.Count(o=>o.Items.Count>1)
            //    })
            //    .FirstOrDefault();
            //Console.WriteLine($"Orders:{customerOrderWithMoreThanOneItem.Orders}");

            var order = db
                .Orders
                .Where(o => o.CustomerId == customerId)
                .Where(o => o.Items.Count > 1)
                .Count();

            Console.WriteLine($"Orders:{order}");
        }

        private static void PrintCustomerData(ShopDb db)
        {
            var customerId = int.Parse(Console.ReadLine());

            var customerData = db
                .Customers
                .Where(c => c.Id == customerId)
                .Select(c => new
                {
                    c.Name,
                    Orders=c.Orders.Count,
                    Revies=c.Reviews.Count,
                    SalesmanName=c.Salesman.Name
                })
                .FirstOrDefault();

            Console.WriteLine($"Customer: {customerData.Name}{Environment.NewLine}Orders count: {customerData.Orders}");
            Console.WriteLine($"Reviews: {customerData.Revies}{Environment.NewLine}Salesman: {customerData.SalesmanName}");
        }

        private static void PrintCustomerOrdersAndReviews(ShopDb db)
        {
            var customerId = int.Parse(Console.ReadLine());

            var customerData=db
                .Customers
                .Where(c => c.Id == customerId)
                .Select(c => new
                {
                    Orders = c.Orders
                        .Select(o => new
                        {
                            o.Id,
                            Items = o.Items.Count
                        })
                        .OrderBy(o => o.Id),
                    Reviews = c.Reviews.Count
                })
                .FirstOrDefault();

            foreach (var order in customerData.Orders)
            {
                Console.WriteLine($"order {order.Id}: {order.Items} items");
            }

            Console.WriteLine($"reviews: {customerData.Reviews}");
        }

        private static void ReadItemCommands(ShopDb db)
        {
            while (true)
            {
                var commandInfo = Console.ReadLine()
                .Split(';');

                if (commandInfo[0] == "END")
                {
                    break;
                }

                AddItem(db,commandInfo[0], decimal.Parse(commandInfo[1]));
            }
        }

        private static void AddItem(ShopDb db, string name, decimal price)
        {
            db
                .Items
                .Add(new Item { Name = name, Price = price });
            db.SaveChanges();
        }

        private static void PrintCustomersWithOrdersCountAndReviewCount(ShopDb db)
        {
            db
                .Customers
                .Select(c => new
                {
                    c.Name,
                    Orders = c.Orders.Count,
                    Reviews = c.Reviews.Count
                })
                .OrderByDescending(c=>c.Orders)
                .ThenByDescending(c=>c.Reviews)
                .ToList()
                .ForEach(c => Console.WriteLine($"{c.Name}{Environment.NewLine}Orders: {c.Orders}{Environment.NewLine}Reviews: {c.Reviews}"));
        }

        private static void PrintSalesmenWithCustomersCount(ShopDb db)
        {
            db
                .Salesmans
                .Select(x => new
                {
                    x.Name,
                    Customers = x.Customers.Count
                })
                .OrderByDescending(s => s.Customers)
                .ThenBy(s => s.Name)
                .ToList()
                .ForEach(x => Console.WriteLine($"{x.Name} - {x.Customers} customers"));
        }

        private static void ProcessCommands(ShopDb db)
        {
            while (true)
            {
                var commandInfo = Console.ReadLine()
                    .Split(new[] { ' ', '-', ';' }, StringSplitOptions.RemoveEmptyEntries);

                if (commandInfo[0] == "END")
                {
                    break;
                }
                switch (commandInfo[0])
                {
                    case "register": RegisterCustomer(db, commandInfo);
                        break;
                    case "order": MakeOrder(db, commandInfo);
                        break;
                    case "review": LeaveReview(db, commandInfo);
                        break;
                    default:
                        break;
                }
            }
        }

        private static void LeaveReview(ShopDb db, string[] commandInfo)
        {
            var customerId = int.Parse(commandInfo[1]);
            var itemId = int.Parse(commandInfo[2]);
            db
                .Reviews
                .Add(new Review { CustomerId = customerId, ItemId=itemId });
            db.SaveChanges();
        }

        private static void MakeOrder(ShopDb db, string[] commandInfo)
        {
            var customerId = int.Parse(commandInfo[1]);
            var order = new Order { CustomerId = customerId };
            for (int i = 2; i < commandInfo.Length; i++)
            {
                order.Items.Add(new ItemOrder
                {
                    ItemId = int.Parse(commandInfo[i])
                });
            }
            db
                .Orders
                .Add(order);
            db.SaveChanges();
        }

        private static void RegisterCustomer(ShopDb db, string[] commandInfo)
        {
            var customerName = commandInfo[1];
            var salesmanId = int.Parse(commandInfo[2]);

            db.Customers.Add(new Customer { Name = customerName, SalesmanId = salesmanId });
            db.SaveChanges();
        }

        private static void SaveSalesmen(ShopDb db)
        {
            var names = Console.ReadLine()
                .Split(new[] { ';', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var name in names)
            {
                db.Salesmans.Add(new Salesman { Name = name });
            }
            db.SaveChanges();
        }

        private static void ClearDatabase(ShopDb db)
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        }
    }
}
