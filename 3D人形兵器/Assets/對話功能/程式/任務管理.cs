using UnityEngine;

public class 任務管理 : MonoBehaviour
{
    public enum 任務狀態
    {
        任務階段1, 任務階段2, 任務階段3
    }
    [Header("任務狀態")]
    public 任務狀態 state;

    public void 任務進行中()
    {
        state = 任務狀態.任務階段2;
    }
    public void 任務結束()
    {
        state = 任務狀態.任務階段3;
    }
}
