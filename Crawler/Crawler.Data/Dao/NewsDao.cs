using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crawler.Data.Infrastructure;
using Crawler.Model;
using Crawler.Model.News;

namespace Crawler.Data.Dao
{
    public class NewsDao
    {
        /// <summary>
        /// 将数据添加到MongoDb中
        /// </summary>
        /// <param name="models"></param>
        public static bool InsertToMongo(IEnumerable<NewsModel> models)
        {
            var result = MongoDbHelper.AddMany(models, "News");
            return result;
        }
    }
}
