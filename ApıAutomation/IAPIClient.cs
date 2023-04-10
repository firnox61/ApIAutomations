using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiAutomation
{
    public interface IAPIClient
    {//bir değer döndürmeyen ve genellikle zaman uyumsuz olarak yürütülen tek bir işlemi temsil eder
        Task<RestResponse>CreateUser<T>(T payload) where T: class;//user var sadece
        //generic yapılardır T kullanıyoruz
        Task<RestResponse>UpdateUser<T>(T payload, string id) where T : class;//id içeriyor ekledik
        Task<RestResponse> DeleteUser(string id);//id içeriyor ekledik
        Task<RestResponse> GetUser(string id);//id içeriyor ekledik
        Task<RestResponse> GetListOfUsers(int pageNumber);//id içeriyor ekledik
    }
}
