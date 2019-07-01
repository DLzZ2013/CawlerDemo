using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Crawler.Config;

namespace Crawler.Common.Net
{
    public class WebUtil
    {
        /// <summary>
        /// Http客户端
        /// </summary>
        private static readonly HttpClient client;
        /// <summary>
        /// 获取http请求响应
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="accept">请求接收头</param>
        /// <param name="userAgent">请求用户代理</param>
        /// <param name="headers">请求头其他参数</param>
        /// <returns></returns>
        public static string GetHttpContent(string url,
            string accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8,*/*",
            string userAgent =
                "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.94 Safari/537.36",
            Dictionary<string, string> headers = null)
        {
            //设置请求头及请求参数
            var request = WebRequest.Create(url);
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }
            //请求响应
            try
            {
                var content = "";
                var response = request.GetResponse();
                var stream = response.GetResponseStream();
                if (stream != null)
                {
                    content = new StreamReader(stream).ReadToEnd();
                }
                response.Close();
                return content;
            }
            catch (Exception e)
            {
                throw new Exception("请求异常！\r\n" + "Message:" + e.Message);
            }
        }

    }
}
