using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace eBook
{
    public class MvcApplication : Spring.Web.Mvc.SpringMvcApplication ///System.Web.HttpApplication 換成用spring的架構,改用spring去繼承
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
