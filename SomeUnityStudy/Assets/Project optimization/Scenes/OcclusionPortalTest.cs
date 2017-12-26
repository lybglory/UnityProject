using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OcclusionPortalTest : MonoBehaviour {
    private OcclusionPortal OccPortal;
	void Start () {
        OccPortal = gameObject.GetComponent<OcclusionPortal>();
    }
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.O)) {
            OccPortal.open = true;  //开启遮蔽通道，渲染室内物体
            gameObject.GetComponent<MeshRenderer>().enabled = false;//door不渲染
        }
        else if (Input.GetKeyUp(KeyCode.O)) {
            OccPortal.open = false;  //关闭遮蔽通道，不渲染室内物体
            gameObject.GetComponent<MeshRenderer>().enabled = true;//door渲染
        }
	}
}
