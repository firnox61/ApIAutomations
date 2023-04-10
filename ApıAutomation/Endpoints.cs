using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiAutomation
{
    public class Endpoints
    {//nesne metodun üreteceği sonucu etkilemeyecek ise o metot static olarak tanımlanır.
        //Readonly tanımlı değişkeni salt okunur moduna getirmektedir.
        //Tüm uç noktaları burada listeleyeceğiz Request pathleri
        public static readonly string CREATE_USER = "/api/users";
        public static readonly string UPDATE_USER = "/api/users/{id}";//parametresi varsa giriyoruz
        public static readonly string DELETE_USER = "/api/users/{id}";
        public static readonly string GET_SINGLE_USER = "/api/users/{id}";
        public static readonly string GET_LIST_USER = "/api/users";//page numberı eklemedik bakmak lazım
        
    }
}
