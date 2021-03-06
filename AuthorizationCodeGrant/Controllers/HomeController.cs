﻿using System;
using System.Web.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using PagatudoSDK;
using Newtonsoft.Json.Linq;
using AuthorizationCodeGrant.Service;

namespace AuthorizationCodeGrant.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            if(Session["pagaduto_access_token"]!=null)
            {
                // SAMPLE CALL THAT GETS OPEN EWALLETS
                var client = new HttpClient(PagatudoAuthService.GetClient().CreateAuthorizingHandler(Session["pagaduto_access_token"].ToString()));
                var result = await client.GetStringAsync(new Uri(PagatudoAuthService.URL + "/PagatudoAPI/Members/Account"));
                JObject jsonAccount = JObject.Parse(result);
                var accountlist = (JArray)(jsonAccount["responseBody"]);
                ViewBag.AccountList = accountlist.ToObject<List<Account>>();

                // SAMPLE CALL THAT GETS CUSTOMER KYC DETAILS
                result = await client.GetStringAsync(new Uri(PagatudoAuthService.URL + "/PagatudoAPI/Members/Profile"));
                var profileData = ((JArray)JObject.Parse(result)["responseBody"])[0];
                ViewBag.Profile = profileData.ToObject<Profile>();
                // Load Dashboard
                // ViewBag.ApiResponse = body;
            }
            return View();
        }
    }
}