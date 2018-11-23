//===================================================
//AuthorName：小斌
//CreateTime：2018-11-22 17:45:12
//Description：
//===================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCtrl : MonoBehaviour {
    /// <summary>
    /// 定义无参委托
    /// </summary>
    public System.Action<GameObject> ActBoxObjOnHit;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    /// <summary>
    /// 执行委托
    /// </summary>
    public void BoxHit() {
        if (ActBoxObjOnHit!=null) {
            ActBoxObjOnHit(gameObject);
        }
    }
}
