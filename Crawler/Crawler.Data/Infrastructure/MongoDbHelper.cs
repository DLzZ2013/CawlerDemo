using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Crawler.Data.Infrastructure
{
    public class MongoDbHelper
    {
        #region MongoDb配置信息
        private static string _connectionStr = string.Empty;
        private static MongoClient _client;
        /// <summary>
        /// MongoDb客户端
        /// </summary>
        public static MongoClient MongoDBClient
        {
            get
            {
                if (_client == null)
                {
                    _client = new MongoClient(ConnectionStr);
                }
                return _client;
            }
            set { _client = value; }
        }
        /// <summary>
        /// 获取MongoDB数据库
        /// </summary>
        /// <returns></returns>
        public static IMongoDatabase GetDatabase()
        {
            var databaseName = ConfigurationManager.AppSettings["MongoDatabaseName"];
            return MongoDBClient.GetDatabase(databaseName);
        }
        /// <summary>
        /// 配置连接字符串
        /// </summary>
        public static string ConnectionStr
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionStr))
                {
                    _connectionStr = "mongodb://" + ConfigurationManager.AppSettings["MongoConnectionStr"];
                }

                return _connectionStr;
            }
            set { _connectionStr = value; }
        }
        #endregion

        #region MongoDb数据库CRUD
        /// <summary>
        /// 添加单条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="collectionName"></param>
        /// <returns></returns>
        public static bool Add<T>(T t, string collectionName) where T : class
        {
            try
            {
                var c = GetDatabase().GetCollection<T>(collectionName);
                c.InsertOne(t);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        /// <summary>
        /// 批量添加数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="collectionName"></param>
        /// <returns></returns>
        public static bool AddMany<T>(IEnumerable<T> t, string collectionName) where T : class
        {
            try
            {
                var c = GetDatabase().GetCollection<T>(collectionName);
                c.InsertMany(t);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        /// <summary>
        /// 删除单条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <param name="collectionName"></param>
        /// <returns></returns>
        public static DeleteResult Remove<T>(Expression<Func<T, bool>> where, string collectionName) where T : class
        {
            try
            {
                var c = GetDatabase().GetCollection<T>(collectionName);
                return c.DeleteOne(where);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 删除多条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <param name="collectionName"></param>
        /// <returns></returns>
        public static DeleteResult RemoveMany<T>(Expression<Func<T, bool>> where, string collectionName) where T : class
        {
            try
            {
                var c = GetDatabase().GetCollection<T>(collectionName);
                return c.DeleteMany(where);

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 更新单挑数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TField"></typeparam>
        /// <param name="collName"></param>
        /// <param name="t"></param>
        /// <param name="id"></param>
        /// <param name="isObjectId"></param>
        /// <returns></returns>
        public static UpdateResult Update<T, TField>(string collName, T t, string id, bool isObjectId = true) where T : class
        {
            try
            {
                var c = GetDatabase().GetCollection<T>(collName);
                //修改条件
                FilterDefinition<T> filter;
                if (isObjectId)
                {
                    filter = Builders<T>.Filter.Eq("_id", new ObjectId(id));
                }
                else
                {
                    filter = Builders<T>.Filter.Eq("_id", id);
                }
                //修改字段
                var list = new List<UpdateDefinition<T>>();
                foreach (var item in t.GetType().GetProperties())
                {
                    if (item.Name.ToLower() == "_id") continue;
                    list.Add(Builders<T>.Update.Set(item.Name, item.GetValue(t)));
                }
                var updatefilter = Builders<T>.Update.Combine(list);
                return c.UpdateOne(filter, updatefilter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 查询单条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <param name="collName"></param>
        /// <returns></returns>
        public static T SelectOne<T>(Expression<Func<T, bool>> where, string collName)
        {
            try
            {
                var c = GetDatabase().GetCollection<T>(collName);
                return c.Find(where).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 查询多条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <param name="collName"></param>
        /// <returns></returns>
        public static IEnumerable<T> SelectMany<T>(Expression<Func<T, bool>> where, string collName)
        {
            try
            {
                var c = GetDatabase().GetCollection<T>(collName);
                return c.Find(where).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion
    }
}
