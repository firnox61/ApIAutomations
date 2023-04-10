using ApiAutomation.Models.Response;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiAutomation.Auth
{//restapi isteğimizi doğrulamamk için kullanılır
    public class APIAuthenticator : AuthenticatorBase//bu sınıfı miras alavağız ve inhgerit edeceğiz
    {//token bulmak için
        readonly string baseUrl;//ADRES
        readonly string clientId;//MÜŞTERİ ID
        readonly string clientSecret;//müşteri kimliği ortamdan ortama farklılık gösterir
        public APIAuthenticator():base("")//üst sınıfa belirteç açtık token
        {

        }
        protected override async ValueTask<Parameter> GetAuthenticationParameter(string accessToken)
        {//GetAuthenticationParameter nasıl uygularız boşmu yoksa dolumu
            var token = string.IsNullOrEmpty(Token) ? await GetToken() : Token;
            //header parametresi olarak döndürmesi gerekir yetkilendirmeyi ve otorizeyi taşıdık
            return new HeaderParameter(KnownHeaders.Authorization, token);    
        }
        private async Task<string> GetToken()
        {//Requestimizi client üzerinden ilgili servise göndermeliyiz
            var options = new RestClientOptions(baseUrl);//api istemcisi için oluşturduğumuzu gibi
            var client = new RestClient(options)//Yine elimizdeki baseurl ile bir RestClient oluşturuyoruz
            {//müşteri iblgilerini belirttik
                Authenticator = new HttpBasicAuthenticator(clientId, clientSecret),
            };//jetonu almak için request etmek gerek ve değişiklik yapabilirilz duruma göre token
            var request = new RestRequest("oauth2/token").AddParameter("grant type", "client credenatials");
            var response = await client.PostAsync<TokenResponse>(request);//yanıtı gönderiyoruz ve model oluşturuyoruz tokenREsponse
            return $"{response.TokenType} {response.AccesToken}";//token değerinin yanıtını döndüreceğiz
        }
    }
}
