using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("MyGame/Player")]
public class Player : MonoBehaviour {
    public float m_speed = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float movev = 0;
        float moveh = 0;
        //控制左右 (往左，值越小。往右值越大.由于模型Y轴转了180。Z轴向前，控制左右得相反。)
        if (Input.GetKey(KeyCode.A))
        {
            moveh += m_speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveh -= m_speed * Time.deltaTime;
        }
        //控制前后，向后值越小。向前值越大.由于模型Y轴转了180。Z轴向前，控制前后得相反。
        if (Input.GetKey(KeyCode.W)) {
            movev -= m_speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S)) {
            movev += m_speed * Time.deltaTime;
        }
        this.transform.Translate(new Vector3(moveh, 0, movev));
    }
}
