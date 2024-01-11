using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 单例模式基类
/// </summary>
/// <typeparam name="T"></typeparam>
public class BaseInstance<T> where T : new()
{
    private static T ins;
    /// <summary>
    /// 静态属性写法
    /// </summary>
    public static T Ins
    {
        get
        {
            if (ins == null)
            {
                ins = new T();
                return ins;
            }
            return ins;
        }
    }
}
