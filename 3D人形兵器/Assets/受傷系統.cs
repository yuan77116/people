using UnityEngine;
/// <summary>
/// 血輛,動畫,死亡
/// </summary>
public class 受傷系統 : MonoBehaviour
{
    public float hp = 100;
    public string 受傷觸發 = "受傷觸發";
    public string 死亡觸發 = "死亡觸發";
    private Animator ani;
    private void Awake()
    {
        ani = GetComponent<Animator>();
    }
    public void 受傷(float 傷害值)
    {
        hp -= 傷害值;
        ani.SetTrigger("受傷");
        if (hp <= 0)
        {
            死亡();
        }
    }
    private void 死亡()
    {
        hp = 0;
    }
}
