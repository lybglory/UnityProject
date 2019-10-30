using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    [SerializeField]
    private Transform transPlayer;
    /// <summary>
    /// 用来存储获取人物的x坐标
    /// </summary>
    private float camerax;
    /// <summary>
    /// 用来存储获取人物的z坐标
    /// </summary>
    private float cameraz;
    /// <summary>
    /// 相机和人物的高度，方便外部更改
    /// </summary>
    public float flY=7;
    /// <summary>
    /// 一个插值，方便外部更改
    /// </summary>
    public float flZ=-5;

	// Update is called once per frame
	void Update () {
        if (transPlayer) {
            //获取人物的x,z坐标。y不获取，固定
            camerax = transPlayer.localPosition.x;
            cameraz = transPlayer.localPosition.z;
            transform.position = new Vector3(camerax , flY, cameraz+ flZ); 
        }
	}
}
