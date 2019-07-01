using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Crawler.Common.Text
{
    public class ConvertUtil
    {
        /// <summary>
        /// 将对象序列化为Json数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string ToJson<T>(T t) where T : class
        {
            return JsonConvert.SerializeObject(t);
        }
        /// <summary>
        /// 批量序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static IEnumerable<string> ToJson<T>(IEnumerable<T> t) where T : class
        {
            var list = new List<string>();
            foreach (var x1 in t)
            {
                list.Add(JsonConvert.SerializeObject(x1));
            }
            return list;
        }
        /// <summary>
        /// 获取字符串的字节数组
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] ToUTF8Bytes(string str)
        {
            return System.Text.Encoding.UTF8.GetBytes(str);
        }
        /// <summary>
        ///获取批量字符串的二维字节数组
        /// </summary>
        /// <param name="strs"></param>
        /// <returns></returns>
        public static byte[][] ToUTF8BytesArr(IEnumerable<string> strs)
        {
            var bytes = new byte[strs.Count()][];
            var strArr = strs.ToArray();
            for (int i = 0; i < strArr.Length; i++)
            {
                bytes[i] = System.Text.Encoding.UTF8.GetBytes(strArr[i]);
            }
            return bytes;
        }
    }
}
