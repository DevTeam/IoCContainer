// ReSharper disable ClassNeverInstantiated.Global
namespace EntityFrameworkCore
{
    using System;
    using IoC;

    public class Program
    {
        public static void Main()
        {
            using var program =
                // Create a container
                Container.Create()
                // And configure it
                .Using<Configuration>()
                // Build up program
                .BuildUp<Program>();
        }

        internal Program(
            // DB context factory
            Func<PeopleDbContext> newCtx,
            // Person factory
            Func<Person> newPerson)
        {
            // Create DB context
            using var ctx = newCtx();

            // Fill DB by people
            var nik = newPerson();
            nik.Name = "Nik";
            ctx.People.Add(nik);

            var john = newPerson();
            john.Name = "John";
            ctx.People.Add(john);

            ctx.SaveChanges();

            // Fetch people
            foreach (var person in ctx.People)
            {
                Console.WriteLine($"{person.Id}: {person.Name}");
            }
        }
    }
}
