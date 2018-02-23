using Core.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAUploadForWinform.Business
{
    class RequestGenerater
    {
        public static HttpItem GetHttpItem(string url,string method,string data,bool auth,string authKey,string authValue)
        {
            HttpItem item = new HttpItem();
            item.URL = url;
            item.ContentType = "application/json";
            item.Encoding = Encoding.UTF8;
            if(method.ToLower()=="post")
            {
                item.Postdata = data;
                item.PostDataType = PostDataType.String;
                item.PostEncoding = Encoding.UTF8;
            }
            if(auth==true)
            {
                item.Header.Add(authKey, authValue);
            }
            item.Method = method;
            item.Accept = "application/json";
            item.ResultType = ResultType.String;
            return item;
        }
    }
}
