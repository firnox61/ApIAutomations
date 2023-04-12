using ApiAutomation.Auth;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiAutomation
{
    public class APIClient : IAPIClient, IDisposable//impelelnt ediyoruz ve tek kullanımlık disposable ekliyoruz

    {// IDisposable bellek/kaynak yönetimi yapmak için kullanılan bir arayüzdür. bellekte jalmasını engelleyerek siler
       // Var anahtarı, değişken tanımı yaparken tür belirtmeksizin tanım yapmamızı sağlamaktadır.
        //bir müştreriye ihtiyacımız var
        readonly RestClient client;
         const string BASE_URL= "https://reqres.in/";
        //restşarp http sunucusudur ve uç noktalarımız olan endpointi oluşturalım
        //ilk uç nokta apı kullanıcıları
        public APIClient()//seçenekler seçenek sınıfına taşıdnı ve seçim sağlandı
        {
            var options = new RestClientOptions(BASE_URL);//3 aşırı yükleyiciyyi yönetiriz
            client = new RestClient(options)
            {//Kimlik doğrulama burada belirteç oluşturmaktan sorumlu
               // Authenticator = new APIAuthenticator()
            };
            
        }    
        
        
        public async Task<RestResponse> CreateUser<T>(T payload) where T : class
        {
            //talep yaratmalıyız
            var request = new RestRequest(Endpoints.CREATE_USER, Method.Post);//end point ile talep oluşturuyoruz
            request.AddBody(payload);//bu istek payload gerektirir
            return await client.ExecuteAsync<T>(request);//ve yanıtı döndürürüzb böylece müşteri senkronizasyon yöntemi görürür
        }

        public async Task<RestResponse> DeleteUser(string id)
        {//kullanıcı kimliğini dinamik kullanmamaız için url segment kullandık
            var request=new RestRequest(Endpoints.DELETE_USER, Method.Delete);
            request.AddUrlSegment(id, id);//kullanıcı kimliğinin dinamik değerini kulllanmalıyız
                                          //ve isteğe rl segment yollamamız gerekir
            return await client.ExecuteAsync(request);
            //silme işleminde bir yanıt gövdesi olmayacak sadece durum kodu olacak generic(T) kullanmadık
            //durum kodu ağırlık yaratır async kullanduk
        }

        public void Dispose()
        {//dahili sarılımış http istemcisine atmak için kullanacağız
            client?.Dispose();
            GC.SuppressFinalize(this);//çöp toplayıcı çağırıdk

        }

        public async Task<RestResponse> GetListOfUsers(int pageNumber)
        {//sorgu parametresi kullandığımız içinaddquery
            var request=new RestRequest(Endpoints.GET_LIST_USER, Method.Get);
            //bu istekle al diyebileceğimiz bir query sorgu parametresi uygulamamız gerek
            request.AddQueryParameter("page", pageNumber);
            return await client.ExecuteAsync(request);
        }

        public async Task<RestResponse> GetUser(string id)
        {//deletede url segment kullandığımız iiçn burada da kullanacağız
            var request=new RestRequest(Endpoints.GET_SINGLE_USER, Method.Get);
            request.AddUrlSegment(id, id);//kullanıcı kimliğinin dinamik değerini kulllanmalıyız
                                          //ve isteğe url segment yollamamız gerekir
            return await client.ExecuteAsync(request);
        }

        public async Task<RestResponse> UpdateUser<T>(T payload, string id) where T : class
        {//gövdeyi eklemeliyiz
            var request = new RestRequest(Endpoints.UPDATE_USER, Method.Put);
            request.AddUrlSegment(id, id);//kullanıcı kimliğinin dinamik değerini kulllanmalıyız
                                          //ve isteğe url segment yollamamız gerekir
            request.AddBody(payload);//yükü buraya ilettik
            return await client.ExecuteAsync<T>(request);
            //CreateUser ve update işlemlerinde payload kullandık
        }
    }
}
