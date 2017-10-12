using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 刺道具上升或下降
/// </summary>
public class ThornUpDownMoving : MonoBehaviour {
    /// <summary>
    /// 移动速度
    /// </summary>
    private float _moveSpeed=0.01f;
    /// <summary>
    /// 标志位，控制上升移动还是下降移动
    /// </summary>
    private int _flag=1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (this.transform.position.y > -4.7f) {
            _flag = -1;//乘以负数：下降
        } else if (this.transform.position.y <-6.2f) {
            _flag = 1;//乘以正数:上升
        }
        this.transform.Translate(Vector3.up*_flag * _moveSpeed,Space.World);
    }
}
