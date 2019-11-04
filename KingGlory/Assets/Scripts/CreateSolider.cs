using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSolider : MonoBehaviour {
    [SerializeField]
    private GameObject ObjSolider;
    [SerializeField]
    private Transform startTransSolider;
    [SerializeField]
    private Transform objSoliderParent;
    private bool isCreate = true;
    /// <summary>
    /// 一波小兵的数量
    /// </summary>
    public int soliderNum=3;
	// Use this for initialization
	void Start () {
        StartCoroutine(IECreateSolider(3,2,5));
	}

    /// <summary>
    /// 生成小兵的方法
    /// </summary>
    private void CreateSoliders() {
        GameObject obj= ( GameObject.Instantiate(ObjSolider, startTransSolider.position, Quaternion.identity)) as GameObject;
        obj.transform.parent = objSoliderParent;
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
                CreateSoliders();
                yield return new WaitForSeconds(delayTime);
            }
            yield return new WaitForSeconds(nextWaveTime);
            
        }
    }
}
