using Microsoft.AspNetCore.SignalR;

namespace SignalR.WebServer.HubsFilters
{
    public class GeneralHubFilter:IHubFilter
    {
        public async ValueTask<object> InvokeMethodAsync(HubInvocationContext invocationContext, Func<HubInvocationContext, ValueTask<object>> next)
        {
            Console.WriteLine($"(GeneralHubFilter)=> Calling hub method '{invocationContext.HubMethodName}'");
            try
            {
                return await next(invocationContext);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"(GeneralHubFilter)=> Exception calling '{invocationContext.HubMethodName}': {ex}");
                throw;
            }
        }

        // Optional method
        public Task OnConnectedAsync(HubLifetimeContext context, Func<HubLifetimeContext, Task> next)
        {
            return next(context);
        }

        // Optional method
        public Task OnDisconnectedAsync(HubLifetimeContext context, Exception exception, Func<HubLifetimeContext, Exception, Task> next)
        {
            return next(context, exception);
        }

    }
}
