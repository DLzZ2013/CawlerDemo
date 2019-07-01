using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Crawler.Business;
using Crawler.Common.Text;
using Crawler.Config;

namespace Crawler.Web
{
    public partial class news : System.Web.UI.Page
    {
        /// <summary>
        /// 获取百度热点新闻
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static string GetBaiduNews()
        {
            var url = NetConstants.NewsWebSite["Baidu"];
            var data = CrawlerBll.GetBaiduNewsContent(url);
            return ConvertUtil.ToJson(data);

        }
    }
}