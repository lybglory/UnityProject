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
    /// <summary>
    /// 敌军小兵集合
    /// </summary>
    [SerializeField]
    private List<Transform> lsSoldiersTarget = new List<Transform>();

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
        float dis = Vector3.Distance(transform.position,transTarget.position);
        
        if (dis<= 5f)
        {   //Debug.Log("distance="+ dis);
            navSoldier.speed = 0;
            this.transform.LookAt(transTarget.position);
            HPHealth soldierHP = this.GetComponent<HPHealth>();
            if (soldierHP.healthSliderHp.HpValue>0) {
                aniSoldier.CrossFade("Attack1");
            }
            
        }
        else {
            navSoldier.speed = 3.5f;
        }
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

    /// <summary>
    /// 攻击动画伤害方法(绑定动画事件英雄-Attack1-Copy出来。自带的不能编辑。)
    /// </summary>
    /// <param name="soldierDamg">伤害值</param>
    public void SoldierTakeDamage(float soldierDamg) {
        Debug.Log("SoldierTakeDamage调用");
        if (transTarget==null) {
            transTarget = FindTransTarget();
            return;
        }

        HPHealth targetHp = transTarget.GetComponent<HPHealth>();
        float damge = Random.Range(0.1f, 0.5f);
        targetHp.AcceptDamage(damge);
        if (targetHp.healthSliderHp.HpValue<=0) {
            Destroy(transTarget.gameObject);
            //移除目标对象
        }
    }

    /// <summary>
    /// 小兵碰撞触发：进入小兵攻击范围
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        SoldierMove colSoldier=other.GetComponent<SoldierMove>();
        //说明碰到的是敌方小兵
        if (colSoldier && this.type != colSoldier.type) {

        }
       
        
    }
}
