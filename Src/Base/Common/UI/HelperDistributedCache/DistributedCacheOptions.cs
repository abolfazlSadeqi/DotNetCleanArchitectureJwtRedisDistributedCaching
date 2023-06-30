using Microsoft.Extensions.Caching.Distributed;

namespace Common.HelperDistributedCache;

public static class DistributedCacheOptions
{
    public static DistributedCacheEntryOptions GetDistributedCacheEntryOptions()
    {

      return  new DistributedCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600));
    }
}
