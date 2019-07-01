using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Crawler.Common.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using ServiceStack.Redis;
using ServiceStack.Text.Json;

namespace Crawler.Data.Infrastructure
{
    public class RedisDataHelper
    {
        #region Redis配置信息

        private static string _connectionStr = string.Empty;
        private static RedisClient _client;

        /// <summary>
        /// Redis客户端
        /// </summary>
        public static RedisClient RedisDBClient
        {
            get
            {
                if (_client == null)
                {
                    _client = new RedisClient(_connectionStr);
                }

                return _client;
            }
            set { _client = value; }
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
                    _connectionStr = ConfigurationManager.AppSettings["RedisConnectionStr"];
                }

                return _connectionStr;
            }
            set { _connectionStr = value; }
        }

        #endregion

        #region Redis缓存CRUD操作
        /// <summary>
        /// 添加键值对
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static bool StringAdd(string key, string value, TimeSpan timeout = default(TimeSpan))
        {
            try
            {                
                RedisDBClient.Add(key, value);
                if (timeout != default(TimeSpan))
                {
                    RedisDBClient.ExpireEntryIn(key, timeout);
                }

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }     
        /// <summary>
        /// 批量添加Set集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool AddRange<T>(IEnumerable<T> t, string key, TimeSpan timeout = default(TimeSpan))
            where T : class
        {
            try
            {
                var jsonData = ConvertUtil.ToJson(t);
                RedisDBClient.AddRangeToSet(key, jsonData.ToList());
                if (timeout != default(TimeSpan))
                {
                    RedisDBClient.ExpireEntryIn(key, timeout);
                }

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        /// <summary>
        /// 删除Set中单条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Remove<T>(T t, string key) where T : class
        {
            try
            {

                var jsonData = ConvertUtil.ToJson(t);
                RedisDBClient.RemoveItemFromSet(key, jsonData);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        /// <summary>
        /// 删除Set中数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool RemoveAll<T>(IEnumerable<T> t, string key) where T : class
        {
            try
            {
                var jsonData = ConvertUtil.ToJson(t);
                var result = RedisDBClient.Remove(key);
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        /// <summary>
        /// 获取多条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static List<string> SelectData<T>(string key) where T : class
        {
            try
            {
                var count = RedisDBClient.GetAllItemsFromSet(key);
                return count.ToList();
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}
#endregion
