using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Crawler.Common.Text
{
    public class HtmlUtil
    {
        /// <summary>
        /// 匹配html，并获取标签元素中的文本
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static List<string> StripHTML(string html,string regexStr)
        {
            var list = new List<string>();
            html = html.Replace(" ", "");
            var regex = new Regex(regexStr);
            foreach (var match in regex.Matches(html))
            {
                var content = match.ToString().Split('<', '>')[2];
                list.Add(content);
            }

            return list;
        }
    }
}
