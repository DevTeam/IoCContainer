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
            using var container =
                // Create a container
                Container.Create()
                // Configure it
                .Using<Configuration>();
                
            container.Resolve<Program>().Run();
        }

        private readonly Db _db;
        private readonly Func<Person> _person;

        internal Program(Db db, Func<Person> person)
        {
            _db = db;
            _person = person;
        }

        private void Run()
        {
            var nik = _person();
            nik.Name = "Nik";
            _db.People.Add(nik);

            var john = _person();
            john.Name = "John";
            _db.People.Add(john);

            _db.SaveChanges();

            _db.People.ForEachAsync(Console.WriteLine);
        }
    }
}
