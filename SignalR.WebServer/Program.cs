using Microsoft.AspNetCore.SignalR;
using SignalR.WebServer.Hubs;
using SignalR.WebServer.HubsFilters;
using SignalR.WebServer.Services;

namespace SignalR.WebServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Add Services
            builder.Services.AddRazorPages();
            builder.Services.AddCors();
            builder.Services.AddSignalR(options =>
            {
                //Global Hub Filter will Run First
                //options.AddFilter<GeneralHubFilter>();
            }).AddHubOptions<ChatHub>(chatHubOptions =>
            {
                //Local Hub Filter will Run Second
                //chatHubOptions.AddFilter<LocalHubFilter>();
            });

            //Add Custom Services For Dependency Injection
            builder.Services.AddSingleton<IDatabaseService, DatabaseService>();


            //Build App
            var app = builder.Build();

            //Map Routes
            //app.MapGet("/", () => "Welcome To SignalR Server");
            app.MapRazorPages();

            //Map Hubs Routes
            app.MapHub<ChatHub>("/hubs/chatHub");


            // Configure the HTTP request pipeline by adding Middleware to request pipelines
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            //Allow any App out the domain to access this site
            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials());

            //add websocket as middleware to pipeline request (not required with singalR just required when using pure websocket)
            /*
            app.UseWebSockets();
            app.Use(async (context,next) =>
            {
                PrintRequestHeaderInfo(context);

                //Check if the request is websocket request or just http request
                if (context.WebSockets.IsWebSocketRequest)
                {
                    var webSocket = await context.WebSockets.AcceptWebSocketAsync();

                    Console.ForegroundColor= ConsoleColor.Yellow;
                    Console.WriteLine("WebSocket Connected");

                    context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                    context.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type");
                    context.Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
                }
                else
                {
                    //if its just http request then pass the request to the next middleware indside the request pipline
                    await next(context);
                }
                

                
            });
            */
            

            //Run App
            app.Run();
        }

        #region Helper

        private static void PrintRequestHeaderInfo(HttpContext context)
        {
            Console.WriteLine("-----------------------------Start Request Info----------------------------");
            Console.WriteLine($"Method: {context.Request.Method}");
            Console.WriteLine($"Protocol: {context.Request.Protocol}");

            foreach (var requestHeader in context.Request.Headers)
            {
                Console.WriteLine($"=> {requestHeader.Key}: {requestHeader.Value}");
            }

            Console.WriteLine("-----------------------------Finish Request Info----------------------------");
        }

        #endregion

    }
}