namespace EntityFrameworkCore
{
    using System;
    using IoC;

    public static class Program
    {
        public static void Main()
        {
            // Create and configure a container by ASP .NET core feature
            using (var container = Container.Create().Using<Configuration>())
            {
                // Resolve DB context
                using (var ctx = container.Resolve<PeopleDbContext>())
                {
                    // Fill DB
                    ctx.People.Add(new Person {Id = 1, Name = "Nik"});
                    ctx.People.Add(new Person {Id = 2, Name = "John"});
                    ctx.SaveChanges();
                }

                // Resolve DB context
                using (var ctx = container.Resolve<PeopleDbContext>())
                {
                    // Fetch people
                    foreach (var person in ctx.People)
                    {
                        Console.WriteLine($"{person.Id}: {person.Name}");
                    }
                }
            }
        }
    }
}
