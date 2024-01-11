using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface I_EventInfo
{

}
public class MyEventInfo : I_EventInfo
{
    public UnityAction action;
    public MyEventInfo(UnityAction action)
    {
        this.action += action;
    }
}
public class MyEventInfo<T> : I_EventInfo
{
    public UnityAction<T> action;
    public MyEventInfo(UnityAction<T> action)
    {
        this.action += action;
    }
}
/// <summary>
/// 事件中心模块
/// </summary>
public class EventCenterMgr : BaseInstance<EventCenterMgr>
{
    public Dictionary<string, I_EventInfo> eventDic = new Dictionary<string, I_EventInfo>();

    /// <summary>
    /// 添加事件监听
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="action"></param>
    public void AddEventListener(string eventName, UnityAction action)
    {
        if (eventDic.ContainsKey(eventName))
        {
            (eventDic[eventName] as MyEventInfo).action += action;
        }
        else
        {
            eventDic.Add(eventName, new MyEventInfo(action));
        }
    }
    public void AddEventListener<T>(string eventName, UnityAction<T> action)
    {
        if (eventDic.ContainsKey(eventName))
        {
            (eventDic[eventName] as MyEventInfo<T>).action += action;
        }
        else
        {
            eventDic.Add(eventName, new MyEventInfo<T>(action));
        }
    }
    /// <summary>
    /// 移除事件监听
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="action"></param>
    public void RemoveEventListener(string eventName, UnityAction action)
    {
        if (eventDic.ContainsKey(eventName))
        {
            (eventDic[eventName] as MyEventInfo).action -= action;
        }
    }
    public void RemoveEventListener<T>(string eventName, UnityAction<T> action)
    {
        if (eventDic.ContainsKey(eventName))
        {
            (eventDic[eventName] as MyEventInfo<T>).action -= action;
        }
    }
    /// <summary>
    /// 触发事件
    /// </summary>
    /// <param name="eventName"></param>
    public void EventTrigger(string eventName)
    {
        if (eventDic.ContainsKey(eventName))
        {
            (eventDic[eventName] as MyEventInfo).action.Invoke();
        }
    }
    public void EventTrigger<T>(string eventName,T info)
    {
        if (eventDic.ContainsKey(eventName))
        {
            (eventDic[eventName] as MyEventInfo<T>).action.Invoke(info);
        }
    }
    /// <summary>
    /// 清空事件
    /// </summary>
    public void Clear()
    {
        eventDic.Clear();
    }
}
