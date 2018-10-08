namespace PagatudoSDK
{
    public static class Clients
    {
        public readonly static Client Client1 = new Client
        {
            Id = "xxxxx",
            Secret = "xxxxx",
            RedirectUrl = Paths.AuthorizeCodeCallBackPath
        };
    }

    public class Client
    {
        public string Id { get; set; }
        public string Secret { get; set; }
        public string RedirectUrl { get; set; }
    }
}
