using System;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using PagatudoSDK;
using Newtonsoft.Json.Linq;
using DotNetOpenAuth.OAuth2;

namespace AuthorizationCodeGrant.Service
{
    public static class PagatudoResourceService
    {
        public static string URL { get; internal set; }

        public static WebServerClient GetClient()
        {
            return _getClient();
        }

        private static WebServerClient _getClient()
        {
            var resourceServerURI = new Uri(Paths.ResourceServerBaseAddress);
            var authorizationServer = new AuthorizationServerDescription
            {
                AuthorizationEndpoint = new Uri(resourceServerURI, Paths.AuthorizePath),
                TokenEndpoint = new Uri(resourceServerURI, Paths.TokenPath)
            };
            return new WebServerClient(authorizationServer, Clients.Client1.Id, Clients.Client1.Secret);
        }

        public static async Task<string> SendFundsToMerchant(string merchantCode, string Amount , string Currency , string token, HttpClient authorizedClient )
        {
            authorizedClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            dynamic content = new JObject();
            content.merchantCode = merchantCode;
            content.amount = Amount;
            content.currency = Currency;
            content.description = "TEST TRANSACTION";
            content.merchantTransactionId = Guid.NewGuid().ToString();

            HttpContent myContent = new StringContent(content.ToString(), Encoding.UTF8, "application/json");
            try
            {
                var response = await authorizedClient.PostAsync(new Uri(Paths.ResourceServerBaseAddress + "/PagatudoAPI/Members/Transfer/Merchant"), myContent);
                var contents = await response.Content.ReadAsStringAsync();
                
                return contents;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}