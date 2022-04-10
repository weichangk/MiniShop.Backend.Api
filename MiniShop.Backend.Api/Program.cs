using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using MiniShop.Backend.Api.Code.Core;

namespace MiniShop.Backend.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return new MyHostBuilder().Create<Startup>(args)
                .Configure("logging", false, false);
        }
    }
}
