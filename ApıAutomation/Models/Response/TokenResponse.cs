using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiAutomation.Models.Response
{
    public class TokenResponse
    {//tokenin özelliklerini belirtiyoruz
        public string TokenType { get; set; }
        public string AccesToken { get; set; }
    }
}
