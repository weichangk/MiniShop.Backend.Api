using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using MiniShop.Backend.Api.Code.Core;
using MiniShop.Backend.Api.Config;

namespace MiniShop.Backend.Api
{
    public class Startup : AbstractStartup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env) : base(env)
        {
            //绑定配置信息
            configuration.Binding<BasicSetting>("Setting")
                .OnChange(BasicSetting.Setting);
        }
    }
}
