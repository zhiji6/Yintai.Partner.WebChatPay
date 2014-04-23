using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using Yintai.Partner.WebChatPay.Config;

namespace Yintai.Partner.WebChatPay.Common
{
    public class WebUtil
    {
        WxCommonConfig wxCommonConfig = new WxCommonConfig();
        private Hashtable xmlMap;
        private Hashtable parameters;
        private HttpRequestBase httpContext;

        public Hashtable XmlMap
        {
            get { return xmlMap; }
        }

        public string GetUserAgent()
        {
            return HttpContext.Current.Request.UserAgent;
        }

        public string GetWeiXinVersion()
        {
            var userAgent = GetUserAgent();
            var version = userAgent.Substring(userAgent.IndexOf(@"MicroMessenger/"), 3); ////MicroMessenger/5.0
            ////var versionInDecimal = decimal.
            return version;
        }

        public bool IsVersionCanWeiXinPay()
        {
            decimal versionInDecimal = 0.0m;
            decimal targetVersion = 5.0m;
            decimal.TryParse(GetWeiXinVersion(), out versionInDecimal);
            decimal.TryParse(wxCommonConfig.MiniTargetVersion, out targetVersion);
            return versionInDecimal > targetVersion;
        }

        public string GetWeiXinPayTitleParams()
        {
            return "showwxpaytitle=1";
        }

        public static string Nonce()
        {
            return Guid.NewGuid().ToString().Replace("-", "").Substring(0, 32);
        }

        public static long Feng4Decimal(decimal value)
        {
            return (long)decimal.Multiply(value, 100);
        }

        public static string ClientIp(HttpRequestBase request)
        {
            string ipAddress = request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (ipAddress == null || ipAddress.ToLower() == "unknown")
                ipAddress = request.ServerVariables["REMOTE_ADDR"];

            return ipAddress;
        }

        public string GetTimeStampOfNow()
        {
             return ((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000).ToString();
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <returns></returns>
        public string GetPostDataSource(HttpRequestBase httpRequest)
        {
            string postDataSource = string.Empty;
            if (httpRequest.Form.AllKeys.Any())
            {
                postDataSource = httpRequest.Form[0];
            }

            if (string.IsNullOrEmpty(postDataSource) && httpRequest.InputStream.Length > 0)
            {
                System.IO.Stream ns = httpRequest.InputStream;
                StreamReader reader = new StreamReader(ns, Encoding.Default);
                postDataSource = reader.ReadToEnd();
                //LoggerHelper.Error("key:postdata;value:" + postDataSource);
                //var webutil = new WebUtil();
                //webutil.ResponseHandler(this.HttpContext.Request);
                //LoggerHelper.Error("key:postdata;value:" + string.Join(",", webutil.XmlMap.Keys));
                //LoggerHelper.Error("key:postdata;value:" + string.Join(",", webutil.XmlMap.Values));
            }
            return postDataSource;
        }

        //获取页面提交的get和post参数
        public void ResponseHandler(HttpRequestBase httpContext)
        {
            parameters = new Hashtable();
            xmlMap = new Hashtable();

            this.httpContext = httpContext;
            NameValueCollection collection;
            //post data
            if (this.httpContext.HttpMethod == "POST")
            {
                collection = this.httpContext.Form;
                foreach (string k in collection)
                {
                    string v = (string)collection[k];
                    this.setParameter(k, v);
                }
            }
            //query string
            collection = this.httpContext.QueryString;
            foreach (string k in collection)
            {
                string v = (string)collection[k];
                this.setParameter(k, v);
            }
            if (this.httpContext.InputStream.Length > 0)
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(this.httpContext.InputStream);
                XmlNode root = xmlDoc.SelectSingleNode("xml");
                XmlNodeList xnl = root.ChildNodes;

                foreach (XmlNode xnf in xnl)
                {
                    xmlMap.Add(xnf.Name, xnf.InnerText);
                }
            }
        }

        /** 设置参数值 */
        public void setParameter(string parameter, string parameterValue)
        {
            if (parameter != null && parameter != "")
            {
                if (parameters.Contains(parameter))
                {
                    parameters.Remove(parameter);
                }

                parameters.Add(parameter, parameterValue);
            }
        }

        /** 获取XML值 */
        public string getxmlMap(string parameter)
        {
            string s = (string)xmlMap[parameter];
            return (null == s) ? "" : s;
        }

        //string OpenId = resHandler.getxmlMap("OpenId");


    }
}
