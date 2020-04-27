using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace Library
{
   public class HttpClient
    {
       public string getData(string url)
       {
           string result = "";
           try
           {
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)(0xc0 | 0x300 | 0xc00);
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            //    myHttpWebRequest.Method = "POST";
                
               // Sends the HttpWebRequest and waits for a response.
               HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
               if (myHttpWebResponse.StatusCode == HttpStatusCode.OK)
               {
                    using (Stream stream = myHttpWebResponse.GetResponseStream())
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        result  = reader.ReadToEnd();
                     
                    }
               }
           }
           catch (Exception ex)
           {
               ErrorUtil.logError(ex,"");
           }
           return result;
       }
    }
}
