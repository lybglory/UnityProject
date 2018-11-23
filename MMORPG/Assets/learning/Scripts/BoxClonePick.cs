//===================================================
//AuthorName：小斌
//CreateTime：2018-11-22 17:57:08
//Description：
//===================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxClonePick : MonoBehaviour {
    /// <summary>
    /// 预设原型
    /// </summary>
    [SerializeField]
    private GameObject OrignObj;
    /// <summary>
    /// 克隆道具的区域
    /// </summary>
    [SerializeField]
    private Transform ItemAreaTrans;
    /// <summary>
    /// 最大箱子数量
    /// </summary>
    private int maxBoxCount = 10;
    /// <summary>
    /// 当前克隆的个数
    /// </summary>
    private int currentBoxCount = 0;
    /// <summary>
    /// 箱子父对象
    /// </summary>
    [SerializeField]
    private Transform boxParentTransform;
    /// <summary>
    /// 下次克隆时间间隔
    /// </summary>
    private float flNextCloneTime=0;
    /// <summary>
    /// 存储数量的keyname
    /// </summary>
    private string pickBoxKey = "pickBoxNum";
    /// <summary>
    /// 拾取箱子的数量
    /// </summary>
    private int pickupBoxNum;
    

	void Start () {
        OrignObj = Resources.Load("Prefabs/Cube") as GameObject;
        Debug.Log("OrignObj="+ OrignObj.name);
        pickupBoxNum = PlayerPrefs.GetInt(pickBoxKey, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentBoxCount < maxBoxCount)
        {
            
            if (Time.time>flNextCloneTime )
            {

                flNextCloneTime= Time.deltaTime+7;
                //Debug.Log("flNextCloneTime=" + flNextCloneTime);
                GameObject cloneObj = Instantiate(OrignObj) as GameObject;
                cloneObj.transform.parent = boxParentTransform;
                cloneObj.transform.position = ItemAreaTrans.TransformPoint(new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f)));
                BoxCtrl boxCtrl = cloneObj.GetComponent<BoxCtrl>();
                if (boxCtrl != null)
                {
                    boxCtrl.ActBoxObjOnHit = PickupBox;
                }
                currentBoxCount++;
                
            }
            
        }

    }

    /// <summary>
    /// 拾取箱子的方法，必须符合委托的返回值和签名
    /// </summary>
    /// <param name="obj"></param>
    void PickupBox(GameObject obj) {
        currentBoxCount--;
        pickupBoxNum++;
        PlayerPrefs.SetInt(pickBoxKey, pickupBoxNum);
        GameObject.Destroy(obj);
        Debug.Log("you have already pickup box count="+ pickupBoxNum);

    }
}
