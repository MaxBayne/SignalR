using SignalR.WebServer.Hubs;

namespace SignalR.WebServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Add Services
            builder.Services.AddSignalR();
            builder.Services.AddCors();

            //Build App
            var app = builder.Build();

            //Map Routes
            app.MapGet("/", () => "Welcome To SignalR Server");
            
            //Map Hubs Routes
            app.MapHub<ChatHub>("/ChatHub");

            //Allow any App out the domain to access this site
            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials());

            //Run App
            app.Run();
        }
    }
}