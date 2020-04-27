using BR.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Library
{
    public class OCBHttpClient
    {
        
        public string getData(string url)
        {
            string result = "";
            try
            {
                CookieCollection cookies = new CookieCollection();
               
                cookies= getAllCookie(url);
                if(cookies.Count>0)
                {
                  
                    result = SubmitLogin("https://www.ocb.com.vn/Web/ocb_submit_load_laisuat_page.aspx", cookies);
                }

            }
            catch (Exception ex)
            {
                ErrorUtil.logError(ex,"");
            }
            return result;
        }
        public CookieCollection getAllCookie(string url)
        {
            CookieCollection cookies = new CookieCollection();
            try
            {
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                myHttpWebRequest.Timeout = Timeout.Infinite;
                myHttpWebRequest.KeepAlive = true;
                myHttpWebRequest.Method = "GET";
                myHttpWebRequest.Accept = @"text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                myHttpWebRequest.Headers.Add("Accept-Encoding", @"gzip, deflate, br");
                myHttpWebRequest.Headers.Add("Accept-Language", @"en-US,en;q=0.5");
                myHttpWebRequest.UserAgent = @"Mozilla/5.0 (Windows NT 6.1; WOW64; rv:53.0) Gecko/20100101 Firefox/53.0";
                myHttpWebRequest.Host = "www.ocb.com.vn";
                myHttpWebRequest.CookieContainer = new CookieContainer();
                // Sends the HttpWebRequest and waits for a response.
                HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                if (myHttpWebResponse.StatusCode == HttpStatusCode.OK)
                {
                    cookies = myHttpWebResponse.Cookies;
                }
            }
            catch (Exception ex)
            {
                ErrorUtil.logError(ex,"");
            }
            return cookies;

        }

        public string SubmitLogin (string url,CookieCollection cookies)
        {
            string result = "";
            try
            {
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                myHttpWebRequest.ServicePoint.Expect100Continue = false;
                //   myHttpWebRequest.Timeout = Timeout.Infinite;
                myHttpWebRequest.Method = "POST";
                myHttpWebRequest.Accept = @"*/*";
                myHttpWebRequest.Headers.Add("Accept-Encoding", @"gzip, deflate, br");
                myHttpWebRequest.Headers.Add("Accept-Language", @"en-US,en;q=0.5");
                myHttpWebRequest.Headers.Add("Cache-Control", @"max-age=0");
                myHttpWebRequest.ContentLength =67;
                myHttpWebRequest.ContentType = @"application / x - www - form - urlencoded; charset = UTF - 8";
                myHttpWebRequest.UserAgent = @"Mozilla/5.0 (Windows NT 6.1; WOW64; rv:53.0) Gecko/20100101 Firefox/53.0";
                myHttpWebRequest.Host = @"www.ocb.com.vn";
                myHttpWebRequest.Referer = @"https://www.ocb.com.vn/vi-VN/Khach-hang-ca-nhan/Lai-suat/ocb.htm";
                myHttpWebRequest.Headers.Add("X-Requested-With", @"XMLHttpRequest");
                Cookie cookie1 = new Cookie();
                cookie1.Name = "_gat";
                cookie1.Value = "1";
                cookies.Add(cookie1);
                myHttpWebRequest.CookieContainer = new CookieContainer();
                foreach (Cookie cookie in cookies)
                {
                    myHttpWebRequest.CookieContainer.Add(new Cookie(cookie.Name,cookie.Value) { Domain = myHttpWebRequest.Host });
                }
                string postString = string.Format("flag={0}&lang={1}&type={2}&formatdate={3}&flag_dn={4}", "1", "vi-VN", "VN%C4%90", "dd%2FMM%2Fyyyy","0");
                StreamWriter requestWriter = new StreamWriter(myHttpWebRequest.GetRequestStream());
                requestWriter.Write(postString);
                requestWriter.Close();


                //// Sends the HttpWebRequest and waits for a response.
                HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                if (myHttpWebResponse.StatusCode == HttpStatusCode.OK)
                {
                    foreach (Cookie cook in myHttpWebResponse.Cookies)
                    {
                    }
                    using (Stream stream = myHttpWebResponse.GetResponseStream())
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        result = reader.ReadToEnd();

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

