using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAutomation
{
    public interface IAPIClient
    {//bir değer döndürmeyen ve genellikle zaman uyumsuz olarak yürütülen tek bir işlemi temsil eder
        Task<RestResponse>CreateUser<T>(T payload) where T: class;
        Task<RestResponse>UpdateUser<T>(T payload, string id) where T : class;
        Task<RestResponse> DeleteUser(string id);
        Task<RestResponse> GetUser(string id);
        Task<RestResponse> GetListOfUsers(int pageNumber);
    }
}
