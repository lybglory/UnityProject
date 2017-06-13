/***
*	title:Parkour
*	[副标题]
*	Description：
*	[描述]
*	Date：2017
*	Version：0.1
*	Modify Recoder：
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevel_Manager : MonoBehaviour {
    public GameObject ObjHero;                    //Hero GameObject
    public GameObject ObjBridge;                  //Bridge
    public float bridgeSpeed=0.1f;                //Bridge Speed;
    private Vector3 bridgeOriginPosition;         //Original Position
    private Vector3 bridgeCurrentPosition;
    private string strPlayAnimationName = "Walking"; //动画的名称

	// Use this for initialization
	void Start () {
        bridgeOriginPosition=ObjBridge.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        ObjHero.GetComponent<Animation>().Play(strPlayAnimationName);
        
        
        if (ObjBridge.transform.position.z <= -93f)
        {
            ObjBridge.transform.position = bridgeOriginPosition;
            
        }
        else {
            //第三人称视角使用世界坐标系
            ObjBridge.transform.Translate(Vector3.back * bridgeSpeed, Space.World);
            //Debug.Log("大桥的位置" + ObjBridge.transform.position);
        }


    }
}
