// ReSharper disable UnusedAutoPropertyAccessor.Local
// ReSharper disable ClassNeverInstantiated.Global
namespace EntityFrameworkCore
{
    using Microsoft.EntityFrameworkCore;

    internal class PeopleDbContext: DbContext
    {
        public PeopleDbContext(DbContextOptions options)
            :base(options)
        { }

        public DbSet<Person> People { get; private set; }
    }
}
