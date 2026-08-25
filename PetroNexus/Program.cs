
using PetroNexus.Extensions;

namespace PetroNexus
{
    public class Program
    {
        public static async Task Main(string[] args)
        {



            var builder = WebApplication.CreateBuilder(args);



            builder.Services.RegisterAllServices(builder.Configuration);



            var app = builder.Build();




            await app.ConfigurMiddelwares();


            app.Run();
        }
    }
}
