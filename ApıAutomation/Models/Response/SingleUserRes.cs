using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiAutomation.Models.Response
{

    public class SingleUserRes//clasın adını değiştiriyoruz
    {
        //eğer aynı şeyler varsa eklemeye gerek yok support ve diğerini sildik
        public Data data { get; set; }
        public Support support { get; set; }
    }

    

}
