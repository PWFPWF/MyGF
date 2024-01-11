using UnityEngine;
/// <summary>
/// Mono单例基类
/// 继承此类会创建一个不销毁的GameObject存放此脚本
/// </summary>
/// <typeparam name="T"></typeparam>
public class BaseMonoInstance<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T GetInstance()
    {
        if (instance == null)
        {
            GameObject obj = new GameObject(typeof(T).ToString());
            DontDestroyOnLoad(obj);
            instance = obj.AddComponent<T>();
        }
        return instance;
    }

}
