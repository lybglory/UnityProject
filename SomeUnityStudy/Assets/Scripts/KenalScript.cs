using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KenalScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //一旦按下空格键，Cube游戏对象停止旋转
        if (Input.GetKeyDown(KeyCode.Space )) {
            Time.timeScale = 0;
        }
        Debug.Log("Time.time=" + Time.time);
        Debug.Log("Time.fixedDeltaTime="+ Time.fixedDeltaTime);//0.02s
	}
    private void FixedUpdate()
    {
        this.transform.Rotate(Vector3.up, Space.World);
    }
    private void LateUpdate()
    {
        this.transform.Rotate(Vector3.up, Space.World);
    }
}
