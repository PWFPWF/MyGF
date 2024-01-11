using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// UI面板基类
/// </summary>
public class BasePanel : MonoBehaviour
{
    public Dictionary<string, List<UIBehaviour>> UIControlDic = new Dictionary<string, List<UIBehaviour>>();

    private void Awake()
    {
        FindAllChildUIControl<Button>();
        FindAllChildUIControl<Image>();
        FindAllChildUIControl<Text>();
        FindAllChildUIControl<Slider>();
        FindAllChildUIControl<Toggle>();
        FindAllChildUIControl<ScrollRect>();
        FindAllChildUIControl<InputField>();
    }

    //查询该Panel内的控件
    private void FindAllChildUIControl<T>() where T : UIBehaviour
    {
        string name;
        T[] ts = GetComponentsInChildren<T>();

        for (int i = 0; i < ts.Length; i++)
        {
            name = ts[i].gameObject.name;
            if (UIControlDic.ContainsKey(name))
            {
                UIControlDic[name].Add(ts[i]);
            }
            else
            {
                UIControlDic.Add(ts[i].gameObject.name, new List<UIBehaviour> { ts[i] });
            }
        }
    }
    //获取控件
    protected T GetUIControl<T>(string name) where T : UIBehaviour
    {
        if (UIControlDic.ContainsKey(name))
        {
            for (int i = 0; i < UIControlDic[name].Count; ++i)
            {
                if (UIControlDic[name][i] is T)
                    return UIControlDic[name][i] as T;
            }
        }
        return null;
    }
    public virtual void ShowMe() { }

    public virtual void HideMe() { }

    protected virtual void OnClick(string btnName) { }

    protected virtual void OnValueChanged(string toggleName, bool value) { }
}
