using System.Web;
using System.Web.Mvc;

namespace PA_BlogProject_2021
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }

    public class Auth : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {

            if (HttpContext.Current.Session["KullanıcıAdı"] ==null)
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary()
            {
                {"Action","Login" },
                {"Controller","Account"}

            });
            }

          

        }
    }


}
