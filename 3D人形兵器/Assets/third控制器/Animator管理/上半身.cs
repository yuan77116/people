/*
 * 相關
 */
using UnityEngine;
using UnityEngine.Events;
/// <summary>
///     攻擊系統
/// </summary>
public class 上半身 : MonoBehaviour
{
    #region 公開
    [Header("參數名稱")]
    public string parAttackPark = "左攻段數";
    public string parAttackGather = "左集氣";
    [Header("左擊間隔等待時間"), Range(0, 2)]
    public float[] 左intervalwait = { 1f, 0.8f, 0.5f };
    [Header("集氣時間"), Range(0, 2)]
    public float 左集interval = 1f;
    [Header("攻集"), Range(0, 10)]
    public int 左段總長 = 3;
    #endregion

    #region 私人
    private Animator ani;

    private float 左長壓時長;
    private float 左點擊時長;
    private int 左段位;
    #endregion

    #region 事件
    // Awake 喚醒事件 (前)
    private void Awake()
    {
        ani = GetComponent<Animator>();
    }
    // Start 開始事件 (後)
    private void Start()
    {

    }
    private void Update()
    {
        左點擊vo();
    }
    #endregion

    #region 方法：私人
    private void 左點擊vo()
    {
        print("左點擊時間" + 左長壓時長);

        if (Input.GetMouseButton(0))
        {
            左長壓時長 += 1 * Time.deltaTime;
            左點擊時長 += 1 * Time.deltaTime;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (左長壓時長 >= 左集interval)        //長點
            {
                左集氣攻擊();
            }
            else         //單點
            {
                左攻擊();
            }
            左長壓時長 = 0;
        }
    }
    private void 左集氣攻擊()
    {
        ani.SetTrigger("左集氣");
    }
    private void 左攻擊()
    {
        if (左點擊時長 < 左intervalwait[左段位])
        {
            CancelInvoke("恢復左段數");
            Invoke("恢復左段數", 左intervalwait[左段位]);
            左段位++;
        }
        else
        {
            左段位 = 0;
        }
        左點擊時長 = 0;
        ani.SetInteger(parAttackPark, 左段位);
        if (左段位 == 左段總長) { 左段位 = 0; }
    }
    private void 恢復左段數()
    {
        左段位 = 0;
        ani.SetInteger(parAttackPark, 左段位);
    }
    #endregion

    #region 方法：公開

    #endregion
}
