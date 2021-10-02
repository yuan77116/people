/*
 * 相關
 */
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;
/// <summary>
///     攻擊系統
///     變身模式
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
    public float[] 攻擊力 ={10,20,30,40 };
    public Vector3[] 攻擊判定位置;
    public Vector3[] 攻擊判定大小;
    public Color[] 顏色;
    public float[] 攻擊延遲;
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
        //變身後
        //bool 使用變身 = GameObject.Find("變身系統").GetComponent<變身>().變身bo;
        bool 使用變身 = 變身.變身bo;
        if (使用變身)
        {
            ani.SetBool("變身", true);
        }
        else
        {
            ani.SetBool("變身", false);
        }
        //變身前
        if (Input.GetMouseButton(0))
        {
            左長壓時長 += 1 * Time.deltaTime;
            左點擊時長 += 1 * Time.deltaTime;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            //print("左點擊時間" + 左長壓時長);
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
    private IEnumerator 擊中目標(int 擊中編號)
    {
        yield return new WaitForSeconds(攻擊延遲[擊中編號]);
        Collider[] hits = Physics.OverlapBox(transform.position + transform.right 
            * 攻擊判定位置[擊中編號].x + transform.up * 攻擊判定位置[擊中編號].y + transform.forward 
            * 攻擊判定位置[擊中編號].z, 攻擊判定大小[擊中編號] / 2, Quaternion.identity, 1 << 6);
        if (hits.Length > 0)
        {
            hits[0].GetComponent<受傷系統>().受傷(攻擊力[擊中編號]);
            //print(hits[0].name);
        }
    }
    private void 左集氣攻擊()
    {
        ani.SetTrigger("左集氣");
        StartCoroutine(擊中目標(3));
    }
    private void 左攻擊()
    {
        if (左點擊時長 < 左intervalwait[左段位])
        {
            CancelInvoke("恢復左段數");
            Invoke("恢復左段數", 左intervalwait[左段位]);
            StartCoroutine(擊中目標(左段位));
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
    private void OnDrawGizmos()
    {
        for (int i = 0; i < 攻擊力.Length; i++)
        {
            Gizmos.color = 顏色[i];
            Gizmos.matrix = Matrix4x4.TRS(transform.position + transform.right 
                * 攻擊判定位置[i].x + transform.up 
                * 攻擊判定位置[i].y + transform.forward 
                * 攻擊判定位置[i].z, transform.rotation, transform.localScale);
            Gizmos.DrawCube(Vector3.zero, 攻擊判定大小[i]);
        }
    }
    #endregion

    #region 方法：公開

    #endregion
}
