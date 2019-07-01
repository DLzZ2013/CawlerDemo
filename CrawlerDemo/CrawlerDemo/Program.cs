using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CrawlerDemo
{
    class Program
    {
        static readonly HttpClient client = new HttpClient();
        static  void Main()
        {
            try
            {
                var request  =new HttpRequestMessage(HttpMethod.Get, "https://news.baidu.com/");
                request.Headers.Add("UserAgent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.100 Safari/537.3");
                var result =  client.SendAsync(request).ContinueWith((responseMsg) =>
                {
                    HttpResponseMessage response = responseMsg.Result;
                    //响应结果
                    response.EnsureSuccessStatusCode();
                    return response.Content.ReadAsStringAsync().ContinueWith(read =>
                        {
                            var str = read.Result.Replace(" ","");                    
                            var r = "<ahref=\\S+mon=(\"ct=[0-9]&a=[0-9]&c=top&pn=[0-9]+\"|\"ct=[0-9]&amp;a=[0-9]&amp;c=top&pn=[0-9]+\"|\"r=[0-9]\")(\\S*)>\\S+</a>";
                            var regex = new Regex(r);
                            foreach (var gGroup in regex.Matches(str))
                            {
                                var content = gGroup.ToString().Split('<','>')[2];
                                Console.WriteLine(content);
                            }
                           ;                            
                        }
                    );
                });
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine("请求异常!");
                Console.WriteLine("Message :{0} ", e.Message);

            }



        }     
    }
}
