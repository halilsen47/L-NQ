using LİNQ.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;

namespace LİNQ
{
    public class employeeDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //listlinq();
            //SelectClauses();
            //WhereClauses();
            //OfTypeClauses();
            //queryone();
            //queryonewithmethod();
            //queytwomethodandsqlquery();
            //JoinClauses();

            Console.Read();



        }

        private static void JoinClauses()
        {
            var context = new NorthWindContext();
            //var query = context.Products
            //    .Include(p => p.Category)
            //    .Where(p => p.CategoryId != null)
            //    .ToList();

            var query = from prd in context.Products
                        join cat in context.Categories on prd.CategoryId equals cat.CategoryId
                        select new
                        {
                            Product = prd,
                            Category = cat
                        };



            foreach (var item in query)
            {
                Console.WriteLine($"{item.Product.ProductId,-7}" +
                    $"{item.Product.ProductName,-40}" +
                    $"{item.Category.CategoryId,-7}" +
                    $"{item.Category.CategoryName,-15}");
            }
        }

        private static void queytwomethodandsqlquery()
        {
            var context = new NorthWindContext();
            //rt
            var query = from prd in context.Products
                        where prd.ProductName.Contains("rt")
                        orderby prd.ProductName // a-z ascending
                        select new Product
                        {
                            ProductId = prd.ProductId,
                            ProductName = prd.ProductName,
                            UnitsInStock = prd.UnitsInStock,
                            UnitPrice = prd.UnitPrice
                        };

            query = context.Products
                .Where(p => p.ProductName.Contains("rt"))
                .OrderBy(p => p.ProductName)
                .ThenByDescending(p => p.UnitPrice)
                .Select(p => new Product
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    UnitsInStock = p.UnitsInStock,
                    UnitPrice = p.UnitPrice
                });

            foreach (var item in query)
            {
                Console.WriteLine($"{item.ProductId,-7}" +
                    $"{item.ProductName,-40}" +
                    $"{item.UnitPrice,-15}" +
                    $"{item.UnitsInStock,-15}");
            }
        }

        private static void queryonewithmethod()
        {
            var context = new NorthWindContext();
            var query = context.Products
                .Where(p => p.UnitsInStock > 10 && p.UnitsInStock < 50)
                .OrderBy(p => p.UnitsInStock)
                .Take(5)
                .Select(p => new Product
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    UnitPrice = p.UnitPrice,
                    UnitsInStock = p.UnitsInStock
                });


            foreach (var item in query)
            {
                Console.WriteLine($"{item.ProductId,-5}" +
                    $"{item.ProductName,-40}" +
                    $"{item.UnitPrice,-15}" +
                    $"{item.UnitsInStock,-15}");
            }
        }

        private static void queryone()
        {
            //Context
            NorthWindContext context = new NorthWindContext();
            //IQueryAble Sorgu 
            var query = from prd in context.Products //IQueryAble<Product> -> var
                        where prd.UnitPrice > 50 && prd.UnitsInStock > 20 && prd.UnitsInStock < 45
                        orderby prd.UnitPrice
                        select new Product
                        {
                            ProductId = prd.ProductId,
                            ProductName = prd.ProductName,
                            UnitPrice = prd.UnitPrice,
                            UnitsInStock = prd.UnitsInStock
                        };


            foreach (var item in query)
            {
                Console.WriteLine($"{item.ProductId,-5}" +
                    $"{item.ProductName,-40}" +
                    $"{item.UnitPrice,-15}" +
                    $"{item.UnitsInStock,-15}");
            }
        }

        private static void OfTypeClauses()
        {
            var list = new ArrayList
            {
                "Ahmet",
                "Mehmet",
                "Hatice",
                "Fatma",
                12,
                13,
                15,
                23,
                44,
                55,
                61,
                true,
                false,
                DateTime.Now,
                DateTime.Now.AddDays(3),
                DateTime.Now.AddMonths(5),
            };


            var filtereddata = list.OfType<Boolean>();
            var filtereddata2 = GenericList<Boolean>(list);


            foreach (var item in filtereddata2)
            {
                Console.WriteLine(item);
            }
        }
        private static List<T> GenericList<T>(IEnumerable arr) 
        {
            var list = new List<T>();
            foreach (var item in arr)
            {
                if(item is T)
                    list.Add((T)item);
            }
            return list;
        }
        private static void WhereClauses()
        {
            var list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            Func<int, bool> predicate = i => i > 7;
            var filteredNumbers = list.Where(predicate);

            foreach (var number in filteredNumbers)
            {
                Console.WriteLine(number);
            }
        }
        private static bool CheckNumber(int number)
        {
            if(number > 6)
                return true;
            return false;
        }
        private static void SelectClauses()
        {
            var context = new NorthWindContext();
            var query = from emp in context.Employees
                        select new employeeDTO()
                        {
                            Id = emp.EmployeeId,
                            FullName = emp.FirstName + " " + emp.LastName
                        };

            //query = context.Employees.Select(emp => new Employee()
            //{
            //    EmployeeId = emp.EmployeeId,
            //    FirstName = emp.FirstName,
            //    LastName = emp.LastName,    
            //});



            //foreach (var emp in query)
            //{
            //    Console.WriteLine(
            //        $"{emp.EmployeeId,-5}" +
            //        $"{emp.FirstName,-15}" +
            //        $"{emp.LastName,-15}"
            //        );
            //}

            foreach (var emp in query)
            {
                Console.WriteLine(emp);
            }
        }
        private static void listlinq()
        {
            //(1) Data Source
            var list = new List<string>()
            {
                "Ahmet",
                "Mehmet",
                "Ceylan",
                "Hatice",
                "Can"
            };


            //(2) Query Creation
            //(2.1) Query Syntax
            //SQL: Select EmployeeId, FirstName From Employees

            var querySyntax = from name in list
                              where name.Contains("me")
                              select name;

            //(2.2) Method Syntax
            var methodSyntax = list
                .Where(name => name.Contains('e'));


            //(2.3) Mix Syntax
            var mixSyntax = (from name in list
                             select name)
                            .Where(name => name.Contains("e"))
                            .OrderBy(name => name);

            //(3) Query Execute
            foreach (var item in mixSyntax)
            {
                Console.WriteLine(item);
            }


            Console.Read();
        }
    }

}