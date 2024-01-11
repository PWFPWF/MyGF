using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// 场景管理
/// </summary>
public class SceneMgr : BaseInstance<SceneMgr>
{
    //同步加载场景
    public void LoadScene(string sceneName,UnityAction action = null) {
        SceneManager.LoadScene(sceneName);
        //加载完后执行
        action();
    }
    //异步加载场景
    public void LoadSceneAsync(string sceneName,UnityAction action = null) {
        //使用MonoMgr开启协程
        MonoMgr.Ins.StartCoroutine(LoadSceneAsyncIE(sceneName,action));
    }
    private IEnumerator LoadSceneAsyncIE(string sceneName, UnityAction action = null) {
        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName);
        while (!ao.isDone)
        {
            //通过事件中心向外部分发场景加载进度
            EventCenterMgr.Ins.EventTrigger("场景切换进度条",ao.progress);
            //外部UI模块通过以下方式调整进度条
            //EventCenterMgr.Ins.AddEventListener<float>("场景切换进度条",(f)=> { //进度条赋值 });
            yield return ao.progress;
        }
        action();
    }
}

