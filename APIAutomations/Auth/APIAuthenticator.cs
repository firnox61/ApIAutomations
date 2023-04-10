using APIAutomation.Models.Response;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAutomation.Auth
{
    public class APIAuthenticator : AuthenticatorBase
    {//token bulmak için
        readonly string baseUrl;
        readonly string clientId;
        readonly string clientSecret;
        public APIAuthenticator():base("")//üst sınıfa belirteç açtık
        {

        }
        protected override async ValueTask<Parameter> GetAuthenticationParameter(string accessToken)
        {
            var token = string.IsNullOrEmpty(Token) ? await GetToken() : Token;
            return new HeaderParameter(KnownHeaders.Authorization, token);    
        }
        private async Task<string> GetToken()
        {
            var options = new RestClientOptions(baseUrl);
            var client = new RestClient(options)
            {
                Authenticator = new HttpBasicAuthenticator(clientId, clientSecret),
            };
            var request = new RestRequest("oauth2/token").AddParameter("grant type", "client credenatials");
            var response = await client.PostAsync<TokenResponse>(request);
            return $"{response.TokenType} {response.AccesToken}";
        }
    }
}
