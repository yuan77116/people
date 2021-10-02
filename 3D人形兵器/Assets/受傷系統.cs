using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
/// <summary>
/// 血輛,動畫,死亡
/// </summary>
public class 受傷系統 : MonoBehaviour
{
    public float hp = 100;
    public string 受傷觸發 = "受傷觸發";
    public string dead = "死亡觸發";
    private Animator ani;
    public UnityEvent 死亡後;
    public UnityEvent 受傷後;
    public Image imagehp;
    private float hpmax;
    private void Awake()
    {
        ani = GetComponent<Animator>();
        hpmax = hp;
    }
    public void 受傷(float 傷害值)
    {
        if (ani.GetBool(dead)) return;  //如果死亡就跳出
         hp -= 傷害值;
        ani.SetTrigger("受傷");
        受傷後.Invoke();
        if (hp <= 0)
        {
            死亡();
        }
    }
    private void 死亡()
    {
        ani.SetBool(dead, true);
        hp = 0;
        死亡後.Invoke();
    }
    public void 更新血條()
    {
        imagehp.fillAmount = hp / hpmax;
    }

}
