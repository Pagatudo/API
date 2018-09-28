using System;
using System.Web.Mvc;
using System.Threading.Tasks;
using AuthorizationCodeGrant.Service;

namespace AuthorizationCodeGrant.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]
        public ActionResult Index()

        {
            return View();
        }

        [HttpPost]
        [ActionName("Index")]
        public async Task<ActionResult> DoLogin()
        {
            var callbackURL = Url.Action("Process", "Login", routeValues: null, protocol: Request.Url.Scheme );
            var userAuthorization = PagatudoAuthService.GetClient().PrepareRequestUserAuthorization(scopes: new string[] { "USER_COMPLIANCE", "USER_FINANCIAL", "USER_REGISTRATION", "PUBLIC_DATA" }, returnTo: new Uri(callbackURL));
            userAuthorization.Send(HttpContext);
            Response.End();
            return View("~/Views/Home/Index.cshtml");
        }

        [HttpGet]
        [ActionName("Process")]
        public async Task<ActionResult> ProcessLogin()
        {
            var accessToken = Request.Form["AccessToken"];
            if (string.IsNullOrEmpty(accessToken))
            {
                    var authorizationState = PagatudoAuthService.GetClient().ProcessUserAuthorization(Request);
                    if (authorizationState != null)
                    {
                        Session["pagaduto_access_token"] = authorizationState.AccessToken;
                    }
            }
            // This example keeps it in the session. You don't have to
            // Session["pagaduto_access_token"] = userAuthorization
            return RedirectToAction("Index","Home");
        }
    }
}