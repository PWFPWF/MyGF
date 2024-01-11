using UnityEngine;
using UnityEngine.Events;

public class MonoController : MonoBehaviour
{
    private event UnityAction action;

    private void Start()
    {
        GameObject.DontDestroyOnLoad(this);
    }
    private void Update()
    {
        if (action != null)
        {
            action.Invoke();
        }
    }
    public void AddUpdateListener(UnityAction action)
    {
        this.action += action;
    }
    public void DeleteUpdateListener(UnityAction action)
    {
        this.action -= action;
    }
}
