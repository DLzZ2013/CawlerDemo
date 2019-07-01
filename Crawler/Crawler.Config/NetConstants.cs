using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Config
{
    public class NetConstants
    {
        #region public Dictionary<string, string> NewsWebSite 新闻站点
        /// <summary>
        /// 新闻站点
        /// </summary>
        public static Dictionary<string, string> NewsWebSite
        {
            get
            {
                return new Dictionary<string, string>
                {
                    {"Baidu", "https://news.baidu.com/"},
                };
            }
        }
        #endregion
    }
}
