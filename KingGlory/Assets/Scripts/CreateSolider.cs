using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSolider : MonoBehaviour {
    /// <summary>
    /// 小兵预制
    /// </summary>
    [SerializeField]
    private GameObject ObjSoliderPrefab;
    /// <summary>
    /// 小兵父对象
    /// </summary>
    [SerializeField]
    private Transform objSoliderParent;
    private bool isCreate = true;

    /// <summary>
    /// 敌方中路箭塔
    /// </summary>
    [SerializeField]
    private Transform[] enemyMiddleTowers;
    /// <summary>
    /// 敌方左路箭塔
    /// </summary>
    [SerializeField]
    private Transform[] enemyLeftTowers;
    /// <summary>
    /// 敌方右路箭塔
    /// </summary>
    [SerializeField]
    private Transform[] enemyRightTowers;

    /// <summary>
    /// 我方中路箭塔
    /// </summary>
    [SerializeField]
    private Transform[] OwnMiddleTowers;
    /// <summary>
    /// 我方左路箭塔
    /// </summary>
    [SerializeField]
    private Transform[] OwnyLeftTowers;
    /// <summary>
    /// 我方右路箭塔
    /// </summary>
    [SerializeField]
    private Transform[] OwnyRightTowers;

    /// <summary>
    /// 我方小兵出生点(3个:左中右)
    /// </summary>
    [SerializeField]
    private Transform[] OwnyBirthPoints;
    /// <summary>
    /// 敌方小兵出生点(3个:左中右)
    /// </summary>
    [SerializeField]
    private Transform[] EnemyBirthPoints;

    /// <summary>
    /// 一波小兵的数量
    /// </summary>
    public int soliderNum=3;
	// Use this for initialization
	void Start () {
        StartCoroutine(IECreateSolider(3,2,5));
	}

    /// <summary>
    /// 生成小兵
    /// </summary>
    /// <param name="soldierType">小兵类型</param>
    /// <param name="soliderPosition">小兵位置</param>
    /// <param name="targetPosition">己方箭塔目标位置</param>
    /// <param name="roadMask">寻路遮罩层设置</param>
    private void CreateSoliders(SoldierType soldierType,Transform soliderPosition,Transform[] targetPosition,int roadMask) {
        GameObject obj= ( GameObject.Instantiate(ObjSoliderPrefab, soliderPosition.position, Quaternion.identity)) as GameObject;
        obj.transform.parent = objSoliderParent;
        SoldierMove soldierMv = obj.GetComponent<SoldierMove>();
        soldierMv.enemyTowerTrans = targetPosition;
        soldierMv.SetRoadMask(roadMask);
        soldierMv.type = (int)soldierType;
    }

    /// <summary>
    /// 生成小兵的协程
    /// </summary>
    /// <param name="startTime">生成小兵的起始时间</param>
    /// <param name="delayTime">每个小兵生成的间隔时间</param>
    /// <param name="nextWaveTime">小一波小兵生成的时间</param>
    /// <returns></returns>
    private IEnumerator IECreateSolider(float startTime,float delayTime,float nextWaveTime ) {
        yield return new WaitForSeconds(startTime);
        while (true)
        {
            for (int i = 0; i < soliderNum; i++)
            {   //左路，我方寻路己方目标位置  1<<3=2的3次方
                CreateSoliders(SoldierType.OwnSoldierType,OwnyBirthPoints[0],enemyLeftTowers,1<<3);
                //左路，敌方寻路己方目标位置
                CreateSoliders(SoldierType.EnemySoldierType,EnemyBirthPoints[0], OwnyLeftTowers, 1 << 3);

                //中路，我方寻路己方目标位置 1<<4=2的4次方
                CreateSoliders(SoldierType.OwnSoldierType,OwnyBirthPoints[1], enemyMiddleTowers, 1 << 4);
                //中路，敌方寻路己方目标位置
                CreateSoliders(SoldierType.EnemySoldierType, EnemyBirthPoints[1], OwnMiddleTowers, 1 << 4);

                //右路，我方寻路己方目标位置 1<<5=2的5次方
                CreateSoliders(SoldierType.OwnSoldierType, OwnyBirthPoints[2], enemyRightTowers, 1 << 5);
                //右路，敌方寻路己方目标位置
                CreateSoliders(SoldierType.EnemySoldierType, EnemyBirthPoints[2], OwnyRightTowers, 1 << 5);

                yield return new WaitForSeconds(delayTime);
            }
            yield return new WaitForSeconds(nextWaveTime);
            
        }
    }
}
