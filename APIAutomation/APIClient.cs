using APIAutomation.Auth;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAutomation
{
    public class APIClient : IAPIClient, IDisposable
    {// IDisposable bellek/kaynak yönetimi yapmak için kullanılan bir arayüzdür. bellekte jalmasını engelleyerek siler
       // Var anahtarı, değişken tanımı yaparken tür belirtmeksizin tanım yapmamızı sağlamaktadır.
        readonly RestClient client;
        public APIClient(string baseUrl)
        {
            var options = new RestClientOptions(baseUrl);//3 aşırı yükleyiciyyi yönetiriz
            client = new RestClient(options)
            {//doğrulama burada
                A = new APIAuthenticator();
            };
            
        }    
        
        
        public async Task<RestResponse> CreateUser<T>(Task payload) where T : class
        {
            //talep yaratmalıyız
            var request = new RestRequest(Endpoints.CREATE_USER, Method.Post);
            request.AddBody(payload);
            return await client.ExecuteAsync<T>(request);
        }

        public async Task<RestResponse> DeleteUser(string id)
        {//kullanıcı kimliğini dinamik kullanmamaız için url segment kullandık
            var request=new RestRequest(Endpoints.DELETE_USER, Method.Delete);
            request.AddUrlSegment(id, id);
            return await client.ExecuteAsync(request);
            //silme işleminde bir yanıt gövdesi olmayacak sadece durum kodu olacak generic kullanmadık
        }

        public void Dispose()
        {//dahili sarılımış http istemcisine atmak için kullanacağız
            client?.Dispose();
            GC.SuppressFinalize(this);//çöp toplayıcı çağırıdk

        }

        public async Task<RestResponse> GetListOfUsers(int pageNumber)
        {//sorgu parametresi kullandığımız içinaddquery
            var request=new RestRequest(Endpoints.GET_LIST_USER, Method.Get);
            request.AddQueryParameter("page", pageNumber);
            return await client.ExecuteAsync(request);
        }

        public async Task<RestResponse> GetUser(string id)
        {//deletede url segment kullandığımız iiçn burada da kullanacağız
            var request=new RestRequest(Endpoints.GET_SINGLE_USER, Method.Get);
            request.AddUrlSegment(id, id);
            return await client.ExecuteAsync(request);
        }

        public async Task<RestResponse> UpdateUser<T>(T payload, string id) where T : class
        {//gövdeyi eklemeliyiz
            var request = new RestRequest(Endpoints.UPDATE_USER, Method.Put);
            request.AddUrlSegment(id, id);
            request.AddBody(payload);//yükü buraya ilettik
            return await client.ExecuteAsync<T>(request);
        }
    }
}
