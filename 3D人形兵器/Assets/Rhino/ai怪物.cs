using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class ai怪物 : MonoBehaviour
{
    public float 血量;
    public float 速度;
    public float 攻擊力;
    public float 偵測範圍;
    public float 攻擊範圍;
    public float 面相速度;
    public Vector3 攻擊判定位置;
    public Vector3 攻擊判定大小 = Vector3.one;
    public float 攻擊延遲=0.35f;

    public float 攻擊冷卻 = 3;
    private float 攻擊計時器;

    private Animator ani;
    private NavMeshAgent nav;
    private Transform 目標;

    private void Awake()
    {
        ani = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        nav.speed = 速度;
        nav.stoppingDistance = 攻擊範圍;
        攻擊計時器 = 攻擊冷卻;
    }
    private void Update()
    {
        檢查範圍();
        追蹤();
        攻擊();
    }
    private void 檢查範圍()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, 偵測範圍, 1 << 3);
        if (hits.Length > 0)
        {
            目標 = hits[0].transform;
        }
        else
        {
            目標 = null;
        }
    }
    private void 追蹤()
    {
        if (目標)
        {
            nav.isStopped = false;
            nav.SetDestination(目標.position);
        }
        else
        {
            nav.isStopped = true;
        }
        ani.SetBool("走路", !nav.isStopped);
    }
    private void 攻擊()
    {
        if (目標)
        {
            面相目標();
            float 距離 = Vector3.Distance(transform.position, 目標.position);
            if (距離 <= 攻擊範圍)
            {
                if (攻擊計時器 >= 攻擊冷卻)
                {
                    ani.SetTrigger("攻擊");
                    攻擊計時器 = 0;
                    StartCoroutine(擊中目標());
                }
                else
                {
                    攻擊計時器 += 1 * Time.deltaTime;
                    ani.SetBool("走路", false);
                }
            }
        }
    }
    private IEnumerator 擊中目標()
    {
        yield return new WaitForSeconds(攻擊延遲);
        Collider[]hits= Physics.OverlapBox(transform.position + transform.right * 攻擊判定位置.x + transform.up * 攻擊判定位置.y + transform.forward * 攻擊判定位置.z, 攻擊判定大小/2,Quaternion.identity,1<<3);
        if (hits.Length > 0)
        {
            hits[0].GetComponent<受傷系統>().受傷(攻擊力);
        }
    }
    private void 面相目標()
    {
        Vector3 位置 = 目標.position;
        位置.y = transform.position.y;
        //算夾角
        Quaternion look角度 = Quaternion.LookRotation(位置 - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, look角度, 面相速度 * Time.deltaTime);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0.2f, 0.3f);
        Gizmos.DrawSphere(transform.position, 偵測範圍);
        Gizmos.color = new Color(1, 0.2f, 0.5f, 0.3f);
        Gizmos.DrawSphere(transform.position, 攻擊範圍);

        Gizmos.color = new Color(0.2f, 0, 1f, 0.3f);
        Gizmos.matrix = Matrix4x4.TRS(transform.position + transform.right * 攻擊判定位置.x + transform.up * 攻擊判定位置.y + transform.forward * 攻擊判定位置.z, transform.rotation, transform.localScale);
        Gizmos.DrawCube(Vector3.zero, 攻擊判定大小);
    }
}
