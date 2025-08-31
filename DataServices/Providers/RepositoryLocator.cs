using DataServices.Interfaces;
using DataServices.SqlServerRepository;
using System;

namespace DataServices.Providers
{
    public class RepositoryLocator<T> : IRepositoryLocator<T> where T : IPersistentEntity, new()
    {
        // Singleton instance per T
        private static readonly Lazy<RepositoryLocator<T>> _instance =
            new(() => new RepositoryLocator<T>());

        public static RepositoryLocator<T> Instance => _instance.Value;

        private RepositoryType? _cachedRepositoryType;
        private IRepository<T>? _cachedRepository;
        private readonly object _lock = new();

        // Private constructor prevents external instantiation
        private RepositoryLocator() { }

        public IRepository<T> GetRepository(RepositoryType repositoryType, bool isForceRefresh = false)
        {
            // Return cached if valid
            if (!isForceRefresh && _cachedRepositoryType == repositoryType && _cachedRepository != null)
                return _cachedRepository;

            IRepository<T> repository;

            switch (repositoryType)
            {
                case RepositoryType.FromJsonRepository:
                    repository = new FromJsonRepository<T>(typeof(T).Name);
                    repository = new InMemoryRepository<T>(repository);
                    break;

                case RepositoryType.FromDbRepository:
                    var dbLocator = new DbRepositoryLocator<T>();
                    repository = dbLocator.GetRepository();
                    repository = new InMemoryRepository<T>(repository);
                    break;

                default:
                    throw new NotSupportedException($"RepositoryType '{repositoryType}' is not supported.");
            }

            // Thread-safe cache update
            lock (_lock)
            {
                _cachedRepositoryType = repositoryType;
                _cachedRepository = repository;
            }

            return repository;
        }
    }
}
