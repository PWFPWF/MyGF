using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//面板层级
public enum PanelLevel_E
{
    Bot,
    Mid,
    Top,
    Sys,
}
//UI管理器
public class UIMgr : BaseInstance<UIMgr>
{
    public Dictionary<string, BasePanel> panelDic = new Dictionary<string, BasePanel>();
    private GameObject canvas;
    private GameObject eventSystem;
    public Transform Bot;
    public Transform Mid;
    public Transform Top;
    public Transform Sys;
    public UIMgr()
    {
        canvas = ResLoadMgr.Ins.Load<GameObject>("UI/Canvas");
        GameObject.DontDestroyOnLoad(canvas);
        eventSystem = ResLoadMgr.Ins.Load<GameObject>("UI/EventSystem");
        GameObject.DontDestroyOnLoad(eventSystem);
        Bot = canvas.transform.Find("Bot");
        Mid = canvas.transform.Find("Mid");
        Top = canvas.transform.Find("Top");
        Sys = canvas.transform.Find("Sys");
    }
    //显示面板
    public void ShowPanel<T>(string name, PanelLevel_E panelLevel = PanelLevel_E.Bot, UnityAction<T> callback = null) where T : BasePanel
    {
        if (panelDic.ContainsKey(name))
        {
            panelDic[name].ShowMe();
            callback?.Invoke(panelDic[name] as T);
            return;
        }
        //异步加载UI资源
        ResLoadMgr.Ins.LoadAsync<GameObject>("UI/" + name, (obj) =>
        {
            switch (panelLevel)
            {
                case PanelLevel_E.Bot:
                    obj.transform.parent = Bot.transform;
                    break;
                case PanelLevel_E.Mid:
                    obj.transform.parent = Mid.transform;
                    break;
                case PanelLevel_E.Top:
                    obj.transform.parent = Top.transform;
                    break;
                case PanelLevel_E.Sys:
                    obj.transform.parent = Sys.transform;
                    break;
                default:
                    break;
            }
            //设置面板位置
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = Vector3.one;
            (obj.transform as RectTransform).offsetMax = Vector2.one;
            (obj.transform as RectTransform).offsetMin = Vector2.one;
            //获取面板脚本
            T panelCla = obj.GetComponent<T>();
            //传递脚本供外部调用
            callback?.Invoke(panelCla);
            panelCla.ShowMe();
            panelDic.Add(name, panelCla);
        });

    }
    //隐藏面板
    public void HidePanel(string name)
    {
        if (panelDic.ContainsKey(name))
        {
            panelDic[name].HideMe();
            GameObject.Destroy(panelDic[name].gameObject);
            panelDic.Remove(name);
        }
    }
    //获取一个已经显示的面板
    public T GetPanel<T>(string name) where T : BasePanel
    {
        if (panelDic.ContainsKey(name))
        {
            return panelDic[name] as T;
        }
        return null;
    }
    /// <summary>
    /// 使用EventTigger组件给控件添加自定义事件监听
    /// </summary>
    public static void AddCustomEventListener(UIBehaviour control, EventTriggerType type, UnityAction<BaseEventData> callBack)
    {
        EventTrigger trigger = control.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = control.gameObject.AddComponent<EventTrigger>();
        }
        EventTrigger.Entry entry = new EventTrigger.Entry() { eventID = type };
        entry.callback.AddListener(callBack);
        trigger.triggers.Add(entry);
    }
}
