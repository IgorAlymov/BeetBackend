using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebServerSocialNet
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            // разрешаем кросс-браузерные запросы
            HttpContext
                .Current
                .Response
                .AddHeader("Access-Control-Origin",
                           "http://localhost:64931");
            
            if (HttpContext.Current.Request.HttpMethod 
                == "OPTIONS")
            {
                var r = HttpContext.Current.Response;

                r.AddHeader("Access-Control-Allow-Methods",
                            "POST, GET, PUT, DELETE");
                r.AddHeader("Access-Control-Allow-Headers",
                            "Content-Type, Accept");
                r.End();
            }
        }
    }
}
