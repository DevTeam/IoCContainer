// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedVariable
namespace EntityFrameworkCore
{
    using System;
    using IoC;
    using Microsoft.EntityFrameworkCore;

    public class Program
    {
        public static void Main()
        {
            using var program = Container
                // Create a container
                .Create()
                // Configure it
                .Using<Configuration>()
                // Build up a program
                .BuildUp<Program>();
        }

        internal Program(Db db, Func<Person> person)
        {
            var nik = person();
            nik.Name = "Nik";
            db.People.Add(nik);

            var john = person();
            john.Name = "John";
            db.People.Add(john);

            db.SaveChanges();

            db.People.ForEachAsync(Console.WriteLine);
        }
    }
}
