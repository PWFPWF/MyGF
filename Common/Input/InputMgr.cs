using UnityEngine;
/// <summary>
/// 输入管理
/// </summary>
public class InputMgr : BaseInstance<InputMgr>
{
    private bool isStart = false;

    public void ChangeInputSysStatus(bool isStart)
    {
        this.isStart = isStart;
    }
    public InputMgr()
    {
        //Mono外部帧更新
        MonoMgr.Ins.AddUpdateListener(MonoUpdate);
    }

    //分发键盘按下抬起事件
    private void GetKeyCode(KeyCode keyCode)
    {
        if (Input.GetKeyDown(keyCode))
        {
            EventCenterMgr.Ins.EventTrigger("KeyDown", keyCode);
        }
        if (Input.GetKeyUp(keyCode))
        {
            EventCenterMgr.Ins.EventTrigger("KeyUp", keyCode);
            //EventCenterMgr_Cla.Ins.AddEventListener<string>("KeyDown",(keyCode) => { });
        }
    }

    private void MonoUpdate()
    {
        if (isStart)
        {
            GetKeyCode(KeyCode.W);
        }
    }
}