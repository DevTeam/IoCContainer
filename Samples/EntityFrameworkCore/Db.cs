// ReSharper disable UnusedAutoPropertyAccessor.Local
// ReSharper disable ClassNeverInstantiated.Global
namespace EntityFrameworkCore
{
    using Microsoft.EntityFrameworkCore;

    internal class Db: DbContext
    {
        public Db(DbContextOptions options)
            :base(options)
        { }

        public DbSet<Person> People { get; private set; }
    }
}
