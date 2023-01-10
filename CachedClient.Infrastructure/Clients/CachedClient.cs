namespace CachedClient.Infrastructure.Clients;

using LazyCache;

public abstract class CachedClient
{
    protected readonly IAppCache Cache;

    protected CachedClient(IAppCache cache)
    {
        Cache = cache;
    }
}