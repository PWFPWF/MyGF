using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 对象池抽屉结构
/// </summary>
public class MyPoolData
{
    [System.NonSerialized]
    [HideInInspector]
    public GameObject fatherObj;
    public List<GameObject> poolData;

    /// <summary>
    /// 构造方法初始化抽屉，设置好抽屉名字与抽屉父物体，之后将东西放入抽屉
    /// </summary>
    /// <param name="gobj">放入的东西</param>
    /// <param name="poolRoot">根节点</param>
    public MyPoolData(GameObject gobj, GameObject poolRoot)
    {
        //创建父节点
        fatherObj = new GameObject(gobj.name);
        //创建抽屉
        poolData = new List<GameObject>() { gobj };
        gobj.SetActive(false);
        //父节点放入根节点下
        fatherObj.transform.parent = poolRoot.transform;
        //抽屉放在父节点下
        gobj.transform.parent = fatherObj.transform;
    }
}
/// <summary>
/// 通用对象池
/// </summary>
public class BasePoolMgr : BaseInstance<BasePoolMgr>
{
    private Dictionary<string, MyPoolData> poolDic = new Dictionary<string, MyPoolData>();
    public GameObject poolRoot;

    /// <summary>
    /// 初始化对象池根节点
    /// </summary>
    public BasePoolMgr()
    {
        //对象池根节点
        poolRoot = new GameObject
        {
            name = "PoolRoot"
        };
    }

    /// <summary>
    /// 放入
    /// </summary>
    /// <param name="poolName">抽屉名字</param>
    /// <param name="gobj">放入的东西</param>
    public void Push(string poolName, GameObject gobj,UnityAction<GameObject> callback = null)
    {
        //有抽屉，直接放入
        if (poolDic.ContainsKey(poolName))
        {
            gobj.transform.parent = poolDic[poolName].fatherObj.transform;
            poolDic[poolName].poolData.Add(gobj);
            gobj.SetActive(false);
            callback?.Invoke(gobj);
        }//无抽屉，创建抽屉再放入
        else
        {
            poolDic.Add(poolName, new MyPoolData(gobj, poolRoot));
            callback?.Invoke(gobj);
        }
    }
    /// <summary>
    /// 取出
    /// </summary>
    /// <param name="name">东西名字</param>
    /// <returns></returns>
    public IEnumerator Pull(string name, UnityAction<GameObject> callback)
    {
        GameObject gameObject;
        //池内有抽屉，有东西，取出
        if (poolDic.ContainsKey(name) && poolDic[name].poolData.Count > 0)
        {
            //选择抽屉中的第一个
            gameObject = poolDic[name].poolData[0];
            //从抽屉中拿出来
            poolDic[name].poolData.RemoveAt(0);
            //断开与抽屉的联系
            gameObject.transform.parent = null;
            gameObject.SetActive(true);
            callback(gameObject);
            yield return null;
        }
        else
        {
            //池内无抽屉无物体，直接创建
            //通过异步加载资源 创建对象给外部用
            ResLoadMgr.Ins.LoadAsync<GameObject>(name, (o) =>
            {
                o.name = name;
                callback(o);
            });
            yield return null;
        }
    }
    /// <summary>
    /// 取出
    /// </summary>
    /// <param name="name">东西名字</param>
    /// <returns></returns>
    public IEnumerator Pull(string name, UnityAction<GameObject> callback,UnityAction<GameObject> newInitCallback = null)
    {
        GameObject gameObject;
        //池内有抽屉，有东西，取出
        if (poolDic.ContainsKey(name) && poolDic[name].poolData.Count > 0)
        {
            //选择抽屉中的第一个
            gameObject = poolDic[name].poolData[0];
            //从抽屉中拿出来
            poolDic[name].poolData.RemoveAt(0);
            //断开与抽屉的联系
            gameObject.transform.parent = null;
            gameObject.SetActive(true);
            callback(gameObject);
            yield return null;
        }
        else
        {
            //池内无抽屉无物体，直接创建
            //通过异步加载资源 创建对象给外部用
            ResLoadMgr.Ins.LoadAsync<GameObject>(name, (o) =>
            {
                o.name = name;
                newInitCallback?.Invoke(o);
            });
            yield return null;
        }
    }
    /// <summary>
    /// 清空池
    /// </summary>
    public void Clean()
    {
        poolDic.Clear();
        poolRoot = null;
    }
}
