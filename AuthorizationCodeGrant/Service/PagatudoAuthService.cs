using System;
using PagatudoSDK;
using DotNetOpenAuth.OAuth2;

namespace AuthorizationCodeGrant.Service
{
    public static class PagatudoAuthService
    {
        public static string URL {

            get
            {
                return Paths.ResourceServerBaseAddress;
            }
        }

        public static WebServerClient GetClient()
        {
            return _getClient();
        }

        private static WebServerClient _getClient()
        {
            var authorizationServerUri = new Uri(Paths.AuthorizationServerBaseAddress);
            var authorizationServer = new AuthorizationServerDescription
            {
                AuthorizationEndpoint = new Uri(authorizationServerUri, Paths.AuthorizePath),
                TokenEndpoint = new Uri(authorizationServerUri, Paths.TokenPath)
            };
            return new WebServerClient(authorizationServer, Clients.Client1.Id, Clients.Client1.Secret);
        }
    }
}