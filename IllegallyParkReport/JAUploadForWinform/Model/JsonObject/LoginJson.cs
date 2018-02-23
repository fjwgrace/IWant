using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAUploadForWinform.Model
{
    public class LoginRequestJson
    {
        public string UserName { get; set; }
        public string AccessCode { get; set; }
        public string LoginSignature { get; set; }
    }
    public class LoginResponseJson
    {
        public string AccessCode { get; set; }
        public string Encryption { get; set; }
    }
    public class LoginTokenJson
    {
        public string AccessToken { get; set; }
    }
    public  class LogoutResponseJson
    {
        public int ErrCode { get; set; }
        public string ErrMsg { get; set; }
    }
       
    
}
