using System;
using System.Web.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using PagatudoSDK;
using Newtonsoft.Json.Linq;
using AuthorizationCodeGrant.Service;

namespace AuthorizationCodeGrant.Controllers
{
    public class MerchantController : Controller
    {
        // GET: Merchant
        public ActionResult Index()
        {
            return View();
        }
        
        public async Task<ActionResult> Send(string sendAmount)
        {
            if (Session["pagaduto_access_token"] != null)
            {
                var amount = Decimal.Parse(sendAmount).ToString("N2") ;
                
                // SAMPLE CALL THAT SENDS MONEY TO MERCHANT
                var result = await PagatudoResourceService.SendFundsToMerchant("ZTP_MER", amount, "EUR", Session["pagaduto_access_token"].ToString()
                    ,new HttpClient(PagatudoAuthService.GetClient().CreateAuthorizingHandler(Session["pagaduto_access_token"].ToString())));

                var profileData = ((JArray)JObject.Parse(result)["responseBody"])[0];
                ViewBag.TransactionToken = profileData.ToObject<string>();
                // Load Dashboard
                // ViewBag.ApiResponse = body;
            }
            return View();
        }
    }
}