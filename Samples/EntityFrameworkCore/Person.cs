// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local
// ReSharper disable ClassNeverInstantiated.Global
namespace EntityFrameworkCore
{
    internal class Person
    {
        public Person(Id id)
        {
            Id = id;
        }

        public Id Id { get; private set; }

        public string Name { get; set; } = "";

        public override string ToString() => $"{Id} {Name}";
    }
}
