using UnityEngine;

public class 任務管理 : MonoBehaviour
{
    public enum 任務狀態
    {
        任務階段1, 任務階段2, 任務階段3
    }
    [Header("任務狀態")]
    public 任務狀態 state;
    public static 任務管理 ins;
    public 存放對話資料 data;
    private void Start()
    {
        任務管理.ins = GameObject.Find("任務管理").GetComponent<任務管理>();
    }
    public void 任務進行中()
    {
        state = 任務狀態.任務階段2;
    }
    public void 更新任務完成數量(int 更新數目)
    {
        data.此對話任務的需求數量 -= 更新數目;
        if (data.此對話任務的需求數量 == 0)
        {
            任務完成();
        }
    }
    private void 任務完成()
    {
        state = 任務狀態.任務階段3;
        //對話系統.ins.CancelInvoke("修改內容");
        StartCoroutine(對話系統.ins.修改內容(data.結束內容));
    }
}
