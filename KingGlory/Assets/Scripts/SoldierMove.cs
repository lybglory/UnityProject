using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;//NavMeshAgent引用空间

public class SoldierMove : MonoBehaviour {
    /// <summary>
    /// 具体单个目标物
    /// </summary>
    [SerializeField]
    public Transform transTarget;
    /// <summary>
    /// 移动的动画
    /// </summary>
    private Animation aniSoldier;
    /// <summary>
    /// NavMeshAgent句柄
    /// </summary>
    private NavMeshAgent navSoldier;
    /// <summary>
    /// 目标物数组
    /// </summary>
    [SerializeField]
    public Transform[] enemyTrans;
    /// <summary>
    /// 小兵类型
    /// </summary>
    public int type = -1;

    private void Awake()
    {
        navSoldier = this.GetComponent<NavMeshAgent>();
        aniSoldier = this.GetComponent<Animation>();
    }

    void Start () {
        transTarget=FindTransTarget();
    }
	
	// Update is called once per frame
	void Update () {
        SoldiMove();
    }

    /// <summary>
    ///  小兵移动
    /// </summary>
    void SoldiMove() {
        if (transTarget==null) {
            transTarget = FindTransTarget();
            return;
        }

        aniSoldier.CrossFade("Run");
        navSoldier.SetDestination(transTarget.position);
    }

    /// <summary>
    /// 寻找目标物
    /// </summary>
    Transform FindTransTarget() {
        for (int i = 0; i < enemyTrans.Length; i++)
        {
            if (enemyTrans[i]!=null) {
                return enemyTrans[i];
            }
        }
        return null;
    }

    /// <summary>
    /// 设置寻路层
    /// </summary>
    /// <param name="roadMask">寻路层</param>
    public void SetRoadMask(int roadMask) {
        if (navSoldier==null) {
            Debug.LogError("navSoldier is null");
            navSoldier = this.GetComponent<NavMeshAgent>();
        }
        //Debug.Log("roadMask="+ roadMask);
        navSoldier.areaMask = roadMask;
    }
}
