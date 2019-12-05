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
    /// 目标箭塔数组
    /// </summary>
    [SerializeField]
    public Transform[] enemyTowerTrans;
    /// <summary>
    /// 小兵类型(我方小兵0;地方小兵1)
    /// </summary>
    public int type = -1;
    /// <summary>
    /// 敌军小兵集合
    /// </summary>
    [SerializeField]
    private List<Transform> lsSoldiersTarget = new List<Transform>();
    /// <summary>
    /// 英雄集合
    /// </summary>
    [SerializeField]
    private List<Transform> lsHeroTarget = new List<Transform>();

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
        
        if (dis< 5f)
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
        lsSoldiersTarget.RemoveAll(t => t == null);

        if (lsSoldiersTarget.Count>0) {
            //从小兵集合中去第一个小兵为当前目标
            return lsSoldiersTarget[0];
        }
        //没有小兵的话，从箭塔中取
        for (int i = 0; i < enemyTowerTrans.Length; i++)
        {
            if (enemyTowerTrans[i]!=null) {
                return enemyTowerTrans[i];
            }
        }
        //都没有的话，目标物为空
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
            //移除目标对象,先判定目标物是否是 集合中的小兵，如果是，表面当前攻击的是小兵。如果不是表示攻击的是箭塔
            if (lsSoldiersTarget.Contains(transTarget))
            {
                lsSoldiersTarget.Remove(transTarget);
            }
            else {
                //从箭塔集合中移除
                SoldierRemoveTower(transTarget);
            }
            //从集合中移除之后，开始寻找下一个目标
            transTarget=FindTransTarget();
            if (transTarget) {
                navSoldier.SetDestination(transTarget.position);
                navSoldier.speed = 3.5f;
                aniSoldier.CrossFade("Run");
                
            }
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
        if (colSoldier!=null && this.type != colSoldier.type && !lsSoldiersTarget.Contains(other.transform))
        {   //添加到集合
            lsSoldiersTarget.Add(other.transform);
            Debug.Log(" 敌军小兵添加");
            Transform tempSoldierTarget = lsSoldiersTarget[0];
            //把敌军集合里的第一个小兵设置为目标
            //先判断当前目标是否是第一个敌军小兵，如果不是才赋值，避免重复
            if (transTarget!= tempSoldierTarget&& transTarget==null) {
                transTarget = tempSoldierTarget;
            }
        }
        else if(other.gameObject.tag.Equals("Player")&&this.type==1)
        {
            lsHeroTarget.Add(other.transform);
        }
    }//OnTriggerEnter_end

    /// <summary>
    /// 不在小兵攻击范围
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (lsSoldiersTarget.Contains(other.transform)) {
            //lsSoldiersTarget.Remove(other.transform);
            //transTarget = FindTransTarget();
        }
    }

    /// <summary>
    /// 从箭塔集合中移除箭塔
    /// </summary>
    /// <param name="destoryTower">需要移除的箭塔</param>
    private void SoldierRemoveTower(Transform destoryTower) {
        
        for (int i = 0; i < enemyTowerTrans.Length; i++)
        {   //从己方箭塔集合中查找要移除的箭塔
            if (enemyTowerTrans[i]== destoryTower) {
                enemyTowerTrans[i] = null;
                return;
            }
        }
    }
}
