using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using DotNetBay.Data.EF.Migrations;
using DotNetBay.Interfaces;
using DotNetBay.Data.EF;

namespace DotNetBay.Test.Storage
{
    public class EFMainRepositoryTests : MainRepositoryTestBase
    {
        public EFMainRepositoryTests()
        {
            var ensureDLLIsCopied = SqlProviderServices.Instance;
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MainDbContext, Configuration>());
        }

        protected override IRepositoryFactory CreateFactory()
        {
            return new EFMainRepositoryFactory();
        }
    }

    public class EFMainRepositoryFactory : IRepositoryFactory
    {
        private List<EFMainRepository> repositoriesList = new List<EFMainRepository>();

        public void Dispose()
        {
            foreach (var repository in repositoriesList)
            {
                repository.Database.Delete();
            }
        }

        public IMainRepository CreateMainRepository()
        {
            var repository = new EFMainRepository();

            if (!repositoriesList.Any())
            {
                repository.Database.Delete();
            }

            repositoriesList.Add(repository);
            return repository;
        }
    }
}
