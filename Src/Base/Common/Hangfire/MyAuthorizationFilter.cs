using Hangfire.Dashboard;

namespace Common.Hangfire;

public class HangFireNetCoreTutorialAuthorizationFilter : IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context)
    {
        var httpContext = context.GetHttpContext();

        //Do

        return true;
        // Allow all authenticated users to see the Dashboard (potentially dangerous).
        //return httpContext.User.Identity?.IsAuthenticated ?? false;
    }
}
