// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local
namespace EntityFrameworkCore
{
    internal class Person
    {
        public Person(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }

        public string Name { get; set; }
    }
}
