using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSolider : MonoBehaviour {
    [SerializeField]
    private GameObject ObjSoliderPrefab;
    [SerializeField]
    private Transform startTransSolider;
    [SerializeField]
    private Transform objSoliderParent;
    private bool isCreate = true;

    /// <summary>
    ///  敌方箭塔
    /// </summary>
    [SerializeField]
    private Transform[] enemyTowers;

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
    /// <param name="startTranSolider">小兵起始位置</param>
    /// <param name="enemyTowers">敌方箭塔</param>
    private void CreateSoliders(Transform startTranSolider,Transform[] enemyTowers) {
        GameObject obj= ( GameObject.Instantiate(ObjSoliderPrefab, startTransSolider.position, Quaternion.identity)) as GameObject;
        obj.transform.parent = objSoliderParent;
        SoldierMove soldierMv = obj.GetComponent<SoldierMove>();
        soldierMv.enemyTrans = enemyTowers;

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
            {
                CreateSoliders(startTransSolider, enemyTowers);
                yield return new WaitForSeconds(delayTime);
            }
            yield return new WaitForSeconds(nextWaveTime);
            
        }
    }
}
