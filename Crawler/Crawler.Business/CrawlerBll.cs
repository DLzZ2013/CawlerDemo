using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Crawler.Common.Net;
using Crawler.Common.Text;
using Crawler.Config;
using Crawler.Data.Dao;
using Crawler.Data.Infrastructure;
using Crawler.Model;
using Crawler.Model.News;

namespace Crawler.Business
{
    public class CrawlerBll
    {
        /// <summary>
        /// 获取新闻内容
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static List<NewsModel> GetBaiduNewsContent(string url)
        {            
            //请求响应

            var httpContent = WebUtil.GetHttpContent(url);
            //获取新闻标题列表
            var regex =
                "<ahref=\\S+mon=(\"ct=[0-9]&a=[0-9]&c=top&pn=[0-9]+\"|\"ct=[0-9]&amp;a=[0-9]&amp;c=top&pn=[0-9]+\"" +
                "|\"r=[0-9]\")(\\S*)>\\S+</a>";
            var newsList = HtmlUtil.StripHTML(httpContent, regex);
            var listData = new List<NewsModel>();
            //将数据保存到MongoDb中
            foreach (var news in newsList)
            {
                var model = new NewsModel()
                {
                    NewsTitle = news,
                    CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    UpdateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };
                listData.Add(model);
            }

            NewsDao.InsertToMongo(listData);
            return listData;
        }
    }
}
