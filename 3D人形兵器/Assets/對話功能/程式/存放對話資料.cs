using UnityEngine;

//建立素材選項
[CreateAssetMenu(menuName ="人形/對話資料",fileName ="對話資料")]
public class 存放對話資料 : ScriptableObject
{
    public string 對話者名稱;
    [TextArea(2,5)]
    public string[] 對話內容;
    [Range(0,100)]
    public int 此對話任務的需求數量=2;
    [TextArea(2, 5)]
    public string[] 結束內容;
}
