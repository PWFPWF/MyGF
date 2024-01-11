using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 资源加载模块
/// </summary>
public class ResLoadMgr : BaseInstance<ResLoadMgr>
{
    //异步加载
    public void LoadAsync<T>(string name, UnityAction<T> callback) where T : Object
    {
        //开启异步加载的协程
        MonoMgr.Ins.StartCoroutine(ReallyLoadAsync(name, callback));
    }
    //真正的协同程序函数  用于 开启异步加载对应的资源
    private IEnumerator ReallyLoadAsync<T>(string name, UnityAction<T> callback) where T : Object
    {
        ResourceRequest r = Resources.LoadAsync<T>(name);
        yield return r;

        if (r.asset is GameObject)
        {
            callback(GameObject.Instantiate(r.asset) as T);
        }
        else
            callback(r.asset as T);
    }
    //同步加载
    public T Load<T>(string name) where T : Object
    {
        T r = Resources.Load<T>(name);
        if (r is GameObject)
            return GameObject.Instantiate(r);
        else
            return r;

    }
}
