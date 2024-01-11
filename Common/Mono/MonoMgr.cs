using System.Collections;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

public class MonoMgr:BaseInstance<MonoMgr>
{
    private MonoController controller;

    public MonoMgr() {
        GameObject obj = new GameObject("MonoMgr");
        controller = obj.AddComponent<MonoController>();
    }
    /// <summary>
    /// 添加外部帧更新
    /// </summary>
    /// <param name="action"></param>
    public void AddUpdateListener(UnityAction action)
    {
        controller.AddUpdateListener(action);
    }
    /// <summary>
    /// 移除外部帧更新
    /// </summary>
    /// <param name="action"></param>
    public void DeleteUpdateListener(UnityAction action)
    {
        controller.DeleteUpdateListener(action);
    }

    public Coroutine StartCoroutine(string methodName) {
        return controller.StartCoroutine(methodName);
    }
    public Coroutine StartCoroutine(IEnumerator routine) {
        return controller.StartCoroutine(routine);
    }
    public Coroutine StartCoroutine(string methodName, [DefaultValue("null")] object value) {
        return controller.StartCoroutine(methodName, value);
    }
}
