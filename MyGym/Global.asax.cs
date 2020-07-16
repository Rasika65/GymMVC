using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebMatrix.WebData;

namespace MyGym
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            //WebSecurity.InitializeDatabaseConnection("DefaultConnection", "Users", "Id", "UserName", autoCreateTables: true);     
            WebSecurity.InitializeDatabaseConnection("DefaultConnection", "Users", "Id", "UserName", autoCreateTables: false);      
        }

        //protected void Application_AuthenticateRequest(object sender, EventArgs args)
        //{
        //    if (Context.User != null)
        //    {
        //        IEnumerable<role> roles = new UsersService.UsersClient().GetUserRoles(
        //                                                Context.User.Identity.Name);


        //        string[] rolesArray = new string[roles.Count()];
        //        for (int i = 0; i < roles.Count(); i++)
        //        {
        //            rolesArray[i] = roles.ElementAt(i).RoleName;
        //        }

        //        GenericPrincipal gp = new GenericPrincipal(Context.User.Identity, rolesArray);
        //        Context.User = gp;
        //    }
        //}

    }    

}