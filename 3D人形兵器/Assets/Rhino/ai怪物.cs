using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ai怪物 : MonoBehaviour
{
    public float 血量;
    public float 速度;
    public float 攻擊力;
    public float 偵測範圍;

    private Animator ani;
    private NavMeshAgent nav;

    private void Start()
    {
        ani = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0.2f, 0.3f);
        Gizmos.DrawSphere(transform.position, 偵測範圍);
    }
}
