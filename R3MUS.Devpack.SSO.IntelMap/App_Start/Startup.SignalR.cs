using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Cors;

namespace R3MUS.Devpack.SSO.IntelMap.App_Start
{
    public partial class Startup
    {
        public void ConfigureSignalR(IAppBuilder app)
        {
            app.Map("/signalr", map =>
            {
                map.UseCors(new CorsOptions()
                {
                    PolicyProvider = new CorsPolicyProvider()
                    {
                        PolicyResolver = context =>
                        {
                            var policy = new CorsPolicy();
                            policy.Origins.Add("http://localhost:15377");
                            policy.Origins.Add("https://www.r3mus.org");
                            policy.Origins.Add("https://www.r3mus.space");
                            policy.AllowAnyMethod = true;
                            policy.AllowAnyHeader = true;
                            policy.SupportsCredentials = true;
                            return Task.FromResult(policy);
                        }
                    }
                });
                map.RunSignalR(new HubConfiguration());
            });
        }
    }
}