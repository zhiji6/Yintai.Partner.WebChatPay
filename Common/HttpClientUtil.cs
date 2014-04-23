using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Newtonsoft.Json;

namespace Yintai.Partner.WebChatPay.Common
{
    public class HttpClientUtil
    {
        //证书文件 
        private string certFile;

        //证书密码 
        private string certPasswd;

        //字符编码
        private string charset;

        //设置证书信息
        public HttpClientUtil()
        {
            this.charset = "gb2312";
            this.certFile = "";
        }

        //设置证书信息
        public HttpClientUtil(string certFile, string certPasswd)
        {
            this.certFile = certFile;
            this.certPasswd = certPasswd;
            this.charset = "gb2312";
        }

        public void setCharset(string charset)
        {
            this.charset = charset;
        }

        /// <summary>
        /// 
        /// </summary>
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof (HttpClientUtil));

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受  
        }  

        #region Http Method
        /// <summary>
        /// POST data to server
        /// </summary>
        /// <param name="url">Server url</param>
        /// <param name="rquest">Push request data</param>
        /// <returns></returns>
        public string PostData(string url, string rquest)
        {
            string result = null;

            byte[] data = Encoding.GetEncoding(this.charset).GetBytes(rquest);

            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                //如果是发送HTTPS请求  
                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                    request = WebRequest.Create(url) as HttpWebRequest;
                    request.ProtocolVersion = HttpVersion.Version10;
                }

                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);
                if (this.certFile != "")
                {
                    request.ClientCertificates.Add(new X509Certificate2(this.certFile, this.certPasswd));
                }

                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1)";
                request.ContentType = "application/x-www-form-urlencoded";
                request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.Reload);
                request.ServicePoint.Expect100Continue = false;
                request.Method = "POST";
                //// request.Timeout = RequestTimeOut;
                request.ContentLength = data.Length;
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(data, 0, data.Length);
                requestStream.Close();

                var response = (HttpWebResponse)request.GetResponse();
                using (var responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        result = new StreamReader(responseStream, Encoding.GetEncoding(this.charset)).ReadToEnd();
                        responseStream.Close();
                        Debug.WriteLine("PostData OK Response:" + result);
                    }
                }
            }
            catch (WebException wex)
            {
                log.Error(wex.ToString());
                Debug.WriteLine("PostData WebException:" + wex.Status.ToString() + " " + wex.Message);
                if (wex.Response != null)
                {
                    using (var errStream = wex.Response.GetResponseStream())
                    {
                        if (errStream != null)
                        {
                            result = new StreamReader(errStream, Encoding.GetEncoding(this.charset)).ReadToEnd();
                            errStream.Close();
                            Debug.WriteLine("PostData Error Response:" + result);
                        }
                    }
                }
            }
            catch (Exception exx)
            {
                log.Error(exx.ToString());
                Debug.WriteLine("PostData Exception:" + exx.Message);
            }
            return result;
        }

        /// <summary>
        /// POST data to server
        /// </summary>
        /// <param name="url">Server url</param>
        /// <param name="rquest">Push request data</param>
        /// <returns></returns>
        public T PostData<T>(string url, string rquest, string postMethod = "POST") where T : class
        {
            string result = null;

            byte[] data = Encoding.GetEncoding(this.charset).GetBytes(rquest);

            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                //如果是发送HTTPS请求  
                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                    request = WebRequest.Create(url) as HttpWebRequest;
                    request.ProtocolVersion = HttpVersion.Version10;
                }

                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);
                if (this.certFile != "")
                {
                    request.ClientCertificates.Add(new X509Certificate2(this.certFile, this.certPasswd));
                }

                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1)";
                request.Method = postMethod;//"POST";
                
                
                request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.Reload);
                request.ServicePoint.Expect100Continue = false;

                if (postMethod.ToUpper().Equals("POST"))
                {
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.ContentLength = data.Length;
                    Stream requestStream = request.GetRequestStream();
                    requestStream.Write(data, 0, data.Length);
                    requestStream.Close();
                }
                //// request.Timeout = RequestTimeOut;
                

                var response = (HttpWebResponse)request.GetResponse();
                using (var responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        result = new StreamReader(responseStream, Encoding.GetEncoding(this.charset)).ReadToEnd();
                        responseStream.Close();
                        Debug.WriteLine("PostData OK Response:" + result);
                    }
                }
            }
            catch (WebException wex)
            {
                log.Error(wex.ToString());
                Debug.WriteLine("PostData WebException:" + wex.Status.ToString() + " " + wex.Message);
                if (wex.Response != null)
                {
                    using (var errStream = wex.Response.GetResponseStream())
                    {
                        if (errStream != null)
                        {
                            result = new StreamReader(errStream, Encoding.GetEncoding(this.charset)).ReadToEnd();
                            errStream.Close();
                            Debug.WriteLine("PostData Error Response:" + result);
                        }
                    }
                }
            }
            catch (Exception exx)
            {
                log.Error(exx.ToString());
                Debug.WriteLine("PostData Exception:" + exx.Message);
            }

            return JsonConvert.DeserializeObject<T>(result);
            //// return result;
        }
        #endregion
    }
}
