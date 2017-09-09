namespace Yoshi.EntityLayer.Migrations
{
    using System.Data.Entity.Migrations;
    using Yoshi.EntityLayer.Data;

    internal sealed class Configuration : DbMigrationsConfiguration<Yoshi.EntityLayer.Context.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Yoshi.EntityLayer.Context.ApplicationDbContext context)
        {
            Initializer.Register(context);
        }
    }
}
