using Microsoft.AspNetCore.SignalR;

namespace SignalR.WebServer.HubsFilters
{
    public class MethodHubFilter : IHubFilter
    {
        public async ValueTask<object?> InvokeMethodAsync(HubInvocationContext invocationContext, Func<HubInvocationContext, ValueTask<object?>> next)
        {
            Console.WriteLine($"(MethodHubFilter)=> Calling hub method '{invocationContext.HubMethodName}'");
            try
            {
                return await next(invocationContext);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"(MethodHubFilter)=> Exception calling '{invocationContext.HubMethodName}': {ex}");
                throw;
            }
        }

    }
}
